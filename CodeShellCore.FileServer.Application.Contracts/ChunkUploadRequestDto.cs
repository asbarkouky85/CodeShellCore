using CodeShellCore.Files;
using System;

namespace CodeShellCore.FileServer
{
    public class ChunkUploadRequestDto : IFileInfo
    {
        public long? Id { get; set; }
        public int AttachmentTypeId { get; set; }
        public int CurrentChunkIndex { get; set; }
        public int TotalChunkCount { get; set; }
        public string FileName { get; set; }
        public string Extension { get; set; }
        public string Chunk { get; set; }
        public int? Size { get; set; }

        public FileDimesion Dimesion { get; set; }
    }
}