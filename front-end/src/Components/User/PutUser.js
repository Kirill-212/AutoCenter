import React, { useEffect, useContext } from "react";
import Context from "../../context";
import { Navigate } from "react-router-dom";
import PutUsers from "../../Services/User/PutUserService";

const PutUser = () => {
  const { user } = useContext(Context);
  const [firstName, setFirstName] = React.useState("");
  const [lastName, setLastName] = React.useState("");
  const [surname, setSurname] = React.useState("");
  const [dBay, setDBay] = React.useState("");
  const [password, setPassword] = React.useState("");
  const [email, setEmail] = React.useState("");
  const [phoneNumber, setPhoneNumber] = React.useState("");
  const [MessageError, setMessageError] = React.useState("");
  const [redirect, setredirect] = React.useState(false);

  async function submitUser(event) {
    event.preventDefault();
    setMessageError("");
    let response = await PutUsers(
      firstName,
      lastName,
      surname,
      dBay,
      password,
      email,
      phoneNumber
    );
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
        setredirect(true);
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

  function getDate(inputDate) {
    let date = new Date(inputDate);
    let day = date.getDate();
    let month = date.getMonth() + 1;
    let year = date.getFullYear();

    if (month < 10) month = "0" + month;
    if (day < 10) day = "0" + day;

    return year + "-" + month + "-" + day;
  }

  useEffect(() => {
    const query = new URLSearchParams(window.location.search);
    setFirstName(query.get("firstName"));
    setLastName(query.get("lastName"));
    setSurname(query.get("surname"));
    setDBay(getDate(query.get("dBay")));
    setEmail(query.get("email"));
    setPhoneNumber(query.get("phoneNumber"));
  }, []);

  return (
    <div className="container pt-5">
      <div className="d-flex   justify-content-center align-items-center ">
        <div className="p-4  w-100" style={styles}>
          <form onSubmit={submitUser}>
            <h1 className="d-flex   justify-content-center align-items-center ">
              Put user
            </h1>
            <div className="form-group mb-2 ">
              <label>First name</label>
              <input
                value={firstName}
                className="w-100 shadow-lg  bg-white rounded"
                onChange={(e) => setFirstName(e.target.value)}
                name="firstName"
                type="text"
                placeholder="Enter your first name..."
              />
            </div>
            <div className="form-group mb-2">
              <label>Last name</label>
              <input
                value={lastName}
                className="w-100 shadow-lg  bg-white rounded"
                onChange={(e) => setLastName(e.target.value)}
                name="lastName"
                type="text"
                placeholder="Enter your last name..."
              />
            </div>
            <div className="form-group mb-2">
              <label>Email</label>
              <input
                disabled
                value={email}
                className="w-100 shadow-lg  bg-white rounded"
                onChange={(e) => setEmail(e.target.value)}
                name="email"
                type="text"
                placeholder="Enter your email..."
              />
            </div>
            <div className="form-group mb-2">
              <label>Surname</label>
              <input
                value={surname}
                className="w-100 shadow-lg  bg-white rounded"
                onChange={(e) => setSurname(e.target.value)}
                name="surname"
                type="text"
                placeholder="Enter your surname..."
              />
            </div>
            <div className="form-group mb-2">
              <label>Password</label>
              <input
                value={password}
                className="w-100 shadow-lg  bg-white rounded"
                onChange={(e) => setPassword(e.target.value)}
                name="password"
                type="password"
                placeholder="Enter your password..."
              />
            </div>
            <div className="form-group mb-2">
              <label>Phone number</label>
              <input
                value={phoneNumber}
                className="w-100 shadow-lg  bg-white rounded"
                onChange={(e) => setPhoneNumber(e.target.value)}
                name="phoneNumber"
                type="text"
                placeholder="Enter your phoneNumber..."
              />
            </div>
            <div className="form-group mb-2">
              <p>
                Select you birthday:
                <input
                  className="shadow-lg  bg-white rounded w-100"
                  value={dBay}
                  type="date"
                  name="dBay"
                  onChange={(e) => setDBay(e.target.value)}
                />
              </p>
            </div>
            <div className="d-flex justify-content-center form-outline mb-3">
              <div className="flex-fill">
                <button
                  type="submit"
                  disabled={!ValidField()}
                  className="btn btn-secondary btn-rounded  w-100 "
                >
                  Put
                </button>
              </div>
            </div>
          </form>
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

export default PutUser;
