import axios from "axios";
const URI = "http://localhost:37766/ServiceUser/api/User";
async function Register(
  firstName,
  lastName,
  surname,
  dBay,
  password,
  email,
  phoneNumber
) {
  try {
    const response = await axios.post(URI, {
      FirstName: firstName,
      LastName: lastName,
      Surname: surname,
      DBay: dBay,
      Password: password,
      Email: email,
      PhoneNumber: phoneNumber,
    });
    return response;
  } catch (error) {
    return error.response;
  }
}
export default Register;
