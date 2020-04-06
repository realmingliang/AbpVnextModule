

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
