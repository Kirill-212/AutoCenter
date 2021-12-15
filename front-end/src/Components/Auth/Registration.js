import React from "react";
import Regiser from "../../Services/Auth/RegistrationService";
import { Navigate } from "react-router-dom";

const Registration = () => {
  const [firstName, setFirstName] = React.useState("");
  const [lastName, setLastName] = React.useState("");
  const [surname, setSurname] = React.useState("");
  const [email, setEmail] = React.useState("");
  const [password, setPassword] = React.useState("");
  const [dBay, setDBay] = React.useState("");
  const [phoneNumber, setPhoneNumber] = React.useState("");
  const [MessageError, setMessageError] = React.useState("");
  const [redirectLogin, setRedirectLogin] = React.useState(false);

  function ClearField(e) {
    setMessageError("");
  }

  async function submitUser(event) {
    event.preventDefault();
    setMessageError("");
    let response = await Regiser(
      firstName,
      lastName,
      surname,
      dBay,
      password,
      email,
      phoneNumber
    );
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
        setRedirectLogin(true);
      }
    }
  }

  function ValidField() {
    return (
      firstName.length > 0 &&
      lastName.length > 0 &&
      surname.length > 0 &&
      dBay.length > 0 &&
      password.length > 0 &&
      email.length > 0 &&
      phoneNumber.length > 0
    );
  }

  const styles = {
    maxWidth: "700px",
    border: "none",
  };

  return (
    <div className="container pt-5">
      <div className="d-flex   justify-content-center align-items-center ">
        <div className="   p-4  w-100" style={styles}>
          <form onSubmit={submitUser}>
            <h1 className="d-flex   justify-content-center align-items-center ">
              Registration
            </h1>
            <div className="form-group mb-2 ">
              <label>First name</label>
              <input
                className="w-100 shadow-lg  bg-white rounded"
                onChange={(e) => setFirstName(e.target.value)}
                name="firstName"
                type="text"
                placeholder="Enter your first name..."
                required
              />
            </div>
            <div className="form-group mb-2">
              <label>Last name</label>
              <input
                className="w-100 shadow-lg  bg-white rounded"
                onChange={(e) => setLastName(e.target.value)}
                name="lastName"
                type="text"
                placeholder="Enter your last name..."
                required
              />
            </div>
            <div className="form-group mb-2">
              <label>Surname</label>
              <input
                className="w-100 shadow-lg  bg-white rounded"
                onChange={(e) => setSurname(e.target.value)}
                name="surname"
                type="text"
                placeholder="Enter your last name..."
                required
              />
            </div>
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
            <div className="form-group mb-2">
              <p>
                Select you birthday:
                <input
                  aria-label="Default select example"
                  className="shadow-lg  bg-white rounded ml-1 w-100"
                  type="date"
                  name="dBay"
                  onChange={(e) => setDBay(e.target.value)}
                  required
                />
              </p>
            </div>
            <div className="form-group mb-2">
              <p>
                Phone number:
                <input
                  className="shadow-lg  bg-white rounded ml-1 w-100"
                  type="text"
                  name="phoneNumber"
                  onChange={(e) => setPhoneNumber(e.target.value)}
                  required
                />
              </p>
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
          <div className="row ">
            <div className="col">
              <a href="/login"> Authorization</a>
            </div>
          </div>
          <div>
            <p>{MessageError}</p>
            {redirectLogin && <Navigate to="/login" />}
          </div>
        </div>
      </div>
    </div>
  );
};

export default Registration;
