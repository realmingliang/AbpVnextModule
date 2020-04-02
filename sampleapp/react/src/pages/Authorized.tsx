import React from 'react';
import { Redirect } from 'umi';
import { connect } from 'dva';
import Authorized from '@/utils/Authorized';
import { getRouteAuthority } from '@/utils/utils';
import { ConnectProps, ConnectState } from '@/models/connect';
import { ApplicationConfiguration } from '@/models/global';

interface AuthComponentProps extends ConnectProps {
  currentUser?: ApplicationConfiguration.CurrentUser;
}

const AuthComponent: React.FC<AuthComponentProps> = ({
  children,
  route = {
    routes: [],
  },
  location = {
    pathname: '',
  },
  currentUser,
}) => {
  const { routes = [] } = route;
  const isLogin = currentUser && currentUser.userName;

  return (
    <Authorized
      authority={getRouteAuthority(location.pathname, routes) || ''}
      noMatch={isLogin ? <Redirect to="/exception/403" /> : <Redirect to="/account/login" />}
    >
      {children}
    </Authorized>
  );
};

export default connect(({ global }: ConnectState) => ({
  currentUser:global.applicationConfiguration?.currentUser,
}))(AuthComponent);
