
import { PageRequestDto } from '@/services/data';

export interface OrganizationUnitDto{
  parentId:string;
  code:string;
  displayName:string;
  memberCount:number;
  roleCount:number;
  id:string;
}

export interface OrganizationUnitCreateInput{
  displayName:string;
  parentId?:string;
}
export interface CreateOrUpdateOrganizationUnitInput{
  id?:string;
  parentId:string|null;
  displayName:string;
}

export interface GetOrganizationunitUsersInput extends PageRequestDto{

}

export interface GetOrganizationunitRolesInput extends PageRequestDto{

}
export interface RemoveUserFromOrganizationUnit{
  userId:string;
}
export interface RemoveRoleFromOrganizationUnit{
  roleId:string;
}
export interface MoveOrganizationUnitInput{
  newParentId:string;
}

export interface UsersToOrganizationUnitInput{
  userIds:string[];
}
export interface RolesToOrganizationUnitInput{
  roleIds:string[];
}
export interface FindOrganizationUnitRolesInput extends PagedRequestDto{
  organizationUnitId:number|null;
  filter:string;
}
export interface FindOrganizationUnitUsersInput extends PagedRequestDto{
  filter:string;
  organizationUnitId:number|null;
}
