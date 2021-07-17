using System.Collections.Generic;

namespace Entites.DataTransferObjects
{
    public class CompanyForCreationDto
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Countery { get; set; }
        public IEnumerable<EmployeeForCreationDto> Employees { get; set; }
    }

}
