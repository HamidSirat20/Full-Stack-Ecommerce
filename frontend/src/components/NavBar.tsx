import { Link, Outlet } from "react-router-dom";
import {
  AppBar,
  Toolbar,
  Typography,
  IconButton,
  FormControlLabel,
  Switch,
  Avatar,
  Box,
  Divider,
  ListItemIcon,
  Menu,
  MenuItem,
} from "@mui/material";
import Tooltip from "@mui/material/Tooltip";
import HomeIcon from "@mui/icons-material/Home";
import Badge from "@mui/material/Badge";
import PersonIcon from "@mui/icons-material/Person";
import StorefrontIcon from "@mui/icons-material/Storefront";
import InfoOutlinedIcon from "@mui/icons-material/InfoOutlined";
import useAppSelector from "../hooks/useAppSelector";
import { useEffect, useState } from "react";
import useAppDispatch from "../hooks/useAppDispatch";
import { fetchUserProfile } from "../redux/reducers/loginReducer";
import { Logout, ShoppingBagOutlined } from "@mui/icons-material";
import DashboardOutlinedIcon from "@mui/icons-material/DashboardOutlined";

interface Props {
  darkMode: boolean;
  handleDarkMode: () => void;
}

const Navbar = ({ darkMode, handleDarkMode }: Props) => {
  const cartItems = useAppSelector((state) => state.cartReducer.cartItems);
  const login = useAppSelector((state) => state.loginReducer);
  const dispatch = useAppDispatch();

  useEffect(() => {
    dispatch(fetchUserProfile());
  }, []);

  const profileAvatarFirst =
    login.userProfile?.firstName?.[0]?.toLocaleUpperCase() || "";
  const profileAvatarLast =
    login.userProfile?.lastName?.[0]?.toLocaleUpperCase() || "";
  const profile = profileAvatarFirst.concat(profileAvatarLast);

  const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);
  const open = Boolean(anchorEl);
  const handleClick = (event: React.MouseEvent<HTMLElement>) => {
    setAnchorEl(event.currentTarget);
  };
  const handleClose = () => {
    setAnchorEl(null);
  };

  return (
    <>
      <AppBar position="fixed" style={{ background: "white" }}>
        <Toolbar>
          {/* Left side */}
          <div style={{ display: "flex", alignItems: "center", flexGrow: 1 }}>
            <Typography variant="h6">
              <Link
                to="/"
                style={{
                  color: "black",
                  textDecoration: "none",
                  marginRight: "1rem",
                }}
              >
                pinnacleMall
              </Link>
            </Typography>
            <Tooltip title="Switch Mode" arrow>
              <IconButton color="secondary">
                <FormControlLabel
                  control={
                    <Switch checked={darkMode} onChange={handleDarkMode} />
                  }
                  label={darkMode ? "Light Mode" : "Dark Mode"}
                />
              </IconButton>
            </Tooltip>
          </div>
          {/* Right side */}
          <Box
            sx={{
              display: "flex",
              alignItems: "center",
              justifyContent: "flex-end",
              margin: 0,
              padding: 0,
            }}
          >
            <Tooltip title="Home" arrow>
              <IconButton component={Link} to="/" color="inherit">
                <HomeIcon style={{ color: "black" }} />
              </IconButton>
            </Tooltip>
            <Tooltip title="Products" arrow>
              <IconButton component={Link} to="/products" color="inherit">
                <StorefrontIcon style={{ color: "black" }} />
              </IconButton>
            </Tooltip>
            <Tooltip title="About" arrow>
              <IconButton component={Link} to="/about" color="inherit">
                <InfoOutlinedIcon style={{ color: "black" }} />
              </IconButton>
            </Tooltip>
            <Tooltip title="Cart" arrow>
              <IconButton component={Link} to="/cart" color="inherit">
                <Badge badgeContent={cartItems.length} color="secondary">
                  <ShoppingBagOutlined style={{ color: "black" }} />
                </Badge>
              </IconButton>
            </Tooltip>
            {login.userProfile?.role === "Admin" && (
              <Tooltip title="Dashboard" arrow>
                <IconButton component={Link} to="/admin" color="inherit">
                  <DashboardOutlinedIcon style={{ color: "black" }} />
                </IconButton>
              </Tooltip>
            )}
            <Tooltip title="Account">
              <IconButton
                onClick={handleClick}
                size="small"
                sx={{ ml: 2 }}
                aria-controls={open ? "account-menu" : undefined}
                aria-haspopup="true"
                aria-expanded={open ? "true" : undefined}
              >
                <Avatar sx={{ width: 32, height: 32 }}>{profile}</Avatar>
              </IconButton>
            </Tooltip>
          </Box>
          <Menu
            anchorEl={anchorEl}
            id="account-menu"
            open={open}
            onClose={handleClose}
            onClick={handleClose}
            PaperProps={{
              elevation: 0,
              sx: {
                overflow: "visible",
                filter: "drop-shadow(0px 2px 8px rgba(0,0,0,0.32))",
                mt: 1.5,
                "& .MuiAvatar-root": {
                  width: 32,
                  height: 32,
                  ml: -0.5,
                  mr: 1,
                },
                "&:before": {
                  content: '""',
                  display: "block",
                  position: "absolute",
                  top: 0,
                  right: 14,
                  width: 10,
                  height: 10,
                  bgcolor: "background.paper",
                  transform: "translateY(-50%) rotate(45deg)",
                  zIndex: 0,
                },
              },
            }}
            transformOrigin={{ horizontal: "right", vertical: "top" }}
            anchorOrigin={{ horizontal: "right", vertical: "bottom" }}
          >
            {login.isLoggedIn ? (
              <MenuItem onClick={handleClose}>
                <Tooltip title="Profile" arrow>
                  <IconButton component={Link} to="/profile" color="inherit">
                    <Avatar />
                    Profile
                  </IconButton>
                </Tooltip>
              </MenuItem>
            ) : (
              ""
            )}

            <Divider />
            <MenuItem onClick={handleClose}>
              <Tooltip title="Signin" arrow>
                <IconButton component={Link} to="/signin" color="inherit">
                  <PersonIcon style={{ color: "black" }} />
                  Signin
                </IconButton>
              </Tooltip>
            </MenuItem>
            <MenuItem onClick={handleClose}>
              <Tooltip title="Signup" arrow>
                <IconButton component={Link} to="/signup" color="inherit">
                  <PersonIcon style={{ color: "black" }} />
                  Signup
                </IconButton>
              </Tooltip>
            </MenuItem>
            {login.isLoggedIn ? (
              <MenuItem onClick={handleClose}>
                <Tooltip title="Logout" arrow>
                  <IconButton component={Link} to="/logout" color="inherit">
                    <ListItemIcon>
                      <Logout fontSize="small" />
                    </ListItemIcon>
                    Logout
                  </IconButton>
                </Tooltip>
              </MenuItem>
            ) : (
              ""
            )}
          </Menu>
        </Toolbar>
      </AppBar>
      <Outlet />
    </>
  );
};

export default Navbar;
