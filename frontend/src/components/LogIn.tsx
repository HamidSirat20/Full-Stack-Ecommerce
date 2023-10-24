import { useEffect, useRef, useState } from "react";
import { useNavigate } from "react-router-dom";
import {
  Button,
  Container,
  TextField,
  Typography,
  Link,
  Paper,
  Box,
  FormControl,
} from "@mui/material";
import useAppSelector from "../hooks/useAppSelector";
import useAppDispatch from "../hooks/useAppDispatch";
import { autoLogout, userLogin } from "../redux/reducers/loginReducer";

const LogIn = () => {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [message, setMessage] = useState("");
  const { loading, error } = useAppSelector((state) => state.loginReducer);
  const navigate = useNavigate();
  const dispatch = useAppDispatch();

  const focRef = useRef<HTMLInputElement | null>(null);
  useEffect(() => {
    focRef.current?.focus();
  }, []);

  // const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
  //   e.preventDefault();
  //   let userCredentials = {
  //     email,
  //     password,
  //   };
  //   dispatch(userLogin(userCredentials))
  //     .then((result) => {
  //       if (result.payload) {
  //         setEmail("");
  //         setPassword("");
  //         navigate("/profile");
  //       }
  //     })
  //     .then(() => {
  //       setTimeout(() => {
  //         dispatch(autoLogout());
  //       }, 30 * 60 * 1000);
  //     });
  // };
  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    let userCredentials = {
      email,
      password,
    };

    try {
      const result = await dispatch(userLogin(userCredentials));
      if (result.payload) {
        setEmail("");
        setPassword("");
        navigate("/profile");
      }

      await new Promise((resolve) => setTimeout(resolve, 30 * 60 * 1000));

      dispatch(autoLogout());
    } catch (error) {
      console.error(error);
    }
  };

  return (
    <Container
      component="div"
      maxWidth="xs"
      sx={{
        display: "flex",
        flexDirection: "column",
        alignItems: "center",
        justifyContent: "center",
        height: "100vh",
      }}
    >
      <Paper elevation={3} sx={{ padding: 3 }}>
        <Typography variant="h4" align="center">
          Sign In
        </Typography>
        <form onSubmit={handleSubmit}>
          <FormControl fullWidth margin="normal">
            <TextField
              label="Email"
              variant="outlined"
              fullWidth
              value={email}
              onChange={(e) => setEmail(e.target.value)}
              required
              inputRef={focRef}
            />
          </FormControl>
          <FormControl fullWidth margin="normal">
            <TextField
              label="Password"
              type="password"
              variant="outlined"
              fullWidth
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              required
            />
          </FormControl>
          <Typography variant="body2" color="error" align="center">
            {message}
          </Typography>
          <Button type="submit" variant="contained" color="primary" fullWidth>
            {loading ? "Loading..." : "login"}
          </Button>
          {error && <Typography>{error}</Typography>}
        </form>
        <Box sx={{ textAlign: "center", marginTop: 2 }}></Box>
      </Paper>
      <footer>
        <Typography variant="body2" align="center">
          <br />
          &#169; 2023, PinnacleMall <br></br>
          <Link href="#">Conditions of Use</Link> <br />
          <br />
          <Button
            variant="outlined"
            color="primary"
            onClick={() => navigate("/signup")}
          >
            Create an account
          </Button>
        </Typography>
      </footer>
    </Container>
  );
};

export default LogIn;
