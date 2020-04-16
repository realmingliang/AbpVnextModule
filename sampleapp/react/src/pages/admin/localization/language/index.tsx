import React, { useRef, useState, useEffect } from 'react';
import { ModalState } from './model';
import { LanguageDto } from './data.d';
import ProTable, { ActionType, ProColumns } from '@ant-design/pro-table';
import { Button, Menu, Dropdown, Badge } from 'antd';
import { PlusOutlined, SettingOutlined, DownOutlined } from '@ant-design/icons';
import { useLocale, ConnectProps, connect, useAccess, Access } from 'umi';
import CreateForm from './components/createForm';
import UpdateForm from './components/updateForm';
import LanguageManagement from './permissionName';

interface LanguageProps extends ConnectProps {
  Languages?: LanguageDto[];
}

const Language: React.FC<LanguageProps> = ({ dispatch, Languages }) => {
  const actionRef = useRef<ActionType>();
  const access = useAccess();
  const intl = useLocale("LanguageManagement"); //LanguageManagement
  const [createModalVisible, handleCreateModalVisible] = useState<boolean>(false);
  const [updateModalVisible, handleUpdateModalVisible] = useState<boolean>(false);
  const [language, setLanguage] = useState<LanguageDto>();
  useEffect(() => {
    dispatch!({
      type: "language/getLanguages",
    })
  }, []);
  const handlDeleteUser = (id: string) => {
    dispatch!({
      type: "language/deleteLanguage",
      payload: id,
    })
  }
  const handleEdit=async (record: LanguageDto) =>{
    await setLanguage(record);
    await handleUpdateModalVisible(true);
  }
  const columns: ProColumns<LanguageDto>[] = [
    {
      title: intl("Actions"),
      render: (_, record) =>
        <Dropdown
          overlay={
            <Menu
              selectedKeys={[]}
            >
              {
                access[LanguageManagement.Edit] ?
                  <Menu.Item key="edit"
                    onClick={() => handleEdit(record)}>{intl("Edit")}
                  </Menu.Item> : null
              }
              {
                access[LanguageManagement.Delete] ?
                  <Menu.Item key="remove"
                    onClick={() => handlDeleteUser(record.id)}>{intl("Delete")}
                  </Menu.Item> : null
              }
            </Menu>
          }
        >
          <Button type="primary">
            <SettingOutlined /> {intl("Actions")} <DownOutlined />
          </Button>
        </Dropdown>
    },
    {
      title: intl("DisplayName"),
      dataIndex: 'displayName',
    }, {
      title: intl("CultureName"),
      dataIndex: 'cultureName',
    }, {
      title: intl("UiCultureName"),
      dataIndex: 'uiCultureName',
    }, {
      title: intl("IsEnabled"),
      dataIndex: 'isEnabled',
      render: (text, record) => {
        return record.isEnabled ? <Badge status="success" text={intl("Yes")} /> : <Badge status="default" text={intl("No")} />
      }
    },
  ]
  return (
    <>
      <ProTable<LanguageDto>
        headerTitle={intl("Languages")}
        actionRef={actionRef}
        search={false}
        rowKey="id"
        pagination={false}
        dataSource={Languages}
        toolBarRender={() => [
          <Access accessible={access[LanguageManagement.Create]}>
            <Button onClick={() => handleCreateModalVisible(true)} icon={<PlusOutlined />} type="primary" >
              {intl("CreateNewLanguage")}
            </Button>
          </Access>
        ]}
        columns={columns}
      />
      <CreateForm
        visible={createModalVisible}
        onCancel={() => handleCreateModalVisible(false)} />
      <UpdateForm
        language={language!}
        visible={updateModalVisible}
        onCancel={() => handleUpdateModalVisible(false)} />
    </>
  )
}
export default connect(
  ({
    language: { Languages },
    loading,
  }: {
    language: ModalState;
    loading: {
      effects: {
        [key: string]: boolean;
      };
    };
  }) => ({
    Languages,
  }),
)(Language);



