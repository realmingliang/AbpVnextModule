import { PageRequestDto } from "@/services/data";

export interface IdentityUserDto {
  tenantId: string;
  userName: string;
  name: string;
  surname: string;
  email: string;
  emailConfirmed: boolean;
  phoneNumber: string;
  phoneNumberConfirmed: boolean;
  twoFactorEnabled: boolean;
  lockoutEnabled: boolean;
  lockoutEnd: string;
  concurrencyStamp: string;
  isDeleted: boolean;
  deleterId: number;
  deletionTime: Date;
  lastModificationTime: Date;
  lastModifierId: Date;
  creationTime: Date;
  creatorId: string;
  id:string
}

export interface UserQueryParams extends PageRequestDto {
  filter?: string;
  sorting?: string;
}

export interface IdentityUserCreateOrUpdateDto{
  id?:string;
  password?:string;
  userName?:string;
  concurrencyStamp?:string;
  name?:string;
  surname?:string;
  email?:string;
  phoneNumber?:string;
  twoFactorEnabled?:boolean;
  lockoutEnabled?:boolean;
  roleNames?:string[]
}
