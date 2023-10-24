import { configureStore } from "@reduxjs/toolkit";

import productsReducer from "./reducers/productsReducer";
import cartReducer from "./reducers/cartReducer";
import sliderReducer from "./reducers/sliderReducer";
import userReducer from "./reducers/usersReducer";
import categoryReducer from "./reducers/categoryReducer";
import loginReducer from "./reducers/loginReducer";
import orderReducer from "./reducers/orderReducer";
import reviewReducer from "./reducers/reviewReducer";

const store = configureStore({
  reducer: {
    productsReducer,
    userReducer,
    cartReducer,
    sliderReducer,
    categoryReducer,
    loginReducer,
    orderReducer,
    reviewReducer,
  },
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;
export default store;
