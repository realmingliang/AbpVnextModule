export default function() {
  return `\
import React from 'react';
import featureFactory from '@/feature';

export type FeatureInstance = ReturnType<typeof featureFactory>;

const FeatureContext = React.createContext<FeatureInstance>(null!);

export default FeatureContext;
`;
}
