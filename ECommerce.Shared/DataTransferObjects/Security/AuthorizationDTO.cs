using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Shared.DataTransferObjects.Security
{
    public class AuthorizationDTO
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public string Descripton { get; set; }
        public Guid Guid { get; set; } = Guid.NewGuid();
        public bool IsActive { get; set; } = true;
        public Guid? Deleted { get; set; }
        public DateTime CreationTime { get; set; } = DateTime.Now;
        public DateTime? ModificationTime { get; set; }
        public DateTime? DeletionZamani { get; set; }
    }
}
