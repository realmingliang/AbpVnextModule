import { PageRequestDto } from '../../../../services/data.d';


export interface SaasEditionDto {
  displayName:string;
  id:string;
}

export interface EditionQueryParams  extends PageRequestDto{
  filter?: string;
  sorting?: string;
}

export interface EditionCreateDto{
  displayName:string;
}

export interface EditionUpdateDto{
  displayName:string;
}
