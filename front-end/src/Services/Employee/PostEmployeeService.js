import axios from "axios";
import GetJwtToken from "../GetJwtToken";
const URI = "http://localhost:37766/ServiceUser/api/Employee";
async function PostEmployee(address, email, roleId) {
  try {
    console.log({ headers: GetJwtToken() });
    const response = await axios.post(
      URI,

      {
        Address: address,
        Email: email,
        RoleId: roleId,
      },
      { headers: GetJwtToken() }
    );
    return response;
  } catch (error) {
    return error.response;
  }
}
export default PostEmployee;
