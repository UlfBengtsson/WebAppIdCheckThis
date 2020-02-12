using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppIdCheck.Models.ViewModels
{
    public class RoleViewModel
    {
        [Required]
        [StringLength(20,MinimumLength = 4)]
        public string Name { get; set; }
    }
}
