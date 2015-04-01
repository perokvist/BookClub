using System;

namespace BookClub.Core
{
    public interface ICommand
    {
        Guid AggregateId { get;  }

        Guid CorrelationId { get; }
    }
}
