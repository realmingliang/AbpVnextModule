


import request from '@/utils/request';
import { IdentityUserCreateOrUpdateDto, UserQueryParams } from './data';

export async function queryUsers(params?: UserQueryParams): Promise<any> {
  return request('api/identity/users', {
    method: 'GET',
    params,
  });
}
export async function getUser(id:string): Promise<any> {
  return request(`api/identity/users/${id}`);
}
export async function getUserRoles(id:string): Promise<any> {
  return request(`api/identity/users/${id}/roles`);
}
export async function createUser(data:IdentityUserCreateOrUpdateDto): Promise<any> {
  return request("api/identity/users",{
    method:'POST',
    data,
  });
}
export async function updateUser(data:IdentityUserCreateOrUpdateDto): Promise<any> {
  return request(`api/identity/users/${data.id}`,{
    method:'PUT',
    data,
  });
}
export async function deleteUser(id:string): Promise<any> {
  return request(`api/identity/users/${id}`,{
    method:'DELETE',
  });
}

