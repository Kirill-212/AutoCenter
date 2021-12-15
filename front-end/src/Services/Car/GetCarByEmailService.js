import axios from "axios";
import GetJwtToken from "../GetJwtToken";
const URI = "http://localhost:37766/ServiceCar/api/Car";
async function GetCarByEmail(email) {
  try {
    const response = await axios.get(
      URI + "/GetCarByEmail?email=" + email
      // { headers: GetJwtToken() },
    );
    return response;
  } catch (error) {
    return error.response;
  }
}
export default GetCarByEmail;
