import React, { useEffect, useContext } from "react";
import Context from "../../context";
import { Navigate } from "react-router-dom";
import PostCarsEquipment from "../../Services/CarEquipment/PostCarEquipmentService";
import GetFormCarEquipments from "../../Services/CarEquipment/GetFormCarEquipmentService";
import PostImgs from "../../Services/Img/ImgService";

const PostCarEquipment = () => {
  const { user } = useContext(Context);
  const [img, setimg] = React.useState("");
  const [name, setName] = React.useState("");
  const [MessageError, setMessageError] = React.useState("");
  const [redirect, setredirect] = React.useState(false);
  const [flag, setFlag] = React.useState(false);
  const [failed, setFailed] = React.useState(false);
  const [carEquipmentList, setCarEquipmentList] = React.useState([]);
  const fileInput = React.useRef(null);
  const [key, setKey] = React.useState([]);
  const [carEquipment, setCarEquipment] = React.useState([]);

  async function GetFormCarEquipment() {
    setMessageError("");
    let response = await GetFormCarEquipments();
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
        setCarEquipmentList(response.data);
        setFlag(true);
      }
    }
  }

  function RenderRedioButton(input, name) {
    let returnButtons = [];
    for (let j in input) {
      returnButtons.push(
        <div className="form-check">
          <label>
            Value:{input[j].value} Cost:{input[j].cost}
            <input
              className="form-check-input"
              type="radio"
              value={name + " " + input[j].value + " " + input[j].cost}
              name={name}
            />
          </label>
        </div>
      );
    }
    return returnButtons;
  }

  function RenderCarEquipment() {
    let radioButtonsCarEuipment = [];
    for (let i in carEquipmentList.equipmentItems) {
      if (!key.includes(i)) key.push(i);
      radioButtonsCarEuipment.push(
        <div>
          <label>{i}</label>
          <div onChange={(e) => onChangeValue(e)}>
            {RenderRedioButton(carEquipmentList.equipmentItems[i], i)}
          </div>
        </div>
      );
    }
    return radioButtonsCarEuipment;
  }

  async function submitCarEquipment(event) {
    event.preventDefault();
    setMessageError("");
    let count = 0;
    for (let i in carEquipmentList.equipmentItems) {
      count++;
    }
    if (carEquipment.length !== count) {
      setMessageError("Error:input al radio button");
      return;
    }
    let arr = "{";
    for (let i in carEquipment) {
      let str = JSON.stringify(carEquipment[i]).substring(1);
      str = str.substring(0, str.length - 1);
      arr += str + ",";
      console.log(str);
    }
    arr += "}";
    arr = arr.replace(",}", "}");
    let url = await PostImgs.uploadImage(img);
    let response = await PostCarsEquipment(name, JSON.parse(arr), url);
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
        setredirect(true);
      }
    }
  }

  function ValidField() {
    return name.length > 0;
  }

  function AddImgs(value) {
    if (!value) {
      setFailed(true);
      return;
    }
    if (value.type.split("/")[0] !== "image") {
      setFailed(true);
    } else {
      setimg(value);
    }
  }

  function onChangeValue(event) {
    let arr = event.target.value.split(" ");
    let rs = `{"${arr[0]}":{"value":"${arr[1]}","cost":${arr[2]}}}`;
    rs = JSON.parse(rs);
    if (carEquipment.length === 0) {
      carEquipment.push(rs);
    } else {
      for (let variablqe in carEquipment) {
        if (carEquipment[variablqe][arr[0]] !== undefined) {
          carEquipment[variablqe][arr[0]] = rs[arr[0]];
          if (carEquipment[variablqe][arr[0]] === rs[arr[0]]) {
          }
        } else if (carEquipment.length === 1) {
          carEquipment.push(rs);
        }
      }
    }
  }

  const styles = {
    maxWidth: "700px",
    border: "none",
  };

  useEffect(() => {
    GetFormCarEquipment();
  }, []);

  return (
    <div className="row mt-5">
      <div className="d-flex   justify-content-center align-items-center ">
        <div className="p-4  w-100" style={styles}>
          <div className="row mt-5">
            <h1 className="d-flex   justify-content-center align-items-center ">
              Post car equipment
            </h1>
          </div>
          <div className="row mt-5">
            <form onSubmit={submitCarEquipment}>
              <div className="form-group mb-2 ">
                <label>Name car equipment</label>
                <input
                  className="w-100 shadow-lg  bg-white rounded"
                  onChange={(e) => setName(e.target.value)}
                  name="name"
                  type="text"
                  placeholder="Enter your name car equipment..."
                />
              </div>
              <div className="form-group mb-2 ">
                <label>Image</label>
                <div className="input-group mb-3">
                  <div className="input-group-prepend">
                    <span className="input-group-text">Upload</span>
                  </div>
                  <div className="custom-file">
                    <input
                      type="file"
                      accept="image/*"
                      ref={fileInput}
                      onChange={(e) => AddImgs(e.target.files[0])}
                      className="custom-file-input"
                      id="inputGroupFile01"
                    />
                    <label className="custom-file-label" for="inputGroupFile01">
                      Choose file
                    </label>
                  </div>
                  {failed && (
                    <span className="badge badge-warning">
                      Wrong file type!
                    </span>
                  )}
                </div>
              </div>
              <div className="form-group mb-2 ">
                <label>Car equipment</label>
                {flag && RenderCarEquipment()}
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

export default PostCarEquipment;
