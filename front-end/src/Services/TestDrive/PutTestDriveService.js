import axios from "axios";
import GetJwtToken from "../GetJwtToken";
const URI = "http://localhost:37766/ServiceCar/api/TestDrive";
async function PutTestDrive(vin, time, date) {
  try {
    const response = await axios.put(
      URI,
      // { headers: GetJwtToken() },
      {
        vin: vin,
        Time: time,
        Date: date,
      }
    );
    return response;
  } catch (error) {
    return error.response;
  }
}
export default PutTestDrive;
