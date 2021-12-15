export default function SetHomeListOrderAdminTable() {
  let columns = [
    {
      label: "Total cost($)",
      field: "totalCost",
      width: 150,
      sort: "asc",
      attributes: {
        "aria-controls": "DataTable",
        "aria-label": "Name",
      },
    },
    {
      label: "Date of buy car",
      field: "dateOfBuyCar",
      width: 270,
    },
    {
      label: "Name car equipment",
      field: "nameCarEquipment",
      width: 100,
    },
    {
      label: "Vin",
      field: "vin",
      width: 100,
    },
    {
      label: "Cost car($)",
      field: "cost",
      width: 100,
    },
    {
      label: "Car Mileage(km)",
      field: "carMileage",
      width: 100,
    },
    {
      label: "Date of realese car",
      field: "dateOfRealeseCar",
      width: 100,
    },
    {
      label: "Register number",
      field: "registerNumber",
      width: 100,
    },
    {
      label: "Email",
      field: "email",
      width: 100,
    },
    {
      label: "Phone number",
      field: "phoneNumber",
      width: 100,
    },
  ];
  return { columns: columns };
}
