import { Dictionary } from 'lodash';

export interface LanguageDto {
  cultureName: string;
  uiCultureName: string;
  displayName: string;
  flagIcon: string;
  isEnabled: true;
  isDefaultLanguage: true;
  creationTime: Date;
  creatorId: string;
  id: string;
  extraProperties: Dictionary<string>;
}

export interface CreateLanguageDto{
  cultureName: string;
  uiCultureName: string;
  displayName: string;
  flagIcon: string;
  isEnabled: true;
  extraProperties: Dictionary<string>;
}
export interface UpdateLanguageDto{
  displayName: string;
  flagIcon: string;
}
