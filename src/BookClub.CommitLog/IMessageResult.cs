using System.Collections.Generic;

namespace BookClub.CommitLog
{
    public interface IMessageResult
    {
        bool HasMessages();

        IList<Message> Messages { get;  }

        long NextOffset { get; }
    }
}