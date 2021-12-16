import React, { useEffect } from "react";
import GetClientCars from "../../Services/ClientCar/GetClientCarService";
import SetHomeListClientCarAdminTable from "../../SetTable/SetHomeListClientCarAdminTable";
import { MDBDataTableV5 } from "mdbreact";
import DeleteClientCars from "../../Services/ClientCar/DeleteClientCarService";

const ClientCarList = () => {
  const [MessageError, setMessageError] = React.useState("");
  const [listClientCars, setListClientCars] = React.useState({});
  const [viewList, setViewList] = React.useState(false);

  async function GetClientCarsList() {
    let response = await GetClientCars();
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
          setViewList(false);
        } else {
          setMessageError(response.data);
          setViewList(false);
        }
      } else {
        setListClientCars({
          columns: SetHomeListClientCarAdminTable().columns,
          rows: SetOption(response.data),
        });
        setViewList(true);
      }
    }
  }

  async function DeleteClientCar(e) {
    let response = await DeleteClientCars(e.currentTarget.value);
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
          setViewList(false);
        } else {
          setMessageError(response.data);
          setViewList(false);
        }
      } else {
        GetClientCarsList();
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
              href={`/home/car/details?vin=${obj.car.vin}`}
            >
              <i className="fa fa-info-circle" aria-hidden="true"></i>
            </a>
            <a
              className="text-reset ml-1 mr-1"
              href={`/home/ClientCar/put?registerNumber=${obj.registerNumber}&email=${obj.user.email}`}
            >
              <i className="fa fa-money" aria-hidden="true"></i>
            </a>
            <button
              color="purple"
              size="sm"
              value={obj.registerNumber}
              onClick={DeleteClientCar}
            >
              <i className="fa fa-trash" aria-hidden="true "></i>
            </button>
          </>
        ),
        vin: obj.car.vin,
        nameCarEquipment: obj.car.nameCarEquipment,
        cost: obj.car.cost + " $",
        carMileage: obj.car.carMileage + " km",
        dateOfRealeseCar: obj.car.dateOfRealeseCar,
        registerNumber: obj.registerNumber,
        actionCar:
          obj.car.actionCar == null || obj.car.actionCar == undefined
            ? "not found"
            : obj.car.actionCar.sharePercentage + " %",
        email: obj.user.email,
      };
    });
  }

  useEffect(() => {
    GetClientCarsList();
  }, []);

  return (
    <div className="row mt-5">
      <div className="row">
        <h1 className="d-flex justify-content-center align-items-center ">
          Client car List
        </h1>
        <div className="row">
          <p>{MessageError}</p>
        </div>
      </div>
      <div className="row mt-5">
        {viewList && (
          <MDBDataTableV5
            hover
            entriesOptions={[5, 20, 25]}
            entries={5}
            pagesAmount={4}
            data={listClientCars}
          />
        )}
      </div>
    </div>
  );
};

export default ClientCarList;
