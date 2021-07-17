using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
public class Company
{
    [Column("CompanyId")]
    public Guid Id { get; set; }
    [Required(ErrorMessage = "Company Name is a required filed")]
    [MaxLength(60, ErrorMessage = "Maximum length is 60 characters")]
    public string Name { get; set; }
    [Required(ErrorMessage = "Company Address is a required filed")]
    [MaxLength(60, ErrorMessage = "Maximum length for the address is 60 characters")]
    public string Address { get; set; }
    public string Countery { get; set; }
    public ICollection<Employee> Employees { get; set; }
}
