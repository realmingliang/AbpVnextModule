import React, { useRef, useState } from 'react';
import { PageHeaderWrapper } from '@ant-design/pro-layout';
import ProTable, { ActionType, ProColumns } from '@ant-design/pro-table';
import { Button, Dropdown, Menu, Tag, message } from 'antd';
import { PlusOutlined, DownOutlined, SettingOutlined } from '@ant-design/icons';
import PermissionManagement from '@/components/PermissionsManagement';
import { connect } from 'dva';
import { ConnectState, ConnectProps } from '@/models/connect';
import { GetPermissionListResultDto } from '@/services/data';
import { useRequest } from '@umijs/hooks';
import { IdentityRoleDto } from './data';
import { queryRoles, getRole, deleteRole as deleteRoleItem } from './service';
import CreateOrUpdateForm from './components/createOrUpdateForm';

interface IdentityRoleProps extends ConnectProps {
  permissions: GetPermissionListResultDto;
}

const IdentityRole: React.FC<IdentityRoleProps> = props => {
  const { permissions, dispatch } = props;
  const actionRef = useRef<ActionType>();
  const [roleName, handleRoleName] = useState<string>('');
  const [editRole, handleEditRole] = useState<IdentityRoleDto | undefined>(undefined);
  const [modalVisible, handleModalVisible] = useState<boolean>(false);
  const [permissionModalVisible, handlePermissionModalVisible] = useState<boolean>(false);
  const getEditRole = (id: string) => getRole(id);
  const deleteRole = (id: string) => deleteRoleItem(id);
  const { run: runDeleteRole } = useRequest(deleteRole, {
    manual: true,
    onSuccess: async result => {
      if (!result.ok)
        return;
      message.success("删除成功!")
    }
  })
  // 重新获取表格数据
  const tableReload = () => {
    actionRef.current!.reload();
  }

  const { run: runGetEditRole } = useRequest(getEditRole, {
    manual: true,
    onSuccess: async result => {
      if (result) {
        await handleEditRole(result);
        tableReload();
      }
    }
  })
  /**
 * 编辑角色权限
 * @param name 角色名称
 */
  const openPermissionModal = async (name: string) => {
    await handleRoleName(name);
    await dispatch({
      type: 'permission/getPermission',
      payload: {
        providerKey: name,
        providerName: 'R',
      }
    })
    await handlePermissionModalVisible(true);
  };

  /**
  * 编辑角色
  * @param id 角色id
  */
  const openCreateOrUpdateModal = async (id: string | null = null) => {
    if (id === null) {
      await handleEditRole(undefined);
    } else {
      await runGetEditRole(id!);
    }
    await handleModalVisible(true);
  }
  const columns: ProColumns<IdentityRoleDto>[] = [
    {
      title: '操作',
      render: (_, record) =>
        <Dropdown
          overlay={
            <Menu
              selectedKeys={[]}
            >
              <Menu.Item key="edit" onClick={() => openCreateOrUpdateModal(record.id)}>编辑</Menu.Item>
              <Menu.Item key="approval" onClick={() => openPermissionModal(record.name)}>权限</Menu.Item>
              <Menu.Item key="remove" onClick={() => runDeleteRole(record.id)}>删除</Menu.Item>
            </Menu>
          }
        >
          <Button type="primary">
            <SettingOutlined /> 操作 <DownOutlined />
          </Button>
        </Dropdown>

    },
    {
      title: '角色名',
      dataIndex: 'name',
      render: (_, record) => <>{record.name}{record.isDefault ? <Tag style={{ borderRadius: 10, marginLeft: '.25rem' }} color="#108ee9">默认</Tag> : null}
        {record.isPublic ? <Tag style={{ borderRadius: 10, marginLeft: '.25rem' }} color="#17a2b8">公开</Tag> : null}</>
    },
  ]
  return (
    <PageHeaderWrapper>
      <ProTable<IdentityRoleDto>
        headerTitle="用户信息"
        actionRef={actionRef}
        search={false}
        rowKey="id"
        toolBarRender={() => [
          <Button onClick={() => openCreateOrUpdateModal()} icon={<PlusOutlined />} type="primary" >
            新建
          </Button>
        ]}
        request={async (params = {}) => {
          const response = await queryRoles({ skipCount: (params.current! - 1) * params.pageSize!, maxResultCount: params.pageSize });
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
      <PermissionManagement
        providerKey={roleName}
        providerName='R'
        onCancel={() => handlePermissionModalVisible(false)}
        modalVisible={permissionModalVisible}
        permissions={permissions}
      />
      <CreateOrUpdateForm
        editRole={editRole}
        visible={modalVisible}
        onCancel={() => handleModalVisible(false)} />
    </PageHeaderWrapper>
  );
}
export default connect(({ permission }: ConnectState) => ({
  permissions: permission.permissions
}))(IdentityRole);
