import { LogoutOutlined, SettingOutlined } from '@ant-design/icons';
import { Avatar, Menu, Spin } from 'antd';
import { ClickParam } from 'antd/es/menu';
import React from 'react';
import { history, ConnectProps, useModel, connect, useLocale } from 'umi';
import HeaderDropdown from '../HeaderDropdown';
import styles from './index.less';

export interface GlobalHeaderRightProps extends Partial<ConnectProps> {
  menu?: boolean;
}
const AvatarDropdown: React.FC<GlobalHeaderRightProps> = props => {
  const { dispatch } = props;
  const { initialState } = useModel('@@initialState');

  const { currentUser } = initialState!
  const onMenuClick = (event: ClickParam) => {
    const { key } = event;
    if (key === 'logout') {
      if (dispatch) {
        dispatch({
          type: 'login/logout',
        });
      }
      return;
    }else if (key === 'settings') {
      history.push('/accountsettings')
    }
  };
  const intl = useLocale("AbpAccount")
  const menuHeaderDropdown = (
    <Menu className={styles.menu} selectedKeys={[]} onClick={onMenuClick}>
        <Menu.Item key="settings">
          <SettingOutlined />
          {intl("ManageYourProfile")}
        </Menu.Item>
        <Menu.Divider />

      <Menu.Item key="logout">
        <LogoutOutlined />
        {intl("Logout")}
      </Menu.Item>
    </Menu>
  );
  return currentUser && currentUser.userName ? (
    <HeaderDropdown overlay={menuHeaderDropdown}>
      <span className={`${styles.action} ${styles.account}`}>
      <Avatar size="small" className={styles.avatar} src="https://gw.alipayobjects.com/zos/antfincdn/XAosXuNZyF/BiazfanxmamNRoxxVxka.png" alt="avatar" />
      <span className={styles.name}>{currentUser.userName}</span>
      </span>
    </HeaderDropdown>
  ) : (
      <span className={`${styles.action} ${styles.account}`}>
        <Spin
          size="small"
          style={{
            marginLeft: 8,
            marginRight: 8,
          }}
        />
      </span>
    );
}
export default connect()(AvatarDropdown);
