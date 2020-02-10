using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppIdCheck.Models
{
    public class Dog
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Breed { get; set; }
    }
}
