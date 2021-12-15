import axios from "axios";
import GetJwtToken from "../GetJwtToken";
const URI = "http://localhost:37766/ServiceUser/api/Employee";
async function DeleteEmployee(email) {
  try {
    const response = await axios.delete(URI + "?email=" + email, {
      headers: GetJwtToken(),
    });
    return response;
  } catch (error) {
    return error.response;
  }
}
export default DeleteEmployee;
