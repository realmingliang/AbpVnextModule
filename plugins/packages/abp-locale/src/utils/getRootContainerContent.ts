export default function() {
  return `\
import React from 'react';
import LocaleProvider from './LocaleProvider';

export function rootContainer(container: React.ReactNode) {
  return React.createElement(LocaleProvider, null, container);
}
`;
}
