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
  user?: User;
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
const baseUrl = "http://localhost:5049/api/v1";
const initialQueryParams: QueryParamters = {
  Search: "",
  OrderBy: "CreatedAt",
  OrderByDescending: false,
  Offset: 0,
  Limit: 10,
};
// export const authenticate = createAsyncThunk(
//   "authenticate",
//   async (access_token: string) => {
//     try {
//       const authentication = await axios.get<User>(
//         "https://api.escuelajs.co/api/v1/auth/profile",
//         {
//           headers: {
//             Authorization: `Bearer ${access_token}`,
//           },
//         }
//       );
//       return authentication.data;
//     } catch (e) {
//       const error = e as AxiosError;
//       return error;
//     }
//   }
// );
// export const login = createAsyncThunk(
//   "login",
//   async ({ email, password }: UserLogin, { dispatch }) => {
//     try {
//       const result = await axios.post<{ access_token: string }>(
//         "http://localhost:5049/api/v1/users/auth",
//         { email, password }
//       );
//       localStorage.setItem("token", result.data.access_token);
//       const authentication = await dispatch(
//         authenticate(result.data.access_token)
//       );
//       return authentication.payload as User;
//     } catch (e) {
//       const error = e as AxiosError;
//       return error;
//     }
//   }
// );

interface Pagination {
  search?: string;
  orderBy?: string;
  orderByDescending?: boolean;
  offset?: number;
  limit?: number;
}
export const getAllUsers = createAsyncThunk(
  'fetchAllUser',
  async () => {
    try {
      const result = await axios.get<User[]>('https://ecommerce-backend-fs15.azurewebsites.net/api/v1/users');
      return result.data; // The returned result will be inside action.payload
    } catch (e) {
      const error = e as AxiosError;
      return error;
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
  'createUser',
  async ({userData}: { userData: CreateNewUser }) => {
    try {

      const result = await axios.post<CreateNewUser>('http://localhost:5049/api/v1/users', userData);
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

export const authenticate = createAsyncThunk(
  "authenticate",
  async (access_token: string) => {
    try {
      console.log("token", access_token);
      const authentication = await axios.post<User>(
        "http://localhost:5049/api/v1/auth",
        access_token
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
        "http://localhost:5049/api/v1/auth",
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
const usersSlice = createSlice({
  name: "users",
  initialState: initialState,
  reducers: {
    emptyUserInfo: (state) => {
      state.user = {
        id: "",
        email: "",
        password: "",
        firstName: "",
        address: "",
        lastName: "",
        Role: "Client",
        avatar: "",
      };
    },
    cleanUpUserReducer: () => {
      return initialState;
    },
  },
  extraReducers: (build) => {
    build
    .addCase(getAllUsers.fulfilled, (state, action) => {
      if (action.payload instanceof AxiosError) {
          state.error = action.payload.message
      } else {
          state.users = action.payload;
      }
      state.loading = false
    })
    .addCase(getOneUser.fulfilled, (state, action) => {
        if (action.payload instanceof AxiosError) {
            state.error = action.payload.message
        } else {
            state.user = action.payload;

        }
        state.loading = false
    })
    .addCase(getOneUser.pending, (state, action) => {
        state.loading = true
    })
    .addCase(getOneUser.rejected, (state, action) => {
        state.error = "Cannot fetch data"
    })
    .addCase(createUser.fulfilled, (state, action) => {
        if (action.payload instanceof AxiosError) {
            state.error = action.payload.message
        } else {
           state.user = action.payload;
        }
        state.loading = false
    })
    .addCase(createUser.pending, (state) => {
        state.loading = true
    })
    .addCase(createUser.rejected, (state) => {
        state.error = "Cannot fetch data"
    })
    .addCase(updateSingleUser.fulfilled, (state, action:PayloadAction<CreateNewUser>) => {
      if (action.payload instanceof AxiosError) {
          state.error = action.payload.message
      } else {
         state.user = action.payload;
      }
      state.loading = false
    })
    .addCase(updateSingleUser.pending, (state, action) => {
      state.loading = true
    })
    .addCase(updateSingleUser.rejected, (state, action) => {
      state.error = "Cannot fetch data"
    })
      .addCase(login.fulfilled, (state, action) => {
        if (action.payload instanceof AxiosError) {
            state.error = action.payload.message
        } else {
            state.user = action.payload;
        }
        state.loading = false
      })
      .addCase(authenticate.fulfilled, (state, action) => {
          if (action.payload instanceof AxiosError) {
              state.error = action.payload.message
          } else {
              state.user = action.payload
              console.log(state.user);
              state.authenticate = true
          }
          state.loading = false
      })
  },
});

const userReducer = usersSlice.reducer
export const
    {
        emptyUserInfo,
        cleanUpUserReducer
    } = usersSlice.actions
export default userReducer;
