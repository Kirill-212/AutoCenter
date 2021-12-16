import React, { useEffect, useContext } from "react";
import Context from "../../context";
import { Navigate } from "react-router-dom";
import GetUsers from "../../Services/User/GetUserService";
import GetVins from "../../Services/Car/GetCarVinWithoutClientCarService";
import GetCarEuipmentByName from "../../Services/CarEquipment/GetCarEquipmentByNameService";
import PostOrder from "../../Services/Order/PostOrderService";

const PostCar = () => {
  const { user } = useContext(Context);
  const [email, setEmail] = React.useState("");
  const [registerNumber, setRegisterNumber] = React.useState("");
  const [MessageError, setMessageError] = React.useState("");
  const [redirect, setredirect] = React.useState(false);
  const [flag, setFlag] = React.useState(false);
  const [flagVin, setFlagVin] = React.useState(false);
  const [emailList, setEmailList] = React.useState([]);
  const [vinList, setVinList] = React.useState([]);
  const [vin, setVin] = React.useState("");

  async function GetCarEuipment(name) {
    let response = await GetCarEuipmentByName(name);
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
        } else {
          setMessageError(response.data);
        }
      } else {
        return response.data;
      }
    }
  }

  async function GetVin() {
    setMessageError("");
    let response = await GetVins();
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
        setVinList(
          response.data.map((element) => {
            let totalCost = element.cost;
            let carEquipment = GetCarEuipment(element.nameCarEquipment);
            for (let variablqe in carEquipment.equipment)
              totalCost += carEquipment.equipment[variablqe].cost;
            totalCost =
              element.actionCar != null
                ? Number(
                    (totalCost * (100 - element.actionCar.sharePercentage)) /
                      100
                  )
                : totalCost;
            return { vin: element.vin, totalCost: totalCost };
          })
        );
        setFlagVin(true);
      }
    }
  }

  function getVinList() {
    let option = [];
    vinList.forEach((element) => {
      if (element !== undefined)
        option.push(
          <option value={element.vin}>
            {element.vin} Totol cost:{element.totalCost}$
          </option>
        );
    });
    return option;
  }

  function getEmailList() {
    let option = [];
    emailList.forEach((element) => {
      if (element !== undefined)
        option.push(<option value={element}>{element}</option>);
    });
    return option;
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
        setEmailList(
          response.data.map((element) => {
            if (element.status === 1) {
              return element.email;
            }
          })
        );
        setFlag(true);
      }
    }
  }

  async function submitOrder(event) {
    event.preventDefault();
    setMessageError("");
    let response = await PostOrder(email, vin, registerNumber);
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
    return email.length > 0 && registerNumber.length > 0;
  }

  const styles = {
    maxWidth: "700px",
    border: "none",
  };

  useEffect(() => {
    GetVin();
    GetEmails();
  }, []);

  return (
    <div className="row mt-5">
      <div className="d-flex   justify-content-center align-items-center ">
        <div className="   p-4  w-100" style={styles}>
          <div className="row mt-5">
            <h1 className="d-flex   justify-content-center align-items-center ">
              Post order
            </h1>
          </div>
          <div className="row mt-5">
            <form onSubmit={submitOrder}>
              <div className="form-group mb-2 ">
                <label>Register number</label>
                <input
                  className="w-100 shadow-lg  bg-white rounded"
                  onChange={(e) => setRegisterNumber(e.target.value)}
                  name="registerNumber"
                  type="text"
                  placeholder="Enter your register number..."
                />
              </div>
              <div className="form-group mb-2 ">
                <label>Email</label>
                <select
                  size="1"
                  className="form-select"
                  aria-label="Default select example"
                  onChange={(e) => setEmail(e.target.value)}
                >
                  {flag && getEmailList()}
                </select>
              </div>
              <div className="form-group mb-2 ">
                <label>Vin</label>
                <select
                  size="1"
                  className="form-select"
                  aria-label="Default select example"
                  onChange={(e) => setVin(e.target.value)}
                >
                  {flagVin && getVinList()}
                </select>
              </div>
              <div className="d-flex justify-content-center form-outline mb-3">
                <div className="flex-fill">
                  <button
                    type="submit"
                    disabled={!ValidField()}
                    className="btn btn-secondary btn-rounded w-100 "
                  >
                    Post
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

export default PostCar;
