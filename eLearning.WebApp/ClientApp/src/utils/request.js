import authService from '../components/api-authorization/AuthorizeService'

async function request (path) {
  const token = await authService.getAccessToken();
  const response = await fetch(path, {
    headers: !token ? {} : { 'Authorization': `Bearer ${token}` }
  });

  return response.json();
}

export default request;