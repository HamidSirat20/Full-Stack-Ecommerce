import { Link } from "react-router-dom";
import Button from "@mui/material/Button";
import { Container, Typography, Box, Paper, Grid } from "@mui/material";

const AdminDashboard = () => {
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
      <Box mb={4}>
        <Typography
          variant="h2"
          sx={{ marginBottom: 2, color: "#007bff", textAlign: "center" }}
        >
          Admin Dashboard
        </Typography>
        <Grid container spacing={3} justifyContent="center">
          <Grid item xs={12} md={4}>
            <Link to="/create-product" style={{ textDecoration: "none" }}>
              <Button variant="outlined" color="primary" fullWidth>
                Create Product
              </Button>
            </Link>
          </Grid>
          <Grid item xs={12} md={4}>
            <Link to="/delete-product" style={{ textDecoration: "none" }}>
              <Button variant="outlined" color="primary" fullWidth>
                Delete Product
              </Button>
            </Link>
          </Grid>
          <Grid item xs={12} md={4}>
            <Link to="/update-product" style={{ textDecoration: "none" }}>
              <Button variant="outlined" color="primary" fullWidth>
                Update Product
              </Button>
            </Link>
          </Grid>
          <Grid item xs={12} md={4}>
            <Link to="/users" style={{ textDecoration: "none" }}>
              <Button variant="outlined" color="primary" fullWidth>
                Users List
              </Button>
            </Link>
          </Grid>
          <Grid item xs={12} md={4}>
            <Link to="/create-category" style={{ textDecoration: "none" }}>
              <Button variant="outlined" color="primary" fullWidth>
                Create A Category
              </Button>
            </Link>
          </Grid>
          <Grid item xs={12} md={4}>
            <Link to="/create-admin" style={{ textDecoration: "none" }}>
              <Button variant="outlined" color="primary" fullWidth>
                Create Admin Account
              </Button>
            </Link>
          </Grid>
        </Grid>
      </Box>
    </Container>
  );
};

export default AdminDashboard;
