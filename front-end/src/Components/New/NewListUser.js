import React, { useEffect } from "react";
import GetNews from "../../Services/New/GetNewService";
import CarouselUserPage from "./SliderUser";

const NewList = () => {
  const [MessageError, setMessageError] = React.useState("");
  const [listNews, setListNews] = React.useState([]);
  const [viewList, setViewList] = React.useState(false);

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

  const styles = {
    maxWidth: "800px",
    border: "none",
  };

  useEffect(() => {
    GetNewsList();
  }, []);

  return (
    <div className="row mt-5">
      <h1
        className="d-flex justify-content-center align-items-center "
        style={styles}
      >
        New List
      </h1>
      <div className="row">
        <p>{MessageError}</p>
      </div>
      <div className="row mt-5"></div>
      <CarouselUserPage imgs={listNews} />
    </div>
  );
};

export default NewList;
