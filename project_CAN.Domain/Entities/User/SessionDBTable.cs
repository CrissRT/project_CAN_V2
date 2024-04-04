using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_CAN.Domain.Entities.User
{
    public class SessionDBTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int sessionId { get; set; }

        [Required]
        [StringLength(30)]
        public string userName { get; set; }

        [Required]
        public string cookieValue { get; set; }

        [Required]
        public DateTime expireTime { get; set; }
    }
}
