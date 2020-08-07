using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Orchestrate.Identity.API.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        
        public string Role { get; set; }

        public IdentityUser User { get; set; }

        [Required]
        [Display(Name = "Role")]
        public List<SelectListItem> AllRoles => GetAllRoles();

        private List<SelectListItem> GetAllRoles()
        {
            return new List<SelectListItem>
            {
                new SelectListItem("Service Manager", "ServiceManager"),
                new SelectListItem("Workman", "ServiceProvider"),
                new SelectListItem("Property Owner", "PropertyOwner")
            };
        }
    }
}
