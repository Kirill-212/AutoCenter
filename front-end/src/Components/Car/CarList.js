import React, { useEffect } from "react";
import GetCars from "../../Services/Car/GetCarService";
import SetHomeListCarAdminTable from "../../SetTable/SethomeListCarAdminTable";
import { MDBDataTableV5 } from "mdbreact";
import DeleteCars from "../../Services/Car/DeleteCarService";
import DeleteActions from "../../Services/ActionCar/DeleteActionCarService";

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
          columns: SetHomeListCarAdminTable().columns,
          rows: SetOption(response.data),
        });
        setViewList(true);
      }
    }
  }

  async function DeleteCar(e) {
    let response = await DeleteCars(e.currentTarget.value);
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
        GetCarsList();
      }
    }
  }

  function SetOption(data) {
    return data.map(function (obj) {
      return {
        options: (
          <>
            <a className="text-reset" href={`/home/car/details?vin=${obj.vin}`}>
              <i class="fa fa-info-circle" aria-hidden="true"></i>
            </a>
            <a
              className="text-reset ml-1 mr-1"
              href={`/home/car/put?vin=${obj.vin}`}
            >
              <i className="fa fa-wrench" aria-hidden="true"></i>
            </a>
            <button
              color="purple"
              size="sm"
              value={obj.vin}
              onClick={DeleteCar}
            >
              <i className="fa fa-trash" aria-hidden="true "></i>
            </button>
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
        email: obj.clientCar == null ? "For sale" : obj.clientCar.user.email,
      };
    });
  }

  async function DeleteAction() {
    let response = await DeleteActions();
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
        GetCarsList();
      }
    }
  }

  useEffect(() => {
    GetCarsList();
  }, []);

  return (
    <div className="container pt-5">
      <div className="row">
        <div className="row  w-100 h-50 ">
          <div className="col-md-2"></div>
          <div className="col-md-2"></div>
          <div className="col-md-2">
            <button
              color="purple"
              size="sm"
              value="Delete All Action"
              onClick={DeleteAction}
            >
              <i className="fa fa-trash" aria-hidden="true "></i>
            </button>
          </div>
          <div className="col-md-2"></div>
          <div className="col-md-2"></div>
          <div className="col-md-2"></div>
        </div>
        <div className="row">
          <h1 className="d-flex justify-content-center align-items-center ">
            Car List
          </h1>
          <p>{MessageError}</p>
        </div>
        <div className="row">
          {viewList && (
            <MDBDataTableV5
              hover
              entriesOptions={[5, 20, 25]}
              entries={5}
              pagesAmount={4}
              fullPagination
              data={listCars}
            />
          )}
        </div>
      </div>
    </div>
  );
};

export default CarList;
