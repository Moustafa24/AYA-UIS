import { apiService } from './api';
import { API_ENDPOINTS } from '../constants';

class DepartmentService {
  // Get all departments
  async getAllDepartments() {
    try {
      const response = await apiService.get(API_ENDPOINTS.DEPARTMENTS.GET_ALL);
      return response;
    } catch (error) {
      throw error;
    }
  }

  // Get department by ID
  async getDepartmentById(id) {
    try {
      const response = await apiService.get(
        `${API_ENDPOINTS.DEPARTMENTS.GET_BY_ID}/${id}`
      );
      return response;
    } catch (error) {
      throw error;
    }
  }

  // Create new department
  async createDepartment(departmentData) {
    try {
      const response = await apiService.post(
        API_ENDPOINTS.DEPARTMENTS.CREATE,
        departmentData
      );
      return response;
    } catch (error) {
      throw error;
    }
  }

  // Update department
  async updateDepartment(id, departmentData) {
    try {
      const response = await apiService.put(
        `${API_ENDPOINTS.DEPARTMENTS.UPDATE}/${id}`,
        departmentData
      );
      return response;
    } catch (error) {
      throw error;
    }
  }

  // Delete department
  async deleteDepartment(id) {
    try {
      const response = await apiService.delete(
        `${API_ENDPOINTS.DEPARTMENTS.DELETE}/${id}`
      );
      return response;
    } catch (error) {
      throw error;
    }
  }

  // Search departments
  async searchDepartments(query, filters = {}) {
    try {
      const searchParams = new URLSearchParams({
        query,
        ...filters,
      });
      
      const response = await apiService.get(
        `${API_ENDPOINTS.DEPARTMENTS.GET_ALL}?${searchParams}`
      );
      return response;
    } catch (error) {
      throw error;
    }
  }

  // Get departments with pagination
  async getDepartmentsPaginated(page = 1, pageSize = 10, filters = {}) {
    try {
      const searchParams = new URLSearchParams({
        page: page.toString(),
        pageSize: pageSize.toString(),
        ...filters,
      });
      
      const response = await apiService.get(
        `${API_ENDPOINTS.DEPARTMENTS.GET_ALL}?${searchParams}`
      );
      return response;
    } catch (error) {
      throw error;
    }
  }
}

export const departmentService = new DepartmentService();
export default departmentService;