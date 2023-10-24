import { Container } from "@mui/material";
import { useEffect } from "react";
import useAppSelector from "../hooks/useAppSelector";
import useAppDispatch from "../hooks/useAppDispatch";
import { fetchUserProfile } from "../redux/reducers/loginReducer";

const Unauthorized = () => {
  const dispatch = useAppDispatch();
  const { userProfile } = useAppSelector((state) => state.loginReducer);

  useEffect(() => {
    dispatch(fetchUserProfile());
  }, [dispatch, userProfile]);

  let message;

  if (userProfile?.role === "Client") {
    message =
      "Unauthorized. Only admins have access. Please contact the responsible person for access.";
  } else {
    message = "Invalid user role.";
  }

  return (
    <Container
      sx={{
        display: "flex",
        flexDirection: "column",
        alignItems: "center",
        justifyContent: "center",
        height: "100vh",
      }}
    >
      {message}
    </Container>
  );
};

export default Unauthorized;
