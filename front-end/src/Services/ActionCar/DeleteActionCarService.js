import axios from "axios";
import GetJwtToken from "../GetJwtToken";
const URI = "http://localhost:37766/ServiceCar/api/ActionCar";
async function DeleteActionCar() {
  try {
    const response = await axios.delete(URI, { headers: GetJwtToken() });
    return response;
  } catch (error) {
    return error.response;
  }
}
export default DeleteActionCar;
