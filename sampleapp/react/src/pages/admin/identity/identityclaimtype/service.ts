import request from "@/utils/request";
import { ClaimTypeQueryParams, IdentityClaimTypeCreateDto, IdentityClaimTypeUpdateDto } from "./data";



export async function queryClaimTypes(params?: ClaimTypeQueryParams): Promise<any> {
  return request('api/identity/claim-types', {
    method: 'GET',
    params,
  });
}

export async function createClaimType(data: IdentityClaimTypeCreateDto): Promise<any> {
  return request('api/identity/claim-types', {
    method: 'POST',
    data,
  });
}

export async function updateClaimType(id:string,data: IdentityClaimTypeUpdateDto): Promise<any> {
  return request(`api/identity/claim-types/${id}`, {
    method: 'PUT',
    data,
  });
}

export async function deleteClaimType(id:string): Promise<any> {
  return request(`api/identity/claim-types/${id}`, {
    method: 'DELETE',
  });
}

