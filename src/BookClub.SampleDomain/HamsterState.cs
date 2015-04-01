using System;
using BookClub.Core;

namespace BookClub.SampleDomain
{
    public class HamsterState : IState
    {
        public Guid AggregateId { get; set; }

        public string Name { get; set; }

        public void When(IEvent @event)
        {
            Apply((dynamic)@event);
        }

        private void Apply(HamsterRenamedEvent @event)
        {
            Name = @event.NewName;
        }

        private void Apply(HamsterCreatedEvent @event)
        {
            AggregateId = @event.SourceId;
            Name = @event.Name;
        }


        public long Version
        {
            get;
            set;
        }
    }
}