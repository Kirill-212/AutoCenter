import axios from "axios";
import GetJwtToken from "../GetJwtToken";
const URI = "http://localhost:37766/ServiceCar/api/Order";
async function PosrOrder(email, vin, registerNumber) {
  try {
    const response = await axios.post(
      URI,
      // { headers: GetJwtToken() },
      {
        VIN: vin,
        Email: email,
        RegisterNumber: registerNumber,
      }
    );
    return response;
  } catch (error) {
    return error.response;
  }
}
export default PosrOrder;
