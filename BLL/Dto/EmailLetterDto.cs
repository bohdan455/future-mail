using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dto
{
    public class EmailLetterDto
    {
        [Required]
        [StringLength(255)]
        [EmailAddress]
        public string EmailAddress { get; set; } = string.Empty;
        [Required]
        [StringLength(10000)]
        public string Text { get; set; } = string.Empty;
        [Required]
        public DateTime DateOfReceiving { get; set; }
    }
}
