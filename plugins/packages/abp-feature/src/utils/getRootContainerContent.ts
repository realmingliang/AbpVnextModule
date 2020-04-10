export default function() {
  return `\
import React from 'react';
import FeatureProvider from './FeatureProvider';

export function rootContainer(container: React.ReactNode, { routes }) {
  return React.createElement(FeatureProvider, { routes }, container);
}
`;
}
