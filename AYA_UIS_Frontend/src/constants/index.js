// Application Constants
export const API_ENDPOINTS = {
  // Authentication endpoints
  AUTH: {
    LOGIN: '/api/auth/login',
    REGISTER: '/api/auth/register',
    REFRESH: '/api/auth/refresh',
    LOGOUT: '/api/auth/logout',
    RESET_PASSWORD: '/api/auth/reset-password',
  },
  
  // Department endpoints
  DEPARTMENTS: {
    GET_ALL: '/api/departments',
    GET_BY_ID: '/api/departments',
    CREATE: '/api/departments',
    UPDATE: '/api/departments',
    DELETE: '/api/departments',
  },
  
  // Course endpoints
  COURSES: {
    GET_ALL: '/api/courses',
    GET_BY_ID: '/api/courses',
    CREATE: '/api/courses',
    UPDATE: '/api/courses',
    DELETE: '/api/courses',
  },
  
  // Fee endpoints
  FEES: {
    GET_ALL: '/api/fees',
    GET_BY_ID: '/api/fees',
    CREATE: '/api/fees',
    UPDATE: '/api/fees',
    DELETE: '/api/fees',
  },
  
  // Schedule endpoints
  SCHEDULES: {
    GET_ALL: '/api/schedules',
    GET_BY_ID: '/api/schedules',
    CREATE: '/api/schedules',
    UPDATE: '/api/schedules',
    DELETE: '/api/schedules',
  },
};

// Application routes
export const ROUTES = {
  HOME: '/',
  LOGIN: '/login',
  REGISTER: '/register',
  DASHBOARD: '/dashboard',
  
  // Department routes
  DEPARTMENTS: '/departments',
  DEPARTMENTS_CREATE: '/departments/create',
  DEPARTMENTS_EDIT: '/departments/edit',
  
  // Course routes
  COURSES: '/courses',
  COURSES_CREATE: '/courses/create',
  COURSES_EDIT: '/courses/edit',
  
  // Fee routes
  FEES: '/fees',
  FEES_CREATE: '/fees/create',
  FEES_EDIT: '/fees/edit',
  
  // Schedule routes
  SCHEDULES: '/schedules',
  SCHEDULES_CREATE: '/schedules/create',
  SCHEDULES_EDIT: '/schedules/edit',
};

// User roles
export const USER_ROLES = {
  ADMIN: 'Admin',
  FACULTY: 'Faculty', 
  STUDENT: 'Student',
  STAFF: 'Staff',
};

// Application settings
export const APP_CONFIG = {
  NAME: 'AYA University Information System',
  VERSION: '1.0.0',
  COPYRIGHT: '© 2026 AYA University',
};

// Theme constants
export const THEME = {
  COLORS: {
    PRIMARY: '#2563eb',
    SECONDARY: '#64748b',
    SUCCESS: '#10b981',
    WARNING: '#f59e0b',
    ERROR: '#ef4444',
    INFO: '#3b82f6',
  },
  BREAKPOINTS: {
    MOBILE: '768px',
    TABLET: '1024px',
    DESKTOP: '1280px',
  },
};

// Form validation constants
export const VALIDATION = {
  EMAIL_REGEX: /^[^\s@]+@[^\s@]+\.[^\s@]+$/,
  PASSWORD_MIN_LENGTH: 8,
  PHONE_REGEX: /^\+?[\d\s-()]+$/,
};

// Local storage keys
export const STORAGE_KEYS = {
  AUTH_TOKEN: 'authToken',
  REFRESH_TOKEN: 'refreshToken',
  USER_DATA: 'userData',
  THEME: 'theme',
  LANGUAGE: 'language',
};

// Status constants
export const STATUS = {
  IDLE: 'idle',
  LOADING: 'loading',
  SUCCESS: 'success',
  ERROR: 'error',
};

// Pagination defaults
export const PAGINATION = {
  DEFAULT_PAGE_SIZE: 10,
  PAGE_SIZE_OPTIONS: [10, 25, 50, 100],
};