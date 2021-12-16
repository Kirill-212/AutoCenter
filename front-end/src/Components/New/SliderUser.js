import React from "react";

const CarouselPage = (props) => {
  const styles = {
    maxWidth: "700px",
    border: "none",
  };

  return (
    <div className="container-lg" style={styles}>
      {props.imgs.map((element) => {
        let flag = true;
        let i = element.id;
        return (
          <div
            id={"slider" + i}
            className="carousel slide carousel-fade carousel-dark row justify-content-center align-self-center p-4 w-100 border"
            data-mdb-ride="carousel"
          >
            <h1>{element.title}</h1>
            <h5>{element.description}</h5>
            <div className="carousel-inner">
              {element.imgs.map((element) => {
                if (flag) {
                  flag = !flag;
                  return (
                    <div className="carousel-item active h-50 w-100">
                      <img src={element.url} className="d-block w-100" />
                    </div>
                  );
                } else {
                  return (
                    <div className="carousel-item h-50 w-100">
                      <img src={element.url} className="d-block  w-100" />
                    </div>
                  );
                }
              })}
            </div>
            <button
              className="carousel-control-prev pt-5 pl-5 mt-5"
              type="button"
              data-mdb-target={"#slider" + i}
              data-mdb-slide="prev"
            >
              <span
                className="carousel-control-prev-icon"
                aria-hidden="true"
              ></span>
              <span className="visually-hidden">Previous</span>
            </button>
            <button
              className="carousel-control-next pt-5 pr-5 mt-5"
              type="button"
              data-mdb-target={"#slider" + i}
              data-mdb-slide="next"
            >
              <span
                className="carousel-control-next-icon"
                aria-hidden="true"
              ></span>
              <span className="visually-hidden">Next</span>
            </button>
          </div>
        );
      })}
    </div>
  );
};
export default CarouselPage;
