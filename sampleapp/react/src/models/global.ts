
import { Reducer } from 'redux';
import { Effect, Subscription } from 'dva';
import _ from "lodash";
import { NoticeIconData } from '@/components/NoticeIcon';
import { getConfiguration } from '@/services/global';
import { setAuthority } from '@/utils/authority';
import { InitThemeSettings } from '@/utils/utils';
import { ConnectState } from './connect.d';


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

export interface NoticeItem extends NoticeIconData {
  id: string;
  type: string;
  status: string;
}

export interface GlobalModelState {
  applicationConfiguration?: ApplicationConfiguration.Response;
  collapsed: boolean;
  notices: NoticeItem[];
}

export interface GlobalModelType {
  namespace: 'global';
  state: GlobalModelState;
  effects: {
    getApplicationConfiguration: Effect;
    clearNotices: Effect;
    changeNoticeReadState: Effect;
  };
  reducers: {
    changeLayoutCollapsed: Reducer<GlobalModelState>;
    saveConfigiration: Reducer<GlobalModelState>;
    saveNotices: Reducer<GlobalModelState>;
    saveClearedNotices: Reducer<GlobalModelState>;
  };
  subscriptions: { setup: Subscription };
}

const GlobalModel: GlobalModelType = {
  namespace: 'global',

  state: {
    collapsed: false,
    applicationConfiguration: undefined,
    notices: [],
  },

  effects: {
    *getApplicationConfiguration(_, { call, put }) {
      const data = yield call(getConfiguration);
      yield put({
        type: 'saveConfigiration',
        payload: data,
      });
      const themeSettings = InitThemeSettings(data.setting.values);
      yield put({
        type: 'settings/saveAllThemeSettings',
        payload: themeSettings,
      });
    },
    *clearNotices({ payload }, { put, select }) {
      yield put({
        type: 'saveClearedNotices',
        payload,
      });
      const count: number = yield select((state: ConnectState) => state.global.notices.length);
      const unreadCount: number = yield select(
        (state: ConnectState) => state.global.notices.filter(item => !item.read).length,
      );
      yield put({
        type: 'user/changeNotifyCount',
        payload: {
          totalCount: count,
          unreadCount,
        },
      });
    },
    *changeNoticeReadState({ payload }, { put, select }) {
      const notices: NoticeItem[] = yield select((state: ConnectState) =>
        state.global.notices.map(item => {
          const notice = { ...item };
          if (notice.id === payload) {
            notice.read = true;
          }
          return notice;
        }),
      );

      yield put({
        type: 'saveNotices',
        payload: notices,
      });

      yield put({
        type: 'user/changeNotifyCount',
        payload: {
          totalCount: notices.length,
          unreadCount: notices.filter(item => !item.read).length,
        },
      });
    },
  },

  reducers: {
    saveConfigiration(state = { notices: [], collapsed: true }, { payload }): GlobalModelState {
      setAuthority(_.keys(payload.auth.grantedPolicies));
      return {
        ...state,
        applicationConfiguration: payload,
      };
    },
    changeLayoutCollapsed(state = { notices: [], collapsed: true }, { payload }): GlobalModelState {
      return {
        ...state,
        collapsed: payload,
      };
    },
    saveNotices(state, { payload }): GlobalModelState {
      return {
        collapsed: false,
        ...state,
        notices: payload,
      };
    },
    saveClearedNotices(state = { notices: [], collapsed: true }, { payload }): GlobalModelState {
      return {
        collapsed: false,
        ...state,
        notices: state.notices.filter((item): boolean => item.type !== payload),
      };
    },
  },

  subscriptions: {
    setup({ history }): void {
      // Subscribe history(url) change, trigger `load` action if pathname is `/`
      history.listen(({ pathname, search }): void => {
        if (typeof window.ga !== 'undefined') {
          window.ga('send', 'pageview', pathname + search);
        }
      });
    },
  },
};

export default GlobalModel;
