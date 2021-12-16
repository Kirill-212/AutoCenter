import React, { useState, useEffect } from "react";
import TestDrivePost from "../TestDrive/PostTestDrive";
import CarEquipmentList from "../CarEquipment/ListCarEquipment";
import UserRoleList from "../Car/UserCar";
import BuyCarList from "../Car/BuyCarList";
import NewListUser from "../New/NewListUser";
import PostClientCar from "../ClientCar/PostClientCarUser";
import PostCar from "../Car/PostCar";
const HomeUser = () => {
  const [checkTestDriveList, setCheckTestDriveList] = useState(false);
  const [checkCarEquipment, setCheckCarEquipment] = useState(false);
  const [checkUserRoleList, setCheckUserRoleList] = useState(false);
  const [checkBuyCarList, setCheckBuyCarList] = useState(false);
  const [checkNewUserList, setChekNewUserList] = useState(false);
  const [checkClientCar, setCheckClientCar] = useState(false);
  const [checkPostCar, setCheckPostCar] = useState(false);
  function TestDrivePage() {
    setCheckTestDriveList(true);
    setCheckCarEquipment(false);
    setCheckUserRoleList(false);
    setCheckBuyCarList(false);
    setChekNewUserList(false);
    setCheckClientCar(false);
    setCheckPostCar(false);
  }
  function CarEquipmentPage() {
    setCheckTestDriveList(false);
    setCheckCarEquipment(true);
    setCheckUserRoleList(false);
    setCheckBuyCarList(false);
    setChekNewUserList(false);
    setCheckClientCar(false);
    setCheckPostCar(false);
  }
  function UserRolePage() {
    setCheckTestDriveList(false);
    setCheckCarEquipment(false);
    setCheckUserRoleList(true);
    setCheckBuyCarList(false);
    setChekNewUserList(false);
    setCheckClientCar(false);
    setCheckPostCar(false);
  }
  function BuyCarPage() {
    setCheckTestDriveList(false);
    setCheckCarEquipment(false);
    setCheckUserRoleList(false);
    setCheckBuyCarList(true);
    setCheckClientCar(false);
    setChekNewUserList(false);
    setCheckPostCar(false);
  }
  function NewListUserPage() {
    setCheckTestDriveList(false);
    setCheckCarEquipment(false);
    setCheckUserRoleList(false);
    setCheckBuyCarList(false);
    setChekNewUserList(true);
    setCheckClientCar(false);
    setCheckPostCar(false);
  }
  function PostClientCarPage() {
    setCheckTestDriveList(false);
    setCheckCarEquipment(false);
    setCheckUserRoleList(false);
    setCheckBuyCarList(false);
    setChekNewUserList(false);
    setCheckClientCar(true);
    setCheckPostCar(false);
  }
  function PostCarPage() {
    setCheckTestDriveList(false);
    setCheckCarEquipment(false);
    setCheckUserRoleList(false);
    setCheckBuyCarList(false);
    setChekNewUserList(false);
    setCheckClientCar(false);
    setCheckPostCar(true);
  }
  useEffect(() => {
    setCheckTestDriveList(false);
    setCheckCarEquipment(false);
    setCheckUserRoleList(false);
    setCheckBuyCarList(true);
    setChekNewUserList(false);
    setCheckClientCar(false);
    setCheckPostCar(false);
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
          </div>
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
          <div
            className="btn-group btn-group-sm"
            role="group"
            aria-label="Basic example"
          >
            <button
              type="button"
              className="btn btn-secondary btn-rounded"
              onClick={PostClientCarPage}
            >
              Add Car
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
              onClick={PostCarPage}
            >
              Sell car
            </button>
          </div>
        </div>
      </div>
      <div className="row  w-100 h-50 bg-dark text-white "></div>
      <div className="row">
        <div className="col container-fluid">
          {checkTestDriveList && <TestDrivePost />}
          {checkCarEquipment && <CarEquipmentList />}
          {checkUserRoleList && <UserRoleList />}
          {checkBuyCarList && <BuyCarList />}
          {checkNewUserList && <NewListUser />}
          {checkClientCar && <PostClientCar />}
          {checkPostCar && <PostCar />}
        </div>
      </div>
    </div>
  );
};

export default HomeUser;
