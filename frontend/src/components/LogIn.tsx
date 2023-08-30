import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import {
  Button,
  Container,
  TextField,
  Typography,
  Link,
  Paper,
  Box,
} from "@mui/material";
import useAppSelector from "../hooks/useAppSelector";
import useAppDispatch from "../hooks/useAppDispatch";
import { login } from "../redux/reducers/usersReducer";

const LogIn = () => {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [message, setMessage] = useState("");
  const { user } = useAppSelector((state) => state.userReducer);
  const navigate = useNavigate();
  const dispatch = useAppDispatch();

  const Signin = async () => {
    if (email != "" && password != "") {
      const response = await dispatch(login({ email, password }));
      if (
        response.meta.requestStatus === "fulfilled" &&
        user?.firstName != ""
      ) {
        navigate("/");
      } else if (user?.firstName == "") {
        setMessage("There might be some errors");
      } else {
        setMessage("Email or password is incorrect. Please try again");
      }
    } else {
      setMessage("Please enter email and password !!");
    }
  };

  return (
    <Container component="div" maxWidth="xs" sx={{ marginTop: "10rem" }}>
      <Paper elevation={3} sx={{ padding: 3 }}>
        <Typography variant="h4" align="center">
          Sign In
        </Typography>
        <div>
          <TextField
            label="Email"
            variant="outlined"
            fullWidth
            margin="normal"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
          />
        </div>
        <div>
          <TextField
            label="Password"
            type="password"
            variant="outlined"
            fullWidth
            margin="normal"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
          />
        </div>
        <Typography variant="body2" color="error" align="center">
          {message}
        </Typography>
        <Button variant="contained" color="primary" fullWidth onClick={Signin}>
          Sign In
        </Button>
        <Box sx={{ textAlign: "center", marginTop: 2 }}></Box>
      </Paper>
      <footer>
        <Typography variant="body2" align="center"><br />
          &#169; 2023, PinnacleMall <br></br>
          <Link href="#">Conditions of Use</Link> <br /><br />
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
