import { PayloadAction, createSlice } from "@reduxjs/toolkit";
import Product from "../../types/Product";
import { toast } from "react-toastify";
export interface CartItem {
  quantity: number;
  product: Product;
  totalPrice: number;
}
const initialState: {
  cartItems: CartItem[];
} = {
  cartItems: [],
};
const cartSlice = createSlice({
  name: "cart",
  initialState,
  reducers: {
    addToCart: (state, action: PayloadAction<CartItem>) => {
      const cartItem = state.cartItems.find(
        (item) => item.product.id === action.payload.product.id
      );
      if (cartItem) {
        cartItem.quantity++;
        cartItem.totalPrice =
          cartItem.totalPrice + action.payload.product.price;
          toast.success("One product increase in your cart",{
            position:'bottom-right'
          })
      } else {
        state.cartItems.push(action.payload);
        toast.success("A new product added to cart",{
          position:'bottom-right'
        })
      }
    },
    removeFromCart: (state, action: PayloadAction<CartItem>) => {
      const cartItem = state.cartItems.find(
        (item) => item.product.id === action.payload.product.id
      );

      if (cartItem) {
        state.cartItems = state.cartItems.filter(
          (product) => product.product.id !== action.payload.product.id
        );
        toast.warning("You remove a product",{
          position:'bottom-right'
        })
      }
      return state;
    },
    decreaseAmount: (state, action: PayloadAction<CartItem>) => {
      const cartItem = state.cartItems.find(
        (item) => item.product.id === action.payload.product.id
      );
      if (cartItem?.quantity === 1) {
        toast.info("You dropped an item",{
          position:'bottom-right'
        })
        state.cartItems = state.cartItems.filter(
          (product) => product.product.id !== action.payload.product.id
        );
      } else if (cartItem?.quantity) {
        cartItem.quantity--;
        cartItem.totalPrice -= action.payload.product.price;
        toast.info("You dropped an item",{
          position:'bottom-right'
        })
      }
      return state;
    },
    increaseAmount: (state, action: PayloadAction<CartItem>) => {
      const cartItem = state.cartItems.find(
        (item) => item.product.id === action.payload.product.id
      );
      if (cartItem) {
        toast.info("You added an item",{
          position:'bottom-right'
        })
        cartItem.quantity++;
        cartItem.totalPrice += action.payload.product.price;
      }
      return state;
    },
    clearCart: (state) => {
      state.cartItems = [];
      toast.warning("No Item in Cart",{
        position:'bottom-right'
      })
    },
  },
});

export const {
  addToCart,
  removeFromCart,
  increaseAmount,
  decreaseAmount,
  clearCart,
} = cartSlice.actions;
const cartReducer = cartSlice.reducer;
export default cartReducer;
