export default function GetJwtToken() {
  const user = JSON.parse(localStorage.getItem("user"));

  if (user && user.access_token) {
    return { authorization: "Bearer " + user.access_token };
  } else {
    return {};
  }
}
