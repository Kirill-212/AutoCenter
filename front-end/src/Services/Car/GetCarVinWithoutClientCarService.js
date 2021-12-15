import axios from "axios";
import GetJwtToken from "../GetJwtToken";
const URI = "http://localhost:37766/ServiceCar/api/Car";
async function GetCarVinWithoutClientCar() {
  try {
    const response = await axios.get(
      URI + "/GetWithoutClientCar"
      // { headers: GetJwtToken() },
    );
    return response;
  } catch (error) {
    return error.response;
  }
}
export default GetCarVinWithoutClientCar;
