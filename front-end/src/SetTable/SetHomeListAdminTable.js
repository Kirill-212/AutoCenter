export default function setHomeListTableAdmin() {
  let columns = [
    {
      label: "First name",
      field: "firstName",
      width: 150,
      sort: "asc",
      attributes: {
        "aria-controls": "DataTable",
        "aria-label": "Name",
      },
    },
    {
      label: "Last name",
      field: "lastName",
      width: 270,
    },
    {
      label: "Surname",
      field: "surname",
      width: 200,
    },
    {
      label: "Birthday",
      field: "dBay",
      width: 100,
    },
    {
      label: "Status",
      field: "status",
      width: 150,
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
    {
      label: "Role",
      field: "role",
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
