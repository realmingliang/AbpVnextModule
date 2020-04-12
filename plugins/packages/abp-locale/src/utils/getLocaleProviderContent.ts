import { utils } from 'umi';
import { join } from 'path';

export default function(util: typeof utils) {
  return `\
import React, { useMemo } from 'react';
import { useModel } from '../core/umiExports';
import LocaleContext, { LocaleInstance } from './context';


interface Props {
  children: React.ReactNode;
}

function getLocales(initialState:any) {

  const { localization } = initialState;

  return localization.values
}

const LocaleProvider: React.FC<Props> = props => {
  if (typeof useModel !== 'function') {
    throw new Error('[plugin-locale]: useModel is not a function, @umijs/plugin-initial-state is needed.')
  }

  const { children } = props;
  const { initialState } = useModel('@@initialState');

  const locale: LocaleInstance = useMemo(() => getLocales(initialState as any), [initialState]);

  return React.createElement(
    LocaleContext.Provider,
    { value: locale },
    children,
  );
};

export default LocaleProvider;
`;
}
