import React, { createContext, useContext, useReducer, useEffect } from 'react';
import { authService } from '../services/authService';
import { STATUS } from '../constants';

// Auth Context
const AuthContext = createContext();

// Auth reducer
const authReducer = (state, action) => {
  switch (action.type) {
    case 'SET_LOADING':
      return { ...state, status: STATUS.LOADING };
    
    case 'LOGIN_SUCCESS':
      return {
        ...state,
        status: STATUS.SUCCESS,
        user: action.payload.user,
        isAuthenticated: true,
        error: null,
      };
    
    case 'LOGIN_ERROR':
      return {
        ...state,
        status: STATUS.ERROR,
        error: action.payload.error,
        isAuthenticated: false,
        user: null,
      };
    
    case 'LOGOUT':
      return {
        ...state,
        status: STATUS.IDLE,
        user: null,
        isAuthenticated: false,
        error: null,
      };
    
    case 'SET_USER':
      return {
        ...state,
        user: action.payload.user,
        isAuthenticated: true,
      };
    
    case 'CLEAR_ERROR':
      return { ...state, error: null };
    
    default:
      return state;
  }
};

// Initial state
const initialState = {
  user: null,
  isAuthenticated: false,
  status: STATUS.IDLE,
  error: null,
};

// Auth Provider Component
export const AuthProvider = ({ children }) => {
  const [state, dispatch] = useReducer(authReducer, initialState);

  // Initialize authentication state
  useEffect(() => {
    const initializeAuth = () => {
      if (authService.isAuthenticated()) {
        const user = authService.getCurrentUser();
        if (user) {
          dispatch({
            type: 'SET_USER',
            payload: { user },
          });
        }
      }
    };

    initializeAuth();
  }, []);

  // Login function
  const login = async (credentials) => {
    try {
      dispatch({ type: 'SET_LOADING' });
      
      const response = await authService.login(credentials);
      
      dispatch({
        type: 'LOGIN_SUCCESS',
        payload: { user: response.user },
      });
      
      return response;
    } catch (error) {
      dispatch({
        type: 'LOGIN_ERROR',
        payload: { error: error.message },
      });
      throw error;
    }
  };

  // Register function
  const register = async (userData) => {
    try {
      dispatch({ type: 'SET_LOADING' });
      
      const response = await authService.register(userData);
      
      return response;
    } catch (error) {
      dispatch({
        type: 'LOGIN_ERROR',
        payload: { error: error.message },
      });
      throw error;
    }
  };

  // Logout function
  const logout = async () => {
    try {
      await authService.logout();
      dispatch({ type: 'LOGOUT' });
    } catch (error) {
      console.error('Logout error:', error);
      // Even if logout fails, clear local state
      dispatch({ type: 'LOGOUT' });
    }
  };

  // Reset password function
  const resetPassword = async (email) => {
    try {
      dispatch({ type: 'SET_LOADING' });
      
      const response = await authService.resetPassword(email);
      
      return response;
    } catch (error) {
      dispatch({
        type: 'LOGIN_ERROR',
        payload: { error: error.message },
      });
      throw error;
    }
  };

  // Clear error function
  const clearError = () => {
    dispatch({ type: 'CLEAR_ERROR' });
  };

  // Check user role
  const hasRole = (role) => {
    return authService.hasRole(role);
  };

  // Check multiple roles
  const hasAnyRole = (roles) => {
    return authService.hasAnyRole(roles);
  };

  const contextValue = {
    ...state,
    login,
    register,
    logout,
    resetPassword,
    clearError,
    hasRole,
    hasAnyRole,
  };

  return (
    <AuthContext.Provider value={contextValue}>
      {children}
    </AuthContext.Provider>
  );
};

// Custom hook to use Auth context
export const useAuth = () => {
  const context = useContext(AuthContext);
  
  if (!context) {
    throw new Error('useAuth must be used within an AuthProvider');
  }
  
  return context;
};

export default AuthContext;