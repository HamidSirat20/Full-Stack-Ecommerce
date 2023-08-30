export interface User {
  id?: string;
  firstName: string;
  lastName: string;
  email: string;
  address: string;
  avatar: string;
  password: string;
  Role?: "Client" | "Admin";
}

export interface CreateNewUser {
  firstName: string;
  lastName: string;
  email: string;
  avatar: string;
  password: string;
  address: string;
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
