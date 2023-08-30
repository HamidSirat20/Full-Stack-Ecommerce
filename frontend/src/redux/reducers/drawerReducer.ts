import { createSlice } from "@reduxjs/toolkit";

const initialState: {
    isCartVisible: boolean;
  } = {
    isCartVisible:false
  };

  const drawerSlice = createSlice({
    name: 'drawer',
    initialState,
    reducers: {
      isCartVisible: (state) => {
        state.isCartVisible = !state.isCartVisible
      },
    },
  });

  export const { isCartVisible } = drawerSlice.actions;
  const drawerReducer = drawerSlice.reducer;
  export default drawerReducer;