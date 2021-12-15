import axios from "axios";
import GetJwtToken from "../GetJwtToken";
const URI = "http://localhost:37766/ServiceUser/api/User";
async function PutStatusUser(email) {
  try {
    const response = await axios.put(
      URI + "/UpdateStatus?email=" + email
      // {
      //   headers: GetJwtToken(),
      // }
    );
    return response;
  } catch (error) {
    return error.response;
  }
}
export default PutStatusUser;
