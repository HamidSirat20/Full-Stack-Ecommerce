import { Button, CircularProgress, Container } from "@mui/material";
import { logout } from "../redux/reducers/loginReducer";
import useAppDispatch from "../hooks/useAppDispatch";
import useAppSelector from "../hooks/useAppSelector";

const Logout = () => {
  const dispatch = useAppDispatch();
  const { loading } = useAppSelector((state) => state.loginReducer);

  const handleLogout = () => {
    dispatch(logout());
  };

  if (loading) {
    return (
      <Container
        maxWidth="md"
        style={{
          marginTop: "5rem",
          textAlign: "center",
          display: "flex",
          flexDirection: "column",
          justifyContent: "center",
          alignItems: "center",
          minHeight: "100vh",
        }}
      >
        <CircularProgress />
      </Container>
    );
  }

  return (
    <Container
      maxWidth="md"
      style={{
        marginTop: "4.2rem",
        paddingBottom: "5rem",
        minHeight: "100vh",
        display: "flex",
        flexDirection: "column",
        justifyContent: "center",
        alignItems: "center",
      }}
    >
      <Button color="error" variant="contained" onClick={handleLogout}>
        Logout
      </Button>
    </Container>
  );
};

export default Logout;
