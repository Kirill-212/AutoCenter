export default function setHomeListCarAdminTable() {
  let columns = [
    {
      label: "Vin",
      field: "vin",
      width: 150,
      sort: "asc",
      attributes: {
        "aria-controls": "DataTable",
        "aria-label": "Name",
      },
    },
    {
      label: "Name car equipment",
      field: "nameCarEquipment",
      width: 270,
    },
    {
      label: "Cost($)",
      field: "cost",
      width: 200,
    },
    {
      label: "Car mileage(km)",
      field: "carMileage",
      width: 100,
    },
    {
      label: "Date of realese car",
      field: "dateOfRealeseCar",
      width: 150,
    },
    {
      label: "Register number",
      field: "registerNumber",
      width: 150,
    },
    {
      label: "Share percentage(%)",
      field: "actionCar",
      width: 100,
    },
    {
      label: "Email",
      field: "email",
      width: 100,
    },
    {
      label: "Options",
      field: "options",
      sort: "disabled",
      width: 100,
    },
  ];
  return { columns: columns };
}
