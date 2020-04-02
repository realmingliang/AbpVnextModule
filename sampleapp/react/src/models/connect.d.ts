
import { MenuDataItem } from '@ant-design/pro-layout';
import { GlobalModelState } from './global';
import { StateType } from './login';
import { AccountSettingsModalState } from "../pages/common/AccountSettings/model";
import { AuditLogModalState } from "../pages/admin/auditlog/model";
import { IdentityUserModelState } from "../pages/admin/identity/identityuser/model";
import { PermissionModelState } from './permissions';
import { SettingModelState } from './setting';

export { GlobalModelState, SettingModelState };

export interface Loading {
  global: boolean;
  effects: { [key: string]: boolean | undefined };
  models: {
    global?: boolean;
    menu?: boolean;
    setting?: boolean;
    role?: boolean;
    login?: boolean;
    accountSettings?:boolean;
    auditLog?:boolean;
    identityUser?:boolean;
    permission?:boolean;
  };
}

export interface ConnectState {
  global: GlobalModelState;
  loading: Loading;
  settings: SettingModelState;
  role: RoleModelState;
  login: StateType;
  accountSettings:AccountSettingsModalState;
  auditLog:AuditLogModalState;
  identityUser:IdentityUserModelState;
  permission:PermissionModelState;
}

export interface Route extends MenuDataItem {
  routes?: Route[];
}
/**
 * @type T: Params matched in dynamic routing
 */
export interface ConnectProps<T = {}> extends Partial<RouterTypes<Route, T>> {
  dispatch?: Dispatch<AnyAction>;
}
