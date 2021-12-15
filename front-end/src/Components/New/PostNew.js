import React, { useEffect, useContext } from "react";
import { Navigate } from "react-router-dom";
import PostNews from "../../Services/New/PostNewService";
import PostImgs from "../../Services/Img/ImgService";
import Context from "../../context";
const PostNew = () => {
  const [title, setTitle] = React.useState("");
  const [description, setDescription] = React.useState("");
  const [email, setEmail] = React.useState("");
  const [imgs, setImgs] = React.useState([]);
  const [count, setCount] = React.useState(1);
  const [MessageError, setMessageError] = React.useState("");
  const [redirectAdmin, setRedirectAdmin] = React.useState(false);
  const [failed, setFailed] = React.useState(false);
  const fileInput = React.useRef(null);
  const { user } = useContext(Context);

  async function submitNew(event) {
    event.preventDefault();
    if (imgs.length == 0) {
      setMessageError("Error:Input img");
      return;
    }
    let urls = [];
    for (let i = 0; i < imgs.length; i++) {
      let url = await PostImgs.uploadImage(imgs[i]);
      urls.push({ Url: url });
    }
    setMessageError("");
    let response = await PostNews(title, description, user.email, urls);
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
        setRedirectAdmin(true);
      }
    }
  }

  function AddImgs(value) {
    if (!value) {
      setFailed(true);
      return;
    }
    if (value.type.split("/")[0] !== "image") {
      setFailed(true);
    } else {
      imgs.push(value);
    }
  }

  function ValidField() {
    return (
      title.length > 0 && description.length > 0 && user.email !== undefined
    );
  }

  const styles = {
    maxWidth: "700px",
    border: "none",
  };

  function AddField() {
    setCount(count + 1);
  }

  function renderInput() {
    let rows = [];
    for (let i = 0; i < count; i++) {
      rows.push(
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
        </div>
      );
    }
    return rows;
  }

  return (
    <div className="row mt-5">
      <div className="d-flex   justify-content-center align-items-center ">
        <div className="   p-4  w-100" style={styles}>
          <div className="row mt-5">
            <h1 className="d-flex   justify-content-center align-items-center ">
              Post New
            </h1>
          </div>
          <div className="row mt-5">
            <form onSubmit={submitNew}>
              <div className="form-group mb-2 ">
                <label>Title</label>
                <input
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
                <div>{renderInput()}</div>
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
                    Post
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
            {redirectAdmin && <Navigate to={"/home/" + user.role} />}
            <p>{MessageError}</p>
          </div>
        </div>
      </div>
    </div>
  );
};

export default PostNew;
