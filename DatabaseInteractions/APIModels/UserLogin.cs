using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseInteractions.APIModels
{
    public class UserLogin
    {
        [Required]
        public string email { get; set; }

        [Required]
        public string password { get; set; }
    }
}
