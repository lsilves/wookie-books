using System;
using Microsoft.AspNetCore.Identity;

namespace Wookie_Books.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string authorPseudonym { get; set; }

        public ApplicationUser()
        {
        }
    }
}
