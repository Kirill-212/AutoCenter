import React, { useState, useEffect } from "react";
import CarList from "../Car/CarList";
import ClientCarList from "../ClientCar/ClientCarList";
import NewList from "../New/NewList";
import OrderList from "../Order/ListOrder";
import TestDriveList from "../TestDrive/ListTestDrive";
import CarEquipmentList from "../CarEquipment/ListCarEquipment";
import UserRoleList from "../Car/UserCar";
import BuyCarList from "../Car/BuyCarList";
import NewListUser from "../New/NewListUser";
const HomeEmployee = () => {
  const [checkCarList, setCheckCarList] = useState(true);
  const [checkClientCarList, setClientCarList] = useState(false);
  const [checkNewList, setCheckNewList] = useState(true);
  const [checkOrderList, setCheckOrderList] = useState(false);
  const [checkTestDriveList, setCheckTestDriveList] = useState(false);
  const [checkCarEquipment, setCheckCarEquipment] = useState(false);
  const [checkUserRoleList, setCheckUserRoleList] = useState(false);
  const [checkBuyCarList, setCheckBuyCarList] = useState(false);
  const [checkNewUserList, setChekNewUserList] = useState(false);

  function CarPage() {
    setCheckCarList(true);
    setClientCarList(false);
    setCheckNewList(false);
    setCheckOrderList(false);
    setCheckTestDriveList(false);
    setCheckCarEquipment(false);
    setCheckUserRoleList(false);
    setCheckBuyCarList(false);
    setChekNewUserList(false);
  }
  function ClientCarPage() {
    setCheckCarList(false);
    setClientCarList(true);
    setCheckNewList(false);
    setCheckOrderList(false);
    setCheckTestDriveList(false);
    setCheckCarEquipment(false);
    setCheckUserRoleList(false);
    setCheckBuyCarList(false);
    setChekNewUserList(false);
  }
  function NewPage() {
    setCheckCarList(false);
    setClientCarList(false);
    setCheckNewList(true);
    setCheckOrderList(false);
    setCheckTestDriveList(false);
    setCheckCarEquipment(false);
    setCheckUserRoleList(false);
    setCheckBuyCarList(false);
    setChekNewUserList(false);
  }
  function OrderPage() {
    setCheckCarList(false);
    setClientCarList(false);
    setCheckNewList(false);
    setCheckOrderList(true);
    setCheckTestDriveList(false);
    setCheckCarEquipment(false);
    setCheckUserRoleList(false);
    setCheckBuyCarList(false);
    setChekNewUserList(false);
  }
  function TestDrivePage() {
    setCheckCarList(false);
    setClientCarList(false);
    setCheckNewList(false);
    setCheckOrderList(false);
    setCheckTestDriveList(true);
    setCheckCarEquipment(false);
    setCheckUserRoleList(false);
    setCheckBuyCarList(false);
    setChekNewUserList(false);
  }
  function CarEquipmentPage() {
    setCheckCarList(false);
    setClientCarList(false);
    setCheckNewList(false);
    setCheckOrderList(false);
    setCheckTestDriveList(false);
    setCheckCarEquipment(true);
    setCheckUserRoleList(false);
    setCheckBuyCarList(false);
    setChekNewUserList(false);
  }
  function UserRolePage() {
    setCheckCarList(false);
    setClientCarList(false);
    setCheckNewList(false);
    setCheckOrderList(false);
    setCheckTestDriveList(false);
    setCheckCarEquipment(false);
    setCheckUserRoleList(true);
    setCheckBuyCarList(false);
    setChekNewUserList(false);
  }
  function BuyCarPage() {
    setCheckCarList(false);
    setClientCarList(false);
    setCheckNewList(false);
    setCheckOrderList(false);
    setCheckTestDriveList(false);
    setCheckCarEquipment(false);
    setCheckUserRoleList(false);
    setCheckBuyCarList(true);
    setChekNewUserList(false);
  }
  function NewListUserPage() {
    setCheckCarList(false);
    setClientCarList(false);
    setCheckNewList(false);
    setCheckOrderList(false);
    setCheckTestDriveList(false);
    setCheckCarEquipment(false);
    setCheckUserRoleList(false);
    setCheckBuyCarList(false);
    setChekNewUserList(true);
  }
  useEffect(() => {
    setCheckCarList(false);
    setClientCarList(false);
    setCheckNewList(false);
    setCheckOrderList(false);
    setCheckTestDriveList(true);
    setCheckCarEquipment(false);
    setCheckUserRoleList(false);
    setCheckBuyCarList(false);
    setChekNewUserList(false);
  }, []);

  return (
    <div className="d-flex   justify-content-center align-items-center flex-column ">
      <div className="row bg-dark text-white w-100">
        <div
          className="btn-group btn-group-sm"
          role="group"
          aria-label="Basic example"
        >
          <div
            className="btn-group btn-group-sm"
            role="group"
            aria-label="Basic example"
          >
            <button
              type="button"
              className="btn btn-secondary btn-rounded"
              onClick={CarPage}
            >
              Car
            </button>
          </div>
          <div
            className="btn-group btn-group-sm"
            role="group"
            aria-label="Basic example"
          >
            <button
              type="button"
              className="btn btn-secondary btn-rounded"
              onClick={ClientCarPage}
            >
              Client car
            </button>
          </div>
          <div
            className="btn-group btn-group-sm"
            role="group"
            aria-label="Basic example"
          >
            <button
              type="button"
              className="btn btn-secondary btn-rounded"
              onClick={NewPage}
            >
              New
            </button>
          </div>
          <div
            className="btn-group btn-group-sm"
            role="group"
            aria-label="Basic example"
          >
            <button
              type="button"
              className="btn btn-secondary btn-rounded"
              onClick={OrderPage}
            >
              Order
            </button>
          </div>
          <div
            className="btn-group btn-group-sm"
            role="group"
            aria-label="Basic example"
          >
            <button
              type="button"
              className="btn btn-secondary btn-rounded"
              onClick={TestDrivePage}
            >
              TestDrive
            </button>
          </div>
          <div
            className="btn-group btn-group-sm"
            role="group"
            aria-label="Basic example"
          >
            <button
              type="button"
              className="btn btn-secondary btn-rounded"
              onClick={CarEquipmentPage}
            >
              Car equipment
            </button>
          </div>
          <div
            className="btn-group btn-group-sm"
            role="group"
            aria-label="Basic example"
          >
            <button
              type="button"
              className="btn btn-secondary btn-rounded"
              onClick={UserRolePage}
            >
              User
            </button>
          </div>
          <div
            className="btn-group btn-group-sm"
            role="group"
            aria-label="Basic example"
          >
            <button
              type="button"
              className="btn btn-secondary btn-rounded"
              onClick={BuyCarPage}
            >
              Buy
            </button>
          </div>{" "}
          <div
            className="btn-group btn-group-sm"
            role="group"
            aria-label="Basic example"
          >
            <button
              type="button"
              className="btn btn-secondary btn-rounded"
              onClick={NewListUserPage}
            >
              New
            </button>
          </div>
        </div>
      </div>
      <div className="row  w-100 h-50 bg-dark text-white ">
        {checkCarList && (
          <a href="/home/Car/add" className="text-reset">
            <i className="fa fa-plus-circle" aria-hidden="true"></i>
          </a>
        )}
        {checkClientCarList && (
          <a href="/home/ClientCar/add" className="text-reset">
            <i className="fa fa-plus-circle" aria-hidden="true"></i>
          </a>
        )}
        {checkNewList && (
          <a href="/home/New/add" className="text-reset">
            <i className="fa fa-plus-circle" aria-hidden="true"></i>
          </a>
        )}
        {checkOrderList && (
          <a href="/home/Order/add" className="text-reset">
            <i className="fa fa-plus-circle" aria-hidden="true"></i>
          </a>
        )}
        {checkTestDriveList && (
          <a href="/home/TestDrive/add" className="text-reset">
            <i className="fa fa-plus-circle" aria-hidden="true"></i>
          </a>
        )}
        {checkCarEquipment && (
          <a href="/home/CarEquipment/add" className="text-reset">
            <i className="fa fa-plus-circle" aria-hidden="true"></i>
          </a>
        )}
      </div>
      <div className="row">
        <div className="col container-fluid">
          {checkCarList && <CarList />}
          {checkClientCarList && <ClientCarList />}
          {checkNewList && <NewList />}
          {checkOrderList && <OrderList />}
          {checkTestDriveList && <TestDriveList />}
          {checkCarEquipment && <CarEquipmentList />}
          {checkUserRoleList && <UserRoleList />}
          {checkBuyCarList && <BuyCarList />}
          {checkNewUserList && <NewListUser />}
        </div>
      </div>
    </div>
  );
};

export default HomeEmployee;
