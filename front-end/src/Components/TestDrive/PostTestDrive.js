import React, { useEffect, useContext } from "react";
import Context from "../../context";
import { Navigate } from "react-router-dom";
import GetVins from "../../Services/Car/GetCarForUserService";
import GetCarEuipmentByName from "../../Services/CarEquipment/GetCarEquipmentByNameService";
import PostTestDrives from "../../Services/TestDrive/PostTestDriveService";

const PostTestDrive = () => {
  const { user } = useContext(Context);
  const [time, setTime] = React.useState("");
  const [date, setDate] = React.useState("");
  const [MessageError, setMessageError] = React.useState("");
  const [redirect, setredirect] = React.useState(false);
  const [flagVin, setFlagVin] = React.useState(false);
  const [vinList, setVinList] = React.useState([]);
  const [vin, setVin] = React.useState("");

  async function GetCarEuipment(name) {
    let response = await GetCarEuipmentByName(name);
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
        return response.data;
      }
    }
  }

  async function GetVin() {
    setMessageError("");
    let response = await GetVins();
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
        setVinList(
          response.data.map((element) => {
            return { vin: element.vin };
          })
        );
        setFlagVin(true);
      }
    }
  }

  function getVinList() {
    let option = [];
    vinList.forEach((element) => {
      if (element !== undefined)
        option.push(<option value={element.vin}>{element.vin}</option>);
    });
    return option;
  }

  async function submitTestDrive(event) {
    event.preventDefault();
    setMessageError("");
    let response = await PostTestDrives(vin, time, date);
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
        setredirect(true);
      }
    }
  }

  function ValidField() {
    return vin.length > 0 && time.length > 0;
  }

  const styles = {
    maxWidth: "700px",
    border: "none",
  };

  useEffect(() => {
    GetVin();
  }, []);

  return (
    <div className="row mt-5">
      <div className="d-flex   justify-content-center align-items-center ">
        <div className="   p-4  w-100" style={styles}>
          <div className="row mt-5">
            <h1 className="d-flex   justify-content-center align-items-center ">
              Post test drive
            </h1>
          </div>
          <div className="row mt-5">
            <form onSubmit={submitTestDrive}>
              <div className="form-group mb-2">
                <p>
                  Select you time drive date:
                  <select
                    className="form-select"
                    aria-label="Default select example"
                    size="1"
                    onChange={(e) => setTime(e.target.value)}
                    required
                  >
                    <option value="10:00">10:00</option>
                    <option value="11:00">11:00</option>
                    <option value="12:00">12:00</option>
                    <option value="13:00">13:00</option>
                    <option value="14:00">14:00</option>
                    <option value="15:00">15:00</option>
                    <option value="16:00">16:00</option>
                  </select>
                </p>
              </div>
              <div className="form-group mb-2">
                <p>
                  Select you test drive date:
                  <input
                    className="form-select"
                    aria-label="Default select example"
                    className="shadow-lg  bg-white rounded ml-1"
                    type="date"
                    name="date"
                    onChange={(e) => setDate(e.target.value)}
                    required
                  />
                </p>
              </div>
              <div className="form-group mb-2 ">
                <label>Vin</label>
                <select
                  size="1"
                  className="form-select"
                  aria-label="Default select example"
                  onChange={(e) => setVin(e.target.value)}
                  required
                >
                  {flagVin && getVinList()}
                </select>
              </div>
              <div className="d-flex justify-content-center form-outline mb-3">
                <div className="flex-fill">
                  <button
                    type="submit"
                    disabled={!ValidField()}
                    className="btn btn-secondary btn-rounded w-100 "
                  >
                    Post
                  </button>
                </div>
              </div>
            </form>
          </div>
          <div className="row ">
            <div className="col">
              <a href={"/home/" + user.role}>Home</a>
            </div>
          </div>
          <div>
            {redirect && <Navigate to={"/home/" + user.role} />}
            <p>{MessageError}</p>
          </div>
        </div>
      </div>
    </div>
  );
};

export default PostTestDrive;
