import request from "@/utils/request";
import { GetLanguageTextInput, LanguageTextDto, UpdateLanguageTextInput } from "./data";
import { PagedResultDto } from "@/services/data";


export async function queryLanguageTexts(params:GetLanguageTextInput): Promise<PagedResultDto<LanguageTextDto>> {
  return request('api/language-management/language-texts', {
    method: 'GET',
    params,
  });
}
export async function updateLanguageText(input:{params:UpdateLanguageTextInput,value:string}): Promise<any> {
  return request(`api/language-management/language-texts/${input.params.resourceName}/${input.params.cultureName}/${input.params.name}`, {
    method: 'PUT',
    params:{
      value:input.value
    }
  });
}
