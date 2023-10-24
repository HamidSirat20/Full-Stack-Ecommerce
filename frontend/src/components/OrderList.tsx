import { useEffect } from "react";
import useAppDispatch from "../hooks/useAppDispatch";
import { getOrders } from "../redux/reducers/orderReducer";
import { OrderRead } from "../types/Order";
import useAppSelector from "../hooks/useAppSelector";
import {
  Typography,
  Divider,
  List,
  ListItem,
  ListItemText,
  Container,
  Box,
} from "@mui/material";

const OrderList: React.FC = () => {
  const { getOrder, loading, error } = useAppSelector(
    (state) => state.orderReducer
  );
  const dispatch = useAppDispatch();

  useEffect(() => {
    dispatch(getOrders());
  }, [dispatch]);

  if (loading) {
    return <p>Loading...</p>;
  }

  if (error) {
    return <p>Error: {error}</p>;
  }

  if (getOrder?.length === 0) {
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
        <Typography variant="h1">Your Order List is Empty</Typography>
      </Container>
    );
  }

  return (
    <Container maxWidth="md" sx={{ paddingTop: "4rem", paddingBottom: "4rem" }}>
      <Typography variant="h2">Your Orders Summary:</Typography>
      {getOrder?.map((order: OrderRead) => (
        <Box key={order.id} sx={{ marginTop: 4 }}>
          <Divider />
          <Typography variant="h4" sx={{ marginTop: 2 }}>
            Receiver: {order.user.firstName} {order.user.lastName}
          </Typography>
          <Typography>Email: {order.user.email}</Typography>
          <Divider />
          <Typography variant="h5" sx={{ marginTop: 2 }}>
            Delivery Address:
          </Typography>
          <Typography>Shipping Address: {order.shippingAddress}</Typography>
          <Typography>Status: {order.status}</Typography>

          {order.orderItems.map((item) => (
            <List key={item.productId} sx={{ marginTop: 2 }}>
              <ListItem>
                <ListItemText
                  primary={`Order Item: ${item.productId}`}
                  secondary={`Amount: ${item.amount}`}
                />
              </ListItem>
            </List>
          ))}
        </Box>
      ))}
    </Container>
  );
};

export default OrderList;
