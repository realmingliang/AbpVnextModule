import { Reducer } from 'redux';
import { Effect } from 'dva';
import { stringify } from 'querystring';
import { history } from 'umi';
import { login, logout } from '@/services/login';
import { getPageQuery } from '@/utils/utils';
import AppConsts from "../utils/appconst";
import Store from "../utils/store";

export interface StateType {
  status?: 'ok' | 'error';
  currentAuthority?: 'user' | 'guest' | 'admin';
}


export interface LoginModelType {
  namespace: string;
  state: StateType;
  effects: {
    login: Effect;
    logout: Effect;
  };
  reducers: {
    changeLoginStatus: Reducer<StateType>;
  };
}

const Model: LoginModelType = {
  namespace: 'login',

  state: {
    status: undefined,
  },

  effects: {
    *login({ payload,callback }, { call, put }) {
      const formData = new FormData();
      formData.append('password', payload.password);
      formData.append('username', payload.userNameOrEmailAddress);
      formData.append('grant_type', AppConsts.oAuthConfig.grant_type!);
      formData.append('client_id', AppConsts.oAuthConfig.client_id!);
      formData.append('client_secret', AppConsts.oAuthConfig.client_secret!);
      formData.append('scope', AppConsts.oAuthConfig.scope!);
      const response = yield call(login, formData);
      yield put({
        type: 'changeLoginStatus',
        payload: response,
      });

      // Login successfully
      if (response.access_token !== undefined) {
        Store.SetToken(response.access_token);
        callback();
        const urlParams = new URL(window.location.href);
        const params = getPageQuery();
        let { redirect } = params as { redirect: string };
        if (redirect) {
          const redirectUrlParams = new URL(redirect);
          if (redirectUrlParams.origin === urlParams.origin) {
            redirect = redirect.substr(urlParams.origin.length);
            if (redirect.match(/^\/.*#/)) {
              redirect = redirect.substr(redirect.indexOf('#') + 1);
            }
          } else {
            window.location.href = '/';
            return;
          }
        }
        history.replace(redirect || '/');
      }
    },
    *logout(_, { call }) {
      const { redirect } = getPageQuery();
      Store.Clear();
      // Note: There may be security issues, please note
      if (window.location.pathname !== '/account/login' && !redirect) {
        yield call(logout);

        history.replace({
          pathname: '/account/login',
          search: stringify({
            redirect: window.location.href,
          }),
        });
      }
    }
  },

  reducers: {
    changeLoginStatus(state, { payload }) {
      return {
        ...state,
        status: payload.status,
        type: payload.type,
      };
    },
  },
};

export default Model;
