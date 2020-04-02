import request from "@/utils/request";
import { TenantQueryParams, TenantCreateDto, TenantUpdateDto } from "./data";


export async function queryTenants(params?: TenantQueryParams): Promise<any> {
  return request('api/saas/tenants', {
    method: 'GET',
    params,
  });
}

export async function createTenant(data: TenantCreateDto): Promise<any> {
  return request('api/saas/tenants', {
    method: 'POST',
    data,
  });
}

export async function updateTenant(id:string,data: TenantUpdateDto): Promise<any> {
  return request(`api/saas/tenants/${id}`, {
    method: 'PUT',
    data,
  });
}

export async function deleteTenant(id:string): Promise<any> {
  return request(`api/saas/tenants/${id}`, {
    method: 'DELETE',
  });
}
