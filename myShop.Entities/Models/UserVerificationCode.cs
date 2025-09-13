using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myShop.Entities.Models
{
    public class UserVerificationCode
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }   // FK to ApplicationUser

        [Required]
        [MaxLength(6)]
        public string Code { get; set; }

        [Required]
        public DateTime ExpiresAt { get; set; }

        [Required]
        public bool IsUsed { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
    }
}
