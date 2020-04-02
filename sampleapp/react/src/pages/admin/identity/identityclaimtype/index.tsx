
import { PageHeaderWrapper } from '@ant-design/pro-layout';
import React, { useRef, useState } from 'react';
import { Button, Dropdown, Menu, message } from 'antd';
import ProTable, { ActionType, ProColumns } from '@ant-design/pro-table';
import { PlusOutlined, SettingOutlined, DownOutlined } from '@ant-design/icons';
import { useRequest } from '@umijs/hooks';
import { IdentityClaimTypeDto, IdentityClaimTypeUpdateDto } from './data';
import { queryClaimTypes, deleteClaimType } from './service';
import EditForm from './components/editForm';
import CreateForm from './components/createForm';



const IdentityClaimType: React.FC = () => {
  const actionRef = useRef<ActionType>();
  const [createModalVisible, handleCreateModalVisible] = useState<boolean>(false);
  const [editModalVisible, handleEditModalVisible] = useState<boolean>(false);
  const reloadTable = () => {
    actionRef.current!.reload();
  }
  // 操作行Id
  const [selectId, setSelectId] = useState<string>("");
  const { run: doDeleteItem } = useRequest(deleteClaimType, {
    manual: true,
    onSuccess: () => {
      reloadTable();
      message.success("操作成功!");
    }
  })
  const [editClaimTypeItem, setEditClaimTypeItem] = useState<IdentityClaimTypeUpdateDto>();


  const handleEditItem = async (record: IdentityClaimTypeDto) => {
    await setSelectId(record.id);
    await setEditClaimTypeItem(record);
    await handleEditModalVisible(true);
  }
  const columns: ProColumns<IdentityClaimTypeDto>[] = [
    {
      title: '操作',
      render: (_, record) =>
        <Dropdown
          overlay={
            <Menu
              selectedKeys={[]}
            >
              <Menu.Item key="edit" onClick={() => handleEditItem(record)}>编辑</Menu.Item>
              {
                record.isStatic?null:<Menu.Item key="remove" onClick={() => doDeleteItem(record.id)}>删除</Menu.Item>
              }

            </Menu>
          }
        >
          <Button type="primary">
            <SettingOutlined /> 操作 <DownOutlined />
          </Button>
        </Dropdown>

    },
    {
      title: '名称',
      dataIndex: 'name',
    }, {
      title: '值类型',
      dataIndex: 'valueType',
      valueEnum: {
        0: { text: 'String', status: 'Success' },
        1: { text: 'Int', status: 'Success' },
        2: { text: 'Boolean', status: 'Success' },
        3: { text: 'DateTime', status: 'Success' },
      },
    }, {
      title: '描述',
      dataIndex: 'description',
    }, {
      title: '正则',
      dataIndex: 'regex',
    }, {
      title: '必要',
      dataIndex: 'required',
      valueEnum: {
        true: { text: '是', status: 'Success' },
        false: { text: '否', status: "Error" },
      },
    }, {
      title: '静态',
      dataIndex: 'isStatic',
      valueEnum: {
        true: { text: '是', status: 'Success' },
        false: { text: '否', status: 'Error' },
      },
    },
  ]
  return (
    <PageHeaderWrapper >
      <ProTable<IdentityClaimTypeDto>
        headerTitle="声明类型"
        actionRef={actionRef}
        search={false}
        rowKey="id"
        toolBarRender={() => [
          <Button onClick={() => handleCreateModalVisible(true)} icon={<PlusOutlined />} type="primary" >
            新建
        </Button>
        ]}
        request={async (params = {}) => {
          const response = await queryClaimTypes({ skipCount: (params.current! - 1) * params.pageSize!, maxResultCount: params.pageSize });
          const data = response.items;
          return {
            data,
            page: params.current,
            success: true,
            total: response.totalCount,
          }
        }}
        columns={columns}
      />
      <CreateForm
        visible={createModalVisible}
        onSubmit={() => reloadTable()}
        onCancel={() => handleCreateModalVisible(false)} />
      <EditForm
        itemId={selectId}
        editClaimTypeItem={editClaimTypeItem!}
        visible={editModalVisible}
        onSubmit={() => reloadTable()}
        onCancel={() => handleEditModalVisible(false)} />
    </PageHeaderWrapper>
  )
}

export default IdentityClaimType;
