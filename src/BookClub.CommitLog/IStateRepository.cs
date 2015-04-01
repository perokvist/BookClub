using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookClub.Core;

namespace BookClub.CommitLog
{
    public interface IStateRepository<T>
        where T : IState, new()
    {
        Task<T> GetOrCreateAsync(Guid id);

        Task InsertOrUpdateAsync(T state);

        Task<IEnumerable<T>> All();
    }
}