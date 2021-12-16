import React from "react";
import { Route, BrowserRouter as Router, Routes } from "react-router-dom";
import Registration from "./Components/Auth/Registration"; //all +
import Authorization from "./Components/Auth/Authorization"; //all +
import PutUser from "./Components/User/PutUser"; //admin +
import PostEmployee from "./Components/Employee/PostEmployee"; //admin +
import PutEmployee from "./Components/Employee/PutEmployee"; //admin +
import PostNew from "./Components/New/PostNew"; //admin emp +
import ListNew from "./Components/New/NewList"; //admin emp user
import PutNew from "./Components/New/PutNew"; //admin emp
import PostCar from "./Components/Car/PostCar"; //addmin emp
import PutCar from "./Components/Car/PutCar"; //admin emp
import ListCar from "./Components/Car/CarList"; //admin emp
import DetailsCar from "./Components/Car/DetailsCar"; //admin emp
import PostClientCat from "./Components/ClientCar/PostClientCar"; //admin emp
import ListClientCar from "./Components/ClientCar/ClientCarList"; //admin emp
import PutClientCar from "./Components/ClientCar/PutClientCar"; //admin emp
import PostOrder from "./Components/Order/PostOrder"; //admin emp
import ListOrder from "./Components/Order/ListOrder"; //admin emp
import HomeAdmin from "./Components/Home/HomeAdmin"; //admin
import HomeEmployee from "./Components/Home/HomeEmployee"; //emp
import Header from "./Components/Header/Header"; //all
import Context from "./context"; //all
import PostTestDrive from "./Components/TestDrive/PostTestDrive"; //all
import ListTestDrive from "./Components/TestDrive/ListTestDrive"; //admin emp
import ListCarEquipment from "./Components/CarEquipment/ListCarEquipment"; //all
import PostCarEquipment from "./Components/CarEquipment/PostCarEquipment"; //admin emp
import PutCarEquipment from "./Components/CarEquipment/PutCarEquipment"; //admin emp
import CarUser from "./Components/Car/BuyCarList"; //all
import CarUserDetail from "./Components/Car/DetailsBuyCar"; //all
import BuyCar from "./Components/Order/PostOrderBuy"; //all
import UserCarList from "./Components/Car/UserCar"; //all
import UserCarPut from "./Components/Car/PutCarUser"; //all
import NewListUser from "./Components/New/NewListUser";
import HomeUser from "./Components/Home/HomeUser";
function App() {
  const [user, setUser] = React.useState(undefined);
  console.log("user", user);
  if (user === undefined) {
    console.log(localStorage.getItem("user"));
    if (JSON.parse(localStorage.getItem("user")))
      setUser(JSON.parse(localStorage.getItem("user")));
  }
  return (
    <Context.Provider value={{ user, setUser }}>
      <Router>
        <Header />
        <Routes>
          <Route exact path="/" element={<Registration />} />
          <Route path="/login" element={<Authorization />} />
          <Route path="/home/ADMIN" element={<HomeAdmin />} />
          <Route path="/home/EMPLOYEE" element={<HomeEmployee />} />
          <Route path="/home/USER" element={<HomeUser />} />
          <Route exact path="/home/Employee" element={<HomeAdmin />} />
          <Route path="/home/Employee/add" element={<PostEmployee />} />
          <Route path="/home/Employee/put" element={<PutEmployee />} />
          <Route path="/home/put/user" element={<PutUser />} />
          <Route path="/home/Car" element={<ListCar />} />
          <Route path="/home/car/details" element={<DetailsCar />} />
          <Route path="/home/Car/add" element={<PostCar />} />
          <Route path="/home/Car/put" element={<PutCar />} />
          <Route path="/home/ClientCar/add" element={<PostClientCat />} />
          <Route path="/home/ClientCar" element={<ListClientCar />} />
          <Route path="/home/ClientCar/put" element={<PutClientCar />} />
          <Route path="/home/New/add" element={<PostNew />} />
          <Route path="/home/New/put" element={<PutNew />} />
          <Route path="/home/New" element={<ListNew />} />
          <Route path="/home/Order/add" element={<PostOrder />} />
          <Route path="/home/Order" element={<ListOrder />} />
          <Route path="/home/TestDrive" element={<ListTestDrive />} />
          <Route path="/home/TestDrive/add" element={<PostTestDrive />} />
          <Route path="/home/CarEquipment" element={<ListCarEquipment />} />
          <Route path="/home/CarEquipment/add" element={<PostCarEquipment />} />
          <Route path="/home/CarEquipment/put" element={<PutCarEquipment />} />
          <Route path="/home/Car/ListBuy" element={<CarUser />} />
          <Route path="/home/Car/DetailsBuy" element={<CarUserDetail />} />
          <Route path="/home/Car/Buy" element={<BuyCar />} />
          <Route path="/home/Car/User" element={<UserCarList />} />
          <Route path="/home/Car/User/put" element={<UserCarPut />} />
          <Route path="/home/NewUser" element={<NewListUser />} />
        </Routes>
      </Router>
    </Context.Provider>
  );
}

export default App;
