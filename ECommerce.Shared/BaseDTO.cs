using System;

namespace ECommerce.Shared
{
    public class BaseDTO
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public bool IsActive { get; set; } 
        public Guid? Deleted { get; set; }
        public DateTime CreationTime { get; set; } 
        public DateTime? ModificationTime { get; set; }
        public DateTime? DeletionZamani { get; set; }
    }
}
