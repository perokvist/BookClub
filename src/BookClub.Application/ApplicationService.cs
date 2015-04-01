using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookClub.Core;

namespace BookClub.Application
{
    public static class ApplicationService
    {
        public static async Task Execute<TCommand, TAggregate, TState>(
            TCommand command,
            Func<Guid, Task<TState>> stateFactory,
            Func<TState, TAggregate> aggregateFactory,
            Func<TAggregate, IEnumerable<IEvent>> executeCommandUsingThis,
            Func<IEnumerable<IEvent>, Task> publisher
            )
            where TCommand : ICommand
            where TAggregate : class
            where TState : class, IState, new()
        {
            //State
            var state = await stateFactory(command.AggregateId);

            //Aggregate
            var aggregate = aggregateFactory(state);
            var events = executeCommandUsingThis(aggregate)
                .ToArray();

            //Correlation
            events.ForEach(e => e.CorrelationId = command.CorrelationId);

            await publisher(events);

        }
    }
}
