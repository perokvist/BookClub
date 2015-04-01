using System;

namespace BookClub.Core
{
    public interface IState
    {
        Guid AggregateId { get; set; }
        void When(IEvent @event);
        long Version { get; set; }
    }
}