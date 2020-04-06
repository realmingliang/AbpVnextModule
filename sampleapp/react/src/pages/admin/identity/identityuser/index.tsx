
import React, { useRef, useState } from 'react';
import { PageHeaderWrapper } from '@ant-design/pro-layout';
import ProTable, { ActionType, ProColumns } from '@ant-design/pro-table';
import { PlusOutlined, DownOutlined, SettingOutlined, ExclamationCircleOutlined } from '@ant-design/icons';
import { Button, Dropdown, Menu, Modal } from 'antd';
import { Dispatch } from 'redux';
import { connect } from 'dva';
import { ConnectState } from '@/models/connect';
import check from '@/components/Authorized/CheckPermissions';
import Permissions from '@/utils/permissions';
import PermissionManagement from '@/components/PermissionsManagement';
import { GetPermissionListResultDto } from '@/services/data';
import { useRequest } from '@umijs/hooks';
import { IdentityUserDto, IdentityUserCreateOrUpdateDto, IdentityUserClaimDto } from './data';
import { queryUsers, getUserClaimTypes } from './service';
import CreateOrUpdateForm from './components/createOrUpdateForm';
import { IdentityRoleDto } from '../identityrole/data';
import UpdateClaimTypesModal from './components/updateClaimTypesModal';

const { confirm } = Modal;
interface IdentityUserProps {
  dispatch: Dispatch;
  createOrUpdateUser?: IdentityUserCreateOrUpdateDto;
  allRoles: IdentityRoleDto[];
  permissions: GetPermissionListResultDto;
}

const IdentityUser: React.FC<IdentityUserProps> = ({ dispatch, allRoles, createOrUpdateUser, permissions }) => {

  const actionRef = useRef<ActionType>();
  const [modalVisible, handleModalVisible] = useState<boolean>(false);
  const [claimsModalVisible, handleClaimsModalVisible] = useState<boolean>(false);
  const [userClaimTypes, setUserClaimTypes] = useState<IdentityUserClaimDto[]>([]);
  const [userId, handleUserId] = useState<string>("")
  const [permissionModalVisible, handlePermissionModalVisible] = useState<boolean>(false);
  const {run:doGetUserClaimTypes} =useRequest(getUserClaimTypes,{
    manual:true,
    onSuccess:(result)=>{
      setUserClaimTypes(result);
      handleClaimsModalVisible(true);
    }
  })
  /**
   * 编辑或新增用户
   * @param id 用户id
   */
  const handleEditOrAdd = async (id: string | null = null) => {
    await dispatch({
      type: 'identityUser/getRoles',
    })
    await dispatch({
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
  const handlDeleteUser = async (id: string) => {
    confirm({
      title: '确认删除此用户吗?',
      icon: <ExclamationCircleOutlined />,
      onOk() {
        dispatch({
          type: 'identityUser/deleteUser',
          payload: id,
        });
        actionRef.current!.reload();
      },
      onCancel() {

      },
    });
  };

  const handleUpdateUserClaimModalOpen=(id:string)=>{

    doGetUserClaimTypes(id);
  }
  const columns: ProColumns<IdentityUserDto>[] = [
    {
      title: '操作',
      render: (_, record) =>
        <Dropdown
          overlay={
            <Menu
              selectedKeys={[]}
            >
              {
                check(Permissions.AbpIdentity.Users.Create, <Menu.Item onClick={() => { handleEditOrAdd(record.id) }} key="edit">编辑</Menu.Item>, null)
              }
               <Menu.Item key="claims" onClick={() => handleUpdateUserClaimModalOpen(record.id)}>声明</Menu.Item>
              <Menu.Item key="approval" onClick={() => openPermissionModal(record.id)}>权限</Menu.Item>
              <Menu.Item key="remove" onClick={() => handlDeleteUser(record.id)}>删除</Menu.Item>
            </Menu>
          }
        >
          <Button type="primary">
            <SettingOutlined /> 操作 <DownOutlined />
          </Button>
        </Dropdown>

    },
    {
      title: '用户名',
      dataIndex: 'name',
    }, {
      title: '邮箱地址',
      dataIndex: 'email',
    }, {
      title: '手机号',
      dataIndex: 'phoneNumber',
    },
  ]
  return (
    <PageHeaderWrapper>
      <ProTable<IdentityUserDto>
        headerTitle="用户信息"
        actionRef={actionRef}
        search={false}
        rowKey="id"
        toolBarRender={() => [
          <Button icon={<PlusOutlined />} onClick={() => handleEditOrAdd()} type="primary" >
            新建
        </Button>
        ]}
        request={async (params = {}) => {
          const response = await queryUsers({ skipCount: (params.current! - 1) * params.pageSize!, maxResultCount: params.pageSize });
          const data = response.items;
          return {
            data,
            page: params.current,
            success: true,
            total: data.totalCount,
          }
        }}
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
      <UpdateClaimTypesModal
      claimTypes={userClaimTypes}
      onCancel={()=>handleClaimsModalVisible(false)}
      visible={claimsModalVisible}  />
    </PageHeaderWrapper>

  )
}

export default connect(({ identityUser }: ConnectState) => ({
  allRoles: identityUser.allRoles,
  createOrUpdateUser: identityUser.createOrUpdateUser,
}))(IdentityUser);
