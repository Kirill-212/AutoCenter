import React, { useEffect, useContext } from "react";
import Context from "../../context";
import { Navigate } from "react-router-dom";
import PostEmployees from "../../Services/Employee/PostEmployeeService";
import GetRoles from "../../Services/Role/GetRoleService";
import GetUsers from "../../Services/User/GetAllUsersNotAddedToEmp";

const PostEmployee = () => {
  const { user } = useContext(Context);
  const [address, setAddress] = React.useState("");
  const [idRole, setIdRole] = React.useState("");
  const [email, setEmail] = React.useState("");
  const [MessageError, setMessageError] = React.useState("");
  const [redirect, setRedirect] = React.useState(false);
  const [roleList, setRoleList] = React.useState([]);
  const [emailList, setEmailList] = React.useState([]);
  const [flag, setFlag] = React.useState(false);

  async function GetRole() {
    setMessageError("");
    let response = await GetRoles();
    if (response.statusText === "Unauthorized") {
      setMessageError("Unauthorized");
      return;
    }
    if (response === undefined) {
      setMessageError("Check connect server");
    } else {
      if (response.status !== 200) {
        if (response.data.ERROR !== undefined) {
          if (response.data.ERROR.details !== undefined) {
            setMessageError(response.data.ERROR.details[0]["message"]);
          } else {
            setMessageError(response.data.ERROR);
          }
        } else if (response.data.errors !== undefined) {
          let errorResult = "";
          Object.keys(response.data.errors).forEach(function (key) {
            errorResult += key + " : " + response.data.errors[key] + " | ";
          });
          setMessageError(errorResult);
        } else {
          setMessageError(response.data);
        }
      } else {
        setRoleList(response.data);
        setFlag(true);
      }
    }
  }

  async function GetEmails() {
    setMessageError("");
    let response = await GetUsers();
    if (response.statusText === "Unauthorized") {
      setMessageError("Unauthorized");
      return;
    }
    if (response === undefined) {
      setMessageError("Check connect server");
    } else {
      if (response.status !== 200) {
        if (response.data.ERROR !== undefined) {
          if (response.data.ERROR.details !== undefined) {
            setMessageError(response.data.ERROR.details[0]["message"]);
          } else {
            setMessageError(response.data.ERROR);
          }
        } else if (response.data.errors !== undefined) {
          let errorResult = "";
          Object.keys(response.data.errors).forEach(function (key) {
            errorResult += key + " : " + response.data.errors[key] + " | ";
          });
          setMessageError(errorResult);
        } else {
          setMessageError(response.data);
        }
      } else {
        setEmailList(response.data);
      }
    }
  }

  async function submitEmployee(event) {
    event.preventDefault();
    setMessageError("");
    let response = await PostEmployees(address, email, idRole);
    if (response.statusText === "Unauthorized") {
      setMessageError("Unauthorized");
      return;
    }
    if (response === undefined) {
      setMessageError("Check connect server");
    } else {
      if (response.status !== 200) {
        if (response.data.ERROR !== undefined) {
          if (response.data.ERROR.details !== undefined) {
            setMessageError(response.data.ERROR.details[0]["message"]);
          } else {
            setMessageError(response.data.ERROR);
          }
        } else if (response.data.errors !== undefined) {
          let errorResult = "";
          Object.keys(response.data.errors).forEach(function (key) {
            errorResult += key + " : " + response.data.errors[key] + " | ";
          });
          setMessageError(errorResult);
        } else {
          setMessageError(response.data);
        }
      } else {
        setRedirect(true);
      }
    }
  }

  function ValidField() {
    return address.length > 0 && idRole.length > 0 && email.length > 0;
  }

  const styles = {
    maxWidth: "700px",
    border: "none",
  };

  useEffect(() => {
    GetRole();
    GetEmails();
  }, []);

  return (
    <div className="row mt-5">
      <div className="d-flex   justify-content-center align-items-center ">
        <div className="   p-4  w-100" style={styles}>
          <div className="row mt-5">
            <h1 className="d-flex   justify-content-center align-items-center">
              Post Employee
            </h1>
          </div>
          <div className="row mt-5">
            <form onSubmit={submitEmployee}>
              <div className="form-group mb-2 ">
                <label>Address</label>
                <input
                  className="w-100 shadow-lg  bg-white rounded"
                  onChange={(e) => setAddress(e.target.value)}
                  name="address"
                  type="text"
                  placeholder="Enter your address..."
                />
              </div>
              <div className="form-group mb-2 ">
                <label>Role</label>
                <select
                  className="form-select"
                  aria-label="Default select example"
                  size="0"
                  onChange={(e) => setIdRole(e.target.value)}
                >
                  {flag &&
                    roleList.map((element) => {
                      if (element.roleName !== "USER")
                        return (
                          <option value={element.id}>{element.roleName}</option>
                        );
                    })}
                </select>
              </div>
              <div className="form-group mb-2 ">
                <label>Email</label>
                <select
                  className="form-select"
                  aria-label="Default select example"
                  size="1"
                  onChange={(e) => setEmail(e.target.value)}
                >
                  {flag &&
                    emailList.map((element) => (
                      <option value={element.email}>{element.email}</option>
                    ))}
                </select>
              </div>
              <div className="d-flex justify-content-center form-outline mb-3">
                <div className="flex-fill">
                  <button
                    type="submit"
                    disabled={!ValidField()}
                    className="btn btn-secondary btn-rounded w-100 "
                  >
                    Put
                  </button>
                </div>
              </div>
            </form>
          </div>
          <div className="row ">
            <div className="col">
              <a href={"/home/" + user.role}>Home</a>
            </div>
          </div>
          <div>
            {redirect && <Navigate to={"/home/" + user.role} />}
            <p>{MessageError}</p>
          </div>
        </div>
      </div>
    </div>
  );
};

export default PostEmployee;
