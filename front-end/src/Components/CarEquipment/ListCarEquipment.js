import React, { useEffect, useContext } from "react";
import Context from "../../context";
import GetCarEuipment from "../../Services/CarEquipment/GetCarEquipmentService";

const CarDetail = () => {
  const { user } = useContext(Context);
  const [flag, setFlag] = React.useState(false);
  const [MessageError, setMessageError] = React.useState("");
  const [carEquipment, setCarEquipment] = React.useState({});

  async function GetCarsEuipments() {
    let response = await GetCarEuipment();
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
    let totalCost = 0;
    let equipment = [];
    for (let i in carEquipment) {
      equipment.push(
        <div className="row border border-dark border-top-1 border-right-1 border-left-1 border-bottom-0">
          <div className="col-md-4">Name car equipment</div>
          <div className="col-md-2">{carEquipment[i].name}</div>
          <div className="col-md-2">
            <a
              className="text-reset"
              href={`/home/CarEquipment/put?name=` + carEquipment[i].name}
            >
              <i className="fa fa-wrench" aria-hidden="true"></i>
            </a>
          </div>
          <div className="col-md-4">
            <img
              src={carEquipment[i].urlImg}
              className="w-100 h-100 img-thumbnail"
            />
          </div>
        </div>
      );
      for (let variablqe in carEquipment[i].equipment) {
        totalCost += carEquipment[i].equipment[variablqe].cost;
        equipment.push(
          <div className="row border border-dark border-bottom-1">
            <div className="col-md-2">Name</div>
            <div className="col-md-2">{variablqe}</div>
            <div className="col-md-2">Value</div>
            <div className="col-md-2">
              {carEquipment[i].equipment[variablqe].value}
            </div>
            <div className="col-md-2">Cost</div>
            <div className="col-md-2">
              {carEquipment[i].equipment[variablqe].cost} $
            </div>
          </div>
        );
      }
    }

    return equipment;
  }

  useEffect(() => {
    GetCarsEuipments();
  }, []);

  return (
    <div className=" container ">
      <div className="row">
        <h1 className="d-flex justify-content-center align-items-center ">
          Car equipment
        </h1>
        <p>{MessageError}</p>
      </div>
      <div className="row ">
        {/* {flag && <img src={carEquipment.urlImg} className="w-25 h-25" />} */}
        <div className="row">{flag && ViewCarEquipment()}</div>
      </div>
      <div className="row ">
        <div className="col w-100">
          <a href={"/home/" + user.role}>Home</a>
        </div>
      </div>
    </div>
  );
};

export default CarDetail;
