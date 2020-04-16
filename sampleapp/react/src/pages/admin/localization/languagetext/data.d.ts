
import { PageRequestDto } from '@/services/data';


export interface LanguageTextDto{
  resourceName: string,
  cultureName: string,
  baseCultureName: string,
  baseValue: string,
  name: string,
  value: string
}
export interface GetLanguageTextInput extends PageRequestDto{
  filter?:string;
  resourceName?:string;
  baseCultureName?:string;
  targetCultureName?:string;
  getOnlyEmptyValues?:boolean;
  sorting?:string;
}
export interface UpdateLanguageTextInput{
  resourceName:string;
  cultureName:string;
  name:string;
}
