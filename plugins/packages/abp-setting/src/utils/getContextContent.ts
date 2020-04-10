export default function() {
  return `\
import React from 'react';
import settingFactory from '@/access';

export type SettingInstance = ReturnType<typeof settingFactory>;

const SettingContext = React.createContext<SettingInstance>(null!);

export default SettingContext;
`;
}
