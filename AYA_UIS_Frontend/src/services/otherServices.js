import api from './api';
import { API_ENDPOINTS } from '../constants';

export const studyYearService = {
  create: data => api.post(API_ENDPOINTS.STUDY_YEARS.BASE, data),
};

export const semesterService = {
  getByYear: yearId => api.get(API_ENDPOINTS.SEMESTERS.BY_YEAR(yearId)),
  create: (yearId, data) =>
    api.post(API_ENDPOINTS.SEMESTERS.BY_YEAR(yearId), data),
};

export const userStudyYearService = {
  create: data => api.post(API_ENDPOINTS.USER_STUDY_YEARS.BASE, data),
  update: (id, data) => api.put(API_ENDPOINTS.USER_STUDY_YEARS.BY_ID(id), data),
  getMyStudyYears: () => api.get(API_ENDPOINTS.USER_STUDY_YEARS.MY_STUDY_YEARS),
  getMyTimeline: () => api.get(API_ENDPOINTS.USER_STUDY_YEARS.MY_TIMELINE),
  getMyCurrent: () => api.get(API_ENDPOINTS.USER_STUDY_YEARS.MY_CURRENT),
  getByUser: userId => api.get(API_ENDPOINTS.USER_STUDY_YEARS.BY_USER(userId)),
  getUserTimeline: userId =>
    api.get(API_ENDPOINTS.USER_STUDY_YEARS.USER_TIMELINE(userId)),
  promoteAll: () => api.post(API_ENDPOINTS.USER_STUDY_YEARS.PROMOTE_ALL),
  promoteStudent: code => api.post(API_ENDPOINTS.USER_STUDY_YEARS.PROMOTE_STUDENT(code)),
};

export const feeService = {
  getAll: () => api.get(API_ENDPOINTS.DEPARTMENT_FEES.BASE),
  getByDeptGrade: (name, grade) =>
    api.get(API_ENDPOINTS.DEPARTMENT_FEES.BY_DEPT_GRADE(name, grade)),
  update: (name, grade, fees) =>
    api.put(API_ENDPOINTS.DEPARTMENT_FEES.BY_DEPT_GRADE(name, grade), fees),
};

export const scheduleService = {
  getAll: () => api.get(API_ENDPOINTS.SCHEDULES.BASE),
  getById: id => api.get(API_ENDPOINTS.SCHEDULES.BY_ID(id)),
  create: (yearId, deptId, semId, formData) =>
    api.postForm(
      API_ENDPOINTS.SCHEDULES.CREATE(yearId, deptId, semId),
      formData
    ),
  update: (id, data) => api.put(API_ENDPOINTS.SCHEDULES.BY_ID(id), data),
  del: id => api.del(API_ENDPOINTS.SCHEDULES.BY_ID(id)),
};

export const roleService = {
  getAll: () => api.get(API_ENDPOINTS.ROLES.BASE),
  getById: id => api.get(API_ENDPOINTS.ROLES.BY_ID(id)),
  create: data => api.post(API_ENDPOINTS.ROLES.BASE, data),
  update: (id, data) => api.put(API_ENDPOINTS.ROLES.BY_ID(id), data),
  del: id => api.del(API_ENDPOINTS.ROLES.BY_ID(id)),
  updateUserRoleByEmail: data =>
    api.put(API_ENDPOINTS.ROLES.UPDATE_BY_EMAIL, data),
  getUserRoleInfo: code => api.get(API_ENDPOINTS.ROLES.USER_ROLE_INFO(code)),
};

export const userService = {
  getByAcademicCode: code => api.get(API_ENDPOINTS.USER.BY_ACADEMIC_CODE(code)),
  updateProfilePicture: formData =>
    api.patchForm(API_ENDPOINTS.USER.UPDATE_PROFILE_PICTURE, formData),
  updateSpecialization: data =>
    api.patch(API_ENDPOINTS.USER.UPDATE_SPECIALIZATION, data),
};
