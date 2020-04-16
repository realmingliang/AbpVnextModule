import request from "@/utils/request";
import { RoleQueryParams, IdentityRoleDto, IdentityRoleUpdateDto } from "./data";


export async function queryRoles(params?: RoleQueryParams): Promise<any> {
  return request('api/identity/roles', {
    method: 'GET',
    params,
  });
}
export async function deleteRole(id: string): Promise<any> {
  return request(`api/identity/roles/${id}`, {
    method: 'DELETE',
  });
}

export async function createRole(data:IdentityRoleDto): Promise<any> {
  return request("api/identity/roles", {
    method: 'POST',
    data,
  });
}

export async function updateRole(input:{id:string,data:IdentityRoleUpdateDto}): Promise<any> {
  return request(`api/identity/roles/${input.id}`, {
    method: 'PUT',
    data:input.data,
  });
}

export async function getRole(id: string): Promise<any> {
  return request(`api/identity/roles/${id}`, {
    method: 'GET',
  });
}
