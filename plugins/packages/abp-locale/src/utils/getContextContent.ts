export default function() {
  return `\
import React from 'react';
import localeFactory from '../../locale';
export type LocaleInstance = ReturnType<typeof localeFactory>;
const LocaleContext = React.createContext(null!);

export default LocaleContext;
`;
}
