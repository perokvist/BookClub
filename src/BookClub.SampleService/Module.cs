using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Reactive.Threading.Tasks;
using System.Threading;
using System.Threading.Tasks;
using BookClub.Application;
using BookClub.CommitLog;
using BookClub.Core;
using BookClub.MessageVault;
using BookClub.SampleDomain;
using MessageVault.Memory;

namespace BookClub.SampleService
{
    public class Module
    {
        public static Func<ICommand, Task> Run(CancellationToken token, Subject<IEvent> eventSubject, Action<string> logger)
        {
            const string streamName = "hamsters";
            var commitLog = new MessageVaultClientAdapter(new MemoryClient(), logger);
            var dispatcher = new Dispatcher<ICommand, Task>();
            var eventHandlers = new Dispatcher<IEvent, Task>();
            var checkpointWriter = new CheckpointWriterAdapter(new MemoryCheckpointReaderWriter());
            var repository = new InMemoryStateRepository<HamsterState>();
            var streamProcessor = new StreamProcessor(commitLog, streamName, checkpointWriter, Serialization.Deserializer(), Console.WriteLine);

            var eventLog = commitLog.ToStreamPoster(Serialization.Serializer());

            dispatcher.Register<IHamsterCommand>(
                command => ApplicationService.Execute(
                    command,
                    id => repository.GetOrCreateAsync(id),
                    state => new HamsterAggregate(state),
                    aggregate => aggregate.Handle(command),
                    eventLog.ForStream(streamName)));

            eventHandlers.Register<IEvent>(@event => StateEventHandler.ForAsync(@event, repository));

            eventSubject.Subscribe(@event => eventHandlers.Dispatch(@event).Wait(token), () => { }, token);

            streamProcessor.Run(token, @event => eventSubject.OnNext(@event));

            return dispatcher.Dispatch;
            return Mixins.WaitForEventsPublished(dispatcher.Dispatch, eventSubject, logger);
        }
    }


    public static class Mixins
    {
        public static Func<ICommand, Task> WaitForEventsPublished(
            Func<ICommand, Task> dispatcher,
            IObservable<IEvent> observable,
            Action<string> logger)
        {
            Func<ICommand, Task> dec = c =>
            {
                var t2 = observable
                    .FirstAsync(@event => @event.CorrelationId == c.CorrelationId)
                    .Do(@event => logger(string.Format("{0} completed due to {1}", c.GetType(), @event.GetType())))
                    .ToTask();

                var t = dispatcher(c);

                return Task.WhenAll(t, t2);
            };

            return dec;
        }

    }
}