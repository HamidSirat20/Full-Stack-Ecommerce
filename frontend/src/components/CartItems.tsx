import {
  Grid,
  Card,
  CardHeader,
  CardContent,
  Typography,
  Container,
  Button,
  Box,
  Divider,
  CardActions,
  IconButton,
} from "@mui/material";
import AddIcon from "@mui/icons-material/Add";
import RemoveIcon from "@mui/icons-material/Remove";
import { Link } from "react-router-dom";
import useAppSelector from "../hooks/useAppSelector";
import useAppDispatch from "../hooks/useAppDispatch";
import Product from "../types/Product";
import {
  removeFromCart,
  increaseCount,
  decreaseCount,
} from "../redux/reducers/cartReducer";

import image1 from "../data/products/1.jpg";

const CartItems = () => {
  const cartItems = useAppSelector((state) => state.cartReducer.cartItems);
  const dispatch = useAppDispatch();

  const remove = (product: Product) => {
    dispatch(removeFromCart(product.id));
  };

  const totalPrice = useAppSelector((state) =>
    state.cartReducer.cartItems.reduce(
      (total, item) => total + item.product.price * item.quantity,
      0
    )
  );

  return (
    <Container
      maxWidth="lg"
      style={{
        paddingBottom: "3rem",
        minHeight: "100vh",
        display: "flex",
        flexDirection: "column",
      }}
    >
      <Container
        maxWidth="md"
        sx={{ paddingTop: "2rem", paddingBottom: "1rem" }}
      >
        <Typography
          variant="h4"
          color="primary"
          gutterBottom
          sx={{ paddingTop: "4rem" }}
        >
          Shopping Cart
        </Typography>
        <Divider sx={{ marginBottom: "1rem" }} />
        {cartItems.length ? (
          <Grid container spacing={3}>
            {cartItems.map((product) => (
              <Grid item xs={12} sm={6} md={4} key={product.product.id}>
                <Card
                  elevation={3}
                  sx={{
                    height: "100%",
                    display: "flex",
                    flexDirection: "column",
                  }}
                >
                  <img
                    src={image1}
                    alt={product.product.title}
                    style={{
                      width: "100%",
                      height: "200px",
                      objectFit: "cover",
                      borderTopLeftRadius: "8px",
                      borderTopRightRadius: "8px",
                    }}
                  />
                  <CardHeader
                    title={product.product.title}
                    subheader={`$${product.product.price}`}
                    titleTypographyProps={{ variant: "h6" }}
                  />
                  <CardContent style={{ flex: 1 }}>
                    <Typography variant="body2" color="textSecondary" paragraph>
                      {product.product.description}
                    </Typography>
                    <Typography variant="body2" color="textSecondary" paragraph>
                      Quantity: {product.quantity}
                    </Typography>
                  </CardContent>
                  <CardActions
                    sx={{
                      display: "flex",
                      flexDirection: "row",
                      alignItems: "center",
                      justifyContent: "center",
                    }}
                  >
                    <IconButton
                      onClick={() => {
                        dispatch(decreaseCount(product.product.id));
                      }}
                    >
                      <RemoveIcon />
                    </IconButton>
                    <Typography variant="h6" sx={{ marginX: 2 }}>
                      {product.quantity}
                    </Typography>
                    <IconButton
                      onClick={() => {
                        dispatch(increaseCount(product.product.id));
                      }}
                    >
                      <AddIcon />
                    </IconButton>
                  </CardActions>
                  <Box p={2} display="flex" justifyContent="center">
                    <Button
                      variant="contained"
                      color="secondary"
                      onClick={() => remove(product.product)}
                    >
                      Remove From Cart
                    </Button>
                  </Box>
                </Card>
              </Grid>
            ))}
          </Grid>
        ) : (
          <Typography
            variant="h4"
            color="primary"
            gutterBottom
            sx={{ paddingTop: "4rem" }}
          >
            {" "}
            No products found in your cart
          </Typography>
        )}
      </Container>

      <Divider sx={{ margin: "2rem 0" }} />

      <Container maxWidth="md" sx={{ paddingBottom: "2rem" }}>
        <Typography variant="h5" color="primary" textAlign="right" gutterBottom>
          Total Amount: ${totalPrice.toFixed(2)}
        </Typography>
        <Box mt={4} display="flex" justifyContent="flex-end">
          <Button
            variant="contained"
            color="primary"
            component={Link}
            to="/orders"
            sx={{
              marginRight: "1rem",
              backgroundColor: "#1976D2",
              color: "white",
              padding: "12px 24px",
              borderRadius: "25px",
              textTransform: "uppercase",
              fontWeight: "bold",
              fontSize: "14px",
              transition: "0.3s ease-in-out",
              "&:hover": {
                backgroundColor: "#1565C0",
              },
            }}
          >
            Go to Orders
          </Button>
          {cartItems.length ? (
            <Button
              variant="contained"
              color="primary"
              component={Link}
              to="/create-orders"
              sx={{
                backgroundColor: "#4caf50",
                color: "white",
                padding: "15px 40px",
                borderRadius: "25px",
                boxShadow: "0px 4px 6px rgba(0, 0, 0, 0.1)",
                textTransform: "uppercase",
                fontWeight: "bold",
                fontSize: "16px",
                transition: "0.3s ease-in-out",
                "&:hover": {
                  backgroundColor: "#388e3c",
                },
              }}
            >
              Create Order
            </Button>
          ) : (
            ""
          )}
        </Box>
      </Container>
    </Container>
  );
};

export default CartItems;
