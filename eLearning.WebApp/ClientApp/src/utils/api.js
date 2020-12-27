import authService from '../components/api-authorization/AuthorizeService'

const api = {
  get: async (path) => {
    const token = await authService.getAccessToken();
    const response = await fetch(path, {
      headers: !token ? {} : { 'Authorization': `Bearer ${token}` }
    });

    return response.json();
  },
  post: async (path, data) => {
    const token = await authService.getAccessToken();
    const response = await fetch(path, {
      method: 'POST',
      headers: !token ? {} : { 'Authorization': `Bearer ${token}`, 'Content-Type': 'application/json' },
      body: JSON.stringify(data),
    });

    return response.json();
  }
}

export default api;