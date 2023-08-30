import React from "react";
import {
  AppBar,
  Button,
  IconButton,
  Stack,
  Toolbar,
  Typography,
} from "@mui/material";
import ShoppingCartIcon from "@mui/icons-material/ShoppingCart";
import { NavLink, Outlet } from "react-router-dom";

import useAppSelector from "../hooks/useAppSelector";
import useAppDispatch from "../hooks/useAppDispatch";
import { isCartVisible } from "../redux/reducers/drawerReducer";

const NavBar = () => {
  const toggle = useAppSelector((state) => state.drawerReducer.isCartVisible);
  const cartProduct = useAppSelector((state) => state.cartReducer.cartItems);
  const totalQuantity = cartProduct.reduce((accumulator, currentProduct) => {
    return accumulator + currentProduct.quantity;
  }, 0);
  const dispatch = useAppDispatch();
  const handleCartToggle = () => {
    dispatch(isCartVisible());
  };
  return (
    <>
      <AppBar>
        <Toolbar>
          <Typography
            color="white"
            variant="h5"
            component="div"
            sx={{ flexGrow: 1 }}
          >
            <NavLink to="/">
              <Button
                color="inherit"
                sx={{ color: "white", fontWeight: "bold" }}
              >
                PrimePicks
              </Button>
            </NavLink>
          </Typography>
          <Stack direction="row" spacing={2}>
            <NavLink to="/">
              <Button
                color="inherit"
                sx={{ color: "white", fontWeight: "bold" }}
              >
                Home
              </Button>
            </NavLink>
            <NavLink to="/products">
              <Button
                color="inherit"
                sx={{ color: "white", fontWeight: "bold" }}
              >
                Products
              </Button>
            </NavLink>
            <IconButton onClick={handleCartToggle}>
              <ShoppingCartIcon
                fontSize="large"
                color="inherit"
                aria-aria-label="shopping-cart"
              ></ShoppingCartIcon>
              <Typography
                sx={{
                  background: "orange",
                  borderRadius: "50%",
                  width: "27px",
                }}
              >
                {totalQuantity}
              </Typography>
            </IconButton>
          </Stack>
        </Toolbar>
      </AppBar>
      <Outlet />
    </>
  );
};

export default NavBar;
