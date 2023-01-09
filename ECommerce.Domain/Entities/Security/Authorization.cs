using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ECommerce.Domain.Entities.Security
{
    [Table("Authorization")]
    public class Authorization
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Code { get; set; }

        [Required]
        [MaxLength(127)]
        public string Name { get; set; }

        [MaxLength(511)]
        public string Descripton { get; set; }
        public Guid Guid { get; set; } = Guid.NewGuid();
        public bool IsActive { get; set; } = true;
        public Guid? Deleted { get; set; }
        public DateTime CreationTime { get; set; } = DateTime.Now;
        public DateTime? ModificationTime { get; set; }
        public DateTime? DeletionZamani { get; set; }
    }
}
