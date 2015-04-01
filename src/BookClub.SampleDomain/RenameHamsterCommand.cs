using System;

namespace BookClub.SampleDomain
{
    public class RenameHamsterCommand : IHamsterCommand
    {
        public RenameHamsterCommand(Guid aggregateId, string newName)
        {
            AggregateId = aggregateId;
            NewName = newName;
            CorrelationId = Guid.NewGuid();
        }

        public string NewName { get; private set; }

        public Guid AggregateId { get; private set; }

        public Guid CorrelationId { get; private set; }
    }
}