import React, { Fragment, useState } from "react";
import { Modal, Form, Input } from "antd";
import { useRequest } from '@umijs/hooks'
import { getTenantByName } from "@/services/tenant";
import Store from "../../utils/store";
import { useModel, useLocale } from "umi";

const TenantsSelect: React.FC = () => {
  const { initialState } = useModel("@@initialState");
  const [modalVisible, handleModalVisible] = useState<boolean>(false);
  const { run: changeTenant } = useRequest(getTenantByName, {
    manual: true,
    onSuccess: (result) => {
      if (result.success) {
        Store.SetTenantId(result.tenantId)
      }
      handleModalVisible(false);
    }
  });
  const intl = useLocale("AbpSaas")
  const AbpUiMultiTenancy=useLocale("AbpUiMultiTenancy")
  const [form] = Form.useForm();
  const { currentTenant:tenant} =initialState!;
  const triggerRender = () => {
    const tenantName = tenant === null || tenant?.name == null || tenant === undefined ? AbpUiMultiTenancy("NotSelected") : tenant!.name;
    return (
      <div style={{ textAlign: 'center' }}>
        {intl("TenantName")}:{tenantName}(<a href="#" onClick={() => handleModalVisible(true)}>{AbpUiMultiTenancy("Switch")}</a>)
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
        }
        window.location.reload();
      })
  }
  return (
    <Fragment>
      {triggerRender()}
      <Modal
        title={AbpUiMultiTenancy("SwitchTenant")}
        onOk={modalOkHandler}
        visible={modalVisible}
        onCancel={() => handleModalVisible(false)}>
        <Form name="change_tenant" form={form} layout='vertical'>
          <Form.Item name="tenantName" label={intl("TenantName")}>
            <Input />
          </Form.Item>
        </Form>
      </Modal>
    </Fragment>
  )
}

export default TenantsSelect;
