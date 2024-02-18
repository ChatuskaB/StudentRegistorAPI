using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentRegistorAPI.Models
{
    public class Student
    {
     
        public int StudentId { get; set; }
        public int MobileNumber { get; set; }
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Address { get; set; }
        [NotMapped]
        public IFormFile? Image { get; set; }
        public string? ImagePath { get; set; }
    }
}

