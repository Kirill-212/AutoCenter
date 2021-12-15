import axios from "axios";
import GetJwtToken from "../GetJwtToken";
const URI = "http://localhost:37766/ServiceUser/api/Role";
async function GetRole() {
  try {
    const response = await axios.get(URI, {
      headers: GetJwtToken(),
    });
    return response;
  } catch (error) {
    return error.response;
  }
}
export default GetRole;
