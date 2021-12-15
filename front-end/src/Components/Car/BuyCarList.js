import React, { useEffect } from "react";
import GetCars from "../../Services/Car/GetCarForUserService";
import SetTableBuyCar from "../../SetTable/SetTableBuyCar";
import { MDBDataTableV5 } from "mdbreact";

const CarList = () => {
  const [MessageError, setMessageError] = React.useState("");
  const [listCars, setListCars] = React.useState({});
  const [viewList, setViewList] = React.useState(false);

  async function GetCarsList() {
    let response = await GetCars();
    if (response === undefined) {
      setMessageError("Check connect server");
    } else {
      if (response.status !== 200) {
        if (response.data.ERROR !== undefined) {
          setMessageError(response.data.ERROR);
          setViewList(false);
        } else {
          setMessageError(response.data);
          setViewList(false);
        }
      } else {
        setListCars({
          columns: SetTableBuyCar().columns,
          rows: SetOption(response.data),
        });
        setViewList(true);
      }
    }
  }

  function SetOption(data) {
    return data.map(function (obj) {
      return {
        options: (
          <>
            <a
              className="text-reset"
              href={`/home/Car/DetailsBuy?vin=${obj.vin}`}
            >
              <i class="fa fa-info-circle" aria-hidden="true"></i>
            </a>
            {obj.actionCar === null && (
              <a
                className="text-reset ml-1"
                href={`/home/Car/Buy?vin=${obj.vin}&name=${obj.nameCarEquipment}&cost=${obj.cost}`}
              >
                <i class="fa fa-shopping-cart" aria-hidden="true"></i>
              </a>
            )}
            {obj.actionCar !== null && (
              <a
                className="text-reset ml-1"
                href={`/home/Car/Buy?vin=${obj.vin}&name=${obj.nameCarEquipment}&cost=${obj.cost}&sharePercentage=${obj.actionCar.sharePercentage}`}
              >
                <i class="fa fa-shopping-cart" aria-hidden="true"></i>
              </a>
            )}
          </>
        ),
        vin: obj.vin,
        nameCarEquipment: obj.nameCarEquipment,
        cost: obj.cost + " $",
        carMileage: obj.carMileage + " km",
        dateOfRealeseCar: obj.dateOfRealeseCar,
        actionCar:
          obj.actionCar == null || obj.actionCar == undefined
            ? "not found"
            : obj.actionCar.sharePercentage + "%",
      };
    });
  }

  useEffect(() => {
    GetCarsList();
  }, []);

  return (
    <div className="row mt-5">
      <div className="row">
        <h1 className="d-flex justify-content-center align-items-center ">
          Car List
        </h1>
        <p>{MessageError}</p>
      </div>
      <div className="row mt-5">
        {viewList && (
          <MDBDataTableV5
            hover
            entriesOptions={[5, 20, 25]}
            entries={5}
            pagesAmount={4}
            data={listCars}
          />
        )}
      </div>
    </div>
  );
};

export default CarList;
