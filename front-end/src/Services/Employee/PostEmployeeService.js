import axios from "axios";
import GetJwtToken from "../GetJwtToken";
const URI = "http://localhost:37766/ServiceUser/api/Employee";
async function PostEmployee(address, email, roleId) {
  try {
    const response = await axios.post(
      URI,
      // { headers: GetJwtToken() },
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
export default PostEmployee;
