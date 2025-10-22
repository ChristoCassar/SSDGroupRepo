
// I, Nicholas Cassar, student number 000902104, certify that this material is my
// original work. No other person's work has been used without due
// acknowledgement and I have not made my work available to anyone else.
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace Lab1_SecureSoftware_NicholasCassar.Models
{
    public class Company
    {

        //Id - The key reference to the company.
        [Key]
        public string Id { get; set; }

        //Name - The name of the company.
        [Required, Display(Name = "Name")]
        public string Name { get; set; }

        // YearsInBusiness - The number of years a business has been in operation.
        [Required, Display(Name = "Years In Business")]
        public int YearsInBusiness { get; set; }

        //Website - The URL to the companies website.
        [Required, Display(Name = "Website")]
        public string Website { get; set; }

        //Province - The province at which the business is located.
        public string Province { get; set; }

        //public ICollection<JobListing> JobListings { get; set; }


    }
}
