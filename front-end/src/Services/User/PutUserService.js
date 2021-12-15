import axios from "axios";
import GetJwtToken from "../GetJwtToken";
const URI = "http://localhost:37766/ServiceUser/api/User";
async function PutUser(
  firstName,
  lastName,
  surname,
  dBay,
  password,
  email,
  phoneNumber
) {
  try {
    const response = await axios.put(
      URI,
      // { headers: GetJwtToken() },
      {
        FirstName: firstName,
        LastName: lastName,
        Surname: surname,
        DBay: dBay,
        Password: password,
        Email: email,
        PhoneNumber: phoneNumber,
      }
    );
    return response;
  } catch (error) {
    return error.response;
  }
}
export default PutUser;
