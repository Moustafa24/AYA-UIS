import { STORAGE_KEYS } from '../constants';

class ApiService {
  getToken() {
    return localStorage.getItem(STORAGE_KEYS.TOKEN);
  }

  getHeaders(isFormData = false) {
    const headers = {};
    if (!isFormData) headers['Content-Type'] = 'application/json';
    const token = this.getToken();
    if (token) headers['Authorization'] = `Bearer ${token}`;
    return headers;
  }

  async request(url, options = {}) {
    const res = await fetch(url, {
      ...options,
      headers: { ...this.getHeaders(options.isFormData), ...options.headers },
    });
    if (!res.ok) {
      const err = await res
        .json()
        .catch(() => ({ errorMessage: res.statusText }));
      throw { status: res.status, ...(err || {}) };
    }
    if (res.status === 204) return null;
    return res.json();
  }

  get(url) {
    return this.request(url);
  }
  post(url, data) {
    return this.request(url, { method: 'POST', body: JSON.stringify(data) });
  }
  put(url, data) {
    return this.request(url, { method: 'PUT', body: JSON.stringify(data) });
  }
  patch(url, data) {
    return this.request(url, { method: 'PATCH', body: JSON.stringify(data) });
  }
  del(url) {
    return this.request(url, { method: 'DELETE' });
  }

  postForm(url, formData) {
    return this.request(url, {
      method: 'POST',
      body: formData,
      isFormData: true,
      headers: { Authorization: `Bearer ${this.getToken()}` },
    });
  }
  patchForm(url, formData) {
    return this.request(url, {
      method: 'PATCH',
      body: formData,
      isFormData: true,
      headers: { Authorization: `Bearer ${this.getToken()}` },
    });
  }
}

const apiService = new ApiService();
export default apiService;
