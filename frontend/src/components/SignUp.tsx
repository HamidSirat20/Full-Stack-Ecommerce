import { useState, useEffect, useRef } from "react";
import { useNavigate } from "react-router-dom";
import {
  Button,
  TextField,
  Typography,
  Container,
  Link,
  Box,
} from "@mui/material";

import useAppDispatch from "../hooks/useAppDispatch";
import useAppSelector from "../hooks/useAppSelector";
import User from "../types/User";
import { createUser, getAllUsers } from "../redux/reducers/usersReducer";

const SignUp = () => {
  const [fname, setFName] = useState("");
  const [lname, setLName] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [password1, setPassword1] = useState("");
  const [avatar, setAvatar] = useState("");

  const [message, setMessage] = useState("");

  const canSave =
    Boolean(fname) &&
    Boolean(lname) &&
    Boolean(email) &&
    Boolean(password) &&
    Boolean(password1);

  const { users } = useAppSelector((state) => state.userReducer);
  const dispatch = useAppDispatch();
  const navigate = useNavigate();

  const focRef = useRef<HTMLInputElement | null>(null);

  useEffect(() => {
    focRef.current?.focus();
    dispatch(getAllUsers({ limit: 30, offset: 0 }));
  }, []);

  const AddUser = () => {
    let emailAvailable: User[] = users.filter((u) => u.email === email);
    if (emailAvailable.length === 0) {
      if (
        fname === "" ||
        lname === "" ||
        email === "" ||
        password === "" ||
        password1 === "" ||
        avatar === ""
      ) {
        setMessage("Please enter all input");
      } else if (
        email.includes("@") === false ||
        email.includes(".") === false
      ) {
        setMessage("This is not an email");
      } else if (password !== password1) {
        setMessage("Re-enter password does not match password.");
      } else {
        dispatch(
          createUser({
            userData: {
              firstName: fname,
              lastName: lname,
              email: email,
              password: password,
              avatar: avatar,
            },
          })
        );
        navigate("/signin");
      }
    } else {
      alert(`This email is already registered !`);
    }
  };

  return (
    <Container
      component="div"
      maxWidth="xs"
      sx={{ marginTop: "4rem", paddingBottom: "4.4rem" }}
    >
      <Typography variant="h4" align="center">
        Create account
      </Typography>
      <div>
        <TextField
          label="Your First Name"
          variant="outlined"
          fullWidth
          margin="dense"
          value={fname}
          onChange={(e) => setFName(e.target.value)}
          inputRef={focRef}
        />
        <div>
          <TextField
            label="Your Last Name"
            variant="outlined"
            fullWidth
            margin="dense"
            value={lname}
            onChange={(e) => setLName(e.target.value)}
          />
        </div>

        <div>
          <TextField
            label="Email"
            variant="outlined"
            fullWidth
            margin="dense"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
          />
        </div>

        <div>
          <TextField
            label="Avatar"
            variant="outlined"
            fullWidth
            margin="dense"
            value={avatar}
            onChange={(e) => setAvatar(e.target.value)}
          />
        </div>

        <div>
          <TextField
            label="Password"
            type="password"
            variant="outlined"
            fullWidth
            margin="dense"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
          />
        </div>

        <div>
          <TextField
            label="Re-enter Password"
            type="password"
            variant="outlined"
            fullWidth
            margin="dense"
            value={password1}
            onChange={(e) => setPassword1(e.target.value)}
          />
        </div>
      </div>
      <Typography variant="body2" color="error" align="center">
        {message}
      </Typography>
      <Button
        disabled={!canSave}
        variant="contained"
        color="primary"
        fullWidth
        onClick={AddUser}
      >
        Sign Up
      </Button>
      <div>
        <Typography variant="body2" align="center">
          By creating an account, you agree to PinnacleMall's
          <Link href="#">Conditions of Use</Link>
        </Typography>
      </div>
      <Box sx={{ textAlign: "center", marginTop: 2 }}>
        <Button
          variant="outlined"
          color="primary"
          onClick={() => navigate("/signin")}
        >
          Aready have an account?
        </Button>
      </Box>
      <footer>
        <Typography variant="body2" align="center">
          &#169; 2023, PinnacleMall.com
        </Typography>
      </footer>
    </Container>
  );
};

export default SignUp;
