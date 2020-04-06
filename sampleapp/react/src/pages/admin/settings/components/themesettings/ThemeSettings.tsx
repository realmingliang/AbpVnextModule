import React from 'react';
import { Divider, List, Switch, Button, message } from 'antd';
import { connect } from 'dva';
import { ConnectState, ConnectProps } from '@/models/connect';
import { ThemeSettingsDto } from '@/services/data';
import useMergeValue from 'use-merge-value';
import styles from './ThemeSettings.less';
import BlockCheckbox from './BlockCheckbox';
import ThemeColor from './ThemeColor';
import LayoutSetting, { renderLayoutSettingItem } from './LayoutChange';
import { InitThemeSettings } from '@/utils/utils';
import { useModel } from 'umi';

interface BodyProps {
  title: string;
}

const colors: {
  key: string;
  color: string;
}[] = [{
  key: 'daybreak',
  color: '#1890ff',
},
{
  key: 'dust',
  color: '#F5222D',
},
{
  key: 'volcano',
  color: '#FA541C',
}, {
  key: 'sunset',
  color: '#FAAD14',
}, {
  key: 'cyan',
  color: '#13C2C2',
}, {
  key: 'green',
  color: '#52C41A',
}, {
  key: 'geekblue',
  color: '#2F54EB',
}, {
  key: 'purple',
  color: '#722ED1',
}]
const getThemeList = () => {
  const themeList = [
    {
      key: 'light',
      url:
        'https://gw.alipayobjects.com/zos/antfincdn/NQ%24zoisaD2/jpRkZQMyYRryryPNtyIC.svg',
      title: '亮色菜单风格',
    },
    {
      key: 'dark',
      url:
        'https://gw.alipayobjects.com/zos/antfincdn/XwFOFbLkSM/LCkqqYNmvBEbokSDscrm.svg',
      title: '暗色菜单风格',
    },
  ];

  return {
    colorList: colors,
    themeList,
  };
};
const Body: React.FC<BodyProps> = ({ children, title }) => (
  <div style={{ marginBottom: 24 }}>
    <h3 className="ant-pro-setting-drawer-title">{title}</h3>
    {children}
  </div>
);
interface ThemeSettingsProps extends ConnectProps {
  submitLoading?: boolean;
}


const ThemeSettings: React.FC<ThemeSettingsProps> = props => {
  const themeList = getThemeList();
  const { dispatch, submitLoading } = props;
  const { initialState } = useModel('@@initialState');
  let themeSettings=InitThemeSettings(initialState!.setting.values as any);
  const [settings, setThemeSettings] = useMergeValue<ThemeSettingsDto>(themeSettings);
  const changeSettings = (key: string, value: string | boolean, ) => {
    const nextState = { ...settings };
    nextState[key] = value;
    if (key === 'layout') {
      nextState.contentWidth = value === 'topmenu' ? 'Fixed' : 'Fluid';
    }
    setThemeSettings(nextState);
  }
  const handleSubmit = async () => {
    const hide = message.loading("提交中...");
    await dispatch({
      type: 'settings/updateAllThemeSettings',
      payload: settings
    })
    if (!submitLoading) {
      hide();
    }
  }
  return (
    <div className={styles.main}>
      <Body
        title="整体风格设置"
      >
        <BlockCheckbox
          list={themeList.themeList}
          value={settings.navTheme!}
          onChange={value => { changeSettings("navTheme", value) }}
        />
        <ThemeColor
          title="主题色"
          value={settings.primaryColor}
          colors={themeList.colorList}
          onChange={value => { changeSettings("primaryColor", value) }}
        />
      </Body>
      <Body title='导航模式'>
        <BlockCheckbox
          value={settings.layout}
          onChange={value => { changeSettings("layout", value) }}
        />
      </Body>
      <LayoutSetting settings={settings} changeSetting={changeSettings} />
      <Divider />
      <Body title="其他设置">
        <List
          split={false}
          renderItem={renderLayoutSettingItem}
          dataSource={[
            {
              title: "色弱模式",
              action: (
                <Switch
                  size="small"
                  checked={settings.colorWeak}
                  onChange={checked => { changeSettings("colorWeak", checked) }}
                />
              ),
            },
          ]}
        />
      </Body>
      <div className={styles.saveBtn}>
        <Button onClick={handleSubmit} type="primary">保存设置</Button>
      </div>
    </div>
  )
}
export default connect(({ loading }: ConnectState) => ({
  submitLoading: loading.effects["settings/updateAllThemeSettings"]
}))(ThemeSettings);
