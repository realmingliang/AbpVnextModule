import React, { Component } from 'react';
import { PageHeaderWrapper, GridContent } from '@ant-design/pro-layout';
import { Menu } from 'antd';
import { Dispatch, connect } from 'dva';
import ThemeSettings from './components/themesettings/ThemeSettings';
import styles from './style.less';

const { Item } = Menu;
type SettingsStateKeys = 'theme' | 'security';

interface SettingsState {
  mode: 'inline' | 'horizontal';
  menuMap: {
    [key: string]: React.ReactNode;
  };
  selectKey: SettingsStateKeys;
}
interface SettingsProps {
  dispatch?: Dispatch;
}

class Settings extends Component<
  SettingsProps,
  SettingsState
  > {
    main: HTMLDivElement | undefined = undefined;

    constructor(props: SettingsProps) {
      super(props);
      const menuMap = {
        theme: '主题设置',
        security: '个人设置',
      };
      this.state = {
        mode: 'inline',
        menuMap,
        selectKey: 'theme',
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

    selectKey = async (key: SettingsStateKeys) => {
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
        case 'theme':
          return <ThemeSettings/>;
        case 'security':
          return 123;
        default:
          break;
      }
      return null;
    };

  render() {
    const { mode, selectKey } = this.state;
    return (
      <PageHeaderWrapper>
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
                onClick={({ key }) => this.selectKey(key as SettingsStateKeys)}
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
      </PageHeaderWrapper>
    )
  }
}

export default connect()(Settings);;
