using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookRepositoryDemoMVC_Client.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Book Name is required.")]
        [Display(Name = "Book Name")]
        public string Name { get; set; }
        public string Author { get; set; }

        [Required]
        public double Price { get; set; }
        public bool IsAvailable { get; set; }
    }
}
