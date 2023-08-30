import React, { useState } from 'react';

import TextField from '@mui/material/TextField';
import Button from '@mui/material/Button';
import CircularProgress from '@mui/material/CircularProgress';
import useAppSelector from '../hooks/useAppSelector';
import useAppDispatch from '../hooks/useAppDispatch';
import { CreateNewUser } from '../types/User';
import { createUser } from '../redux/reducers/usersReducer';

interface RootState {
  user: {
    loading: boolean;
    isSuccess: boolean;
    error: string | null;
  };
}

const Signin: React.FC = () => {
  const dispatch = useAppDispatch();
  const loading = useAppSelector((state) => state.usersReducers.loading);
  const isSuccess = useAppSelector((state) => state.usersReducers.isSuccess);
  const error = useAppSelector((state) => state.usersReducers.error);

  const initialNewUser: CreateNewUser = {
    firstName: '',
    lastName: '',
    email: '',
    avatar: '',
    password: '',
  };

  const [newUser, setNewUser] = useState<CreateNewUser>(initialNewUser);

  const handleCreateUser = () => {
    dispatch(createUser(newUser));
    if (isSuccess) {
      setNewUser(initialNewUser); // Reset fields after successful user creation
    }
  };

  return (
    <div style={{ display: 'flex', flexDirection: 'column', alignItems: 'center', justifyContent: 'center', minHeight: '100vh' }}>
      <h2 style={{ marginBottom: '1rem', color: '#333' }}>Create New User</h2>
      <form style={{ display: 'flex', flexDirection: 'column', gap: '1rem', maxWidth: '400px', width: '100%' }}>
        <TextField
          label="First Name"
          value={newUser.firstName}
          onChange={(e) =>
            setNewUser((prevUser) => ({ ...prevUser, firstName: e.target.value }))
          }
        />
         <TextField
          label="Last Name"
          value={newUser.lastName}
          onChange={(e) =>
            setNewUser((prevUser) => ({ ...prevUser, lastName: e.target.value }))
          }
        />
        <TextField
          label="Email"
          value={newUser.email}
          onChange={(e) =>
            setNewUser((prevUser) => ({ ...prevUser, email: e.target.value }))
          }
        />
        <TextField
          label="Password"
          type="password"
          value={newUser.password}
          onChange={(e) =>
            setNewUser((prevUser) => ({ ...prevUser, password: e.target.value }))
          }
        />
        <TextField
          label="Avatar URL"
          value={newUser.avatar}
          onChange={(e) =>
            setNewUser((prevUser) => ({ ...prevUser, avatar: e.target.value }))
          }
        />
        <Button variant="contained" color="primary" onClick={handleCreateUser}>
          Create User
        </Button>
      </form>

      {loading && <CircularProgress style={{ marginTop: '1rem' }} />}
      {isSuccess && <p style={{ color: 'green', marginTop: '1rem' }}>User created successfully!</p>}
      {error && <p style={{ color: 'red', marginTop: '1rem' }}>{error}</p>}
    </div>
  );
};

export default Signin;
