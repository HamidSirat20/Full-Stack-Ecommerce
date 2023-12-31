import { PayloadAction, createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import axios, { AxiosError } from "axios";

import Product from "../../types/Product";
import { NewProduct } from "../../types/NewProduct";
import { UpdateSingleProduct } from "../../types/UpdateSingleProduct";

const baseUrl = "https://pinnaclemall.azurewebsites.net/api/v1";

interface RetrieveProducts {
  loading: boolean;
  error: string;
  products: Product[];
  createProduct?: NewProduct;
}
const initialState: RetrieveProducts = {
  loading: false,
  error: "",
  products: [],
};

export interface FetchAllParams {
  search?: string;
  orderBy?: string;
  orderByDescending?: boolean;
  offset: number;
  limit: number;
}

export const fetchAllProducts = createAsyncThunk(
  "fetchAllProducts",
  async ({ offset, limit }: FetchAllParams) => {
    try {
      const fetchProducts = axios.get<Product[]>(
        `${baseUrl}/products?offset=${offset}&limit=${limit}`
      );
      return (await fetchProducts).data;
    } catch (e) {
      const error = e as AxiosError;
      return error.message;
    }
  }
);

export const fetchSingleProduct = createAsyncThunk(
  "fetchSingleProduct",
  async (id: string) => {
    try {
      const fetchProducts = axios.get<Product>(`${baseUrl}/products/${id}`);
      return (await fetchProducts).data;
    } catch (e) {
      const error = e as AxiosError;
      return error.message;
    }
  }
);

export const createNewProducts = createAsyncThunk(
  "createNewProducts",
  async (product: NewProduct) => {
    try {
      const storedToken = localStorage.getItem("mytoken");
      const token = storedToken ? storedToken.replace(/^"(.*)"$/, "$1") : null;
      const result = await axios.post(`${baseUrl}/products`, product, {
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

export const updateSingleProduct = createAsyncThunk(
  "updateSingleProduct",
  async (updateProduct: UpdateSingleProduct) => {
    const { id, update } = updateProduct;
    try {
      const result = await axios.put(`baseUrl/products/${id}`, update);
      return result.data;
    } catch (e) {
      const error = e as AxiosError;
      return error.message;
    }
  }
);
export const deleteSignleProduct = createAsyncThunk(
  "deleteSigleProduct",
  async (id: string) => {
    try {
      const result = await axios.delete(`${baseUrl}/products/${id}`);
      return result.data;
    } catch (e) {
      const error = e as AxiosError;
      return error.message;
    }
  }
);

const productsSlice = createSlice({
  name: "products",
  initialState,
  reducers: {
    emptyProductList: (state) => {
      return initialState;
    },
    sortPrice: (state, action: PayloadAction<"asc" | "desc">) => {
      if (action.payload === "asc") {
        state.products.sort((a, b) => a.price - b.price);
      } else {
        state.products.sort((a, b) => b.price - a.price);
      }
    },
  },
  extraReducers: (build) => {
    build
      .addCase(fetchAllProducts.pending, (state, action) => {
        state.loading = true;
      })
      .addCase(fetchAllProducts.rejected, (state, action) => {
        state.loading = false;
        state.error = "Cannot fetch this time, try later";
      })
      .addCase(fetchAllProducts.fulfilled, (state, action) => {
        state.loading = false;
        if (typeof action.payload === "string") {
          state.error = action.payload;
        } else {
          state.products = action.payload;
        }
      })
      .addCase(fetchSingleProduct.pending, (state) => {
        state.loading = true;
      })
      .addCase(fetchSingleProduct.rejected, (state, action) => {
        state.loading = false;
        state.error = "Cannot fetch this time, try later";
      })
      .addCase(fetchSingleProduct.fulfilled, (state, action) => {
        state.loading = false;
        if (typeof action.payload === "string") {
          state.error = action.payload;
        } else {
          state.products = [action.payload];
        }
      })
      .addCase(createNewProducts.pending, (state) => {
        state.loading = true;
        state.error = "";
      })
      .addCase(createNewProducts.rejected, (state, action) => {
        state.loading = false;
        state.error = "Cannot create new product, try later";
      })
      .addCase(createNewProducts.fulfilled, (state, action) => {
        state.loading = false;
        if (typeof action.payload === "string") {
          state.error = action.payload;
        } else {
          state.createProduct = action.payload;
        }
        state.loading = false;
      })
      .addCase(updateSingleProduct.pending, (state, action) => {
        state.loading = true;
      })
      .addCase(updateSingleProduct.rejected, (state, action) => {
        state.loading = false;
        state.error = "Cannot update the product now, try later";
      })
      .addCase(updateSingleProduct.fulfilled, (state, action) => {
        const updatedProduct = action.payload;
        state.loading = false;
        const index = state.products.findIndex(
          (product) => product.id === updatedProduct.id
        );

        if (index !== -1) {
          state.products[index] = updatedProduct;
        }
      })
      .addCase(deleteSignleProduct.pending, (state, action) => {
        state.loading = true;
      })
      .addCase(deleteSignleProduct.rejected, (state, action) => {
        state.loading = false;
        state.error = "Cannot delete the product now, try later";
      })
      .addCase(deleteSignleProduct.fulfilled, (state, action) => {
        const newProducts = state.products.filter(
          (product) => product.id !== action.payload.id
        );
        state.products = newProducts;
        state.loading = false;
      });
  },
});

const productsReducer = productsSlice.reducer;
export const { sortPrice, emptyProductList } = productsSlice.actions;
export default productsReducer;
