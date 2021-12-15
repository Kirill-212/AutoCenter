import React, { useState, useEffect } from "react";
import CarList from "../Car/CarList";
import ClientCarList from "../ClientCar/ClientCarList";
import NewList from "../New/NewList";
import OrderList from "../Order/ListOrder";
const HomeEmployee = () => {
  const [checkCarList, setCheckCarList] = useState(true);
  const [checkClientCarList, setClientCarList] = useState(false);
  const [checkNewList, setCheckNewList] = useState(false);
  const [checkOrderList, setCheckOrderList] = useState(false);

  function CarPage() {
    setCheckCarList(true);
    setClientCarList(false);
    setCheckNewList(false);
    setCheckOrderList(false);
  }
  function ClientCarPage() {
    setCheckCarList(false);
    setClientCarList(true);
    setCheckNewList(false);
    setCheckOrderList(false);
  }
  function NewPage() {
    setCheckCarList(false);
    setClientCarList(false);
    setCheckNewList(true);
    setCheckOrderList(false);
  }
  function OrderPage() {
    setCheckCarList(false);
    setClientCarList(false);
    setCheckNewList(false);
    setCheckOrderList(true);
  }

  useEffect(() => {
    setCheckCarList(true);
    setClientCarList(false);
    setCheckNewList(false);
    setCheckOrderList(false);
  }, []);

  return (
    <div className="d-flex   justify-content-center align-items-center flex-column">
      <div className="row bg-dark text-white w-100">
        <div className="col-md-2">
          <button
            type="button"
            className="btn btn-secondary btn-rounded"
            onClick={CarPage}
          >
            Car page
          </button>
        </div>
        <div className="col-md-2">
          <button
            type="button"
            className="btn btn-secondary btn-rounded"
            onClick={ClientCarPage}
          >
            Client car page
          </button>
        </div>
        <div className="col-md-2">
          <button
            type="button"
            className="btn btn-secondary btn-rounded"
            onClick={NewPage}
          >
            New page
          </button>
        </div>
        <div className="col-md-2">
          <button
            type="button"
            className="btn btn-secondary btn-rounded"
            onClick={OrderPage}
          >
            Order page
          </button>
        </div>
        <div className="row  w-100 h-25 bg-dark text-white ">
          <div className="col-md-2">
            {checkCarList && (
              <a href="/home/Car/add" className="text-reset">
                <i className="fa fa-plus-circle" aria-hidden="true"></i>
              </a>
            )}
          </div>
          <div className="col-md-2">
            {checkClientCarList && (
              <a href="/home/ClientCar/add" className="text-reset">
                <i className="fa fa-plus-circle" aria-hidden="true"></i>
              </a>
            )}
          </div>
          <div className="col-md-2">
            {checkNewList && (
              <a href="/home/New/add" className="text-reset">
                <i className="fa fa-plus-circle" aria-hidden="true"></i>
              </a>
            )}
          </div>
          <div className="col-md-2">
            {checkOrderList && (
              <a href="/home/Order/add" className="text-reset">
                <i className="fa fa-plus-circle" aria-hidden="true"></i>
              </a>
            )}
          </div>
        </div>
      </div>
      <div className="row">
        <div className="col container-fluid">
          {checkCarList && <CarList />}
          {checkClientCarList && <ClientCarList />}
          {checkNewList && <NewList />}
          {checkOrderList && <OrderList />}
        </div>
      </div>
    </div>
  );
};

export default HomeEmployee;
