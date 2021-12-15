import axios from "axios";
import GetJwtToken from "../GetJwtToken";
const URI = "http://localhost:37766/ServiceCar/api/Car";
async function GetByVinCar(vin) {
  try {
    const response = await axios.get(
      URI + "/GetByVin?vin=" + vin
      // { headers: GetJwtToken() },
    );
    return response;
  } catch (error) {
    return error.response;
  }
}
export default GetByVinCar;
