import React, { useState } from 'react';
import ProTable, { ProColumns } from '@ant-design/pro-table';
import { Button, Dropdown, Menu, Tag } from 'antd';
import { PlusOutlined, DownOutlined, SettingOutlined } from '@ant-design/icons';
import PermissionManagement from '@/components/PermissionsManagement';
import { IdentityRoleDto, RoleQueryParams } from './data';
import CreateOrUpdateForm from './components/createOrUpdateForm';
import { IdentityRoleModelState, connect, ConnectProps } from 'umi';
import { PagedResultDto } from '@/services/data';
import { useEffect } from 'react';
import { PaginationConfig } from 'antd/lib/pagination';

interface IdentityRoleProps extends ConnectProps {
  rolesResult: PagedResultDto<IdentityRoleDto>;
  editRole:IdentityRoleDto;
}

const IdentityRole: React.FC<IdentityRoleProps> = ({ dispatch, rolesResult,editRole }) => {
  const [roleName, handleRoleName] = useState<string>('');
  const [modalVisible, handleModalVisible] = useState<boolean>(false);
  const [permissionModalVisible, handlePermissionModalVisible] = useState<boolean>(false);
  const defaultRequestParams: RoleQueryParams = {
    skipCount: 0,
    maxResultCount: 10,
    filter: "",
    sorting: "",
  };
  const [requestParams, setRequestParams] = useState<RoleQueryParams>(defaultRequestParams);

  useEffect(() => {
    dispatch!({
      type: 'identityRole/getRoles',
      payload: requestParams
    })
  }, [requestParams])
  const handleTableChange = async (params: PaginationConfig) => {
    await setRequestParams(
      { ...requestParams, skipCount: (params.current! - 1) * params.pageSize!, maxResultCount: params.pageSize }
    )

  }

  const deleteRole=(id: string)=>{
    dispatch!({
      type: 'identityRole/deleteRole',
      payload: id
    })
  }
  const getEditRole = async (id: string) => {
    await dispatch!({
      type: 'identityRole/getRole',
      payload: id
    })
  };

  /**
 * 编辑角色权限
 * @param name 角色名称
 */
  const openPermissionModal = async (name: string) => {
    await handleRoleName(name);
    await handlePermissionModalVisible(true);
  };

  /**
  * 编辑角色
  * @param id 角色id
  */
  const openCreateOrUpdateModal = async (id: string | null = null) => {
    if (id === null) {
      await dispatch!({
        type:"identityRole/saveEditRole",
        payload:undefined,
      })
    } else {
      await getEditRole(id!);
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
              <Menu.Item key="remove" onClick={() => deleteRole(record.id)}>删除</Menu.Item>
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
    <>
      <ProTable<IdentityRoleDto>
        headerTitle="角色信息"
        search={false}
        onChange={handleTableChange}
        rowKey="id"
        toolBarRender={() => [
          <Button onClick={() => openCreateOrUpdateModal()} icon={<PlusOutlined />} type="primary" >
            新建
          </Button>
        ]}
        pagination={{ total: rolesResult.totalCount, pageSize: 10 }}
        dataSource={rolesResult.items}
        columns={columns}
      />
      <PermissionManagement
        providerKey={roleName}
        providerName='R'
        onCancel={() => handlePermissionModalVisible(false)}
        modalVisible={permissionModalVisible}
      />
      <CreateOrUpdateForm
        editRole={editRole}
        visible={modalVisible}
        onCancel={() => handleModalVisible(false)} />
    </>
  );
}
export default connect(
  ({
    identityRole: { rolesResult,editRole },
  }: {
    identityRole: IdentityRoleModelState;
    loading: {
      effects: {
        [key: string]: boolean;
      };
    };
  }) => ({
    rolesResult,
    editRole
  }),
)(IdentityRole);
