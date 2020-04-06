import { ApplicationConfiguration } from '@/services/data';

export default function(initialState:ApplicationConfiguration.Response) {

  const { auth } = initialState;

  return auth.grantedPolicies
}
