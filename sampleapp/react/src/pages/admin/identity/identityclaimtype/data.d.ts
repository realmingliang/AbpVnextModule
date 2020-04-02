
import { PageRequestDto } from "@/services/data";


export interface IdentityClaimTypeDto{
  name:string;
  required:boolean;
  isStatic:boolean;
  regex:string;
  regexDescription:string;
  description:string;
  valueType:IdentityClaimValueType;
  valueTypeAsString:string;
  id:string;
}
export interface IdentityClaimTypeCreateDto{
  name:string;
  required:boolean;
  regex:string;
  regexDescription:string;
  description:string;
  valueType:IdentityClaimValueType;
}
export interface IdentityClaimTypeUpdateDto{
  name:string;
  required:boolean;
  regex:string;
  regexDescription:string;
  description:string;
  valueType:IdentityClaimValueType;
}

export interface ClaimTypeQueryParams extends PageRequestDto{
  filter?: string;
  sorting?: string;
}
export enum IdentityClaimValueType{
  String,
  Int,
  Boolean,
  DateTime
}
