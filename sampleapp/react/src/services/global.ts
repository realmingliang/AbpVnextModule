import request from "@/utils/request";
import { ApplicationConfiguration } from "./data";

export async function getConfiguration():Promise<ApplicationConfiguration.Response> {
  return request('api/abp/application-configuration', {
    method: 'GET',
  });
}


export async function abpApiDefinition() {
  return request('api/abp/api-definition', {
    method: 'GET',
  });
}
