import { ApplicationConfiguration } from '@/services/data';

export default function(initialState:ApplicationConfiguration.Response) {

  const { localization } = initialState;

  return localization.values
}
