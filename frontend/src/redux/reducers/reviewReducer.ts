import { PayloadAction, createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import axios from "axios";
import { CreateReview } from "../../types/Review";

const baseUrl = "https://pinnaclemall.azurewebsites.net/api/v1";

const initialState: {
  loading: boolean;
  review: CreateReview | null;
  error: string | undefined;
} = {
  loading: false,
  error: "",
  review: null,
};

export const createReview = createAsyncThunk(
  "add/review",
  async (reveiw: CreateReview) => {
    const request = await axios.post(`${baseUrl}/reviews`, reveiw);
    const response = await request.data;
    return response;
  }
);

const reviewSlice = createSlice({
  name: "review",
  initialState: initialState,
  reducers: {},
  extraReducers: (build) => {
    build
      .addCase(createReview.pending, (state) => {
        state.loading = true;
        state.review = null;
        state.error = "";
      })
      .addCase(createReview.fulfilled, (state, action) => {
        state.loading = false;
        state.review = action.payload;
        state.error = "";
      })
      .addCase(createReview.rejected, (state, action) => {
        state.loading = false;
        state.review = null;
        if (action.error.message === "Request failed with status code 401") {
          state.error = "Access denied! Invalid Credentials";
        } else {
          state.error = action.error.message;
        }
      });

    //********************************** */
  },
});

export const {} = reviewSlice.actions;

const reviewReducer = reviewSlice.reducer;
export default reviewReducer;
