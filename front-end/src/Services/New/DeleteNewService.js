import axios from "axios";
import GetJwtToken from "../GetJwtToken";
const URI = "http://localhost:37766/ServiceNew/api/New";
async function DeleteNew(title) {
  try {
    const response = await axios.delete(URI + "?title=" + title, {
      headers: GetJwtToken(),
    });
    return response;
  } catch (error) {
    return error.response;
  }
}
export default DeleteNew;
