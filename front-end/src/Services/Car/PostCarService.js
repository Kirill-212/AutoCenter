import axios from "axios";
import GetJwtToken from "../GetJwtToken";
const URI = "http://localhost:37766/ServiceCar/api/Car";
async function PostCar(
  nameCarEquipment,
  cost,
  vin,
  dateOfRealeseCar,
  carMileage,
  sharePercentage = null
) {
  try {
    const response = await axios.post(
      URI,

      {
        NameCarEquipment: nameCarEquipment,
        Cost: cost,
        VIN: vin,
        DateOfRealeseCar: dateOfRealeseCar,
        CarMileage: carMileage,
        SharePercentage: sharePercentage,
      },
      { headers: GetJwtToken() }
    );
    return response;
  } catch (error) {
    return error.response;
  }
}
export default PostCar;
