import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import Product from "../../types/Product";
import { toast } from "react-toastify";

export interface CartItem {
  quantity: number;
  product: Product;
  totalPrice: number;
}

export interface CartState {
  cartItems: CartItem[];
  isCartOpen: boolean;
}

const initialState: CartState = {
  cartItems: [],
  isCartOpen: false,
};

const cartSlice = createSlice({
  name: "cart",
  initialState,
  reducers: {
    addToCart: (state, action: PayloadAction<Product>) => {
      const { id, price } = action.payload;
      const existingCartItem = state.cartItems.find(
        (item) => item.product.id === id
      );

      if (existingCartItem) {
        existingCartItem.quantity++;
        existingCartItem.totalPrice += price;
        toast.success("One product increased in your cart", {
          position: "bottom-right",
        });
      } else {
        state.cartItems.push({
          product: action.payload,
          quantity: 1,
          totalPrice: price,
        });
        toast.success("A new product added to cart", {
          position: "bottom-right",
        });
      }
    },
    removeFromCart: (state, action: PayloadAction<string>) => {
      state.cartItems = state.cartItems.filter(
        (item) => item.product.id !== action.payload
      );
      toast.warning("You removed a product from the cart", {
        position: "bottom-right",
      });
    },
    decreaseCount: (state, action: PayloadAction<string>) => {
      const existingCartItem = state.cartItems.find(
        (item) => item.product.id === action.payload
      );

      if (existingCartItem && existingCartItem.quantity === 1) {
        state.cartItems = state.cartItems.filter(
          (item) => item.product.id !== action.payload
        );
        toast.info("You removed an item from the cart", {
          position: "bottom-right",
        });
      } else if (existingCartItem && existingCartItem.quantity) {
        existingCartItem.quantity--;
        existingCartItem.totalPrice -= existingCartItem.product.price;
        toast.info("You decreased the quantity of an item", {
          position: "bottom-right",
        });
      }
    },
    increaseCount: (state, action: PayloadAction<string>) => {
      const existingCartItem = state.cartItems.find(
        (item) => item.product.id === action.payload
      );

      if (existingCartItem) {
        existingCartItem.quantity++;
        existingCartItem.totalPrice += existingCartItem.product.price;
        toast.info("You increased the quantity of an item", {
          position: "bottom-right",
        });
      }
    },
    clearCart: (state) => {
      state.cartItems = [];
      toast.warning("No items in the cart", {
        position: "bottom-right",
      });
    },
    setIsCartOpen: (state) => {
      state.isCartOpen = !state.isCartOpen;
    },
  },
});

export const {
  addToCart,
  removeFromCart,
  increaseCount,
  decreaseCount,
  clearCart,
  setIsCartOpen,
} = cartSlice.actions;

const cartReducer = cartSlice.reducer;
export default cartReducer;
