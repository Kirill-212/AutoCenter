import axios from "axios";
import GetJwtToken from "../GetJwtToken";
const URI = "http://localhost:37766/ServiceNew/api/New";
async function PutNew(title, description, email, arrImg) {
  try {
    const response = await axios.put(
      URI,
      {
        New: { Title: title, Description: description, Email: email },
        Imgs: arrImg,
      },
      {
        headers: GetJwtToken(),
      }
    );
    return response;
  } catch (error) {
    return error.response;
  }
}
export default PutNew;
