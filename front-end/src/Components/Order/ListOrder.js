import React, { useEffect } from "react";
import GetOrders from "../../Services/Order/GetOrderService";
import SetHomeListOrderAdminTable from "../../SetTable/SetHomeListOrderAdminTable";
import { MDBDataTableV5 } from "mdbreact";

const OrderList = () => {
  const [MessageError, setMessageError] = React.useState("");
  const [listOrders, setListOrders] = React.useState({});
  const [viewList, setViewList] = React.useState(false);

  async function GetOrdersList() {
    let response = await GetOrders();
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
        setListOrders({
          columns: SetHomeListOrderAdminTable().columns,
          rows: SetOption(response.data),
        });
        setViewList(true);
      }
    }
  }

  function SetOption(data) {
    return data.map(function (obj) {
      return {
        totalCost: obj.totalCost,
        dateOfBuyCar: obj.dateOfBuyCar,
        nameCarEquipment: obj.car.nameCarEquipment,
        vin: obj.car.vin,
        cost: obj.car.cost,
        carMileage: obj.car.carMileage,
        dateOfRealeseCar: obj.car.dateOfRealeseCar,
        registerNumber: obj.car.clientCar.registerNumber,
        email: obj.car.clientCar.user.email,
        phoneNumber: obj.car.clientCar.user.phoneNumber,
      };
    });
  }

  useEffect(() => {
    GetOrdersList();
  }, []);

  return (
    <div className="row mt-5">
      <div className="row">
        <div className="row mt-5">
          <h1 className="d-flex justify-content-center align-items-center ">
            Order List
          </h1>
        </div>
        <div className="row mt-5">
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
            data={listOrders}
          />
        )}
      </div>
    </div>
  );
};

export default OrderList;
