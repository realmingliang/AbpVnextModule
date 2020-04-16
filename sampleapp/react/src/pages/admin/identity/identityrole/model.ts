import { Effect } from 'dva';
import { Reducer } from 'redux';
import { IdentityRoleDto } from './data';
import { queryRoles, deleteRole, getRole, createRole, updateRole } from './service';
import { PagedResultDto } from '@/services/data';

export interface IdentityRoleModelState {
  rolesResult?: PagedResultDto<IdentityRoleDto>;
  editRole?:IdentityRoleDto;
}

export interface IdentityRoleModelType {
  namespace: 'identityRole';
  state: IdentityRoleModelState;
  effects: {
    getRoles: Effect;
    getRole: Effect;
    updateRole:Effect;
    createRole:Effect;
    deleteRole: Effect;
  };
  reducers: {
    saveRoles: Reducer<IdentityRoleModelState>;
    saveEditRole: Reducer<IdentityRoleModelState>;
    handleDeleteRoles: Reducer<IdentityRoleModelState>;
    handleCreateRole: Reducer<IdentityRoleModelState>;
    handleUpdateRole: Reducer<IdentityRoleModelState>;
  };
}

const RoleModel: IdentityRoleModelType = {
  namespace: 'identityRole',

  state: {
    rolesResult: { totalCount: 0, items: [] },
    editRole:undefined
  },

  effects: {
    *getRoles({ payload }, { call, put }) {
      const response = yield call(queryRoles, payload);
      yield put({
        type: 'saveRoles',
        payload: response,
      });
    },
    *getRole({ payload }, { call, put }) {
      const response = yield call(getRole, payload);
      yield put({
        type: 'saveEditRole',
        payload: response,
      });
    },
    *deleteRole({ payload }, { call, put }) {
      yield call(deleteRole, payload);
      yield put({
        type: 'handleDeleteRoles',
        payload,
      });
    },
    *createRole({ payload }, { call, put }) {
      const response = yield call(createRole, payload);
      yield put({
        type: 'handleCreateRole',
        payload:response
      });
    },
    *updateRole({ payload }, { call, put }) {
      const response= yield call(updateRole, payload);
      yield put({
        type: 'handleUpdateRole',
        payload:response
      });
    },
  },

  reducers: {
    saveRoles(state, { payload }) {
      return {
        ...state,
        rolesResult: payload,
      };
    },
    saveEditRole(state, { payload }) {
      return {
        ...state,
        editRole: payload,
      };
    },
    handleDeleteRoles(state, { payload }) {
      return {
        ...state,
        rolesResult: {
          totalCount: state!.rolesResult!.totalCount - 1,
          items: state!.rolesResult!.items.filter((t) => t.id !== payload),
        },
      };
    },
    handleCreateRole(state, { payload }) {
      return {
        ...state,
        rolesResult: {
          totalCount: state!.rolesResult!.totalCount + 1,
          items: state!.rolesResult!.items.concat(payload),
        },
      };
    },
    handleUpdateRole(state, { payload }) {
      const oldRoles= state!.rolesResult!.items;
      const newRole = oldRoles.map(item=>{
        if(item.id === payload.id){
          return payload
        }
        return item;
      })
      return {
        ...state,
        rolesResult: {
          totalCount: state!.rolesResult!.totalCount,
          items: newRole,
        },
      };
    },
  },
};

export default RoleModel;
