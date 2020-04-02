import request from "@/utils/request";

export async function getConfiguration() {
  return request('api/abp/application-configuration', {
    method: 'GET',
  });
}


export async function abpApiDefinition() {
  return request('api/abp/api-definition', {
    method: 'GET',
  });
}
