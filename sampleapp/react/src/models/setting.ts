import { Reducer } from 'redux';
import { ThemeSettingsDto } from '@/services/data';
import { Effect } from 'dva';
import { getAllThemeSettings, updateAllThemeSettings } from '@/services/settings';

export interface SettingModelState {
  themeSettings: ThemeSettingsDto;
}
export interface SettingModelType {
  namespace: 'settings';
  state: SettingModelState;
  effects: {
    getAllThemeSettings: Effect;
    updateAllThemeSettings: Effect;
  };
  reducers: {
    saveAllThemeSettings: Reducer<SettingModelState>;
  };
}


const SettingModel: SettingModelType = {
  namespace: 'settings',
  state: {
    themeSettings: {
      navTheme: 'dark',
      // 拂晓蓝
      primaryColor: '#1890ff',
      layout: 'sidemenu',
      contentWidth: 'Fixed',
      fixedHeader: false,
      autoHideHeader: false,
      fixSiderbar: false,
      colorWeak: false,
      title: "Antd Pro"
    }
  },
  effects: {
    *getAllThemeSettings(_, { call, put }) {
      const response = yield call(getAllThemeSettings);
      yield put({
        type: 'saveAllThemeSettings',
        payload: response,
      })
    },
    *updateAllThemeSettings({ payload }, { call }) {
      yield call(updateAllThemeSettings, payload);
      window.location.reload();
    }
  },
  reducers: {
    saveAllThemeSettings(state, { payload }) {
      return {
        ...state,
        themeSettings: payload
      };
    }
  },
};
export default SettingModel;
