import request from "@/utils/request";
import { ThemeSettingsDto } from "./data";


export async function getAllThemeSettings(): Promise<any> {
  return request('api/themes/settings', {
    method: 'GET',
  });
}
export async function updateAllThemeSettings(data:ThemeSettingsDto): Promise<any> {
  return request('api/themes/settings', {
    method: 'POST',
    data,
  });
}
