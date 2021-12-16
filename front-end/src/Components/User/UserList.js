import React, { useEffect } from "react";
import GetUsers from "../../Services/User/GetUserService";
import DeleteUsers from "../../Services/User/DeleteUserService";
import SetHomeListAdminTabl from "../../SetTable/SetHomeListAdminTable";
import UpdateStatusUsers from "../../Services/User/PutStatusUserService";
import { MDBDataTableV5 } from "mdbreact";

const UserList = () => {
  const [MessageError, setMessageError] = React.useState("");
  const [listUsers, setListUsers] = React.useState({});
  const [viewList, setViewList] = React.useState(false);

  async function GetUsersList() {
    let response = await GetUsers();
    console.log(response.statusText === "Unauthorized");
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
        setListUsers({
          columns: SetHomeListAdminTabl().columns,
          rows: SetOption(response.data),
        });
        setViewList(true);
      }
    }
  }

  async function UpdateStatusUser(e) {
    let response = await UpdateStatusUsers(e.currentTarget.value);
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
        GetUsersList();
      }
    }
  }

  async function DeleteUser(e) {
    let response = await DeleteUsers(e.currentTarget.value);
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
        GetUsersList();
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
              value={obj.email}
              className="pl-1"
              onClick={DeleteUser}
            >
              <i className="fa fa-trash" aria-hidden="true "></i>
            </button>
            {obj.role.roleName === "USER" && (
              <button
                color="purple"
                size="sm"
                className="ml-1"
                value={obj.email}
                onClick={UpdateStatusUser}
              >
                <i className="fa fa-user-plus" aria-hidden="true"></i>
              </button>
            )}

            <a
              className="text-reset pl-1"
              href={`/home/put/user?firstName=${obj.firstName}&lastName=${obj.lastName}&surname=${obj.surname}&email=${obj.email}&phoneNumber=${obj.phoneNumber}&dBay=${obj.dBay}&password=${obj.password}`}
            >
              <i className="fa fa-wrench" aria-hidden="true"></i>
            </a>
          </>
        ),
        role: obj.role.roleName,
        firstName: obj.firstName,
        lastName: obj.lastName,
        surname: obj.surname,
        dBay: obj.dBay,
        status: obj.status == 1 ? "ACTIVE" : "CREATED",
        email: obj.email,
        phoneNumber: obj.phoneNumber,
      };
    });
  }

  useEffect(() => {
    GetUsersList();
  }, []);

  return (
    <div className="row mt-5">
      <div className="row">
        <h1 className="d-flex justify-content-center align-items-center ">
          User List
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
            data={listUsers}
          />
        )}
      </div>
    </div>
  );
};

export default UserList;
