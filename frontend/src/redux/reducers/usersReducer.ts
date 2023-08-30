import {
  PayloadAction,
  createAction,
  createAsyncThunk,
  createSlice,
} from "@reduxjs/toolkit";
import axios, { AxiosError } from "axios";

import { UserLogin } from "../../types/UserLogin";
import User, { CreateNewUser, UpdateNewUser } from "../../types/User";
import QueryParamters from "../../types/QueryParameters";

interface UserReducer {
  users: User[];
  currentUser?: User;
  loading: boolean;
  error: string | null;
  isSuccess: boolean;
  checkemail: boolean;
}
const initialUsers: UserReducer = {
  users: [],
  loading: false,
  error: "",
  isSuccess: false,
  checkemail: false,
};
const initialQueryParams: QueryParamters = {
  Search: "",
  OrderBy: "CreatedAt",
  OrderByDescending: false,
  Offset: 0,
  Limit: 10,
};
export const authenticate = createAsyncThunk(
  "authenticate",
  async (access_token: string) => {
    try {
      const authentication = await axios.get<User>(
        "https://api.escuelajs.co/api/v1/auth/profile",
        {
          headers: {
            Authorization: `Bearer ${access_token}`,
          },
        }
      );
      return authentication.data;
    } catch (e) {
      const error = e as AxiosError;
      return error;
    }
  }
);
export const login = createAsyncThunk(
  "login",
  async ({ email, password }: UserLogin, { dispatch }) => {
    try {
      const result = await axios.post<{ access_token: string }>(
        "http://localhost:5049/api/v1/users/auth",
        { email, password }
      );
      localStorage.setItem("token", result.data.access_token);
      const authentication = await dispatch(
        authenticate(result.data.access_token)
      );
      return authentication.payload as User;
    } catch (e) {
      const error = e as AxiosError;
      return error;
    }
  }
);

interface Pagination {
  search?: string;
  orderBy?: string;
  orderByDescending?: boolean;
  offset?: number;
  limit?: number;
}
export const getAllUsers = createAsyncThunk(
  "users/fetchAll",
  async (pagination: Pagination) => {
    const { search, orderBy, orderByDescending, offset, limit } = pagination;

    try {
      const response = await axios.get<User[]>(
        `http://localhost:5049/api/v1/users`,
        {
          params: {
            Search: search,
            OrderBy: orderBy,
            OrderByDescending: orderByDescending,
            offset,
            limit,
          },
        }
      );
      return response.data;
    } catch (error) {
      const axiosError = error as AxiosError;
      throw axiosError.message;
    }
  }
);

export const createUser = createAsyncThunk(
  "createNewUser",
  async (user: CreateNewUser) => {
    try {
      const result = await axios.post(
        "http://localhost:5049/api/v1/users",
        user
      );
      return result.data;
    } catch (e) {
      const error = e as AxiosError;
      return error.message;
    }
  }
);
export const updateSingleUser = createAsyncThunk(
  "updateSingleUser",
  async (updateUser: UpdateNewUser) => {
    const { id, update } = updateUser;
    try {
      const result = await axios.put(
        `https://api.escuelajs.co/api/v1/users/${id}`,
        update
      );
      return result.data;
    } catch (e) {
      const error = e as AxiosError;
      return error.message;
    }
  }
);
const usersSlice = createSlice({
  name: "users",
  initialState: initialUsers,
  reducers: {
    logout: (state) => {
      state.users = [];
      state.loading = false;
      state.error = null;
      localStorage.removeItem("token");
    },
    reset: (state) => {
      state.users = [];
      state.loading = false;
      state.error = "";
      state.isSuccess = false;
    },
  },
  extraReducers: (build) => {
    build
      .addCase(login.fulfilled, (state, action) => {
        state.isSuccess = true;
        if (action.payload instanceof AxiosError) {
          state.error = action.payload.message;
          state.loading = false;
        } else {
          state.currentUser = action.payload;
          state.loading = false;
        }
      })
      .addCase(login.pending, (state, action) => {
        state.loading = true;
      })
      .addCase(login.rejected, (state, action) => {
        state.error = "Signin failed";
        state.loading = false;
      })
      .addCase(authenticate.fulfilled, (state, action) => {
        if (action.payload instanceof AxiosError) {
          state.error = action.payload.message;
        } else {
          state.currentUser = action.payload;
          console.log(state.currentUser);
        }
        state.loading = false;
      })
      .addCase(authenticate.pending, (state, action) => {
        state.loading = true;
      })
      .addCase(authenticate.rejected, (state, action) => {
        state.error = "Authentication Failed";
      })

      .addCase(getAllUsers.pending, (state, action) => {
        state.loading = true;
      })
      .addCase(getAllUsers.rejected, (state, action) => {
        state.loading = false;
        state.error = "Cannot fetch this time, try later";
      })
      .addCase(getAllUsers.fulfilled, (state, action) => {
        state.loading = false;
        if (typeof action.payload === "string") {
          state.error = action.payload;
        } else {
          state.users = action.payload;
        }
      })
      .addCase(createUser.pending, (state) => {
        state.loading = true;
      })
      .addCase(createUser.rejected, (state) => {
        state.loading = false;
        state.error = "Cannot create new product, try later";
      })
      .addCase(createUser.fulfilled, (state, action: PayloadAction<User>) => {
        state.loading = false;
        state.isSuccess = true;
        if (typeof action.payload === "string") {
          state.error = action.payload;
        } else {
          state.users.push(action.payload);
        }
      })
      .addCase(updateSingleUser.pending, (state, action) => {
        state.loading = true;
      })
      .addCase(updateSingleUser.rejected, (state, action) => {
        state.loading = false;
        state.error = "Cannot update the product now, try later";
      })
      .addCase(updateSingleUser.fulfilled, (state, action) => {
        state.loading = false;
        state.users.map((user) => {
          if (user.id === action.payload.id) {
            return action.payload;
          }
          return user;
        });
      });
  },
});

export const { reset, logout } = usersSlice.actions;
const usersReducers = usersSlice.reducer;
export default usersReducers;
