using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookRepositoryDemoMVC_Client.Models
{
    public class SellRecord
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string BookName { get; set; }
        public double SellQty { get; set; }
        public DateTime date { get; set; }
    }
}
