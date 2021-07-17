using System.Collections.Generic;

namespace Entites.DataTransferObjects
{
    public class CompanyForUpdateDto
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Countery { get; set; }
        public ICollection<EmployeeForCreationDto> Employees { get; set; }
    }
}
