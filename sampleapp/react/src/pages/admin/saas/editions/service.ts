import request from "@/utils/request";
import { EditionQueryParams, EditionCreateDto, EditionUpdateDto } from "./data";




export async function queryEditions(params?: EditionQueryParams): Promise<any> {
  return request('api/saas/editions', {
    method: 'GET',
    params,
  });
}
export async function createEdition(data: EditionCreateDto): Promise<any> {
  return request('api/saas/editions', {
    method: 'POST',
    data,
  });
}

export async function updateEdition(id:string,data: EditionUpdateDto): Promise<any> {
  return request(`api/saas/editions/${id}`, {
    method: 'PUT',
    data,
  });
}

export async function deleteEdition(id:string): Promise<any> {
  return request(`api/saas/editions/${id}`, {
    method: 'DELETE',
  });
}
