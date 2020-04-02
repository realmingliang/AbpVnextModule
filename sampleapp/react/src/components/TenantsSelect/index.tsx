import React, { Fragment, useState } from "react";
import { Modal, Form, Input } from "antd";
import { ConnectProps, ConnectState } from "@/models/connect";
import { ApplicationConfiguration } from "@/models/global";
import { connect } from 'dva';
import { useRequest } from '@umijs/hooks'
import { getTenantByName } from "@/services/tenant";
import Store from "../../utils/store";

interface TenantsSelectProps extends ConnectProps {
  tenant?: ApplicationConfiguration.CurrentTenant;
}
const TenantsSelect: React.FC<TenantsSelectProps> = props => {
  const { tenant } = props;
  const [modalVisible, handleModalVisible] = useState<boolean>(false);
  const { run: changeTenant } = useRequest(getTenantByName, {
    manual: true,
    onSuccess: (result) => {
      if (result.success) {
        Store.SetTenantId(result.tenantId)
      }
      handleModalVisible(false);
      window.location.reload();
    }
  });
  const [form] = Form.useForm();
  const triggerRender = () => {
    const tenantName = tenant === null || tenant?.name == null || tenant === undefined ? '未选择' : tenant!.name;
    return (
      <div style={{ textAlign: 'center' }}>
        当前租户名称:{tenantName}(<a href="#" onClick={() => handleModalVisible(true)}>切换</a>)
      </div>
    )
  }
  const modalOkHandler = () => {
    form
      .validateFields()
      .then(values => {
        form.resetFields();
        if (values.tenantName) {
          changeTenant(values.tenantName);
        } else {
          Store.SetTenantId("");
          window.location.reload();
        }
      })
  }
  return (
    <Fragment>
      {triggerRender()}
      <Modal
        title="切换租户"
        onOk={modalOkHandler}
        visible={modalVisible}
        onCancel={() => handleModalVisible(false)}>
        <Form name="change_tenant" form={form} layout='vertical'>
          <Form.Item name="tenantName" label="租户名称">
            <Input />
          </Form.Item>
        </Form>
      </Modal>
    </Fragment>
  )
}

export default connect(({ global }: ConnectState) => ({
  tenant: global.applicationConfiguration?.currentTenant
}))(TenantsSelect);
