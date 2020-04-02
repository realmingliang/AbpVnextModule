
import { Effect } from 'dva';
import { Reducer } from 'redux';
import { IdentityRoleDto } from './data';
import { queryRoles } from './service';


export interface IdentityRoleModelState {
  roles: IdentityRoleDto[]
}

export interface IdentityRoleModelType {
  namespace: 'identityRole';
  state: IdentityRoleModelState;
  effects: {
    getRoles: Effect;
  };
  reducers: {
    saveRoles: Reducer<IdentityRoleModelState>;
  };
}

const RoleModel: IdentityRoleModelType = {
  namespace: 'identityRole',

  state: {
    roles: []
  },

  effects: {
    *getRoles({ payload }, { call, put }) {
      const response = yield call(queryRoles, payload);
      yield put({
        type: 'saveRoles',
        payload: response.items,
      })
    }

  },

  reducers: {
    saveRoles(state, { payload }) {
      return {
        ...state,
        roles: payload
      };
    }
  },
};

export default RoleModel;
