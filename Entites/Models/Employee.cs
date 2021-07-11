using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
public class Employee
{
    public Guid Id { get; set; }
    [Required(ErrorMessage = "Employee name filed is required")]
    [MaxLength(60, ErrorMessage = "Maximum name length is 60 characters ")]
    public string Name { get; set; }
    [Required(ErrorMessage = "Age filed is required")]
    public int Age { get; set; }
    [Required(ErrorMessage = "Position filed is required")]
    [MaxLength(20, ErrorMessage = "Maximum length for the position is 20 characters")]
    public string Position { get; set; }

    [ForeignKey(nameof(Company))]
    public Guid CompanyId { get; set; }
    public Company Company { get; set; }
}
