import axios from "axios";
import GetJwtToken from "../GetJwtToken";
const URI = "http://localhost:37766/ServiceCar/api/Car";
async function PutCar(
  nameCarEquipment,
  cost,
  VIN,
  dateOfRealeseCar,
  carMileage,
  sharePercentage = null
) {
  try {
    const response = await axios.put(
      URI,
      // { headers: GetJwtToken() },
      {
        NameCarEquipment: nameCarEquipment,
        Cost: cost,
        VIN: VIN,
        DateOfRealeseCar: dateOfRealeseCar,
        CarMileage: carMileage,
        SharePercentage: sharePercentage,
      }
    );
    return response;
  } catch (error) {
    return error.response;
  }
}
export default PutCar;
