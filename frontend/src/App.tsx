import { BrowserRouter, Route, Routes } from "react-router-dom";
import { Paper, ThemeProvider, createTheme } from "@mui/material";
import { useState } from "react";
import "@fontsource/roboto/300.css";
import "@fontsource/roboto/400.css";
import "@fontsource/roboto/500.css";
import "@fontsource/roboto/700.css";
import Home from "./components/Home";
import SignUp from "./components/SignUp";
import LogIn from "./components/LogIn";
import Profile from "./components/Profile";
import CreateProduct from "./components/CreateProduct";
import DeleteProduct from "./components/DeleteProduct";
import About from "./components/About";
import CartItems from "./components/CartItems";
import ProductDetails from "./components/ProductDetails";
import Footer from "./components/Footer";
import Navbar from "./components/NavBar";
import ProductList from "./components/ProductList";
import OrderList from "./components/OrderList";
import PlaceOrder from "./components/PlaceOrder";
import NotFound from "./components/NotFound";
import ProtectedRoute from "./components/ProtectRoute";
import Unauthorized from "./components/Unauthorized";
import Dashboard from "./components/Dashboard";
import UsersList from "./components/UsersList";
import CreateCategory from "./components/CreateCategory";
import CreateAdmin from "./components/CreateAdmin";
import Logout from "./components/Logout";

const App = () => {
  const [darkMode, setDarkMode] = useState(false);
  const theme = createTheme({
    palette: {
      mode: darkMode ? "dark" : "light",
    },
  });

  const handleThemeChange = () => {
    setDarkMode(!darkMode);
  };
  return (
    <>
      <ThemeProvider theme={theme}>
        <Paper
          style={{
            minHeight: "100%",
            background: theme.palette.background.default,
          }}
        >
          <BrowserRouter>
            <Navbar darkMode={darkMode} handleDarkMode={handleThemeChange} />
            <Routes>
              <Route path="/" element={<Home />} />
              <Route path="/signup" element={<SignUp />} />
              <Route path="/signin" element={<LogIn />} />
              <Route path="/profile" element={<Profile />} />
              <Route path="/products" element={<ProductList />} />
              <Route path="/products/add" element={<CreateProduct />} />
              <Route path="/orders" element={<OrderList />} />
              <Route path="/products/delete" element={<DeleteProduct />} />
              <Route path="/about" element={<About />} />
              <Route path="/logout" element={<Logout />} />
              <Route path="/cart" element={<CartItems />} />
              <Route path="/products/:productId" element={<ProductDetails />} />
              <Route path="/create-orders" element={<PlaceOrder />} />
              <Route path="/unauthorized" element={<Unauthorized />} />
              <Route path="/*" element={<NotFound />} />
              <Route
                path="/admin"
                element={
                  <ProtectedRoute
                    element={<Dashboard />}
                    allowedRoles={["Admin"]}
                  />
                }
              />
              <Route
                path="/users"
                element={
                  <ProtectedRoute
                    element={<UsersList />}
                    allowedRoles={["Admin"]}
                  />
                }
              />
              <Route
                path="/create-category"
                element={
                  <ProtectedRoute
                    element={<CreateCategory />}
                    allowedRoles={["Admin"]}
                  />
                }
              />
              <Route
                path="/create-admin"
                element={
                  <ProtectedRoute
                    element={<CreateAdmin />}
                    allowedRoles={["Admin"]}
                  />
                }
              />
              <Route
                path="/create-product"
                element={
                  <ProtectedRoute
                    element={<CreateProduct />}
                    allowedRoles={["Admin"]}
                  />
                }
              />
              <Route
                path="/delete-product"
                element={
                  <ProtectedRoute
                    element={<DeleteProduct />}
                    allowedRoles={["Admin"]}
                  />
                }
              />
            </Routes>
            <Footer />
          </BrowserRouter>
        </Paper>
      </ThemeProvider>
    </>
  );
};

export default App;
