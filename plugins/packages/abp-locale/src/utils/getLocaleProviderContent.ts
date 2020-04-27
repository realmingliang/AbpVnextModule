import { utils } from 'umi';
import { join } from 'path';

export default function(util: typeof utils) {
  return `\
import React, { useMemo } from 'react';
import { useModel } from '../core/umiExports';
import LocaleContext, { LocaleInstance } from './context';
import localeFactory from '../../locale';

interface Props {
  children: React.ReactNode;
}


const LocaleProvider: React.FC<Props> = props => {
  if (typeof useModel !== 'function') {
    throw new Error('[plugin-locale]: useModel is not a function, @umijs/plugin-initial-state is needed.')
  }

  const { children } = props;
  const { initialState } = useModel('@@initialState');

  const locale: LocaleInstance = useMemo(() => localeFactory(initialState as any), [initialState]);
  if (process.env.NODE_ENV === 'development' && (locale === undefined || locale === null)) {
    console.warn('[plugin-locale]: the locale instance created by locale.ts(js) is nullish, maybe you need check it.');
  }
  return React.createElement(
    LocaleContext.Provider,
    { value: locale },
    children,
  );
};

export default LocaleProvider;
`;
}
