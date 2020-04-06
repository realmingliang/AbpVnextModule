import request from "@/utils/request";
import { OrganizationUnitCreateInput,OrganizationUnitUpdateInput } from "./data";




export async function getOrganizationUnits(): Promise<any> {
  return request("api/identity/organization-unit",{
    method:'GET',
  });
}


export async function createOrganizationUnit(data:OrganizationUnitCreateInput): Promise<any> {
  return request("api/identity/organization-unit",{
    method:'POST',
    data,
  });
}
export async function updateOrganizationUnit(id:string,data:OrganizationUnitUpdateInput): Promise<any> {
  return request(`api/identity/organization-unit/${id}`,{
    method:'PUT',
    data,
  });
}

export async function deleteOrganizationUnit(id:string): Promise<any> {
  return request(`api/identity/organization-unit/${id}`,{
    method:'DELETE',
  });
}
