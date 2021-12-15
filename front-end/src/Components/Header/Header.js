import React, { useContext, useEffect, useState } from "react";
import LogOut from "../Auth/LogOut";
import Context from "../../context";

function Header() {
  const { user } = useContext(Context);
  const [welcomeMessage, setWelcomeMessage] = useState("");

  useEffect(() => {
    if (user !== undefined) setWelcomeMessage(`hello, ${user["email"]}`);
    else if (user === undefined) setWelcomeMessage("hello");
  }, [user]);

  return (
    <div className="d-flex justify-content-between  flex-row bg-dark text-white ">
      <div className="py-2 pl-2 pt-1">{welcomeMessage}</div>
      <div className=" pr-3 pt-1 ">Auto Center VW</div>
      <div className=" py-2 pr-3 pt-2 ">
        <LogOut />
      </div>
    </div>
  );
}
export default Header;
