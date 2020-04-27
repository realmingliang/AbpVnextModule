
import { Alert, Checkbox } from 'antd';
import React, { useState } from 'react';
import { Link, ConnectProps, useModel, useLocale } from 'umi';
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
  const { initialState, refresh } = useModel('@@initialState');
  const { multiTenancy } = initialState!;
  const handleSubmit = async (values: LoginParamsType) => {
    const { dispatch } = props;

    await dispatch!({
      type: 'login/login',
      payload: { ...values },
      callback: async () => {
        await refresh();
      }
    });
  };

  const intl = useLocale("AbpAccount")
  return (
    <div className={styles.main}>
      {
        multiTenancy?.isEnabled ? <TenantsSelect /> : null
      }
      <LoginFrom activeKey='account' onSubmit={handleSubmit}>
        <Tab key="account" tab="" >
          {status === 'error' && !submitting && (
            <LoginMessage content={intl("InvalidUserNameOrPassword")} />
          )}
          <UserName
            name="userNameOrEmailAddress"
            placeholder={intl("UserNameOrEmailAddress")}
            rules={[
              {
                required: true,
                message: intl("ThisFieldIsRequired"),
              },
            ]}
          />
          <Password
            name="password"
            placeholder={intl("Password")}
            rules={[
              {
                required: true,
                message: intl("ThisFieldIsRequired"),
              },
            ]}
          />
        </Tab>
        <div>
          <Checkbox checked={autoLogin} onChange={e => setAutoLogin(e.target.checked)}>
            {intl("RememberMe")}
          </Checkbox>
        </div>
        <Submit loading={submitting}>{intl("Login")}</Submit>
        <div className={styles.other}>
          <Link className={styles.register} to="/user/register">
            {intl("Register")}
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
