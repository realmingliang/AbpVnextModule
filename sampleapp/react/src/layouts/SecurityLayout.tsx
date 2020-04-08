import React,{useCallback} from 'react';
import { Redirect, useModel } from 'umi';
import { stringify } from 'querystring';

const SecurityLayout: React.FC<null> = props => {
  const { children } = props;
  const { initialState,refresh} = useModel('@@initialState');
  useCallback(()=>{
    refresh()
  },[])
  const isLogin = initialState?.currentUser && initialState.currentUser.isAuthenticated;
  const queryString = stringify({
    redirect: window.location.href,
  });
  if (!isLogin && window.location.pathname !== '/account/login') {
    return <Redirect to={`/account/login?${queryString}`} />;
  }
  return children!;
}

export default SecurityLayout;
