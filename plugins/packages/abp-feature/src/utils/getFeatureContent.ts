export default function() {
  return `\
import React, { useContext } from 'react';
import FeatureContext, { FeatureInstance as FeatureInstanceType } from './context';

export type FeatureInstance = FeatureInstanceType;

export const useFeature = () => {
  const feature = useContext(FeatureContext);

  return feature;
};

export interface FeatureProps {
  feature: boolean;
  fallback?: React.ReactNode;
}

export const Feature: React.FC<FeatureProps> = props => {
  const { feature, fallback, children } = props;

  if (process.env.NODE_ENV === 'development' && typeof feature === 'function') {
    console.warn(
      '[plugin-access]: provided "feature" prop is a function named "' +
        (feature as Function).name +
        '" instead of a boolean, maybe you need check it.',
    );
  }

  return <>{feature ? children : fallback}</>;
};
`;
}
