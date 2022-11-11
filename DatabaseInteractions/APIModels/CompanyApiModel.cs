using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseInteractions.APIModels
{
    public class CompanyApiModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Exchange { get; set; }
        public string Ticker { get; set; }
        [RegularExpression("/^[A-Za-z]{2}/")]
        public string Isin { get; set; }
        public string? Website { get; set; }
    }
}
