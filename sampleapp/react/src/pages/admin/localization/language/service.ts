import request from "@/utils/request";
import { CreateLanguageDto, UpdateLanguageDto } from "./data";

export async function queryLanguages(): Promise<any> {
  return request('api/language-management/languages', {
    method: 'GET',
  });
}
export async function createLanguage(data:CreateLanguageDto): Promise<any> {
  return request('api/language-management/languages', {
    method: 'POST',
    data
  });
}
export async function updateLanguage(input:{id:string,data:UpdateLanguageDto}): Promise<any> {
  return request(`api/language-management/languages/${input.id}`, {
    method: 'PUT',
    data:input.data
  });
}
export async function deleteLanguage(id:string): Promise<any> {
  return request(`api/language-management/languages/${id}`, {
    method: 'DELETE',
  });
}
