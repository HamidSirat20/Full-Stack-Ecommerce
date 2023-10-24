import { useEffect, useState } from "react";
import { TextField, Button, Container, Typography } from "@mui/material";
import useAppDispatch from "../hooks/useAppDispatch";
import { orderProducts } from "../redux/reducers/orderReducer";
import useAppSelector from "../hooks/useAppSelector";
import { OrderProduct } from "../types/Order";
import { clearCart } from "../redux/reducers/cartReducer";

const PlaceOrder = () => {
  const [shippingAddress, setShippingAddress] = useState("");
  const [status, setStatus] = useState("Pending");
  const [userId, setUserID] = useState<string>("");
  const [message, setMessage] = useState("");
  const [products, setProducts] = useState<
    Array<{ userId: string; productId: string; amount: number }>
  >([]);

  const cartProducts = useAppSelector((state) => state.cartReducer.cartItems);

  const orderProds: OrderProduct[] = cartProducts.map(
    ({ product, quantity }) => ({
      productId: product.id,
      userId: userId || "",
      amount: quantity,
    })
  );

  products.forEach((element) => {
    console.log(
      " Amount " +
        element.amount +
        " ProductId " +
        element.productId +
        " UserID " +
        element.userId
    );
  });
  const dispatch = useAppDispatch();

  const storedToken = localStorage.getItem("userId");
  const uId = storedToken ? storedToken.replace(/^"(.*)"$/, "$1") : null;

  useEffect(() => {
    if (uId !== null) {
      setUserID(uId);
    } else {
      setMessage("You need to be signed in in order to write a review");
    }
  }, [userId]);

  const handleSubmit = () => {
    if (userId) {
      setProducts([...orderProds]);
      dispatch(
        orderProducts({
          shippingAddress: shippingAddress,
          status: status,
          orderProducts: [...products],
        })
      );
      setShippingAddress("");
      setMessage("Your orders made");
      dispatch(clearCart());
    } else {
      setMessage("Please provide valid userId, productId, and amount.");
    }
  };

  return (
    <Container
      style={{
        maxWidth: "500px",
        margin: "0 auto",
        marginTop: "4rem",
        height: "100vh",
      }}
      sx={{
        display: "flex",
        flexDirection: "column",
        alignItems: "center",
        justifyContent: "center",
        height: "100vh",
      }}
    >
      <TextField
        fullWidth
        multiline
        rows={4}
        label="Shipping Address"
        variant="outlined"
        margin="normal"
        value={shippingAddress}
        onChange={(e) => {
          setShippingAddress(e.target.value);
        }}
      />
      <Typography>{message}</Typography>
      <Button
        variant="contained"
        color="primary"
        style={{ marginTop: "20px" }}
        onClick={handleSubmit}
      >
        Submit Order
      </Button>
    </Container>
  );
};

export default PlaceOrder;
