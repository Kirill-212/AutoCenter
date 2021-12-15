import axios from "axios";
const URI = "http://localhost:37766/ServiceAuth/api/Auth";
async function Authorization(email, password) {
  try {
    const response = await axios.post(URI, {
      email: email,
      password: password,
    });
    return response;
  } catch (error) {
    return error.response;
  }
}
export default Authorization;
