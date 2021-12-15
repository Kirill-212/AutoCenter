import axios from "axios";
import GetJwtToken from "../GetJwtToken";
const URI = "http://localhost:37766/ServiceCar/api/ClientCar";
async function PutClientCar(
  email,
  newOwnerEmail = null,
  oldRegisterNumber,
  newRegisterNumber = null
) {
  try {
    const response = await axios.put(
      URI,
      // { headers: GetJwtToken() },
      {
        Email: email,
        NewOwnerEmail: newOwnerEmail,
        OldRegisterNumber: oldRegisterNumber,
        NewRegisterNumber: newRegisterNumber,
      }
    );
    return response;
  } catch (error) {
    return error.response;
  }
}
export default PutClientCar;
