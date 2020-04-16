

import { Effect, Reducer } from 'umi';
import { LanguageTextDto } from './data.d';
import {  updateLanguageText, queryLanguageTexts } from './service';
import { PagedResultDto } from '@/services/data';

export interface ModalState {
  LanguageTexts: PagedResultDto<LanguageTextDto>;
}

export interface ModelType {
  namespace: string;
  state: ModalState;
  effects: {
    getLanguageTexts: Effect;
    updateLanguageText: Effect;
  };
  reducers: {
    saveLanguageTexts: Reducer<ModalState>;
  };
}

const Model: ModelType = {
  namespace: 'languageText',
  state: {
    LanguageTexts: {totalCount:0,items:[]},
  },
  effects: {
    *getLanguageTexts({payload}, { call, put }) {
      const response = yield call(queryLanguageTexts,payload);
      yield put({
        type: 'saveLanguageTexts',
        payload: response,
      });
    },
    *updateLanguageText({ payload }, { call, put }) {
      yield call(updateLanguageText,payload);
    },
  },
  reducers: {
    saveLanguageTexts(state, { payload }) {
      return {
        LanguageTexts: payload,
      };
    },
  },
};

export default Model;
