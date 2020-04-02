import request from "@/utils/request";




export async function getOrganizationUnits(): Promise<any> {
  return request("api/identity/organization-unit",{
    method:'GET',
  });
}
