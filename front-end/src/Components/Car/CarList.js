import React, { useEffect, useContext } from "react";
import Context from "../../context";
import GetCars from "../../Services/Car/GetCarService";
import SetHomeListCarAdminTable from "../../SetTable/SethomeListCarAdminTable";
import { MDBDataTableV5 } from "mdbreact";
import DeleteCars from "../../Services/Car/DeleteCarService";
import DeleteActions from "../../Services/ActionCar/DeleteActionCarService";
import PostCars from "../../Services/Car/PostCarService";
import DeleteClientCars from "../../Services/ClientCar/DeleteClientCarService";
const CarList = () => {
  const { user } = useContext(Context);
  const [MessageError, setMessageError] = React.useState("");
  const [listCars, setListCars] = React.useState({});
  const [viewList, setViewList] = React.useState(false);

  async function GetCarsList() {
    let response = await GetCars();
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
        console.log(response.data);
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
        GetCarsList();
      }
    }
  }
  async function DeleteClientCar(e) {
    let arr = e.currentTarget.value.split(",");
    let response = await DeleteClientCars(arr[0]);
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
        submitCar(arr[1], arr[2], arr[3], arr[4], arr[5], Number(arr[6]));
      }
    }
  }
  async function submitCar(
    nameCarEquipment,
    cost,
    vin,
    dateOfRealeseCar,
    carMileage,
    sharePercentage
  ) {
    console.log(
      nameCarEquipment,
      cost,
      vin,
      dateOfRealeseCar,
      carMileage,
      sharePercentage
    );
    setMessageError("");
    if (sharePercentage == 0 || sharePercentage == null) {
      sharePercentage = null;
    }
    let response = await PostCars(
      nameCarEquipment,
      cost,
      vin,
      dateOfRealeseCar,
      carMileage,
      sharePercentage
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
        GetCarsList();
      }
    }
  }
  function SetOption(data) {
    return data.map(function (obj) {
      console.log(obj);
      return {
        options: (
          <>
            <div className="row">
              <div className="col-md-4">
                {user.role !== "USER" && obj.isActive && (
                  <button
                    color="purple"
                    size="sm"
                    className="ml-1"
                    value={[
                      obj.clientCar.registerNumber,
                      obj.nameCarEquipment,
                      obj.cost,
                      obj.vin,
                      obj.dateOfRealeseCar,
                      obj.carMileage,
                      obj.actionCar == null || obj.actionCar == undefined
                        ? null
                        : obj.actionCar.sharePercentage,
                    ]}
                    onClick={DeleteClientCar}
                  >
                    <i className="fa fa-university" aria-hidden="true"></i>
                  </button>
                )}
              </div>
              <div className="col-md-2">
                <a
                  className="text-reset"
                  href={`/home/car/details?vin=${obj.vin}`}
                >
                  <i className="fa fa-info-circle" aria-hidden="true"></i>
                </a>
              </div>
              <div className="col-md-2">
                <a
                  className="text-reset ml-1 mr-1"
                  href={`/home/car/put?vin=${obj.vin}`}
                >
                  <i className="fa fa-wrench" aria-hidden="true"></i>
                </a>
              </div>
              <div className="col-md-4">
                <button
                  color="purple"
                  size="sm"
                  value={obj.vin}
                  onClick={DeleteCar}
                >
                  <i className="fa fa-trash" aria-hidden="true "></i>
                </button>
              </div>
            </div>
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
          <div className="row">
            <h1 className="d-flex justify-content-center align-items-center ">
              Car List
            </h1>
          </div>
          <div className="row">
            <p>{MessageError}</p>
          </div>
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
