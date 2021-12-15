export default function SetHomeListTestDriveAdminTable() {
  let columns = [
    {
      label: "Date",
      field: "date",
      width: 150,
      sort: "asc",
      attributes: {
        "aria-controls": "DataTable",
        "aria-label": "Name",
      },
    },
    {
      label: "Time",
      field: "time",
      width: 270,
    },
    {
      label: "Is active",
      field: "isActive",
      width: 100,
    },
    {
      label: "Vin",
      field: "vin",
      width: 100,
    },
    {
      label: "Options",
      field: "options",
      width: 200,
    },
  ];
  return { columns: columns };
}
