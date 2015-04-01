using System.Collections.Generic;
using System.Net;
using System.Runtime.CompilerServices;
using BookClub.Core;

namespace BookClub.SampleDomain
{
    public class HamsterAggregate
    {
        private readonly HamsterState _state;

        public HamsterAggregate(HamsterState state)
        {
            _state = state;
        }

        public IEnumerable<IEvent> Handle(IHamsterCommand command)
        {
            return Execute((dynamic)command);
        }

        private IEnumerable<IEvent> Execute(CreateHamsterCommand command)
        {
            return new[] { new HamsterCreatedEvent(_state.AggregateId, command.Name) };
        }

        private IEnumerable<IEvent> Execute(RenameHamsterCommand command)
        {
            return new[] { new HamsterRenamedEvent(_state.AggregateId, _state.Name, command.NewName) };
        }

    }
}
