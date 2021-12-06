using System;

namespace MicroServiceApp.InfrastructureLayer.Models
{
    public class Car
    {
        public int Id { get; set; }

        public string NameCarEquipment { get; set; }

        public string VIN { get; set; }

        public decimal Cost { get; set; }

        public long CarMileage { get; set; }

        public DateTime DateOfRealeseCar { get; set; }

        public ClientCar ClientCar { get; set; }

        public Order Orders { get; set; }

        public int? ActionCarId { get; set; }

        public ActionCar ActionCar { get; set; }
    }
}
