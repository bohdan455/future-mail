using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class Email
    {

        [Key]
        [MaxLength(255)]
        [Column(TypeName ="varchar(255)")]
        public string EmailAddress { get; set; } = string.Empty;
        [Required]
        public bool IsVerified { get; set; }
    }
}
