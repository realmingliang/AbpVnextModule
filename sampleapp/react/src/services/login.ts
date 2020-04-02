import request from '@/utils/request';

export interface LoginParamsType {
  userNameOrEmailAddress: string;
  password: string;
}

export async function login(params: LoginParamsType) {
  return request('connect/token', {
    method: 'POST',
    data: params,
  });
}

export async function logout() {
  return request(`api/account/logout`);
}
