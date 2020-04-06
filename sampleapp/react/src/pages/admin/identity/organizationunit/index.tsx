
import { PageHeaderWrapper } from '@ant-design/pro-layout';
import React, { useEffect, useState } from 'react';
import { Row, Col, Card, Tree, Button, message } from 'antd';
import { useRequest } from '@umijs/hooks';
import { contextMenu, Menu, Item } from 'react-contexify';
import { connect } from 'dva';
import { getOrganizationUnits, deleteOrganizationUnit } from './service';
import { OrganizationUnitDto, CreateOrUpdateOrganizationUnitInput } from './data';
import { createTree } from '@/utils/utils';
import CreateOrUpdateForm from './components/createOrUpdateForm';
import 'react-contexify/dist/ReactContexify.min.css';
import PermissionManagement from '@/components/PermissionsManagement';
import { ConnectState, ConnectProps } from '@/models/connect';
import { GetPermissionListResultDto } from '@/services/data';



const initEmptyOrganizationUnit: CreateOrUpdateOrganizationUnitInput = {
  displayName: "",
  parentId: "",
  id: "",
}
interface OrganizationUnitProps extends ConnectProps {
  permissions: GetPermissionListResultDto;
}
const OrganizationUnit: React.FC<OrganizationUnitProps> = ({permissions,dispatch}) => {
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
  /**
  * 编辑组织机构权限
  * @param id 组织机构
  */
 const openPermissionModal = async () => {
  await dispatch({
    type: 'permission/getPermission',
    payload: {
      providerKey: organizationUnitItem.id!,
      providerName: 'O',
    }
  })
  await handlePermissionModalVisible(true);
};
  const handleDeleteItem = async () => {
    await doDeleteItem(organizationUnitItem.id!);
    await doGetData();
  }
  const RightClientMenu = () => (
    <Menu style={{ zIndex: 1000 }} id="rightMenu">
      <Item key="edit" onClick={() => handleModalVisible(true)}>
        修改
      </Item>
      <Item onClick={openPermissionModal} >
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
  return (
    <PageHeaderWrapper>
      <Row gutter={24}>
        <Col span={8}>
          <Card title="组织机构树" extra={<Button onClick={handleCreate} type="primary">新增根组织</Button>}>
            <Tree.DirectoryTree
              treeData={treeData}
              onRightClick={treeRightClickHandler}
            />
            <RightClientMenu />
          </Card>
        </Col>
        <Col span={16}>
          <Card title="成员管理">
            ...
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
      permissions={permissions}
    />
    </PageHeaderWrapper>
  )
}
export default connect(({  permission }: ConnectState) => ({
  permissions: permission.permissions
}))(OrganizationUnit);
