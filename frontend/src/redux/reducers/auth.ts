import React from 'react';
import {useNavigate } from 'react-router-dom';
import useAppSelector from '../../hooks/useAppSelector';

const Auth = (WrappedComponent: React.ComponentType<any>) => {
  const AuthenticatedComponent: React.FC<any> = (props) => {
    const {authenticate} = useAppSelector(state => state.userReducer);
    const navigate = useNavigate();
    if (authenticate) {
      return React.createElement(WrappedComponent, { ...props });
    } else {
      navigate("/login");
      return null;
    }
  };
  return AuthenticatedComponent;
};

export default Auth;