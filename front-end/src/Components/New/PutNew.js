import React, { useEffect, useContext } from "react";
import { Navigate } from "react-router-dom";
import PutNews from "../../Services/New/PutNewService";
import PostImgs from "../../Services/Img/ImgService";
import GetByTitles from "../../Services/New/GetByTitle";
import Context from "../../context";

const PutNew = () => {
  const [title, setTitle] = React.useState("");
  const [description, setDescription] = React.useState("");
  const [email, setEmail] = React.useState("");
  const [MessageError, setMessageError] = React.useState("");
  const [redirect, setredirect] = React.useState(false);
  const [failed, setFailed] = React.useState(false);
  const [flagCheckEmp, setFlagCheckEmp] = React.useState(false);
  const [news, setNews] = React.useState({});
  const fileInput = React.useRef(null);
  const { user } = useContext(Context);

  async function GetNews(title) {
    setMessageError("");
    let response = await GetByTitles(title);
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
        if (response.data.employee.user.email === user.email) {
          setFlagCheckEmp(true);
        }
        setNews(response.data.imgs);
        setDescription(response.data.description);
        setEmail(response.data.employee.user.email);
      }
    }
  }

  async function submitNew(event) {
    event.preventDefault();
    if (user.email !== email) {
      setMessageError("Check Email and your email");
      return;
    }
    if (news.length == 0) {
      setMessageError("Error:Input img");
      return;
    }
    let urls = [];
    for (let i = 0; i < news.length; i++) {
      if (news[i].name !== undefined) {
        let url = await PostImgs.uploadImage(news[i]);
        urls.push({ Url: url });
      } else {
        urls.push({ Url: news[i].url });
      }
    }
    setMessageError("");
    let response = await PutNews(title, description, email, urls);
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

  function AddImgs(value, i) {
    if (!value) {
      setFailed(true);
      return;
    }
    if (value.type.split("/")[0] !== "image") {
      setFailed(true);
    } else {
      news[i] = value;
    }
  }

  function ValidField() {
    return (
      title.length > 0 && description.length > 0 && user.email !== undefined
    );
  }

  function AddField() {
    setNews([...news, {}]);
  }

  function renderInput() {
    let imgs = news;
    let rows = [];
    for (let i = 0; i < imgs.length; i++) {
      rows.push(
        <div className="drop_zone">
          {imgs[i].url === undefined && (
            <div className="input-group mb-3">
              <div className="input-group-prepend">
                <span className="input-group-text">Upload</span>
              </div>
              <div className="custom-file">
                <input
                  type="file"
                  accept="image/*"
                  ref={fileInput}
                  onChange={(e) => AddImgs(e.target.files[0], i)}
                  className="custom-file-input"
                  id="inputGroupFile01"
                />
                <label className="custom-file-label" for="inputGroupFile01">
                  Choose file
                </label>
              </div>
            </div>
          )}
          {imgs[i].url !== undefined && (
            <div>
              <div className="input-group mb-3">
                <div className="input-group-prepend">
                  <span className="input-group-text">Upload</span>
                </div>
                <div className="custom-file">
                  <input
                    type="file"
                    accept="image/*"
                    ref={fileInput}
                    onChange={(e) => AddImgs(e.target.files[0], i)}
                    className="custom-file-input"
                    id="inputGroupFile01"
                  />
                  <label className="custom-file-label" for="inputGroupFile01">
                    Choose file
                  </label>
                </div>
              </div>
              <img src={imgs[i].url} className="w-25 h-25" />
            </div>
          )}
        </div>
      );
    }
    return rows;
  }
  const styles = {
    maxWidth: "700px",
    border: "none",
  };

  useEffect(() => {
    const query = new URLSearchParams(window.location.search);
    GetNews(query.get("title"));
    setTitle(query.get("title"));
  }, []);

  return (
    <div className="row mt-1">
      <div className="d-flex   justify-content-center align-items-center ">
        <div className="   p-4  w-100" style={styles}>
          <div className="row mt-2">
            <h1 className="d-flex   justify-content-center align-items-center ">
              Put New
            </h1>
          </div>
          <div className="row mt-1">
            <form onSubmit={submitNew}>
              <div className="form-group mb-2 ">
                <label>Title</label>
                <input
                  disabled
                  value={title}
                  className="w-100 shadow-lg  bg-white rounded"
                  onChange={(e) => setTitle(e.target.value)}
                  name="title"
                  type="text"
                  placeholder="Enter your title..."
                />
              </div>
              <div className="form-group mb-2 ">
                <label>Description</label>
                <input
                  value={description}
                  className="w-100 shadow-lg  bg-white rounded"
                  onChange={(e) => setDescription(e.target.value)}
                  name="description"
                  type="text"
                  placeholder="Enter your description..."
                />
              </div>
              <div className="form-group mb-2 ">
                <label>Email</label>
                <input
                  disabled
                  value={user.email}
                  className="w-100 shadow-lg  bg-white rounded"
                  onChange={(e) => setEmail(e.target.value)}
                  name="email"
                  type="text"
                  placeholder="Enter your email..."
                />
              </div>
              <div className="form-group mb-2 ">
                <label>Imgs</label>
                <br />
                {news !== undefined && renderInput(news)}
                {failed && (
                  <span className="badge badge-warning">Wrong file type!</span>
                )}
              </div>
              <div className="d-flex justify-content-center form-outline mb-3 p-5">
                <div className="flex-fill">
                  <button
                    type="submit"
                    disabled={!ValidField()}
                    className="btn btn-secondary btn-rounded w-100 "
                  >
                    Put
                  </button>
                </div>
              </div>
            </form>
          </div>
          <div>
            <button className="btn btn-dark btn-rounded" onClick={AddField}>
              Add input file
            </button>
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

export default PutNew;
