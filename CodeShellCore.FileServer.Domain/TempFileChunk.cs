using CodeShellCore.Helpers;
using System;

namespace CodeShellCore.FileServer
{
    public class TempFileChunk : FileServerBaseModel
    {
        public long TempFileId { get; protected set; }
        public TempFile TempFile { get; protected set; }
        public int ChunkIndex { get; protected set; }
        public string ReferenceId { get; protected set; }

        protected TempFileChunk()
        {
            Id = Utils.GenerateID();
        }

        public TempFileChunk(long tempFileId, int currentChunkIndex, string referenceId) : this()
        {
            TempFileId = tempFileId;
            ChunkIndex = currentChunkIndex;
            ReferenceId = referenceId;
        }

    }
}
