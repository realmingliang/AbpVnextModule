export default function() {
  return `\
import { useContext } from 'react';
import SettingContext, { SettingInstance as SettingInstanceType } from './context';

export type SettingInstance = SettingInstanceType;

export const useSetting = () => {
  const setting = useContext(SettingContext);

  return setting;
};

`;
}
