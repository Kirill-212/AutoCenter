import React, { useEffect, useContext } from "react";
import Context from "../../context";
import { Navigate } from "react-router-dom";
import PostOrder from "../../Services/Order/PostOrderService";
import GetCarEuipmentByName from "../../Services/CarEquipment/GetCarEquipmentByNameService";

const BuyCar = () => {
  const { user } = useContext(Context);
  const [email, setEmail] = React.useState("");
  const [registerNumber, setRegisterNumber] = React.useState("");
  const [MessageError, setMessageError] = React.useState("");
  const [redirect, setredirect] = React.useState(false);
  const [flag, setFlag] = React.useState(false);
  const [vin, setVin] = React.useState("");
  const [totalCosts, setTotalCost] = React.useState(0);

  async function GetCarEuipment(name, cost, sharePercentage) {
    let response = await GetCarEuipmentByName(name);
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
        let totalCost = Number(cost);
        let carEquipment = response.data;
        for (let variablqe in carEquipment.equipment) {
          console.log(carEquipment.equipment[variablqe].cost);
          totalCost += Number(carEquipment.equipment[variablqe].cost);
        }
        totalCost =
          sharePercentage != null
            ? Number((totalCost * (100 - sharePercentage)) / 100)
            : totalCost;
        setTotalCost(totalCost);
      }
    }
  }

  async function submitOrder(event) {
    event.preventDefault();
    setMessageError("");
    let response = await PostOrder(user.email, vin, registerNumber);
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
    return user.email.length > 0 && registerNumber.length > 0;
  }

  const styles = {
    maxWidth: "700px",
    border: "none",
  };
  useEffect(() => {
    const query = new URLSearchParams(window.location.search);
    setVin(query.get("vin"));
    GetCarEuipment(
      query.get("name"),
      query.get("cost"),
      query.get("sharePercentage")
    );
  }, []);
  return (
    <div className="row mt-5">
      <div className="d-flex   justify-content-center align-items-center ">
        <div className="   p-4  w-100" style={styles}>
          <div className="row mt-5">
            <h1 className="d-flex   justify-content-center align-items-center ">
              Buy car
            </h1>
          </div>
          <div className="row mt-5">
            <form onSubmit={submitOrder}>
              <div className="form-group mb-2 ">
                <label>Total cost {totalCosts} $</label>
              </div>
              <div className="form-group mb-2 ">
                <label>Register number</label>
                <input
                  required
                  className="w-100 shadow-lg  bg-white rounded"
                  onChange={(e) => setRegisterNumber(e.target.value)}
                  name="registerNumber"
                  type="text"
                  placeholder="Enter your register number..."
                />
              </div>
              <div className="form-group mb-2 ">
                <label>Email</label>
                <input
                  disabled
                  value={user.email}
                  className="w-100 shadow-lg  bg-white rounded"
                  onChange={(e) => setEmail(e.target.value)}
                  name="email"
                  type="text"
                  placeholder="Enter your email..."
                />
              </div>
              <div className="form-group mb-2 ">
                <label>Vin</label>
                <input
                  disabled
                  value={vin}
                  className="w-100 shadow-lg  bg-white rounded"
                  onChange={(e) => setVin(e.target.value)}
                  name="vin"
                  type="text"
                  placeholder="Enter your vin..."
                />
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

export default BuyCar;
