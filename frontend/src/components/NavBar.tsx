import React from 'react';
import { Link, Outlet } from 'react-router-dom';
import { AppBar, Toolbar, Typography, IconButton } from '@mui/material';
import HomeIcon from '@mui/icons-material/Home';
import ShoppingBasketIcon from '@mui/icons-material/ShoppingBasket';
import PersonIcon from '@mui/icons-material/Person';
import StorefrontIcon from '@mui/icons-material/Storefront';
import InfoIcon from '@mui/icons-material/Info';

const Navbar = () => {
  return (
   <>
    <AppBar position="fixed">
      <Toolbar>
        {/* Left side */}
        <Typography variant="h6" sx={{ flexGrow: 1 }}>
          <Link to="/" style={{ color: 'white', textDecoration: 'none' }}>
            pinnacleMall
          </Link>
        </Typography>

        {/* Right side */}
        <IconButton component={Link} to="/" color="inherit">
          <HomeIcon />
        </IconButton>
        <IconButton component={Link} to="/products" color="inherit">
          <StorefrontIcon />
        </IconButton>
        <IconButton component={Link} to="/about" color="inherit">
          <InfoIcon />
        </IconButton>
        <IconButton component={Link} to="/cart" color="inherit">
          <ShoppingBasketIcon />
        </IconButton>
        <IconButton component={Link} to="/signup" color="inherit">
          <PersonIcon />
        </IconButton>
      </Toolbar>
    </AppBar>
    <Outlet/>
   </>

  );
};

export default Navbar;
