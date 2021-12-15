import axios from "axios";
import GetJwtToken from "../GetJwtToken";
const URI = "http://localhost:37766/ServiceUser/api/User";
async function GetAllUsersNotAddedToEmp() {
  try {
    const response = await axios.get(URI + "/GetAllUsersNotAddedToEmp", {
      headers: GetJwtToken(),
    });
    return response;
  } catch (error) {
    return error.response;
  }
}
export default GetAllUsersNotAddedToEmp;
