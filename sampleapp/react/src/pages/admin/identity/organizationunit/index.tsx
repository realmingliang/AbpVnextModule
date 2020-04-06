
import { PageHeaderWrapper } from '@ant-design/pro-layout';
import React, { useEffect, useState } from 'react';
import { Row, Col, Card, Tree, Button, message, Tabs, Table } from 'antd';
import { useRequest } from '@umijs/hooks';
import { contextMenu, Menu, Item } from 'react-contexify';
import { getOrganizationUnits, deleteOrganizationUnit } from './service';
import { OrganizationUnitDto, CreateOrUpdateOrganizationUnitInput } from './data';
import { createTree } from '@/utils/utils';
import CreateOrUpdateForm from './components/createOrUpdateForm';
import 'react-contexify/dist/ReactContexify.min.css';
import PermissionManagement from '@/components/PermissionsManagement';
import { ConnectProps } from '@/models/connect';
import { GetPermissionListResultDto } from '@/services/data';
import { PlusOutlined } from '@ant-design/icons';


const { TabPane } = Tabs;
const { DirectoryTree } = Tree;
const initEmptyOrganizationUnit: CreateOrUpdateOrganizationUnitInput = {
  displayName: "",
  parentId: "",
  id: "",
}
interface OrganizationUnitProps extends ConnectProps {
  permissions: GetPermissionListResultDto;
}
const OrganizationUnit: React.FC<OrganizationUnitProps> = ({ permissions, dispatch }) => {
  const [modalVisible, handleModalVisible] = useState<boolean>(false);

  const [organizations, setOrganizationUnit] = useState<OrganizationUnitDto[]>([]);
  const [organizationUnitItem, setOrganizationUnitItem] = useState<CreateOrUpdateOrganizationUnitInput>(initEmptyOrganizationUnit);
  const [permissionModalVisible, handlePermissionModalVisible] = useState<boolean>(false);

  const { run: doGetData } = useRequest(getOrganizationUnits, {
    manual: true,
    onSuccess: (result) => {
      setOrganizationUnit(result.items)
    }
  });
  useEffect(() => {
    doGetData();
  }, []);
  const treeData = createTree(organizations, "parentId", "id", null, "children", [{
    target: 'key',
    targetFunction(item: OrganizationUnitDto) {
      return item.id;
    },
  }, {
    target: 'title',
    targetFunction(item: OrganizationUnitDto) {
      return item.displayName;
    }
  }]);
  const treeRightClickHandler = async (info: any) => {
    info.event.persist();
    const { data } = info.node;
    await setOrganizationUnitItem({ displayName: data.displayName, parentId: data.parentId, id: data.id })
    contextMenu.show({
      id: 'rightMenu',
      event: info.event,
    });
  }
  const handleAddChildren = async () => {
    await setOrganizationUnitItem({ ...initEmptyOrganizationUnit, parentId: organizationUnitItem.id! })
    await handleModalVisible(true)
  }
  const { run: doDeleteItem } = useRequest(deleteOrganizationUnit, {
    manual: true,
    onSuccess: () => {
      message.success("删除成功!");
    }
  });
  const handleCreate = async () => {
    await setOrganizationUnitItem(initEmptyOrganizationUnit)

    await handleModalVisible(true)
  }
  const handleDeleteItem = async () => {
    await doDeleteItem(organizationUnitItem.id!);
    await doGetData();
  }
  const RightClientMenu = () => (
    <Menu style={{ zIndex: 1000 }} id="rightMenu">
      <Item key="edit" onClick={() => handleModalVisible(true)}>
        修改
      </Item>
      <Item onClick={() => handlePermissionModalVisible(true)} >
        权限
   </Item>
      <Item onClick={handleAddChildren} >
        添加子组织
     </Item>

      <Item onClick={handleDeleteItem}>
        删除
      </Item>
    </Menu>
  );
  const organizationUnitUserTableColumns = [
    {
      title: '操作',
      dataIndex: 'action',
      key: 'action',
      render: (text: any, record: any) => <Button icon="close-circle" type="primary">删除</Button>,
    },
    {
      title: '用户名',
      dataIndex: 'userName',
      key: 'userName',
    },
    {
      title: '添加时间',
      dataIndex: 'addedTime',
      key: 'addedTime',
    },
  ];
  const organizationUnitRoleTableColumns = [
    {
      title: '操作',
      dataIndex: 'action',
      key: 'action',
      render: (text: any, record: any) => <Button icon="close-circle" type="primary">删除</Button>,
    },
    {
      title: '角色',
      dataIndex: 'displayName',
      key: 'displayName',
    },
    {
      title: '添加时间',
      dataIndex: 'addedTime',
      key: 'addedTime',
    },
  ]
   const selectTree = async (selectedKeys: any[],info:any) => {
    const { data } = info.node;
    await setOrganizationUnitItem({ displayName: data.displayName, parentId: data.parentId, id: data.id })
  }

  return (
    <PageHeaderWrapper>
      <Row gutter={24}>
        <Col span={8}>
          <Card title="组织机构树" extra={<Button   icon={<PlusOutlined />} onClick={handleCreate} type="primary">新增根组织</Button>}>
            <DirectoryTree
              onSelect={selectTree}
              treeData={treeData}
              onRightClick={treeRightClickHandler}
            />
            <RightClientMenu />
          </Card>
        </Col>
        <Col span={16}>
          <Card title="成员管理">
            <Tabs type="card" >
              <TabPane tab="组织成员" key="member">
                {
                  organizationUnitItem.id === "" ? (<p>选择一个组织成员</p>) :
                    (<><Col style={{ textAlign: 'right' }}>
                      <Button icon={<PlusOutlined />} type="primary">添加组织成员</Button>
                    </Col>
                      <Table
                        dataSource={
                          []
                        }
                        columns={organizationUnitUserTableColumns} />
                    </>)

                }
              </TabPane>
              <TabPane tab="角色" key="role">
                {
                  organizationUnitItem.id === "" ? (<p>选择一个角色</p>) :
                    (<div>   <Col style={{ textAlign: 'right' }}>
                      <Button icon={<PlusOutlined />} type="primary">添加角色</Button>
                    </Col>
                      <Table
                        dataSource={
                          []
                        }
                        columns={organizationUnitRoleTableColumns} />
                    </div>)

                }
              </TabPane>
            </Tabs>
          </Card>
        </Col>
      </Row>
      <CreateOrUpdateForm
        onSubmit={() => doGetData()}
        onCancel={() => handleModalVisible(false)}
        visible={modalVisible}
        organizationUnit={organizationUnitItem}
      />
      <PermissionManagement
        providerKey={organizationUnitItem.id!}
        providerName='O'
        onCancel={() => handlePermissionModalVisible(false)}
        modalVisible={permissionModalVisible}
      />
    </PageHeaderWrapper>
  )
}
export default OrganizationUnit;
