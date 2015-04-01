using System;

namespace BookClub.CommitLog
{
    public interface ICheckpointReader : IDisposable
    {
        long Read();
    }
}