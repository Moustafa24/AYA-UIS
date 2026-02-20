import api from './api';
import { API_ENDPOINTS, STORAGE_KEYS } from '../constants';

const authService = {
  async login(email, password) {
    const data = await api.post(API_ENDPOINTS.AUTH.LOGIN, { email, password });
    if (data?.token) {
      localStorage.setItem(STORAGE_KEYS.TOKEN, data.token);
      localStorage.setItem(STORAGE_KEYS.USER, JSON.stringify(data));
    }
    return data;
  },

  async register(userData, role = 'Student') {
    return api.post(`${API_ENDPOINTS.AUTH.REGISTER}?role=${role}`, userData);
  },

  async registerStudent(deptId, studentData) {
    return api.post(API_ENDPOINTS.AUTH.REGISTER_STUDENT(deptId), studentData);
  },

  async resetPassword(data) {
    return api.put(API_ENDPOINTS.AUTH.RESET_PASSWORD, data);
  },

  logout() {
    localStorage.removeItem(STORAGE_KEYS.TOKEN);
    localStorage.removeItem(STORAGE_KEYS.USER);
  },

  getCurrentUser() {
    const u = localStorage.getItem(STORAGE_KEYS.USER);
    return u ? JSON.parse(u) : null;
  },

  isAuthenticated() {
    return !!localStorage.getItem(STORAGE_KEYS.TOKEN);
  },

  getUserRole() {
    const u = this.getCurrentUser();
    return u?.role || null;
  },

  hasRole(role) {
    return this.getUserRole() === role;
  },
};

export default authService;
