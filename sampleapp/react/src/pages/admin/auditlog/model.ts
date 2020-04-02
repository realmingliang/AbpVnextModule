import { AnyAction, Reducer } from 'redux';
import { EffectsCommandMap } from 'dva';
import { getAuditLog } from './service';
import { AuditLogDto } from './data.d';


export interface AuditLogModalState {
  auditlog: AuditLogDto;
}

export type Effect = (
  action: AnyAction,
  effects: EffectsCommandMap & { select: <T>(func: (state: AuditLogModalState) => T) => T },
) => void;

export interface ModelType {
  namespace: string;
  state: AuditLogModalState;
  effects: {
    getAuditLog: Effect;

  };
  reducers: {
    saveAuditlog: Reducer<AuditLogModalState>;
  };
}

const Model: ModelType = {
  namespace: 'auditLog',

  state: {
    auditlog: {}
  },

  effects: {
    *getAuditLog({ payload }, { call, put }) {
      const response = yield call(getAuditLog, payload);
      yield put({
        type: 'saveAuditlog',
        payload: response,
      })
    },
  },

  reducers: {
    saveAuditlog(state, action) {
      return {
        ...state,
        auditlog: action.payload
      }
    },
  },
};

export default Model;
