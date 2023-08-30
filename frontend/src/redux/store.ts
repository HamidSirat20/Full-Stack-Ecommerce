import { configureStore } from "@reduxjs/toolkit";

import productsReducer from "./reducers/productsReducer";
import usersReducers from "./reducers/usersReducer";
import cartReducer from "./reducers/cartReducer";
import sliderReducer from "./reducers/sliderReducer";
import categoryReducer, { catReducer } from "./reducers/categoryReducer";
import drawerReducer from "./reducers/drawerReducer";

const store = configureStore({
  reducer: {
    productsReducer,
    usersReducers,
    cartReducer,
    sliderReducer,
    categoryReducer,
    drawerReducer,
    catReducer,
  },
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;
export default store;
