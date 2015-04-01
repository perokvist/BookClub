using System;
using System.Threading.Tasks;

namespace BookClub.CommitLog
{
    public interface IMessagePump
    {
        void OnMessage(Func<Message, Task> messageAction);
    }
}