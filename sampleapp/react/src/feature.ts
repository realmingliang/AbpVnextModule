import { ApplicationConfiguration } from '@/services/data';

export default function(initialState:ApplicationConfiguration.Response) {

  const { features } = initialState;

  return features.values
}
