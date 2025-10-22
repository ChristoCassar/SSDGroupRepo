
// I, Nicholas Cassar, student number 000902104, certify that this material is my
// original work. No other person's work has been used without due
// acknowledgement and I have not made my work available to anyone else.
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Lab1_SecureSoftware_NicholasCassar.Models
{
    public class ApplicationUser : IdentityUser
    {
        // Represents a application users first name
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        // Represents a application users last name
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        // Represents a application users city
        [Display(Name = "City")]
        public string City { get; set; }

    }
}
