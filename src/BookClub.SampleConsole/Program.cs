using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Reactive.Threading.Tasks;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BookClub.Core;
using BookClub.SampleDomain;

namespace BookClub.SampleConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Starting");
            Thread.Sleep(1000);

            RunAsync().Wait();
            System.Console.ReadLine();
        }

        private static async Task RunAsync()
        {
            Action<string> logger = System.Console.WriteLine;
            var subject = new Subject<IEvent>();
            
            //Simple Projection
            var renamedHamters = new List<string>();
            subject
                .OfType<HamsterRenamedEvent>()
                .Subscribe(@event => renamedHamters.Add(@event.NewName));

            //Event logger
            subject.Subscribe(@event => logger(string.Format("Subscription logged event {0} on thread {1}", @event.GetType(), Thread.CurrentThread.ManagedThreadId)), () => logger("completed"), CancellationToken.None);

            var sender = SampleService.Module.Run(CancellationToken.None, subject, logger);
            for (int i = 0; i < 10; i++)
            {
                await sender(new CreateHamsterCommand(Guid.NewGuid(), "Otto"));
                if(i%2!=0)
                    await sender(new RenameHamsterCommand(Guid.NewGuid(), "Otto the " + i));
                logger("Posted. no." + i);
                renamedHamters.ForEach(h => logger(string.Format("Renamed hamster : {0}", h)));
                await Task.Delay(250);
            }
        }
    }
}
