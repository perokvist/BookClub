using System;

namespace BookClub.SampleDomain
{
    public class CreateHamsterCommand : IHamsterCommand
    {
        public CreateHamsterCommand(Guid aggregateId, string name)
        {
            AggregateId = aggregateId;
            Name = name;
            CorrelationId = Guid.NewGuid();
        }

        public string Name { get; private set; }

        public Guid AggregateId { get; private set; }

        public Guid CorrelationId { get; private set; }
    }
}