import axios from "axios";
import GetJwtToken from "../GetJwtToken";
const URI = "http://localhost:37766/ServiceUser/api/Employee";
async function PutEmployee(address, email, roleId) {
  try {
    const response = await axios.put(
      URI,
      //   { headers: GetJwtToken() },
      {
        Address: address,
        Email: email,
        RoleId: roleId,
      }
    );
    return response;
  } catch (error) {
    return error.response;
  }
}
export default PutEmployee;
