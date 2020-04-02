import React, { Component } from 'react';

import { Dispatch } from 'redux';
import { GridContent } from '@ant-design/pro-layout';
import { Menu } from 'antd';
import { connect } from 'dva';
import ChangePasswordViewView from './components/changepassword';
import SecurityView from './components/security';
import styles from './style.less';

const { Item } = Menu;

interface AccountSettingsProps {
  dispatch: Dispatch<any>;
}

type AccountSettingsStateKeys = 'changepassword' | 'security' ;
interface AccountSettingsState {
  mode: 'inline' | 'horizontal';
  menuMap: {
    [key: string]: React.ReactNode;
  };
  selectKey: AccountSettingsStateKeys;
}

class AccountSettings extends Component<
  AccountSettingsProps,
  AccountSettingsState
> {
  main: HTMLDivElement | undefined = undefined;

  constructor(props: AccountSettingsProps) {
    super(props);
    const menuMap = {
      changepassword: '修改密码',
      security: '个人设置',
    };
    this.state = {
      mode: 'inline',
      menuMap,
      selectKey: 'changepassword',
    };
  }

  componentDidMount() {
    window.addEventListener('resize', this.resize);
    this.resize();
  }

  componentWillUnmount() {
    window.removeEventListener('resize', this.resize);
  }

  getMenu = () => {
    const { menuMap } = this.state;
    return Object.keys(menuMap).map(item => <Item key={item}>{menuMap[item]}</Item>);
  };

  getRightTitle = () => {
    const { selectKey, menuMap } = this.state;
    return menuMap[selectKey];
  };

  selectKey = async (key: AccountSettingsStateKeys) => {
    if(key==='security') {
      const { dispatch } = this.props;
      await dispatch({
        type: 'accountSettings/getMyProfile',
      })
    }
    this.setState({
      selectKey: key,
    });
  };

  resize = () => {
    if (!this.main) {
      return;
    }
    requestAnimationFrame(() => {
      if (!this.main) {
        return;
      }
      let mode: 'inline' | 'horizontal' = 'inline';
      const { offsetWidth } = this.main;
      if (this.main.offsetWidth < 641 && offsetWidth > 400) {
        mode = 'horizontal';
      }
      if (window.innerWidth < 768 && offsetWidth > 400) {
        mode = 'horizontal';
      }
      this.setState({
        mode,
      });
    });
  };

  renderChildren = () => {
    const { selectKey } = this.state;
    switch (selectKey) {
      case 'changepassword':
        return <ChangePasswordViewView />;
      case 'security':
        return <SecurityView />;
      default:
        break;
    }
    return null;
  };

  render() {
    const { mode, selectKey } = this.state;
    return (
      <GridContent>
        <div
          className={styles.main}
          ref={ref => {
            if (ref) {
              this.main = ref;
            }
          }}
        >
          <div className={styles.leftMenu}>
            <Menu
              mode={mode}
              selectedKeys={[selectKey]}
              onClick={({ key }) => this.selectKey(key as AccountSettingsStateKeys)}
            >
              {this.getMenu()}
            </Menu>
          </div>
          <div className={styles.right}>
            <div className={styles.title}>{this.getRightTitle()}</div>
            {this.renderChildren()}
          </div>
        </div>
      </GridContent>
    );
  }
}

export default connect()(AccountSettings);
