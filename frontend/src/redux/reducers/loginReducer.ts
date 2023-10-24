import { PayloadAction, createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import axios from "axios";
import { Login, UserProfile } from "../../types/User";

const baseUrl = "https://pinnaclemall.azurewebsites.net/api/v1";

const initialState: {
  loading: boolean;
  user: Login | null;
  error: string | undefined;
  userProfile: UserProfile | null;
  isLoggedIn: boolean;
} = {
  loading: false,
  error: "",
  user: null,
  userProfile: null,
  isLoggedIn: false,
};

export const userLogin = createAsyncThunk(
  "user/login",
  async (userCrendials: Login) => {
    const request = await axios.post(`${baseUrl}/auth`, userCrendials);
    const response = await request.data;
    localStorage.setItem("mytoken", JSON.stringify(response));
    return response;
  }
);

export const autoLogout = createAsyncThunk(
  "user/autoLogout",
  async (_, { dispatch }) => {
    dispatch(logout());
  }
);

export const fetchUserProfile = createAsyncThunk(
  "fetchUserProfile",
  async () => {
    const storedToken = localStorage.getItem("mytoken");
    const token = storedToken ? storedToken.replace(/^"(.*)"$/, "$1") : null;
    try {
      const response = await axios.get<UserProfile>(`${baseUrl}/profile`, {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      });
      localStorage.setItem("userId", JSON.stringify(response.data.id));
      localStorage.setItem("Role", JSON.stringify(response.data.role));
      return response.data;
    } catch (error) {
      throw error;
    }
  }
);

const loginSlice = createSlice({
  name: "login",
  initialState: initialState,
  reducers: {
    logout: (state) => {
      localStorage.removeItem("mytoken");
      localStorage.removeItem("userId");
      localStorage.removeItem("Role");
      return {
        ...state,
        user: null,
        userProfile: null,
        isLoggedIn: false,
      };
    },
  },
  extraReducers: (build) => {
    build
      .addCase(userLogin.pending, (state) => {
        state.loading = true;
        state.user = null;
        state.error = "";
        state.isLoggedIn = false;
      })
      .addCase(userLogin.fulfilled, (state, action) => {
        state.loading = false;
        state.user = action.payload;
        state.error = "";
        state.isLoggedIn = true;
      })
      .addCase(userLogin.rejected, (state, action) => {
        state.loading = false;
        state.user = null;
        state.isLoggedIn = false;
        if (action.error.message === "Request failed with status code 401") {
          state.error = "Access denied! Invalid Credentials";
        } else {
          state.error = action.error.message;
        }
      })
      .addCase(fetchUserProfile.pending, (state, action) => {
        state.loading = true;
        state.userProfile = null;
        state.error = "";
      })
      .addCase(
        fetchUserProfile.fulfilled,
        (state, action: PayloadAction<UserProfile>) => {
          state.loading = false;
          state.userProfile = action.payload;
          state.error = "";
          state.isLoggedIn = true;
        }
      )
      .addCase(fetchUserProfile.rejected, (state, action) => {
        state.loading = false;
        state.userProfile = null;
        state.error = action.error.message || "An error occurred";
        state.isLoggedIn = false;
        localStorage.removeItem("mytoken");
        localStorage.removeItem("userId");
        localStorage.removeItem("Role");
      })
      .addCase(autoLogout.fulfilled, (state) => {
        state.userProfile = null;
        localStorage.removeItem("mytoken");
        localStorage.removeItem("userId");
        localStorage.removeItem("Role");

        state.isLoggedIn = false;
      });
  },
});

export const { logout } = loginSlice.actions;

const loginReducer = loginSlice.reducer;
export default loginReducer;
