import axios from "axios";
import GetJwtToken from "../GetJwtToken";
const URI = "http://localhost:37766/ServiceCar/api/CarEquipment";
async function GetFormCarEquipment() {
  try {
    const response = await axios.get(
      URI + "/GetForm"
      // { headers: GetJwtToken() },
    );
    return response;
  } catch (error) {
    return error.response;
  }
}
export default GetFormCarEquipment;
