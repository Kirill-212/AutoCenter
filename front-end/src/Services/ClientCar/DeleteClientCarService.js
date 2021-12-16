import axios from "axios";
import GetJwtToken from "../GetJwtToken";
const URI = "http://localhost:37766/ServiceCar/api/ClientCar";
async function DeleteClientCar(registerNumber) {
  try {
    const response = await axios.delete(
      URI + "?registerNumber=" + registerNumber,
      { headers: GetJwtToken() }
    );
    return response;
  } catch (error) {
    return error.response;
  }
}
export default DeleteClientCar;
