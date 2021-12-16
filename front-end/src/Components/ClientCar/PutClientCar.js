import React, { useEffect, useContext } from "react";
import Context from "../../context";
import { Navigate } from "react-router-dom";
import PutClientCars from "../../Services/ClientCar/PutClientCarService";
import GetUsers from "../../Services/User/GetUserService";

const PutClientCar = () => {
  const { user } = useContext(Context);
  const [MessageError, setMessageError] = React.useState("");
  const [email, setEmail] = React.useState("");
  const [redirect, setredirect] = React.useState(false);
  const [newOwnerEmail, setNewOwnerEmail] = React.useState("");
  const [oldRegisterNumber, setOldRegisterNumber] = React.useState("");
  const [newRegisterNumber, setNewRegisterNumber] = React.useState("");
  const [emailList, setEmailList] = React.useState([]);
  const [flag, setFlag] = React.useState(false);

  async function submitClientCar(event) {
    event.preventDefault();
    setMessageError("");
    let response = await PutClientCars(
      email,
      newOwnerEmail,
      oldRegisterNumber,
      newRegisterNumber
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

  async function GetUser(emailUser) {
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
        setEmailList(
          response.data.map((element) => {
            if (element.status === 1) {
              if (element.email !== emailUser) return element.email;
            }
          })
        );
        setFlag(true);
      }
    }
  }

  function ValidField() {
    return (
      email.length > 0 &&
      newOwnerEmail.length > 0 &&
      oldRegisterNumber.length > 0 &&
      newRegisterNumber.length > 0
    );
  }

  function getEmailList() {
    let option = [];
    emailList.forEach((element) => {
      if (element !== undefined)
        option.push(<option value={element}>{element}</option>);
    });
    return option;
  }

  const styles = {
    maxWidth: "700px",
    border: "none",
  };

  useEffect(() => {
    const query = new URLSearchParams(window.location.search);
    setEmail(query.get("email"));
    setOldRegisterNumber(query.get("registerNumber"));
    GetUser(query.get("email"));
  }, []);

  return (
    <div className="d-flex   justify-content-center align-items-center ">
      <div className="   p-4  w-100" style={styles}>
        <form onSubmit={submitClientCar}>
          <h1 className="d-flex   justify-content-center align-items-center ">
            Put Client Car
          </h1>
          <div className="form-group mb-2 ">
            <label>Email</label>
            <input
              value={email}
              disabled
              className="w-100 shadow-lg  bg-white rounded"
              onChange={(e) => setEmail(e.target.value)}
              name="email"
              type="text"
              placeholder="Enter your email..."
            />
          </div>
          <div className="form-group mb-2 ">
            <label>Old register number</label>
            <input
              value={oldRegisterNumber}
              className="w-100 shadow-lg  bg-white rounded"
              onChange={(e) => setOldRegisterNumber(e.target.value)}
              name="oldRegisterNumber"
              type="text"
              disabled
              placeholder="Enter your old register number...(Example 2222 MM-4)"
            />
          </div>
          <div className="form-group mb-2 ">
            <label>New register number</label>
            <input
              className="w-100 shadow-lg  bg-white rounded"
              onChange={(e) => setNewRegisterNumber(e.target.value)}
              name="newRegisterNumber"
              type="text"
              placeholder="Enter your new register number...(Example 2222 MM-4)"
            />
          </div>
          <div className="form-group mb-2 ">
            <label>New owner email</label>
            <select
              className="form-select"
              aria-label="Default select example"
              size="1"
              onChange={(e) => setNewOwnerEmail(e.target.value)}
            >
              {flag && getEmailList()}
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
  );
};

export default PutClientCar;
