using System.Collections.Generic;

namespace MicroServiceApp.InfrastructureLayer.Models
{
    public class New
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int EmployeeId { get; set; }

        public Employee Employee { get; set; }

        public List<Img> Imgs { get; set; }
    }
}
