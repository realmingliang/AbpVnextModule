import { utils } from 'umi';
import { join } from 'path';

export default function(util: typeof utils) {
  return `\
import React, { useMemo } from 'react';
import { IRoute } from 'umi';
import { useModel } from '../core/umiExports';
import featureFactory from '../../feature';
import FeatureContext, { FeatureInstance } from './context';
import { traverseModifyRoutes } from '${util.winPath(
    join(__dirname, '..', 'utils', 'runtimeUtil'),
  )}';

type Routes = IRoute[];

interface Props {
  routes: Routes;
  children: React.ReactNode;
}

const FeatureProvider: React.FC<Props> = props => {
  if (typeof useModel !== 'function') {
    throw new Error('[plugin-feature]: useModel is not a function, @umijs/plugin-initial-state is needed.')
  }

  const { children } = props;
  const { initialState } = useModel('@@initialState');

  const feature: FeatureInstance = useMemo(() => featureFactory(initialState as any), [initialState]);

  if (process.env.NODE_ENV === 'development' && (feature === undefined || feature === null)) {
    console.warn('[plugin-feature]: the feature instance created by feature.ts(js) is nullish, maybe you need check it.');
  }

  props.routes.splice(0, props.routes.length, ...traverseModifyRoutes(props.routes, feature));

  return React.createElement(
    FeatureContext.Provider,
    { value: feature },
    children,
  );
};

export default FeatureProvider;
`;
}
