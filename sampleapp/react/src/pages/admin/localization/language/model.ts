import { Effect, Reducer } from 'umi';
import { LanguageDto } from './data.d';
import { createLanguage, updateLanguage, queryLanguages, deleteLanguage } from './service';

export interface ModalState {
  Languages: LanguageDto[];
}

export interface ModelType {
  namespace: string;
  state: ModalState;
  effects: {
    getLanguages: Effect;
    createLanguage: Effect;
    updateLanguage: Effect;
    deleteLanguage: Effect;
  };
  reducers: {
    saveLanguages: Reducer<ModalState>;
    handleAddLanguages: Reducer<ModalState>;
    handleDeleteLanguage: Reducer<ModalState>;
    handleUpdateLanguage: Reducer<ModalState>;
  };
}

const Model: ModelType = {
  namespace: 'language',
  state: {
    Languages: [],
  },
  effects: {
    *getLanguages(_, { call, put }) {
      const response = yield call(queryLanguages);
      yield put({
        type: 'saveLanguages',
        payload: response.items,
      });
    },
    *createLanguage({ payload }, { call, put }) {
      const response = yield call(createLanguage, payload);
      yield put({
        type: 'handleAddLanguages',
        payload: response,
      });
    },
    *deleteLanguage({ payload }, { call, put }) {
      yield call(deleteLanguage, payload);
      yield put({
        type: 'handleDeleteLanguages',
        payload,
      });
    },
    *updateLanguage({ payload }, { call, put }) {
      const response = yield call(updateLanguage, payload);
      yield put({
        type: 'handleDeleteLanguages',
        payload: response,
      });
    },
  },
  reducers: {
    saveLanguages(state, { payload }) {
      return {
        Languages: payload,
      };
    },
    handleAddLanguages(state, { payload }) {
      return {
        Languages: state!.Languages.concat(payload),
      };
    },
    handleDeleteLanguage(state, { payload }) {
      return {
        Languages: state!.Languages.filter((t) => t.id !== payload),
      };
    },
    handleUpdateLanguage(state, { payload }) {
      const oldLanguages = state!.Languages!;
      const newLanguages = oldLanguages.map((item) => {
        if (item.id === payload.id) {
          return payload;
        }
        return item;
      });
      return {
        ...state,
        Languages: newLanguages,
      };
    },
  },
};

export default Model;
