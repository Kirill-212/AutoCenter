import React, { useEffect } from "react";
import GetNews from "../../Services/New/GetNewService";
import CarouselPage from "./Slider";
import DeleteNew from "../../Services/New/DeleteNewService";

const NewList = () => {
  const [MessageError, setMessageError] = React.useState("");
  const [listNews, setListNews] = React.useState([]);
  const [viewList, setViewList] = React.useState(false);
  const [title, setTitle] = React.useState("");

  async function GetNewsList() {
    let response = await GetNews();
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
        setListNews(response.data);
        setViewList(true);
      }
    }
  }

  async function DeleteNews(e) {
    if (title === null || title === undefined || title === "") {
      setMessageError("Error:Select Title");
      return;
    }
    let response = await DeleteNew(title);
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
        } else if (response.data.ERROR.details !== undefined) {
          setMessageError(response.data.ERROR.details[0]["message"]);
        } else {
          setMessageError(response.data);
        }
      } else {
        GetNewsList();
      }
    }
  }

  const styles = {
    maxWidth: "800px",
    border: "none",
  };

  useEffect(() => {
    GetNewsList();
  }, []);

  return (
    <div className="row mt-5">
      <h1 className="d-flex justify-content-center align-items-center ">
        New List
      </h1>
      <div className="row">
        <p>{MessageError}</p>
      </div>
      <div className="row mt-5">
        <div className="d-flex justify-content-center align-items-center ">
          <label>Delete news</label>
          <select
            size="1"
            value={title}
            className="form-select w-50"
            aria-label="Default select example"
            onChange={(e) => setTitle(e.target.value)}
          >
            {listNews.map((element) => {
              return <option value={element.title}>{element.title}</option>;
            })}
          </select>
          <button color="purple" size="sm" onClick={DeleteNews}>
            <i className="fa fa-trash" aria-hidden="true "></i>
          </button>
        </div>
      </div>
      <CarouselPage imgs={listNews} />
    </div>
  );
};

export default NewList;
