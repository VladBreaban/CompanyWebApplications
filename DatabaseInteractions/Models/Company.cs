using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseInteractions.Models
{
    [Table("Companies")]
    public class Company
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Exchange { get; set; }
        public string Ticker { get; set; }
        [RegularExpression("/^[A-Za-z]{2}/")]
        public string Isin { get; set; }
        public string Website { get; set; }

       
    }
}
