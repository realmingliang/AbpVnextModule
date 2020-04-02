import request from "@/utils/request";

export async function getTenantByName(name:string) {
  return request(`api/abp/multi-tenancy/tenants/by-name/${name}`, {
    method: 'GET',
  });
}
