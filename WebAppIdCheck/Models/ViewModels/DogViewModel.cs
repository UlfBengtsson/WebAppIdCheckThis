using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppIdCheck.Models.ViewModels
{
    public class DogViewModel
    {
        [Required]
        [StringLength(80, MinimumLength = 2)]
        public string Name { get; set; }
        [Required]
        [StringLength(60, MinimumLength = 2)]
        public string Breed { get; set; }
    }
}
