using System.Collections.Generic;
using System.Linq;
using BookClub.CommitLog;
using MessageVault;
using Message = BookClub.CommitLog.Message;

namespace BookClub.MessageVault
{
    public class MessageResultAdapter : IMessageResult
    {
        private readonly MessageResult _result;

        public MessageResultAdapter(global::MessageVault.MessageResult result)
        {
            _result = result;
        }

        public bool HasMessages()
        {
            return _result.HasMessages();
        }

        public IList<Message> Messages
        {
            get
            {
                return _result.Messages
                .Select(x => new Message(new MessageIdAdapter(x.Id), x.Key, x.Value))
                .ToList();
            }
        }

        public long NextOffset
        {
            get { return _result.NextOffset; }
        }
    }
}