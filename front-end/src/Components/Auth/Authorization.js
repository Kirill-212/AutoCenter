import React from "react";
import Auth from "../../Services/Auth/AuthService";
import { Navigate } from "react-router-dom";

const Authorization = () => {
  const [email, setEmail] = React.useState("");
  const [password, setPassword] = React.useState("");
  const [MessageError, setMessageError] = React.useState("");
  const [redirectAdmin, setredirectAdmin] = React.useState(false);
  const [redirectEmployee, setredirectEmployee] = React.useState(false);
  const [redirectUser, setredirectUser] = React.useState(false);

  function ClearField() {
    setMessageError("");
  }

  async function submitUser(event) {
    event.preventDefault();
    setMessageError("");
    let response = await Auth(email, password);
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
        } else {
          setMessageError(response.data);
        }
      } else {
        localStorage.setItem("user", JSON.stringify(response.data));
        // console.log(localStorage.getItem("user"));
        setMessageError("completed successfully");
        if (response.data["role"] === "ADMIN") {
          setredirectAdmin(true);
          setredirectEmployee(false);
          setredirectUser(false);
        } else if (response.data["role"] === "EMPLOYEE") {
          setredirectAdmin(false);
          setredirectEmployee(true);
          setredirectUser(false);
        } else {
          setredirectAdmin(false);
          setredirectEmployee(false);
          setredirectUser(true);
        }
      }
    }
  }

  function ValidField() {
    return password.length > 0 && email.length > 0;
  }

  const styles = {
    maxWidth: "700px",
    border: "none",
  };

  return (
    <div className="container pt-5 lead">
      <div className="d-flex   justify-content-center align-items-center ">
        <div className="   p-4  w-100" style={styles}>
          <form onSubmit={submitUser}>
            <h1 className="d-flex   justify-content-center align-items-center ">
              Authorization
            </h1>
            <div className="form-group mb-2">
              <label>Email</label>
              <input
                className="w-100 shadow-lg  bg-white rounded"
                onChange={(e) => setEmail(e.target.value)}
                name="email"
                type="text"
                placeholder="Enter your email..."
                required
              />
            </div>
            <div className="form-group mb-2">
              <label>Password</label>
              <input
                className="w-100 shadow-lg  bg-white rounded"
                onChange={(e) => setPassword(e.target.value)}
                name="password"
                type="password"
                placeholder="Enter your password..."
                required
              />
            </div>
            <div className="d-flex justify-content-center form-outline mb-3">
              <div className="flex-fill mr-2 ">
                <button
                  type="submit"
                  disabled={!ValidField()}
                  className="btn btn-secondary btn-rounded w-100 "
                >
                  Submit
                </button>
              </div>
              <div className="flex-fill">
                <button
                  type="reset"
                  className="btn btn-warning btn-rounded w-100 "
                  onClick={ClearField}
                >
                  Cancel
                </button>
              </div>
            </div>
          </form>
          <div className="row">
            {redirectAdmin && (
              <a href="/home/ADMIN" value="Admin">
                Admin
              </a>
            )}
            {redirectEmployee && <a href="/home/EMPLOYEE">Employee</a>}
            {redirectUser && (
              <a href="/home/USER" value="User">
                User
              </a>
            )}
          </div>
          <div className="row">
            <a href="/" value="User">
              Reguster
            </a>
          </div>
          <div className="row">
            <p>{MessageError}</p>
          </div>
        </div>
      </div>
    </div>
  );
};

export default Authorization;
