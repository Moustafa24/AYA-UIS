import React, {
  createContext,
  useContext,
  useReducer,
  useCallback,
  useEffect,
} from 'react';
import authService from '../services/authService';
import { STATUS, USER_ROLES } from '../constants';

const AuthContext = createContext(null);

const initialState = {
  user: authService.getCurrentUser(),
  isAuthenticated: authService.isAuthenticated(),
  status: STATUS.IDLE,
  error: null,
};

function authReducer(state, action) {
  switch (action.type) {
    case 'LOADING':
      return { ...state, status: STATUS.LOADING, error: null };
    case 'LOGIN_SUCCESS':
      return {
        ...state,
        user: action.payload,
        isAuthenticated: true,
        status: STATUS.SUCCESS,
        error: null,
      };
    case 'LOGOUT':
      return {
        ...state,
        user: null,
        isAuthenticated: false,
        status: STATUS.IDLE,
        error: null,
      };
    case 'ERROR':
      return { ...state, status: STATUS.ERROR, error: action.payload };
    case 'CLEAR_ERROR':
      return { ...state, error: null, status: STATUS.IDLE };
    default:
      return state;
  }
}

export function AuthProvider({ children }) {
  const [state, dispatch] = useReducer(authReducer, initialState);

  const login = useCallback(async (email, password) => {
    dispatch({ type: 'LOADING' });
    try {
      const data = await authService.login(email, password);
      dispatch({ type: 'LOGIN_SUCCESS', payload: data });
      return data;
    } catch (err) {
      dispatch({
        type: 'ERROR',
        payload: err?.errorMessage || err?.ErrorMessage || 'Login failed',
      });
      throw err;
    }
  }, []);

  const logout = useCallback(() => {
    authService.logout();
    dispatch({ type: 'LOGOUT' });
  }, []);

  const clearError = useCallback(() => dispatch({ type: 'CLEAR_ERROR' }), []);

  const isAdmin = state.user?.role === USER_ROLES.ADMIN;
  const isStudent = state.user?.role === USER_ROLES.STUDENT;
  const isInstructor = state.user?.role === USER_ROLES.INSTRUCTOR;

  return (
    <AuthContext.Provider
      value={{
        ...state,
        login,
        logout,
        clearError,
        isAdmin,
        isStudent,
        isInstructor,
      }}
    >
      {children}
    </AuthContext.Provider>
  );
}

export function useAuth() {
  const ctx = useContext(AuthContext);
  if (!ctx) throw new Error('useAuth must be used within AuthProvider');
  return ctx;
}
