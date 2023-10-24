import { Link } from "react-router-dom";
import Button from "@mui/material/Button";
import { Container, Typography, Box, Paper } from "@mui/material";

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
        <div className="admin-buttons">
          <Link to="/create-product" style={{ textDecoration: "none" }}>
            <Button variant="outlined" color="primary" sx={{ mr: 2 }}>
              Create Product
            </Button>
          </Link>
          <Link to="/delete-product" style={{ textDecoration: "none" }}>
            <Button variant="outlined" color="primary" sx={{ mr: 2 }}>
              Delete Product
            </Button>
          </Link>
          <Link to="/update-product" style={{ textDecoration: "none" }}>
            <Button variant="outlined" color="primary" sx={{ mr: 2 }}>
              Update Product
            </Button>
          </Link>
          <Link to="/users" style={{ textDecoration: "none" }}>
            <Button variant="outlined" color="primary" sx={{ mr: 2 }}>
              Users List
            </Button>
          </Link>
          <Link to="/create-category" style={{ textDecoration: "none" }}>
            <Button variant="outlined" color="primary" sx={{ mr: 2 }}>
              Create A Category
            </Button>
          </Link>
          <Link to="/create-admin" style={{ textDecoration: "none" }}>
            <Button variant="outlined" color="primary" sx={{ mr: 2 }}>
              Create Admin Account
            </Button>
          </Link>
        </div>
      </Box>
    </Container>
  );
};

export default AdminDashboard;
