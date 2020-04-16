
import { Effect } from 'dva';
import { Reducer } from 'redux';
import { IdentityUserCreateOrUpdateDto } from './data';
import { getUserRoles, createUser, updateUser, getUser, deleteUser, queryUsers } from './service';
import { IdentityRoleDto } from '../identityrole/data';
import { queryRoles } from '../identityrole/service';
import { PagedResultDto } from '@/services/data';
import { IdentityUserDto } from './data.d';



export interface IdentityUserModelState {
  usersResult:PagedResultDto<IdentityUserDto>
  allRoles: IdentityRoleDto[];
  createOrUpdateUser?: IdentityUserCreateOrUpdateDto;
}

export interface IdentityUserModelType {
  namespace: 'identityUser';
  state: IdentityUserModelState;
  effects: {
    getRoles: Effect;
    getUsers:Effect;
    getUser: Effect;
    createUser: Effect;
    updateUser: Effect;
    deleteUser: Effect;
  };
  reducers: {
    saveUsers: Reducer<IdentityUserModelState>;
    saveRoles: Reducer<IdentityUserModelState>;
    saveCreateOrUpdateUser: Reducer<IdentityUserModelState>;
  };
}

const RoleModel: IdentityUserModelType = {
  namespace: 'identityUser',

  state: {
    usersResult:{totalCount:0,items:[]},
    allRoles: [],
    createOrUpdateUser: {},
  },

  effects: {
    *getUsers({ payload }, { call, put }) {
      const response = yield call(queryUsers, payload);
      yield put({
        type: 'saveUsers',
        payload: response,
      })
    },
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
    saveUsers(state, { payload }) {
      return {
        ...state,
        usersResult:payload,
        allRoles: []
      };
    },
    saveRoles(state, { payload }) {
      return {
        ...state,
        usersResult:state!.usersResult,
        allRoles: payload
      };
    },
    saveCreateOrUpdateUser(state, { payload }) {
      return {
        ...state,
        usersResult:state!.usersResult,
        allRoles: state!.allRoles,
        createOrUpdateUser: payload
      };
    },
  },
};

export default RoleModel;
