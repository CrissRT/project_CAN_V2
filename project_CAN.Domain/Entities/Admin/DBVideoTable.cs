using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_CAN.Domain.Entities.Admin
{
    [Table("videoTable")]
    public class DBVideoTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int videoLinkId { get; set; }

        [Required]
        public string videoLink { get; set; }
    }
}
