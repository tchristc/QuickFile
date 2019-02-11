using System;
using System.Collections.Generic;

namespace QuickFile.WebApi.Models
{
    public partial class FileStore
    {
        public int Id { get; set; }
        public Guid UniqueId { get; set; }
        public byte[] FileData { get; set; }
    }
}
