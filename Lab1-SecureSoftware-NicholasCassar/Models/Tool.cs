using System.ComponentModel.DataAnnotations;

namespace Lab1_SecureSoftware_NicholasCassar.Models
{
    public class Tool
    {
        [Key]
        public string Id { get; set; }
        [Required, Display(Name = "Name")]
        public string Name { get; set; }
        [Required, Display(Name = "Type")]
        public string Type { get; set; }
        [Required, Display(Name = "Description")]
        public string Description { get; set; }
        [Required, Display(Name = "Serial Number")]
        public string SerialNumber { get; set; }
        [Required, Display(Name = "Cost")]
        public float Cost { get; set; }
        [Required, Display(Name = "InUse")]
        public bool InUse { get; set; }
    }
}
