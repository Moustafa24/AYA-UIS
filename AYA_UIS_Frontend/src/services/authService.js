import { apiService } from './api';
import { API_ENDPOINTS, STORAGE_KEYS } from '../constants';

class AuthService {
  // Login user
  async login(credentials) {
    try {
      const response = await apiService.post(
        API_ENDPOINTS.AUTH.LOGIN,
        credentials,
        false
      );
      
      if (response.token) {
        this.setAuthToken(response.token);
        if (response.refreshToken) {
          this.setRefreshToken(response.refreshToken);
        }
        if (response.user) {
          this.setUserData(response.user);
        }
      }
      
      return response;
    } catch (error) {
      throw error;
    }
  }

  // Register new user
  async register(userData) {
    try {
      const response = await apiService.post(
        API_ENDPOINTS.AUTH.REGISTER,
        userData,
        false
      );
      
      return response;
    } catch (error) {
      throw error;
    }
  }

  // Logout user
  async logout() {
    try {
      await apiService.post(API_ENDPOINTS.AUTH.LOGOUT);
    } catch (error) {
      console.error('Logout error:', error);
    } finally {
      this.clearAuthData();
    }
  }

  // Reset password
  async resetPassword(email) {
    try {
      const response = await apiService.post(
        API_ENDPOINTS.AUTH.RESET_PASSWORD,
        { email },
        false
      );
      
      return response;
    } catch (error) {
      throw error;
    }
  }

  // Refresh authentication token
  async refreshToken() {
    try {
      const refreshToken = this.getRefreshToken();
      if (!refreshToken) {
        throw new Error('No refresh token available');
      }

      const response = await apiService.post(
        API_ENDPOINTS.AUTH.REFRESH,
        { refreshToken },
        false
      );
      
      if (response.token) {
        this.setAuthToken(response.token);
        if (response.refreshToken) {
          this.setRefreshToken(response.refreshToken);
        }
      }
      
      return response;
    } catch (error) {
      this.clearAuthData();
      throw error;
    }
  }

  // Get current user data
  getCurrentUser() {
    const userDataString = localStorage.getItem(STORAGE_KEYS.USER_DATA);
    return userDataString ? JSON.parse(userDataString) : null;
  }

  // Check if user is authenticated
  isAuthenticated() {
    const token = this.getAuthToken();
    return !!token && !this.isTokenExpired(token);
  }

  // Get authentication token
  getAuthToken() {
    return localStorage.getItem(STORAGE_KEYS.AUTH_TOKEN);
  }

  // Set authentication token
  setAuthToken(token) {
    localStorage.setItem(STORAGE_KEYS.AUTH_TOKEN, token);
  }

  // Get refresh token
  getRefreshToken() {
    return localStorage.getItem(STORAGE_KEYS.REFRESH_TOKEN);
  }

  // Set refresh token
  setRefreshToken(token) {
    localStorage.setItem(STORAGE_KEYS.REFRESH_TOKEN, token);
  }

  // Set user data
  setUserData(userData) {
    localStorage.setItem(STORAGE_KEYS.USER_DATA, JSON.stringify(userData));
  }

  // Clear all authentication data
  clearAuthData() {
    localStorage.removeItem(STORAGE_KEYS.AUTH_TOKEN);
    localStorage.removeItem(STORAGE_KEYS.REFRESH_TOKEN);
    localStorage.removeItem(STORAGE_KEYS.USER_DATA);
  }

  // Check if token is expired
  isTokenExpired(token) {
    try {
      const payload = JSON.parse(atob(token.split('.')[1]));
      const currentTime = Date.now() / 1000;
      return payload.exp < currentTime;
    } catch (error) {
      return true;
    }
  }

  // Get user role
  getUserRole() {
    const user = this.getCurrentUser();
    return user?.role || null;
  }

  // Check if user has specific role
  hasRole(role) {
    const userRole = this.getUserRole();
    return userRole === role;
  }

  // Check if user has any of the specified roles
  hasAnyRole(roles) {
    const userRole = this.getUserRole();
    return roles.includes(userRole);
  }
}

export const authService = new AuthService();
export default authService;