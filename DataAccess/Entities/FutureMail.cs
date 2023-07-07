using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class FutureMail
    {
        [Key]
        public long Id { get; set; }
        public Email Email { get; set; }
        [Required]
        [MaxLength(10000)]
        public string Text { get; set; } = string.Empty;
        public DateTime DateOfReceiving { get; set; }

    }
}
