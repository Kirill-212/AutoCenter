import axios from "axios";
import GetJwtToken from "../GetJwtToken";
const URI = "http://localhost:37766/ServiceCar/api/CarEquipment";
async function PostCarEquipment(name, arr, url) {
  try {
    const response = await axios.post(
      URI,

      {
        name: name,
        equipment: arr,
        urlImg: url,
      },
      { headers: GetJwtToken() }
    );
    return response;
  } catch (error) {
    return error.response;
  }
}
export default PostCarEquipment;
