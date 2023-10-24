import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import axios from "axios";
import { Order, OrderRead } from "../../types/Order";

const baseUrl = "https://pinnaclemall.azurewebsites.net/api/v1";

export const orderProducts = createAsyncThunk(
  "post/order",

  async (orders: Order) => {
    const storedToken = localStorage.getItem("mytoken");
    const token = storedToken ? storedToken.replace(/^"(.*)"$/, "$1") : null;
    const response = await axios.post<Order>(`${baseUrl}/orders`, orders, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
    const data = await response.data;
    return data;
  }
);

export const getOrders = createAsyncThunk("get/order", async () => {
  const storedToken = localStorage.getItem("mytoken");
  const token = storedToken ? storedToken.replace(/^"(.*)"$/, "$1") : null;
  try {
    const response = await axios.get<OrderRead[]>(`${baseUrl}/orders`, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
    return response.data;
  } catch (error) {
    throw error;
  }
});

interface Props {
  loading: boolean;
  error: string | undefined;
  order: Order[] | null;
  getOrder: OrderRead[] | null;
}
const initialState: Props = {
  loading: false,
  error: "",
  order: null,
  getOrder: null,
};
const orderSlice = createSlice({
  name: "order",
  initialState: initialState,
  reducers: {},
  extraReducers: (build) => {
    build
      .addCase(orderProducts.pending, (state) => {
        state.loading = true;
        state.error = "";
        state.order = null;
      })
      .addCase(orderProducts.fulfilled, (state, action) => {
        state.loading = false;
        state.error = "";
        state.order?.push(action.payload);
      })
      .addCase(orderProducts.rejected, (state, action) => {
        state.loading = false;
        state.error = action.error.message;
        state.order = null;
      })
      .addCase(getOrders.pending, (state) => {
        state.loading = true;
        state.error = "";
        state.getOrder = null;
      })
      .addCase(getOrders.fulfilled, (state, action) => {
        state.loading = false;
        state.error = "";
        state.getOrder = action.payload;
      })
      .addCase(getOrders.rejected, (state, action) => {
        state.loading = false;
        state.error = action.error.message;
        state.getOrder = null;
      });
  },
});

const orderReducer = orderSlice.reducer;

export default orderReducer;
