import { getConfiguration } from "./services/global";
import { setAuthority } from "./utils/authority";
import _ from "lodash";
export async function getInitialState() {
  const data = await getConfiguration();
  setAuthority(_.keys(data.auth.grantedPolicies));

  return data;
}
