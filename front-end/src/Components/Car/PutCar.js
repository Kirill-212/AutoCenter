import React, { useEffect, useContext } from "react";
import Context from "../../context";
import { Navigate } from "react-router-dom";
import PutCars from "../../Services/Car/PutCarService";
import GetDetails from "../../Services/Car/GetByVinCarService";
import GetCarEquipments from "../../Services/CarEquipment/GetCarEquipmentService";

const PutCar = () => {
  const { user } = useContext(Context);
  const [nameCarEquipment, setNameCarEquipment] = React.useState("");
  const [cost, setCost] = React.useState(0);
  const [vin, setVin] = React.useState("");
  const [dateOfRealeseCar, setDateOfRealeseCar] = React.useState("");
  const [carMileage, setCarMileage] = React.useState(0);
  const [sharePercentage, setSharePercentage] = React.useState(0);
  const [MessageError, setMessageError] = React.useState("");
  const [redirect, setredirect] = React.useState(false);
  const [carEquipmentList, setCarEquipmentList] = React.useState([]);
  const [nameCarEquipmentSelect, setNameCarEquipmentSelect] =
    React.useState("");
  const [flag, setFlag] = React.useState(false);

  async function GetCarByVin(vin) {
    let response = await GetDetails(vin);
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
        setCarMileage(response.data.carMileage);
        setVin(response.data.vin);
        setDateOfRealeseCar(getDate(response.data.dateOfRealeseCar));
        setCost(response.data.cost);
        if (response.data.actionCar != null) {
          setSharePercentage(response.data.actionCar.sharePercentage);
        }
        setNameCarEquipmentSelect(response.data.nameCarEquipment);
        GetCarEquipment();
      }
    }
  }

  function getDate(inputDate) {
    let date = new Date(inputDate);
    let day = date.getDate();
    let month = date.getMonth() + 1;
    let year = date.getFullYear();
    if (month < 10) month = "0" + month;
    if (day < 10) day = "0" + day;
    return year + "-" + month + "-" + day;
  }

  async function GetCarEquipment() {
    setMessageError("");
    let response = await GetCarEquipments();
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

  async function submitCar(event) {
    event.preventDefault();
    let shp = sharePercentage;
    if (sharePercentage === null || sharePercentage === 0) {
      shp = null;
    }
    setMessageError("");
    let response = await PutCars(
      nameCarEquipment,
      cost,
      vin,
      dateOfRealeseCar,
      carMileage,
      shp
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
        setredirect(true);
      }
    }
  }

  function ValidField() {
    return (
      nameCarEquipment.length > 0 &&
      dateOfRealeseCar.length > 0 &&
      carMileage > 0 &&
      cost > 0 &&
      vin.length > 0
    );
  }

  function getCarEquipmentList() {
    let option = [];
    carEquipmentList.forEach((element) => {
      if (nameCarEquipmentSelect == element) {
        option.push(
          <option selected value={element.name}>
            {element.name}
          </option>
        );
      } else {
        option.push(<option value={element.name}>{element.name}</option>);
      }
    });
    return option;
  }

  const styles = {
    maxWidth: "700px",
    border: "none",
  };

  useEffect(() => {
    const query = new URLSearchParams(window.location.search);
    GetCarByVin(query.get("vin"));
  }, []);

  return (
    <div className="row mt-5">
      <div className="d-flex   justify-content-center align-items-center ">
        <div className="   p-4  w-100" style={styles}>
          <div className="row mt-5">
            <h1 className="d-flex   justify-content-center align-items-center ">
              Put Car
            </h1>
          </div>
          <div className="row mt-5">
            <form onSubmit={submitCar}>
              <div className="form-group mb-2 ">
                <label>VIN</label>
                <input
                  disabled
                  value={vin}
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
                  value={dateOfRealeseCar}
                  className="w-100 shadow-lg  bg-white rounded"
                  onChange={(e) => setDateOfRealeseCar(e.target.value)}
                  name="dateOfRealeseCar"
                  type="date"
                  disabled
                  placeholder="Enter your date of realese car..."
                />
              </div>
              <div className="form-group mb-2 ">
                <label>Cost($)</label>
                <input
                  value={cost}
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
                  disabled
                  value={carMileage}
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
                  value={sharePercentage}
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
                  value={nameCarEquipment}
                  size="1"
                  onChange={(e) => setNameCarEquipment(e.target.value)}
                >
                  {getCarEquipmentList()}
                </select>
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

export default PutCar;
