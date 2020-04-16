
import React, { useRef, useState } from 'react';
import ProTable, { ActionType, ProColumns } from '@ant-design/pro-table';
import { PlusOutlined, DownOutlined, SettingOutlined, ExclamationCircleOutlined } from '@ant-design/icons';
import { Button, Dropdown, Menu, Modal } from 'antd';
import { connect } from 'dva';
import PermissionManagement from '@/components/PermissionsManagement';
import { IdentityUserDto, IdentityUserCreateOrUpdateDto } from './data';
import CreateOrUpdateForm from './components/createOrUpdateForm';
import { IdentityRoleDto } from '../identityrole/data';
import { IdentityUserModelState } from './model';
import { Access, useAccess, ConnectProps } from 'umi';
import AbpIdentityUser from './permissionName';
import { useLocale } from 'umi';
import { useEffect } from 'react';
import { PagedResultDto } from '@/services/data';

const { confirm } = Modal;
interface IdentityUserProps extends ConnectProps{
  createOrUpdateUser?: IdentityUserCreateOrUpdateDto;
  allRoles: IdentityRoleDto[];
  usersResult:PagedResultDto<IdentityUserDto>
}

const IdentityUser: React.FC<IdentityUserProps> = ({ dispatch, allRoles, usersResult,createOrUpdateUser }) => {

  const access = useAccess();
  const intl = useLocale("AbpIdentity");
  const actionRef = useRef<ActionType>();
  const [modalVisible, handleModalVisible] = useState<boolean>(false);
  const [userId, handleUserId] = useState<string>("")
  const [permissionModalVisible, handlePermissionModalVisible] = useState<boolean>(false);
  useEffect(()=>{
    dispatch!({
      type: 'identityUser/getUsers',
    })
  },[])
  /**
   * 编辑或新增用户
   * @param id 用户id
   */
  const handleEditOrAdd = async (id: string | null = null) => {
    await dispatch!({
      type: 'identityUser/getRoles',
    })
    await dispatch!({
      type: 'identityUser/getUser',
      payload: id
    })
    handleModalVisible(true);
  };
  /**
  * 编辑用户权限
  * @param id 用户名称
  */
  const openPermissionModal = async (id: string) => {
    await handleUserId(id);
    await handlePermissionModalVisible(true);
  };
  /**
   * 删除用户
   * @param id 用户名称
   */
  const handlDeleteUser = async (record: IdentityUserDto) => {
    confirm({
      title: intl("UserDeletionConfirmationMessage", [record.userName]),
      icon: <ExclamationCircleOutlined />,
      onOk() {
        dispatch!({
          type: 'identityUser/deleteUser',
          payload: record.id,
        });
        actionRef.current!.reload();
      },
      onCancel() {

      },
    });
  };

  const columns: ProColumns<IdentityUserDto>[] = [
    {
      title: intl("Actions"),
      render: (_, record) =>
        <Dropdown
          overlay={
            <Menu
              selectedKeys={[]}
            >
              {
                access[AbpIdentityUser.Update] ? <Menu.Item onClick={() => { handleEditOrAdd(record.id) }} key="edit">编辑</Menu.Item> : null
              }
              {
                access[AbpIdentityUser.ManagePermissions] ? <Menu.Item key="approval" onClick={() => openPermissionModal(record.id)}>权限</Menu.Item> : null
              }
              {
                access[AbpIdentityUser.Delete] ? <Menu.Item key="remove" onClick={() => handlDeleteUser(record)}>删除</Menu.Item> : null
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
      title: intl("UserName"),
      dataIndex: 'name',
    }, {
      title: intl("EmailAddress"),
      dataIndex: 'email',
    }, {
      title: intl("PhoneNumber"),
      dataIndex: 'phoneNumber',
    },
  ]
  return (
    <>
      <ProTable<IdentityUserDto>
        headerTitle={intl("Users")}
        actionRef={actionRef}
        search={false}
        rowKey="id"
        toolBarRender={() => [
          <Access accessible={access[AbpIdentityUser.Create]}>
            <Button icon={<PlusOutlined />} onClick={() => handleEditOrAdd()} type="primary" >
              {intl("NewUser")}
            </Button>
          </Access>
        ]}
        pagination={{ total: usersResult.totalCount, pageSize: 10 }}
        dataSource={usersResult.items}
        columns={columns}
      />
      <CreateOrUpdateForm
        onSubmit={() => actionRef.current?.reload()}
        modalVisible={modalVisible}
        onCancel={() => handleModalVisible(false)}
        allRoles={allRoles}
        formValues={createOrUpdateUser}
      />
      <PermissionManagement
        providerKey={userId}
        providerName='U'
        onCancel={() => handlePermissionModalVisible(false)}
        modalVisible={permissionModalVisible}
      />
    </>

  )
}
export default connect(
  ({
    identityUser: { allRoles,createOrUpdateUser,usersResult },
    loading,
  }: {
    identityUser: IdentityUserModelState;
    loading: {
      effects: {
        [key: string]: boolean;
      };
    };
  }) => ({
    allRoles,
    usersResult,
    createOrUpdateUser
  }),
)(IdentityUser);
