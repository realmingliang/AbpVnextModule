
import { Effect } from 'dva';
import { Reducer } from 'redux';
import { IdentityUserCreateOrUpdateDto } from './data';
import { getUserRoles, createUser, updateUser, getUser, deleteUser } from './service';
import { IdentityRoleDto } from '../identityrole/data';
import { queryRoles } from '../identityrole/service';



export interface IdentityUserModelState {
  allRoles: IdentityRoleDto[];
  createOrUpdateUser?: IdentityUserCreateOrUpdateDto;
}

export interface IdentityUserModelType {
  namespace: 'identityUser';
  state: IdentityUserModelState;
  effects: {
    getRoles: Effect;
    getUser: Effect;
    createUser: Effect;
    updateUser: Effect;
    deleteUser: Effect;
  };
  reducers: {
    saveRoles: Reducer<IdentityUserModelState>;
    saveCreateOrUpdateUser: Reducer<IdentityUserModelState>;
  };
}

const RoleModel: IdentityUserModelType = {
  namespace: 'identityUser',

  state: {
    allRoles: [],
    createOrUpdateUser: {},
  },

  effects: {
    *getRoles({ payload }, { call, put }) {
      const response = yield call(queryRoles, payload);
      yield put({
        type: 'saveRoles',
        payload: response.items,
      })
    },
    *deleteUser({ payload }, { call }) {
      yield call(deleteUser, payload);
    },
    *getUser({ payload }, { call, put }) {
      let response = {};
      let currentUserRoleNames = [];
      if (payload !== null) {
        response = yield call(getUser, payload);
        const userRolesResponse = yield call(getUserRoles, payload);
        currentUserRoleNames = userRolesResponse.items?.map((item: { name: any; }) => (item.name));
      }
      yield put({
        type: 'saveCreateOrUpdateUser',
        payload: {
          ...response,
          roleNames: currentUserRoleNames,
        },
      })
    },
    *createUser({ payload }, { call }) {
      yield call(createUser, payload);
    },
    *updateUser({ payload }, { call }) {
      yield call(updateUser, payload);
    },
  },
  reducers: {
    saveRoles(state = { allRoles: [] }, { payload }) {
      return {
        ...state,
        allRoles: payload
      };
    },
    saveCreateOrUpdateUser(state = { allRoles: [] }, { payload }) {
      return {
        ...state,
        allRoles: state!.allRoles,
        createOrUpdateUser: payload
      };
    },
  },
};

export default RoleModel;
