import request from "@/utils/request";
import { OrganizationUnitCreateInput,CreateOrUpdateOrganizationUnitInput, GetOrganizationunitUsersInput, GetOrganizationunitRolesInput, MoveOrganizationUnitInput, RemoveRoleFromOrganizationUnit, RemoveUserFromOrganizationUnit, RolesToOrganizationUnitInput, UsersToOrganizationUnitInput, FindOrganizationUnitUsersInput, FindOrganizationUnitRolesInput } from "./data";




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
export async function updateOrganizationUnit(id:string,data:CreateOrUpdateOrganizationUnitInput): Promise<any> {
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
export async function moveOrganization(id:string,data:MoveOrganizationUnitInput): Promise<any> {
  return request(`api/identity/organization-unit/${id}/move`,{
    method:'PUT',
    data
  });
}

export async function removeUserFromOrganizationUnit(id:string,params:RemoveUserFromOrganizationUnit): Promise<any> {
  return request(`api/identity/organization-unit/${id}/remove-user-from-organizationunit`,{
    method:'DELETE',
    params
  });
}
export async function removeRoleFromOrganizationUnit(id:string,params:RemoveRoleFromOrganizationUnit): Promise<any> {
  return request(`api/identity/organization-unit/${id}/remove-role-from-organizationunit`,{
    method:'DELETE',
    params
  });
}

export async function addUsersToOrganizationUnit(id:string,data: UsersToOrganizationUnitInput) {
  return request(`api/identity/organization-unit/${id}/add-users-to-organizationunit`, {
    method: 'POST',
    data,
  });
}
export async function findUsers(params: FindOrganizationUnitUsersInput) {
  return request('/api/identity/organization-unit/find-users', {
    method: 'GET',
    params,
  });
}

export async function findRoles(params: FindOrganizationUnitRolesInput) {
  return request('/api/identity/organization-unit/find-roles', {
    method: 'GET',
    params,
  });
}

export async function addRolesToOrganizationUnit(id:string,data: RolesToOrganizationUnitInput) {
  return request(`api/identity/organization-unit/${id}/add-roles-to-organizationunit`, {
    method: 'POST',
    data,
  });
}
export async function getOrganizationunitUsers(id:string,params:GetOrganizationunitUsersInput): Promise<any> {
  return request(`api/identity/organization-unit/${id}/get-organizationunit-users`,{
    method:'GET',
    params
  });
}
export async function getOrganizationunitRoles(id:string,params:GetOrganizationunitRolesInput): Promise<any> {
  return request(`api/identity/organization-unit/${id}/get-organizationunit-roles`,{
    method:'GET',
    params
  });
}
