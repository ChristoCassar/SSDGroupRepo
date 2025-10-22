using System.ComponentModel.DataAnnotations;

namespace Lab1_SecureSoftware_NicholasCassar.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [Required, Display(Name="Full Name")] 
        public string Name { get; set; }
        [Required, Display(Name = "Email")]
        public string Email { get; set; }
        [Required, Display(Name = "Phone Number")]
        public string Phone { get; set; }
        [Required, Display(Name = "Social Insurance Number")]
        public int SIN { get; set; }
        [Required, Display(Name = "Street Name")]
        public string StreetName {  get; set; }
        [Required, Display(Name = "City")]
        public string City { get; set; }
        [Required, Display(Name = "Postal Code")]
        public string PostalCode { get; set; }
        [Required, Display(Name = "Country")]
        public string Country { get; set; }
        [Required, Display(Name = "Address")]
        public int Address  { get; set; }
    }
}
