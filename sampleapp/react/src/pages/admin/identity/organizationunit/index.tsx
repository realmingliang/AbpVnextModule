
import { PageHeaderWrapper } from '@ant-design/pro-layout';
import React, { useEffect, useState } from 'react';
import { Row, Col, Card, Tree } from 'antd';
import { useRequest } from '@umijs/hooks';
import { getOrganizationUnits } from './service';
import { OrganizationUnitDto } from './data';
import { createTree } from '@/utils/utils';


const OrganizationUnit: React.FC = () => {

  const [organizations, setOrganizationUnit] = useState<OrganizationUnitDto[]>([]);
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
    // right click
  }
  return (
    <PageHeaderWrapper>
      <Row gutter={24}>
        <Col span={8}>
          <Card title="组织机构树">
            <Tree.DirectoryTree
              treeData={treeData}
              onRightClick={treeRightClickHandler}
            />
          </Card>
        </Col>
        <Col span={16}>
          <Card title="成员管理">
            ...
           </Card>
        </Col>
      </Row>
    </PageHeaderWrapper>
  )
}

export default OrganizationUnit;
