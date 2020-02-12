using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppIdCheck.Models.ViewModels
{
    public class UserRoleViewModel
    {
        // Data type Guid
        [Required]
        public string UserId { get; set; }
        [Required]
        public string RoleId { get; set; }

        public List<IdentityUser> UserList { get; set; }
        public List<IdentityRole> RoleList { get; set; }
    }
}
