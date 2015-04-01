using System;
using BookClub.Core;

namespace BookClub.SampleDomain
{
    [Serializable]
    public class HamsterCreatedEvent : IEvent
    {
        public string Name { get; set; }

        public HamsterCreatedEvent(Guid aggregateId, string name)
        {
            SourceId = aggregateId;
            Name = name;
        }

        public Guid CorrelationId { get; set; }

        public Guid SourceId { get; set; }
    }
}