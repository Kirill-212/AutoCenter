import axios from "axios";
import GetJwtToken from "../GetJwtToken";
const URI = "http://localhost:37766/ServiceCar/api/ClientCar";
async function PostCLientCar(
  nameCarEquipment,
  cost,
  vin,
  dateOfRealeseCar,
  carMileage,
  sharePercentage = null,
  email,
  registerNumber
) {
  try {
    const response = await axios.post(
      URI,

      {
        PostCarDto: {
          NameCarEquipment: nameCarEquipment,
          Cost: cost,
          VIN: vin,
          DateOfRealeseCar: dateOfRealeseCar,
          CarMileage: carMileage,
          SharePercentage: sharePercentage,
        },
        Email: email,
        RegisterNumber: registerNumber,
      },
      { headers: GetJwtToken() }
    );
    return response;
  } catch (error) {
    return error.response;
  }
}
export default PostCLientCar;
