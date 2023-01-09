using ECommerce.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Entities.Security
{
    [Table("Role")]
   public class Role:BaseEntity
    {
        [Required]
        [MaxLength(127)]
        public string Name { get; set; }


        [MaxLength(511)]
        public string Description { get; set; }
    }
}
