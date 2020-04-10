import { utils } from 'umi';
import { join } from 'path';

export default function(util: typeof utils) {
  return `\
import React, { useMemo } from 'react';
import { useModel } from '../core/umiExports';
import settingFactory from '../../setting';
import SettingContext, { SettingInstance } from './context';


interface Props {
  children: React.ReactNode;
}

const SettingProvider: React.FC<Props> = props => {
  if (typeof useModel !== 'function') {
    throw new Error('[plugin-setting]: useModel is not a function, @umijs/plugin-initial-state is needed.')
  }

  const { children } = props;
  const { initialState } = useModel('@@initialState');

  const setting: SettingInstance = useMemo(() => settingFactory(initialState as any), [initialState]);

  if (process.env.NODE_ENV === 'development' && (setting === undefined || setting === null)) {
    console.warn('[plugin-setting]: the setting instance created by setting.ts(js) is nullish, maybe you need check it.');
  }

  return React.createElement(
    SettingContext.Provider,
    { value: setting },
    children,
  );
};

export default SettingProvider;
`;
}
