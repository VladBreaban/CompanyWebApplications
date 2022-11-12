using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseInteractions.Models
{
    [Table("Users")]
    public class User
    {
        public Guid Id { get; set; }
        [Required]
        public string email { get; set; }

        [Required]
        public string password { get; set; }
    }
}
