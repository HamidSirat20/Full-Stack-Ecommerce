export interface User {
  id: string;
  firstName: string;
  lastName: string;
  email: string;
  avatar: string;
  role?: string;
}

export interface CreateNewUser {
  firstName: string;
  lastName: string;
  email: string;
  avatar: string;
  password: string;
}
export interface Login {
  email: string;
  password: string;
}
export interface UserProfile {
  id: string;
  firstName: string;
  lastName: string;
  email: string;
  avatar: string;
  role: string;
}

export interface UpdateNewUser {
  id: string;
  update: {
    firstName: string;
    lastName: string;
    avatar: string;
  };
}
export default User;
