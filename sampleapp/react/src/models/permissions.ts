


import { Effect } from 'dva';
import { Reducer } from 'redux';
import { GetPermissionListResultDto } from '@/services/data';
import { getPermissions, updatePermissions } from '@/services/permission';


export interface PermissionModelState {
  permissions: GetPermissionListResultDto;
}

export interface PermissionModelType {
  namespace: 'permission';
  state: PermissionModelState;
  effects: {
    getPermission: Effect;
    updatePermission: Effect;
  };
  reducers: {
    savePermission: Reducer<PermissionModelState>;
  };
}

const RoleModel: PermissionModelType = {
  namespace: 'permission',

  state: {
    permissions: {
      entityDisplayName: '',
      groups: [],
    }
  },

  effects: {
    *getPermission({ payload }, { call, put }) {
      const response = yield call(getPermissions, payload);
      yield put({
        type: 'savePermission',
        payload: response,
      })
    },
    *updatePermission({ payload }, { call }) {
      yield call(updatePermissions, payload);
    },
  },
  reducers: {
    savePermission(state, { payload }) {
      return {
        ...state,
        permissions: payload
      };
    }
  },
};

export default RoleModel;
