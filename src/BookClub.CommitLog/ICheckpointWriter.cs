using System;

namespace BookClub.CommitLog
{
   	public interface ICheckpointWriter : IDisposable{
		long GetOrInitPosition();
		void Update(long position);
	}
}