using System.ComponentModel.DataAnnotations;

namespace Intake.Models;

public class User
{
    public int Id { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 50 characters")]
    [RegularExpression(@"^[a-zA-Z\s'-]+$", ErrorMessage = "Name contains invalid characters")]
    public string Name { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Surname must be between 2 and 50 characters")]
    [RegularExpression(@"^[a-zA-Z\s'-]+$", ErrorMessage = "Surname contains invalid characters")]
    public string Surname { get; set; }

    [Required]
    [StringLength(10, ErrorMessage = "Cellphone number cannot exceed 10 characters")]
    [RegularExpression(@"^\d{10}$", ErrorMessage = "Cellphone number must contain exactly 10 digits")]
    public string CellphoneNumber { get; set; }
}