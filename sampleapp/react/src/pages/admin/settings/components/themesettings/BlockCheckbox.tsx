import { CheckOutlined } from '@ant-design/icons';
import { Tooltip } from 'antd';

import React from 'react';

export interface BlockCheckboxProps {
  value: string;
  onChange: (key: string) => void;
  list?: {
    title: string;
    key: string;
    url: string;
  }[];
}

const baseClassName = 'ant-pro-setting-drawer-block-checbox';

const BlockCheckbox: React.FC<BlockCheckboxProps> = ({
  value,
  onChange,
  list: propsList,
}) => {

  const list = propsList || [
    {
      key: 'sidemenu',
      url:
        'https://gw.alipayobjects.com/zos/antfincdn/XwFOFbLkSM/LCkqqYNmvBEbokSDscrm.svg',
      title: "侧边菜单",
    },
    {
      key: 'topmenu',
      url:
        'https://gw.alipayobjects.com/zos/antfincdn/URETY8%24STp/KDNDBbriJhLwuqMoxcAr.svg',
      title:"顶部菜单"
    },
  ];

  return (
    <div className={baseClassName} key={value}>
      {list.map(item => (
        <Tooltip title={item.title} key={item.key}>
          <div
            className={`${baseClassName}-item`}
            onClick={() => onChange(item.key)}
          >
            <img src={item.url} alt={item.key} />
            <div
              className={`${baseClassName}-selectIcon`}
              style={{
                display: value === item.key ? 'block' : 'none',
              }}
            >
              <CheckOutlined />
            </div>
          </div>
        </Tooltip>
      ))}
    </div>
  );
};

export default BlockCheckbox;
