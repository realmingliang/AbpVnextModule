
import { ApplicationConfiguration } from "./services/data";

export default function(initialState:ApplicationConfiguration.Response) {

  const { setting } = initialState;

  return setting.values
}
