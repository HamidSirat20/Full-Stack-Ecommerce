import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import axios, { AxiosError } from "axios";
import { Category, CreateCategory } from "../../types/Category";

const baseUrl = "https://pinnaclemall.azurewebsites.net/api/v1";

export const fetchAllCategories = createAsyncThunk(
  "fetch/Categories",
  async () => {
    try {
      const fetchProducts = axios.get<Category[]>(`${baseUrl}/categories`);
      return (await fetchProducts).data;
    } catch (e) {
      const error = e as AxiosError;
      return error.message;
    }
  }
);

export const createNewCategory = createAsyncThunk(
  "create/category",
  async (category: CreateCategory) => {
    try {
      const storedToken = localStorage.getItem("mytoken");
      const token = storedToken ? storedToken.replace(/^"(.*)"$/, "$1") : null;
      const result = await axios.post(`${baseUrl}/categories`, category, {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      });
      return result.data;
    } catch (e) {
      const error = e as AxiosError;
      return error.message;
    }
  }
);

const initialState: {
  category: Category[];
  loading: boolean;
  error: string;
} = {
  category: [],
  loading: false,
  error: "",
};

export const categorySlice = createSlice({
  name: "categorySlice",
  initialState,
  reducers: {},
  extraReducers: (build) => {
    build
      .addCase(fetchAllCategories.pending, (state, action) => {
        state.loading = true;
      })
      .addCase(fetchAllCategories.rejected, (state, action) => {
        state.loading = false;
        state.error = "Cannot fetch this time, try later";
      })
      .addCase(fetchAllCategories.fulfilled, (state, action) => {
        state.loading = true;
        if (typeof action.payload === "string") {
          state.error = action.payload;
        } else {
          state.category = action.payload;
        }
      })
      .addCase(createNewCategory.pending, (state) => {
        state.loading = true;
        state.error = "";
      })
      .addCase(createNewCategory.rejected, (state, action) => {
        state.loading = false;
        state.error = "Cannot create new product, try later";
      })
      .addCase(createNewCategory.fulfilled, (state, action) => {
        state.loading = false;
        if (typeof action.payload === "string") {
          state.error = action.payload;
        } else {
          state.category = action.payload;
        }
        state.loading = false;
      });
    // .addCase(fetchCatProducts.fulfilled, (state, action) => {
    //     try {
    //       const categoryId: number = action.meta.arg;
    //       if (Array.isArray(action.payload)) {
    //         const filteredCategory: Product[] = action.payload.filter(
    //           (product: Product) => product.category.id == categoryId.toString()
    //         );
    //         state.category = filteredCategory;
    //       } else {
    //         // Handle the case when action.payload is a string (error message)
    //         // You can choose to set an error state or handle it in any other way
    //       }
    //     } catch (e) {
    //       const error = e as AxiosError;

    //     }
    //   })
  },
});

const categoryReducer = categorySlice.reducer;
export default categoryReducer;
