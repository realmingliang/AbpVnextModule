import React from 'react';
import { Redirect } from 'umi';
import { stringify } from 'querystring';
import Store from './../utils/store';


const SecurityLayout: React.FC<null> = props => {
  const { children } = props;
  const token = Store.GetToken();

  const isLogin = token && token!=null && token!=undefined;
  const queryString = stringify({
    redirect: window.location.href,
  });
  if (!isLogin && window.location.pathname !== '/account/login') {
    return <Redirect to={`/account/login?${queryString}`} />;
  }
  return children!;
}

export default SecurityLayout;
