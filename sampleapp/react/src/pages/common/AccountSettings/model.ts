import { AnyAction, Reducer } from 'redux';
import { EffectsCommandMap } from 'dva';
import { changePassword, getMyProfile, updateMyProfile } from './service';
import { ProfileDto } from './data';


export interface AccountSettingsModalState {
  myProfile: ProfileDto
}

export type Effect = (
  action: AnyAction,
  effects: EffectsCommandMap & { select: <T>(func: (state: AccountSettingsModalState) => T) => T },
) => void;

export interface ModelType {
  namespace: string;
  state: AccountSettingsModalState;
  effects: {
    changePassword: Effect;
    getMyProfile: Effect;
    updateMyProfile: Effect;
  };
  reducers: {
    saveMyProfile: Reducer<AccountSettingsModalState>;
  };
}

const Model: ModelType = {
  namespace: 'accountSettings',

  state: {
    myProfile: {}
  },

  effects: {
    *changePassword({ payload }, { call }) {
      yield call(changePassword, payload);
    },
    *getMyProfile(_, { call, put }) {
      const response = yield call(getMyProfile);
      yield put({
        type: 'saveMyProfile',
        payload: response,
      })
    },
    *updateMyProfile({ payload }, { call }) {
      yield call(updateMyProfile, payload);
    },
  },

  reducers: {
    saveMyProfile(state, action) {
      return {
        ...state,
        myProfile: action.payload
      }
    },
  },
};

export default Model;
