using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseInteractions.Models;

    [Table("Users")]
    public class User
    {
        public Guid Id { get; set; }
        [Required]
        public string email { get; set; }

        [Required]
        public string password { get; set; }
    }

