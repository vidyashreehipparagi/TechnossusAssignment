namespace TechnossusAssignment.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Student
    {
        [Key]
        public int Student_Id { get; set; }

        [Required(ErrorMessage = "Student Name is required")]
        [StringLength(50, ErrorMessage = "Student Name must be at most 50 characters long")]
        public string StudentName { get; set; }

        [Required(ErrorMessage = "Father's Name is required")]
        [StringLength(50, ErrorMessage = "Father's Name must be at most 50 characters long")]
        public string FatherName { get; set; }

        [StringLength(50, ErrorMessage = "Mother's Name must be at most 50 characters long")]
        public string MotherName { get; set; }

        [Range(0, 150, ErrorMessage = "Age must be between 0 and 150")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Home Address is required")]
        [StringLength(100, ErrorMessage = "Home Address must be at most 100 characters long")]
        public string HomeAddress { get; set; }

        [DataType(DataType.DateTime)]
        
        [Display(Name = "Registration Date")]
        public DateTime RegistrationDate { get; set; }

        [Range(0, 1, ErrorMessage = "IsActive must be either 0 or 1")]
        public int IsActive { get; set; }
    }

}
