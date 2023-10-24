import React, { useEffect } from "react";
import { Button, CircularProgress, Container, Typography } from "@mui/material";
import { fetchUserProfile, logout } from "../redux/reducers/loginReducer";
import useAppDispatch from "../hooks/useAppDispatch";
import useAppSelector from "../hooks/useAppSelector";
import avatar from "../data/avatar.png";

const Profile = () => {
  const dispatch = useAppDispatch();
  const { userProfile, loading, error } = useAppSelector(
    (state) => state.loginReducer
  );

  useEffect(() => {
    dispatch(fetchUserProfile());
  }, [dispatch]);

  const handleLogout = () => {
    dispatch(logout());
  };

  if (loading) {
    return (
      <Container
        maxWidth="md"
        style={{ marginTop: "5rem", textAlign: "center" }}
      >
        <CircularProgress />
      </Container>
    );
  }

  if (error) {
    return (
      <Container
        maxWidth="md"
        style={{
          marginTop: "2rem",
          paddingBottom: "5rem",
          textAlign: "center",
          minHeight: "100vh",
          display: "flex",
          flexDirection: "column",
        }}
      >
        <Typography variant="h6" color="error">
          {error}
        </Typography>
      </Container>
    );
  }

  if (!userProfile) {
    return null;
  }

  return (
    <Container
      maxWidth="md"
      style={{
        marginTop: "2rem",
        paddingBottom: "5rem",
        textAlign: "center",
        minHeight: "100vh",
        display: "flex",
        flexDirection: "column",
      }}
    >
      <Typography variant="h4" align="center" gutterBottom>
        User Profile
      </Typography>
      <div
        style={{
          display: "flex",
          flexDirection: "column",
          alignItems: "center",
        }}
      >
        <img
          src={avatar}
          alt={`${userProfile.firstName} ${userProfile.lastName}`}
          style={{
            width: "150px",
            height: "150px",
            borderRadius: "50%",
            marginBottom: "1rem",
          }}
        />
        <Typography variant="h5">{`${userProfile.firstName} ${userProfile.lastName}`}</Typography>
        <Typography variant="body1">{`Email: ${userProfile.email}`}</Typography>
        <Typography variant="body1">{`Role: ${userProfile.role}`}</Typography>
        <Button variant="contained" onClick={handleLogout}>
          Logout
        </Button>
      </div>
    </Container>
  );
};

export default Profile;
