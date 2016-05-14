using System;

namespace TreeGecko.Library.Common.Objects
{
    public class AbstractAuditObject
    {
        public Guid Guid { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public DateTime? LastModifiedDateTime { get; set; }

        public int CreatedUserId { get; set; }

        public int LastModifiedUserId { get; set; }
    }
}