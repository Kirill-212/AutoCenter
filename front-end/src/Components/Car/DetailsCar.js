import React, { useEffect, useContext } from "react";
import Context from "../../context";
import GetDetails from "../../Services/Car/GetByVinCarService";
import GetCarEuipmentByName from "../../Services/CarEquipment/GetCarEquipmentByNameService";

const CarDetail = () => {
  const { user } = useContext(Context);
  const [flag, setFlag] = React.useState(false);
  const [MessageError, setMessageError] = React.useState("");
  const [detailCar, setDetailCar] = React.useState({});
  const [carEquipment, setCarEquipment] = React.useState({});

  async function GetCarByVin(vin) {
    let response = await GetDetails(vin);
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
        setDetailCar(response.data);
        GetCarEuipment(response.data.nameCarEquipment);
      }
    }
  }

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
        setCarEquipment(response.data);
        setFlag(true);
      }
    }
  }

  function ViewCarEquipment() {
    let totalCost = detailCar.cost;
    let equipment = [];
    equipment.push(
      <div className="row">
        <div className="col-md-4">Name</div>
        <div className="col-md-8">{carEquipment.name}</div>
      </div>
    );
    for (let variablqe in carEquipment.equipment) {
      totalCost += carEquipment.equipment[variablqe].cost;
      equipment.push(
        <div className="row">
          <div className="col-md-2">Name</div>
          <div className="col-md-2">{variablqe}</div>
          <div className="col-md-2">Value</div>
          <div className="col-md-2">
            {carEquipment.equipment[variablqe].value}
          </div>
          <div className="col-md-2">Cost</div>
          <div className="col-md-2">
            {carEquipment.equipment[variablqe].cost} $
          </div>
        </div>
      );
    }
    totalCost =
      detailCar.actionCar != null
        ? Number(
            (totalCost * (100 - detailCar.actionCar.sharePercentage)) / 100
          )
        : totalCost;
    equipment.push(
      <div className="row">
        <div className="col-md-8">Total cost</div>
        <div className="col-md-4">{totalCost} $</div>
      </div>
    );
    return equipment;
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
    <div className=" container ">
      <div className="row">
        <div className="row d-flex   justify-content-center align-items-center">
          <div className="col-md-3">
            <div className="col-md-6 justify-content-center"></div>
            <div className="row">
              <h1>Car details</h1>
            </div>
            <div className="row">
              <p>{MessageError}</p>
            </div>
          </div>
          <div className="col-md-3"></div>
        </div>
        <div className="row">
          <div className="row">
            <div className="col-md-3"></div>
            <div className="col-md-6">
              <figure className="figure">
                {flag && (
                  <img
                    src={carEquipment.urlImg}
                    className="figure-img img-fluid rounded rounded"
                  />
                )}
              </figure>
            </div>
            <div className="col-md-3"></div>
          </div>
          {flag && detailCar !== undefined && (
            <div className="row ">
              <div className="row ">
                <div className="col-md-3"></div>
                <div className="col-md-6">Information about car</div>
                <div className="col-md-3"></div>
              </div>
              <div className="row">
                <div className="col-md-4">Vin</div>
                <div className="col-md-8">{detailCar.vin}</div>
              </div>
              <div className="row">
                <div className="col-md-4">Cost</div>
                <div className="col-md-8">{detailCar.cost} $</div>
              </div>
              <div className="row">
                <div className="col-md-4">Car mileage</div>
                <div className="col-md-8">{detailCar.carMileage} km</div>
              </div>
              <div className="row">
                <div className="col-md-4">Date of realese car</div>
                <div className="col-md-8">{detailCar.dateOfRealeseCar}</div>
              </div>
            </div>
          )}
          {flag && detailCar !== undefined && detailCar.clientCar !== null && (
            <div className="row">
              <div className="row">
                <div className="col-md-4">Information about client</div>
              </div>
              <div className="row">
                <div className="col-md-4">Register number</div>
                <div className="col-md-4">
                  {detailCar.clientCar.registerNumber}
                </div>
              </div>
              <div className="row">
                <div className="col-md-4">Email</div>
                <div className="col-md-8">{detailCar.clientCar.user.email}</div>
              </div>
              <div className="row">
                <div className="col-md-4">Phone number</div>
                <div className="col-md-8">
                  {detailCar.clientCar.user.phoneNumber}
                </div>
              </div>
            </div>
          )}
          {detailCar !== undefined && detailCar.actionCar != null && (
            <div className="row">
              <div className="row">
                <div className="col-md-12">
                  Information about share percentage
                </div>
              </div>
              <div className="row">
                <div className="col-md-4">Share percentage</div>
                <div className="col-md-8">
                  {detailCar.actionCar.sharePercentage} %
                </div>
              </div>
            </div>
          )}
          <div className="row">{flag && ViewCarEquipment()}</div>
        </div>
        <div className="row ">
          <div className="col">
            <a href={"/home/" + user.role}>Home</a>
          </div>
        </div>
      </div>
    </div>
  );
};

export default CarDetail;
