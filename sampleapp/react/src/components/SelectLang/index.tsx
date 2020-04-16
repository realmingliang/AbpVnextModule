import { GlobalOutlined } from '@ant-design/icons';
import { Menu } from 'antd';
import { ClickParam } from 'antd/es/menu';
import React from 'react';
import { useModel } from 'umi'
import classNames from 'classnames';
import HeaderDropdown from '../HeaderDropdown';
import styles from './index.less';
import Store from "../../utils/store";
import IconFont from '../IconFont';
import { formatterFlagIcon } from '@/utils/utils';

interface SelectLangProps {
  className?: string;
}

const SelectLang: React.FC<SelectLangProps> = props => {
  const { className } = props;
  const { initialState } = useModel("@@initialState");
  const changeLang = ({ key }: ClickParam): void => {
    Store.SetLanguage(key);
    window.location.reload();
  };
  const { localization } = initialState!;
  const langMenu = (
    <Menu className={styles.menu} selectedKeys={[localization.currentCulture.cultureName]} onClick={changeLang}>
      {localization?.languages.map(locale => (
        <Menu.Item key={locale.cultureName}>
        <IconFont type={formatterFlagIcon(locale.cultureName)} />
          {locale.displayName}
        </Menu.Item>
      ))}
    </Menu>
  );
  return (
    <HeaderDropdown overlay={langMenu} placement="bottomRight">
      <span className={classNames(styles.dropDown, className)}>
        <GlobalOutlined title="语言" />
      </span>
    </HeaderDropdown>
  );
};
export default SelectLang;
