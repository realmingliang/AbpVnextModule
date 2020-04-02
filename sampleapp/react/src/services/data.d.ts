import { MenuTheme } from 'antd/es/menu/MenuContext';

export interface PageRequestDto {
  skipCount?: nunber;
  maxResultCount?: number;
}
export interface GetPermissionsInput {
  providerName: string;
  providerKey: string;
}
export interface UpdatePermissionsInput {
  providerName: string;
  providerKey: string;
  permissions: UpdatePermissionDto[];
}
export interface UpdatePermissionDto {
  name: string;
  isGranted: boolean;
}
export interface ThemeSettingsDto {
  navTheme: MenuTheme | 'realDark' | undefined;
  primaryColor: string;
  layout: 'sidemenu' | 'topmenu';
  contentWidth: 'Fluid' | 'Fixed';
  fixedHeader: boolean;
  autoHideHeader: boolean;
  fixSiderbar: boolean;
  colorWeak: boolean;
  title: string
}
export interface GetPermissionListResultDto {
  entityDisplayName: string;
  groups: PermissionGroupDto[];
}

export interface PermissionGroupDto {
  name: string;
  displayName: string;
  permissions: PermissionGrantInfoDto[];
}
export interface PermissionGrantInfoDto {
  name: string;
  displayName: string;
  parentName: string;
  isGranted: boolean;
  allowedProviders: string[];
  grantedProviders: ProviderInfoDto[];
}
export interface ProviderInfoDto {
  providerName: string;
  providerKey: string;
}
