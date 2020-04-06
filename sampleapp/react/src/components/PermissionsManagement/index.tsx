import React, { useState, useEffect } from 'react';
import { Modal, Tabs, Checkbox, Divider, Tree, Button, message } from 'antd';
import { GetPermissionListResultDto, PermissionGroupDto, PermissionGrantInfoDto } from '@/services/data';
import _ from 'lodash';
import { createTree } from "../../utils/utils";
import { useRequest } from '@umijs/hooks';
import { updatePermissions, getPermissions } from '@/services/permission';

const { TabPane } = Tabs;
interface IsGrantedPermissions {
  name: string, isGranted: boolean
}
interface TabContentProps {
  permissionItem: PermissionGroupDto;
  checkItem: (groupname: string, permissions: IsGrantedPermissions[]) => void;
  checkAll: (name: string, checked: boolean) => void;
}
const TabContent: React.FC<TabContentProps> = ({ permissionItem, checkAll, checkItem }) => {
  const keys = _.map(permissionItem.permissions, 'name');
  const defaultCheckedKeys = _.map(_.filter(permissionItem.permissions, t => t.isGranted), 'name');
  const [checKedKeys, handleChecKedKeys] = useState<string[]>(defaultCheckedKeys);
  const selectedAllChange = (e: any) => {
    handleChecKedKeys(e.target.checked ? keys : []);
    checkAll(permissionItem.name, e.target.checked);
  }
  const treeData = createTree(permissionItem.permissions, 'parentName', 'name', null, 'children', [{
    target: 'key',
    targetFunction(item: PermissionGrantInfoDto) {
      return item.name;
    },
  }, {
    target: 'title',
    targetFunction(item: PermissionGrantInfoDto) {
      return item.displayName;
    }
  }])
  const treeCheckHandle = (checkedKeys: any) => {
    handleChecKedKeys(checkedKeys);
    const permissions: IsGrantedPermissions[] = keys.map(item => {
      if ((checkedKeys as string[]).includes(item)) {
        return { name: item, isGranted: true, }
      }
      return { name: item, isGranted: false, }
    })
    checkItem(permissionItem.name, permissions);
  }
  return (
    <>
      <Checkbox onChange={selectedAllChange}>选择全部</Checkbox>
      <Divider />
      <Tree
        defaultExpandAll
        checkedKeys={checKedKeys}
        onCheck={treeCheckHandle}
        checkable
        treeData={treeData}
      />
    </>
  )
}
interface PermissionManagementProps {
  modalVisible: boolean;
  onCancel: () => void;
  providerName: string;
  providerKey: string;
}
const PermissionManagement: React.FC<PermissionManagementProps> = props => {
  const [permissions,setPermissions] =useState<GetPermissionListResultDto>({entityDisplayName:"",groups:[]});
  const { entityDisplayName, groups } =permissions;
  const { modalVisible, onCancel, providerKey, providerName } = props;
  const allPermissions = _.groupBy(groups, 'name');
  const { run: doUpdatePermissions } = useRequest(updatePermissions, {
    manual: true,
    onSuccess: () => {
      message.success("操作成功！");
      onCancel();
    },onError:()=>{
      message.success("操作失败!");
      onCancel();
    }
  });
  const { run: doGetPermissions } = useRequest(getPermissions, {
    manual: true,
    onSuccess: (result) => {
      setPermissions(result)
    }
  });
  const handlecheckItem = (groupname: string, permissions: IsGrantedPermissions[]) => {
    _.merge(allPermissions[groupname][0].permissions, permissions);
  }
  const handlecheckAll = (name: string, checked: boolean) => {
    allPermissions[name][0].permissions.forEach(item => {
      item.isGranted = checked;
    })
  }
  const allPermissionToList = (): IsGrantedPermissions[] => {
    const list: PermissionGrantInfoDto[] = [];
    _.map(allPermissions, "[0].permissions").map(item => list.push(...item));
    const result = list.map(item => ({ name: item.name, isGranted: item.isGranted }));
    return result;
  }
  const updateAllPermission = (permissions: IsGrantedPermissions[]) => {
    doUpdatePermissions({providerKey,
      providerName,
      permissions,})
  }
  const handleSubmit = () => {
    const permissions = allPermissionToList();
    updateAllPermission(permissions);
  }
  const grantedAllPermission = () => {
    const permissions = allPermissionToList();
    permissions.forEach(item => { item.isGranted = true })
    updateAllPermission(permissions);
  }
  const footer = (
    <>

      <Button onClick={onCancel}>取消</Button>
      <Button onClick={grantedAllPermission} style={{ marginLeft: 8 }} type='primary'>授予所有权限</Button>
      <Button style={{ marginLeft: 8 }} onClick={handleSubmit} type='primary'>确认</Button>
    </>
  )
  useEffect(()=>{
    if(providerKey&&providerName){
      doGetPermissions({ providerKey,providerName});
    }
  },[providerKey,providerName])
  return (
    <Modal
      title={`权限管理:${entityDisplayName}`}
      visible={modalVisible}
      footer={footer}
      onCancel={onCancel}>

      <Tabs tabPosition="left">
        {groups.map(item =>
          <TabPane tab={item.displayName} key={item.name}>
            <TabContent checkItem={handlecheckItem} checkAll={handlecheckAll} permissionItem={item} />
          </TabPane>)}
      </Tabs>
    </Modal>
  )
}
export default PermissionManagement;
