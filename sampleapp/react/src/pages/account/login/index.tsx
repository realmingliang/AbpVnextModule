
import { Alert, Checkbox } from 'antd';
import React, { useState } from 'react';
import { Link, ConnectProps, useModel } from 'umi';
import { connect } from 'dva';
import { StateType } from '@/models/login';
import { LoginParamsType } from '@/services/login';
import { ConnectState } from '@/models/connect';
import LoginFrom from './components/Login';
import styles from './style.less';
import TenantsSelect from '@/components/TenantsSelect';

const { Tab, UserName, Password, Submit } = LoginFrom;
interface LoginProps extends ConnectProps {
  userLogin: StateType;
  submitting?: boolean;
}

const LoginMessage: React.FC<{
  content: string;
}> = ({ content }) => (
  <Alert
    style={{
      marginBottom: 24,
    }}
    message={content}
    type="error"
    showIcon
  />
);

const Login: React.FC<LoginProps> = props => {
  const { userLogin = {}, submitting } = props;
  const { status } = userLogin;
  const [autoLogin, setAutoLogin] = useState(true);
  const { initialState,refresh} = useModel('@@initialState');
  const {multiTenancy} = initialState!;
  const handleSubmit = async (values: LoginParamsType) => {
    const { dispatch } = props;

   await dispatch!({
      type: 'login/login',
      payload: { ...values },
      callback: async ()=>{
       await refresh();
      }
    });
  };


  return (
    <div className={styles.main}>
      {
        multiTenancy?.isEnabled ? <TenantsSelect /> : null
      }
      <LoginFrom activeKey='account' onSubmit={handleSubmit}>
        <Tab key="account" tab="账户密码登录">
          {status === 'error' && !submitting && (
            <LoginMessage content="账户或密码错误（admin/ant.design）" />
          )}
          <UserName
            name="userNameOrEmailAddress"
            placeholder="用户名或邮箱"
            rules={[
              {
                required: true,
                message: '请输入用户名!',
              },
            ]}
          />
          <Password
            name="password"
            placeholder="密码"
            rules={[
              {
                required: true,
                message: '请输入密码！',
              },
            ]}
          />
        </Tab>
        <div>
          <Checkbox checked={autoLogin} onChange={e => setAutoLogin(e.target.checked)}>
            自动登录
          </Checkbox>
          <a
            style={{
              float: 'right',
            }}
          >
            忘记密码
          </a>
        </div>
        <Submit loading={submitting}>登录</Submit>
        <div className={styles.other}>
          <Link className={styles.register} to="/user/register">
            注册账户
          </Link>
        </div>
      </LoginFrom>
    </div>
  );
};

export default connect(({ login, loading }: ConnectState) => ({
  userLogin: login,
  submitting: loading.effects['login/login'],
}))(Login);
