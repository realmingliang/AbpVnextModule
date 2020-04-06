import { MenuTheme } from 'antd/es/menu/MenuContext';

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
export interface PageRequestDto {
  skipCount?: nunber;
  maxResultCount?: number;
}
export namespace ApplicationConfiguration {
  export interface Response {
    localization: Localization;
    auth: Auth;
    setting: Value;
    currentUser: CurrentUser;
    features: Value;
    multiTenancy:MultiTenancy;
    currentTenant:CurrentTenant;
  }

  export interface Localization {
    values: LocalizationValue;
    languages: Language[];
  }

  export interface LocalizationValue {
    [key: string]: { [key: string]: string };
  }

  export interface Language {
    cultureName: string;
    uiCultureName: string;
    displayName: string;
    flagIcon: string;
  }

  export interface Auth {
    policies: Policy;
    grantedPolicies: Policy;
  }

  export interface Policy {
    [key: string]: boolean;
  }

  export interface Value {
    values: Dictionary<string>;
  }

  export interface Dictionary<T = any> {
    [key: string]: T;
  }
  export interface MultiTenancy{
    isEnabled:boolean;
  }
  export interface CurrentUser {
    isAuthenticated: boolean;
    id: string;
    tenantId: string;
    userName: string;
  }
  export interface CurrentTenant {
    id: string;
    name: string;
    isAvailable: boolean;
  }
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
