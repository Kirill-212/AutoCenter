export default function SetHomeListEmployeeAdminTable() {
  let columns = [
    {
      label: "Email",
      field: "email",
      width: 150,
      sort: "asc",
      attributes: {
        "aria-controls": "DataTable",
        "aria-label": "Name",
      },
    },
    {
      label: "Address",
      field: "address",
      width: 270,
    },
    {
      label: "Start work date",
      field: "startWorkDate",
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
