const AppConsts = {
  userManagement: {
    defaultAdminUserName: 'admin',
  },
  localization: {
    defaultLocalizationSourceName: 'test',
  },
  authorization: {
    encrptedAuthTokenName: 'enc_auth_token',
  },
  oAuthConfig: {
    grant_type: process.env.REACT_APP_Grant_Type,
    client_id:  process.env.REACT_APP_Client_Id,
    client_secret: process.env.REACT_APP_Client_Secret,
    scope: process.env.REACT_APP_Scope,
  },
  appBaseUrl: process.env.REACT_APP_APP_BASE_URL,
  remoteServiceBaseUrl: process.env.REACT_APP_REMOTE_SERVICE_BASE_URL,
};
export default AppConsts;
