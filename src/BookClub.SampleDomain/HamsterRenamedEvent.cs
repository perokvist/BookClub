using System;
using BookClub.Core;

namespace BookClub.SampleDomain
{
    [Serializable]
    public class HamsterRenamedEvent : IEvent
    {
        public HamsterRenamedEvent(Guid sourceId, string oldName, string newName)
        {
            SourceId = sourceId;
            OldName = oldName;
            NewName = newName;
        }

        public Guid CorrelationId { get; set; }

        public Guid SourceId { get; set; }

        public string NewName { get; set; }
        public string OldName { get; set; }
    }
}