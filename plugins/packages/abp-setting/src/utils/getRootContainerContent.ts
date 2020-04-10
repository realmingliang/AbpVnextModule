export default function() {
  return `\
import React from 'react';
import SettingProvider from './SettingProvider';

export function rootContainer(container: React.ReactNode) {
  return React.createElement(SettingProvider, null, container);
}
`;
}
