
import { PageHeaderWrapper } from '@ant-design/pro-layout';
import React, { useRef, useState } from 'react';
import ProTable, { ActionType, ProColumns } from '@ant-design/pro-table';
import { Button, Dropdown, Menu, message } from 'antd';
import { PlusOutlined, SettingOutlined, DownOutlined } from '@ant-design/icons';
import { useRequest } from '@umijs/hooks';
import { SaasEditionDto } from './data';
import { queryEditions, deleteEdition } from './service';
import CreateOrUpdateForm from './components/createOrUpdateForm';


const initEditionItem:SaasEditionDto={displayName:"",id:""};

const Editions: React.FC = () => {
  const actionRef = useRef<ActionType>();
  const [createOrUpdateModalVisible, handleCreateOrUpdateModalVisible] = useState<boolean>(false);
  const [editEdition, setEditEdition] = useState<SaasEditionDto>(initEditionItem);
  const reloadTable = () => {
    actionRef.current!.reload();
  }
  const { run: doDeleteEdition } = useRequest(deleteEdition, {
    manual: true,
    onSuccess: () => {
      message.success("操作成功！");
      reloadTable();
    }
  });
  const handleOpenEditModal = async (item:SaasEditionDto)=>{
    await setEditEdition(item);
    // open modal
    await handleCreateOrUpdateModalVisible(true);
  }
  const columns: ProColumns<SaasEditionDto>[] = [
    {
      title: '操作',
      render: (_, record) =>
        <Dropdown
          overlay={
            <Menu
              selectedKeys={[]}
            >
              <Menu.Item onClick={()=>handleOpenEditModal(record)} key="edit">编辑</Menu.Item>
              <Menu.Item onClick={() => doDeleteEdition(record.id)} key="delete">删除</Menu.Item>
            </Menu>
          }
        >
          <Button type="primary">
            <SettingOutlined /> 操作 <DownOutlined />
          </Button>
        </Dropdown>
    },
    {
      title: '版本名称',
      dataIndex: 'displayName',
    }
  ]
  return (
    <PageHeaderWrapper>
      <ProTable<SaasEditionDto>
        headerTitle="版本"
        actionRef={actionRef}
        search={false}
        rowKey="id"
        toolBarRender={() => [
          <Button icon={<PlusOutlined />} onClick={()=>handleOpenEditModal(initEditionItem)} type="primary" >
            新建
          </Button>
        ]}
        request={async (params = {}) => {
          const response = await queryEditions({ skipCount: (params.current! - 1) * params.pageSize!, maxResultCount: params.pageSize });
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
      onSubmit={reloadTable}
      editEdition={editEdition!}
      visible={createOrUpdateModalVisible}
      onCancel={()=>handleCreateOrUpdateModalVisible(false)}/>
    </PageHeaderWrapper>
  )
}
export default Editions;
