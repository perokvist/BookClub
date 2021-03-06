﻿using System.Net;
using BookClub.CommitLog;

namespace BookClub.MessageVault
{
    public class CheckpointReaderAdapter : ICheckpointReader
    {
        private readonly global::MessageVault.ICheckpointReader _checkpointReader;

        public CheckpointReaderAdapter(global::MessageVault.ICheckpointReader checkpointReader)
        {
           
            _checkpointReader = checkpointReader;
        }

        public void Dispose()
        {
            _checkpointReader.Dispose();
        }

        public long Read()
        {
            return _checkpointReader.Read();
        }

    }
}