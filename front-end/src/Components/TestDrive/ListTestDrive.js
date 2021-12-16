import React, { useEffect } from "react";
import GetTestDriveLists from "../../Services/TestDrive/GetTestDriveService";
import SetHomeListTestDriveListAdminTable from "../../SetTable/SetHomeListTestDriveAdminTable";
import UpdateStatusTestDrive from "../../Services/TestDrive/PutTestDriveService";
import { MDBDataTableV5 } from "mdbreact";

const TestDriveList = () => {
  const [MessageError, setMessageError] = React.useState("");
  const [listTestDriveLists, setListTestDriveLists] = React.useState({});
  const [viewList, setViewList] = React.useState(false);

  async function GetTestDriveListsList() {
    let response = await GetTestDriveLists();
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
        setListTestDriveLists({
          columns: SetHomeListTestDriveListAdminTable().columns,
          rows: SetOption(response.data),
        });
        setViewList(true);
      }
    }
  }

  async function UpdateStatus(e) {
    let arr = e.currentTarget.value.split(" ");
    let response = await UpdateStatusTestDrive(arr[2], arr[1], arr[0]);
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
        GetTestDriveListsList();
      }
    }
  }

  function SetOption(data) {
    return data.map(function (obj) {
      return {
        options: (
          <>
            <button
              color="purple"
              size="sm"
              value={obj.date + " " + obj.time + " " + obj.car.vin}
              onClick={UpdateStatus}
            >
              {obj.isActive === 1 && (
                <i className="fa fa-ban" aria-hidden="true"></i>
              )}
              {obj.isActive !== 1 && (
                <i className="fa fa-car" aria-hidden="true"></i>
              )}
            </button>
          </>
        ),
        date: obj.date,
        time: obj.time,
        isActive: obj.isActive == 1 ? "ACTIVE" : "NOT ACTIVE",
        vin: obj.car.vin,
      };
    });
  }

  useEffect(() => {
    GetTestDriveListsList();
  }, []);

  return (
    <div className="row mt-5">
      <div className="row">
        <h1 className="d-flex justify-content-center align-items-center ">
          Test drive List
        </h1>
      </div>
      <div className="row">
        <p>{MessageError}</p>
      </div>
      <div className="row mt-5">
        {viewList && (
          <MDBDataTableV5
            hover
            entriesOptions={[5, 20, 25]}
            entries={5}
            pagesAmount={4}
            data={listTestDriveLists}
          />
        )}
      </div>
    </div>
  );
};

export default TestDriveList;
