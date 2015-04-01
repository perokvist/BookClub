using System;

namespace BookClub.Core
{
    public interface IEvent
    {
        Guid CorrelationId { get; set; }
        Guid SourceId { get; }

    }
}