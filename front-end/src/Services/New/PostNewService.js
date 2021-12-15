import axios from "axios";
import GetJwtToken from "../GetJwtToken";
const URI = "http://localhost:37766/ServiceNew/api/New";
async function PostNew(title, description, email, arrImg) {
  try {
    const response = await axios.post(
      URI,
      // {
      //   headers: GetJwtToken(),
      // },
      {
        New: { Title: title, Description: description, Email: email },
        Imgs: arrImg,
      }
    );
    return response;
  } catch (error) {
    return error.response;
  }
}
export default PostNew;
