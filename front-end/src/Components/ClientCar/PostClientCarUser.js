import React, { useEffect, useContext } from "react";
import { Navigate } from "react-router-dom";
import PostClientCars from "../../Services/ClientCar/PostClientCarService";
import GetCarEquipments from "../../Services/CarEquipment/GetCarEquipmentService";
import Context from "../../context";

const PostClientCar = () => {
  const [nameCarEquipment, setNameCarEquipment] = React.useState("");
  const [cost, setCost] = React.useState(0);
  const [vin, setVin] = React.useState("");
  const [dateOfRealeseCar, setDateOfRealeseCar] = React.useState("");
  const [carMileage, setCarMileage] = React.useState(0);
  const [sharePercentage, setSharePercentage] = React.useState(0);
  const [registerNumber, setRegisterNumber] = React.useState("");
  const [email, setEmail] = React.useState("");
  const { user } = useContext(Context);
  const [MessageError, setMessageError] = React.useState("");
  const [carEquipmentList, setCarEquipmentList] = React.useState([]);
  const [flag, setFlag] = React.useState(false);

  async function GetCarEquipment() {
    setMessageError("");
    let response = await GetCarEquipments();
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
        setCarEquipmentList(response.data);
        setFlag(true);
      }
    }
  }

  async function submitClientCar(event) {
    setEmail(user.email);
    event.preventDefault();
    setMessageError("");
    if (sharePercentage === null || sharePercentage === 0) {
      setSharePercentage(null);
    }
    let response = await PostClientCars(
      nameCarEquipment,
      cost,
      vin,
      dateOfRealeseCar,
      carMileage,
      sharePercentage,
      user.email,
      registerNumber
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
        setMessageError("completed successfully");
      }
    }
  }

  function ValidField() {
    return (
      nameCarEquipment.length > 0 &&
      dateOfRealeseCar.length > 0 &&
      carMileage > 0 &&
      cost > 0 &&
      vin.length > 0 &&
      registerNumber.length > 0
    );
  }

  const styles = {
    maxWidth: "700px",
    border: "none",
  };

  useEffect(() => {
    GetCarEquipment();
  }, []);

  return (
    <div className="row mt-2">
      <div className="d-flex   justify-content-center align-items-center ">
        <div className="   p-4  w-100" style={styles}>
          <div className="row mt-2">
            <h1 className="d-flex   justify-content-center align-items-center ">
              Post Client Car
            </h1>
          </div>
          <div className="row mt-5">
            <form onSubmit={submitClientCar}>
              <div className="form-group mb-2 ">
                <label>VIN</label>
                <input
                  className="w-100 shadow-lg  bg-white rounded"
                  onChange={(e) => setVin(e.target.value)}
                  name="vin"
                  type="text"
                  placeholder="Enter your VIN..."
                />
              </div>
              <div className="form-group mb-2 ">
                <label>Date of realese car</label>
                <input
                  className="w-100 shadow-lg  bg-white rounded"
                  onChange={(e) => setDateOfRealeseCar(e.target.value)}
                  name="dateOfRealeseCar"
                  type="date"
                  placeholder="Enter your date of realese car..."
                />
              </div>
              <div className="form-group mb-2 ">
                <label>Cost($)</label>
                <input
                  className="w-100 shadow-lg  bg-white rounded"
                  onChange={(e) => setCost(e.target.value)}
                  name="cost"
                  type="number"
                  placeholder="Enter your cost..."
                />
              </div>
              <div className="form-group mb-2 ">
                <label>Car mileage(km)</label>
                <input
                  className="w-100 shadow-lg  bg-white rounded"
                  onChange={(e) => setCarMileage(e.target.value)}
                  name="carMileage"
                  type="number"
                  placeholder="Enter your car mileage..."
                />
              </div>
              <div className="form-group mb-2 ">
                <label>Share percentage(%)</label>
                <input
                  className="w-100 shadow-lg  bg-white rounded"
                  onChange={(e) => setSharePercentage(e.target.value)}
                  name="sharePercentage"
                  type="number"
                  placeholder="Enter your share percentage..."
                />
              </div>
              <div className="form-group mb-2 ">
                <label>Car equipment</label>
                <select
                  className="form-select"
                  aria-label="Default select example"
                  size="1"
                  onChange={(e) => setNameCarEquipment(e.target.value)}
                >
                  {flag &&
                    carEquipmentList.map((element) => {
                      return (
                        <option value={element.name}>{element.name}</option>
                      );
                    })}
                </select>
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
                <label>Register number</label>
                <input
                  className="w-100 shadow-lg  bg-white rounded"
                  onChange={(e) => setRegisterNumber(e.target.value)}
                  name="registerNumber"
                  type="text"
                  placeholder="Enter your register number...(Example 2222 MM-4)"
                />
              </div>
              <div className="d-flex justify-content-center form-outline mb-3">
                <div className="flex-fill">
                  <button
                    type="submit"
                    disabled={!ValidField()}
                    className="btn btn-secondary btn-rounded w-100"
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
            <p>{MessageError}</p>
          </div>
        </div>
      </div>
    </div>
  );
};

export default PostClientCar;
