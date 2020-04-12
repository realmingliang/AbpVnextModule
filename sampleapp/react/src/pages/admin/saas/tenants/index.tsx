
import { PageHeaderWrapper } from '@ant-design/pro-layout';
import React, { useRef, useState } from 'react';
import ProTable, { ActionType, ProColumns } from '@ant-design/pro-table';
import { PlusOutlined, SettingOutlined, DownOutlined } from '@ant-design/icons';
import { Button, Dropdown, Menu, message } from 'antd';
import { useRequest } from '@umijs/hooks';
import { SaasTenantDto } from './data';
import { queryTenants, deleteTenant } from './service';
import CreateForm from './components/createForm';
import EditForm from './components/editForm';
import { SaasEditionDto } from "../editions/data.d";
import { queryEditions } from '../editions/service';
import { useLocale } from 'umi';


const initTenantEmpty:SaasTenantDto={id:"",name:"",editionId:"",editionName:""}

const Tenants: React.FC = () => {
  const actionRef = useRef<ActionType>();
  const [createModalVisible, handleCreateModalVisible] = useState<boolean>(false);
  const [editModalVisible, handleEditModalVisible] = useState<boolean>(false);
  const [editTenant, setEditTenant] = useState<SaasTenantDto>(initTenantEmpty);
  const [editionOptions, setEditionOptions] = useState<SaasEditionDto[]>([]);

  const { run: doGetEditions } = useRequest(queryEditions, {
    manual: true,
    onSuccess: (result) => {
      setEditionOptions(result.items);
    }
  });
  const reloadTable = () => {
    actionRef.current!.reload();
  }
  const { run: doDeleteTenant } = useRequest(deleteTenant, {
    manual: true,
    onSuccess: () => {
      message.success("操作成功！");
      reloadTable();
    }
  });
  const intl = useLocale("AbpSaas")
  const handleEditTenant = async (item: SaasTenantDto) => {
    await doGetEditions();
    await setEditTenant(item)
    await handleEditModalVisible(true)
  }
  const handleOpenCreateModal=async()=>{
    await doGetEditions();
    await handleCreateModalVisible(true);
  }
  const columns: ProColumns<SaasTenantDto>[] = [
    {
      title: '操作',
      render: (_, record) =>
        <Dropdown
          overlay={
            <Menu
              selectedKeys={[]}
            >
              <Menu.Item onClick={() => handleEditTenant(record!)} key="edit">编辑</Menu.Item>
              <Menu.Item onClick={() => doDeleteTenant(record.id)} key="delete">删除</Menu.Item>
            </Menu>
          }
        >
          <Button type="primary">
            <SettingOutlined /> 操作 <DownOutlined />
          </Button>
        </Dropdown>
    },
    {
      title: intl("TenantName"),
      dataIndex: 'name',
    },
    {
      title: intl("EditionName"),
      dataIndex: 'editionName',
    }
  ]
  return (
    <PageHeaderWrapper>
      <ProTable<SaasTenantDto>
        headerTitle="租户信息"
        actionRef={actionRef}
        search={false}
        rowKey="id"
        toolBarRender={() => [
          <Button icon={<PlusOutlined />} onClick={() => handleOpenCreateModal()} type="primary" >
            新建
          </Button>
        ]}
        request={async (params = {}) => {
          const response = await queryTenants({ skipCount: (params.current! - 1) * params.pageSize!, maxResultCount: params.pageSize });
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
      <CreateForm
        editionOptions={editionOptions}
        visible={createModalVisible}
        onCancel={() => handleCreateModalVisible(false)}
        onSubmit={() => reloadTable()} />
      <EditForm
        editionOptions={editionOptions}
        visible={editModalVisible}
        onCancel={() => handleEditModalVisible(false)}
        onSubmit={() => reloadTable()}
        editTenant={editTenant!} />
    </PageHeaderWrapper>
  )
}

export default Tenants;
