import React, { useEffect } from "react";
import GetEmployees from "../../Services/Employee/GetEmployeeService";
import SetEmployeeListAdminTabl from "../../SetTable/SetHomeListEmployeeAdminTable";
import DeleteEmployees from "../../Services/Employee/DeleteEmployeeService";
import { MDBDataTableV5 } from "mdbreact";

const EmployeeList = () => {
  const [MessageError, setMessageError] = React.useState("");
  const [listEmployees, setListEmployees] = React.useState({});
  const [viewList, setViewList] = React.useState(false);

  async function GetEmployeesList() {
    let response = await GetEmployees();
    if (response.statusText === "Unauthorized") {
      setMessageError("Unauthorized");
      return;
    }
    if (response === undefined) {
      setMessageError("Check connect server");
    } else {
      if (response.status !== 200) {
        if (response.data.ERROR !== undefined) {
          setMessageError(response.data.ERROR);
          setViewList(false);
        } else {
          setMessageError(response.data);
          setViewList(false);
        }
      } else {
        setListEmployees({
          columns: SetEmployeeListAdminTabl().columns,
          rows: SetOption(response.data),
        });
        setViewList(true);
      }
    }
  }

  async function DeleteEmployee(e) {
    let response = await DeleteEmployees(e.currentTarget.value);
    if (response.statusText === "Unauthorized") {
      setMessageError("Unauthorized");
      return;
    }
    if (response === undefined) {
      setMessageError("Check connect server");
    } else {
      if (response.status !== 200) {
        if (response.data.ERROR !== undefined) {
          setMessageError(response.data.ERROR);
        } else if (response.data.ERROR.details !== undefined) {
          setMessageError(response.data.ERROR.details[0]["message"]);
        } else {
          setMessageError(response.data);
        }
      } else {
        GetEmployeesList();
      }
    }
  }

  function SetOption(data) {
    return data.map(function (obj) {
      return {
        options: (
          <>
            <button
              color="purple"
              size="sm"
              value={obj.user.email}
              onClick={DeleteEmployee}
            >
              <i className="fa fa-trash" aria-hidden="true "></i>
            </button>
            <a
              className="text-reset ml-1"
              href={`/home/Employee/put?email=${obj.user.email}&address=${obj.address}&idRole=${obj.user.roleId}`}
            >
              <i className="fa fa-wrench" aria-hidden="true"></i>
            </a>
          </>
        ),
        email: obj.user.email,
        address: obj.address,
        startWorkDate: obj.startWorkDate,
      };
    });
  }

  useEffect(() => {
    GetEmployeesList();
  }, []);

  return (
    <div className="row mt-5">
      <div className="row">
        <h1 className="d-flex justify-content-center align-items-center ">
          Employee List
        </h1>
      </div>
      <div className="row">
        <p>{MessageError}</p>
      </div>
      <div className="row mt-5">
        {viewList && (
          <MDBDataTableV5
            hover
            entriesOptions={[5, 20, 25]}
            entries={5}
            pagesAmount={4}
            data={listEmployees}
          />
        )}
      </div>
    </div>
  );
};

export default EmployeeList;
