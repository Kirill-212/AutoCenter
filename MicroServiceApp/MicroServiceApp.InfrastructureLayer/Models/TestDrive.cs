using System;
using System.Collections.Generic;

namespace MicroServiceApp.InfrastructureLayer.Models
{
    public class TestDrive
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string Time { get; set; }

        public int CarId { get; set; }

        public bool IsActive { get; set; }

        public Car Car { get; set; }
    }
}
