import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import axios, { AxiosError } from "axios";

import Product, { Category } from "../../types/Product";

export const fetchAllCategories = createAsyncThunk('fetchAllCategories',
async () => {
    try {
        const result = await axios.get('https://api.escuelajs.co/api/v1/categories');
        return result.data
    } catch (e) {
        const error = e as AxiosError
    }
})
export const fetchCatProducts = createAsyncThunk(
    "fetchCatProducts",
    async (catId: number) => {
      try {
        const fetchProducts = axios.get<Product[]>(
            `https://api.escuelajs.co/api/v1/categories/${catId}/products`

        );
        return (await fetchProducts).data;
      } catch (e) {
        const error = e as AxiosError;
        return error.message;
      }
    }
  );

  const initialState: {
    category: Product[];
  } = {
    category: [],
  };

export const categorySlice = createSlice({
    name: 'categorySlice',
    initialState,
    reducers: {

    },
    extraReducers: (build) => {
        build
            .addCase(fetchAllCategories.fulfilled, (state, action) => {
                if (action.payload instanceof AxiosError) {
                    return state
                } else {
                    return action.payload
                }
            })
            .addCase(fetchCatProducts.fulfilled, (state, action) => {
                try {
                  const categoryId: number = action.meta.arg;
                  if (Array.isArray(action.payload)) {
                    const filteredCategory: Product[] = action.payload.filter(
                      (product: Product) => product.category.id == categoryId.toString()
                    );
                    state.category = filteredCategory;
                  } else {
                    // Handle the case when action.payload is a string (error message)
                    // You can choose to set an error state or handle it in any other way
                  }
                } catch (e) {
                  const error = e as AxiosError;

                }
              })





    },
})
const initialState2:Category [] =[]
export const catSlice = createSlice({
    name: 'catSlice',
    initialState:initialState2,
    reducers: {

    },
    extraReducers: (build) => {
        build
            .addCase(fetchAllCategories.fulfilled, (state, action) => {
                if (action.payload instanceof AxiosError) {
                    return state
                } else {
                    return action.payload
                }
            })

    },
})

export const catReducer = catSlice.reducer
const categoryReducer = categorySlice.reducer
export default categoryReducer
