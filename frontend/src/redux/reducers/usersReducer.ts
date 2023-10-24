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

// interface UserReducer {
//   users: User[];
//   currentUser?: User;
//   loading: boolean;
//   error: string | null;
//   isSuccess: boolean;
//   checkemail: boolean;
// }
// const initialUsers: UserReducer = {
//   users: [],
//   loading: false,
//   error: "",
//   isSuccess: false,
//   checkemail: false,
// };

const initialState: {
  createUser?: CreateNewUser;
  users: User[];
  checkemail: boolean;
  loading: boolean;
  error: string;
  token: string;
  authenticate: boolean;
} = {
  users: [],
  checkemail: false,
  loading: false,
  error: "",
  token: "",
  authenticate: false,
};
export interface FetchAllParams {
  search?: string;
  orderBy?: string;
  orderByDescending?: boolean;
  offset: number;
  limit: number;
}

const baseUrl = "https://pinnaclemall.azurewebsites.net/api/v1";

export const getAllUsers = createAsyncThunk(
  "getallusers",
  async ({ offset, limit }: FetchAllParams) => {
    try {
      const storedToken = localStorage.getItem("mytoken");
      const token = storedToken ? storedToken.replace(/^"(.*)"$/, "$1") : null;
      const fetchUsers = axios.get<User[]>(
        `${baseUrl}/users?offset=${offset}&limit=${limit}`,
        {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        }
      );
      return (await fetchUsers).data;
    } catch (e) {
      const error = e as AxiosError;
      return error.message;
    }
  }
);

export const getOneUser = createAsyncThunk(
  "fetchAUser",
  async ({ userId }: { userId: string }) => {
    try {
      const result = await axios.get<User>(`baseUrl/users/${userId}`);
      return result.data;
    } catch (e) {
      const error = e as AxiosError;
      return error;
    }
  }
);

export const createUser = createAsyncThunk(
  "createNewUser",
  async ({ userData }: { userData: CreateNewUser }) => {
    try {
      const result = await axios.post<CreateNewUser>(
        `${baseUrl}/users`,
        userData
      );
      return result.data;
    } catch (e) {
      const error = e as AxiosError;
      return error;
    }
  }
);

export const createAdmin = createAsyncThunk(
  "create/admin",
  async ({ userData }: { userData: CreateNewUser }) => {
    try {
      const storedToken = localStorage.getItem("mytoken");
      const token = storedToken ? storedToken.replace(/^"(.*)"$/, "$1") : null;
      const result = await axios.post<CreateNewUser>(
        `${baseUrl}/users/admin`,
        userData,
        {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        }
      );
      return result.data;
    } catch (e) {
      const error = e as AxiosError;
      return error;
    }
  }
);

export const updateSingleUser = createAsyncThunk(
  "updateSingleUser",
  async (updateUser: UpdateNewUser) => {
    const { id, update } = updateUser;
    try {
      const result = await axios.put(
        `http://localhost:5049/api/v1/users/${id}`,
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
  initialState: initialState,
  reducers: {
    cleanUpUserReducer: () => {
      return initialState;
    },
  },
  extraReducers: (build) => {
    build
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
      .addCase(getOneUser.fulfilled, (state, action) => {
        if (action.payload instanceof AxiosError) {
          state.error = action.payload.message;
        } else {
          state.users.push(action.payload);
        }
        state.loading = false;
      })
      .addCase(getOneUser.pending, (state, action) => {
        state.loading = true;
      })
      .addCase(getOneUser.rejected, (state, action) => {
        state.error = "Cannot fetch data";
      })
      .addCase(createUser.fulfilled, (state, action) => {
        if (action.payload instanceof AxiosError) {
          state.error = action.payload.message;
        } else {
          state.createUser = action.payload;
        }
        state.loading = false;
      })
      .addCase(createUser.pending, (state) => {
        state.loading = true;
      })
      .addCase(createUser.rejected, (state) => {
        state.error = "Cannot fetch data";
      })
      .addCase(createAdmin.fulfilled, (state, action) => {
        if (action.payload instanceof AxiosError) {
          state.error = action.payload.message;
        } else {
          state.createUser = action.payload;
        }
        state.loading = false;
      })
      .addCase(createAdmin.pending, (state) => {
        state.loading = true;
      })
      .addCase(createAdmin.rejected, (state) => {
        state.error = "Cannot fetch data";
      })
      .addCase(
        updateSingleUser.fulfilled,
        (state, action: PayloadAction<CreateNewUser>) => {
          if (action.payload instanceof AxiosError) {
            state.error = action.payload.message;
          } else {
            // state.users.push(action.payload);
          }
          state.loading = false;
        }
      )
      .addCase(updateSingleUser.pending, (state, action) => {
        state.loading = true;
      })
      .addCase(updateSingleUser.rejected, (state, action) => {
        state.error = "Cannot fetch data";
      });
  },
});

const userReducer = usersSlice.reducer;
export const { cleanUpUserReducer } = usersSlice.actions;
export default userReducer;
