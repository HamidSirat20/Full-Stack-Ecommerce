import { Button, Container } from "@mui/material";
import React from "react";
import { NavLink } from "react-router-dom";

const NotFound = () => {
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
      No Page Found
      <Button>
        <NavLink to="/">Go to Home</NavLink>
      </Button>
    </Container>
  );
};

export default NotFound;
