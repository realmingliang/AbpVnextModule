import React from 'react';
import { List, Tooltip, Select, Switch } from 'antd';
import { SettingItemProps } from '@ant-design/pro-layout/lib/SettingDrawer';
import { ThemeSettingsDto } from '@/services/data';


export const renderLayoutSettingItem = (item: SettingItemProps) => {
  const action = React.cloneElement(item.action, {
    disabled: item.disabled,
  });
  return (
    <Tooltip title={item.disabled ? item.disabledReason : ''} placement="left">
      <List.Item actions={[action]}>
        <span style={{ opacity: item.disabled ? 0.5 : 1 }}>{item.title}</span>
      </List.Item>
    </Tooltip>
  );
};
const LayoutSetting: React.FC<{
  settings: ThemeSettingsDto;
  changeSetting: (key: string, value: any, hideLoading?: boolean) => void;
}> = ({ settings = {}, changeSetting }) => {
  const { contentWidth, fixedHeader, layout, fixSiderbar } =
    settings ;
  return (
    <List
      split={false}
      dataSource={[
        {
          title: "内容区域宽度",
          action: (
            <Select<string>
              value={contentWidth || 'Fixed'}
              size="small"
              onSelect={value => changeSetting('contentWidth', value)}
              style={{ width: 150 }}
            >
              {layout === 'sidemenu' ? null : (
                <Select.Option value="Fixed">
                  定宽
                </Select.Option>
              )}
              <Select.Option value="Fluid">
                流式
              </Select.Option>
            </Select>
          ),
        },
        {
          title: '固定Header',
          action: (
            <Switch
              size="small"
              checked={!!fixedHeader}
              onChange={checked => changeSetting('fixedHeader', checked)}
            />
          ),
        },
        {
          title: '固定侧边菜单',
          disabled: layout === 'topmenu',
          disabledReason: '侧边菜单布局时可配置',
          action: (
            <Switch
              size="small"
              checked={!!fixSiderbar}
              onChange={checked => changeSetting('fixSiderbar', checked)}
            />
          ),
        },
      ]}
      renderItem={renderLayoutSettingItem}
    />
  );
};

export default LayoutSetting;
