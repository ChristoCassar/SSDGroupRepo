using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab1_SecureSoftware_NicholasCassar.Models
{
    public class JobListing
    {
        //Id - The key reference to the company.
        [Key]
        public int JobId { get; set; }

        [Required, Display(Name = "Job Address")]
        public string JobAddress { get; set; }
        //Name - The name of the company.
        [Required, Display(Name = "Job Name")]
        public string JobName { get; set; }

        // YearsInBusiness - The number of years a business has been in operation.
        [Required, Display(Name = "Urgency")]
        public string Urgency { get; set; }

        //Website - The URL to the companies website.
        [Required, Display(Name = "Job Details")]
        public string JobDetails { get; set; }

        //Province - The province at which the business is located. (can be remote)
        [Required, Display(Name = "Location")]
        public string Location { get; set; }
        //The pay for completing the job.
        [Required, Display(Name = "Salary")]
        public float Salary { get; set; }

    }
}



