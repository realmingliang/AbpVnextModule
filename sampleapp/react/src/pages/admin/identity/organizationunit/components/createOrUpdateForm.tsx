import React from "react";
import { Modal, Form, Input, message } from "antd";
import { useRequest } from '@umijs/hooks';
import { CreateOrUpdateOrganizationUnitInput } from "../data";
import { createOrganizationUnit, updateOrganizationUnit } from "../service";

interface CreateOrUpdateFormProps {
  visible: boolean;
  onCancel: () => void;
  onSubmit: () => void;
  organizationUnit: CreateOrUpdateOrganizationUnitInput;
}

const CreateOrUpdateForm: React.FC<CreateOrUpdateFormProps> = props => {
  const { visible, onCancel, organizationUnit,onSubmit} = props;
  const [form] = Form.useForm();
  const { run: doCreateProductGroup } = useRequest(createOrganizationUnit, {
    manual: true ,
    onSuccess:()=>{
      message.success("操作成功!");
      onSubmit();
      onCancel();

    }
  });
  const { run: doUpdateProductGroup } = useRequest(updateOrganizationUnit, {
    manual: true ,
    onSuccess:()=>{
      message.success("操作成功!");
      onSubmit();
      onCancel();
    }
  });
  const handleOk = () => {
    form.validateFields().then(values => {
      if (organizationUnit.id === "") {
        doCreateProductGroup({ displayName: values.displayName, parentId:organizationUnit.parentId! })
      } else {
        doUpdateProductGroup(organizationUnit.id!, { displayName: values.displayName })
      }
    })
  }
  const handleOncancel=()=>{
    form.resetFields();
    onCancel();
  }
  form.setFieldsValue({...organizationUnit})
  const title = organizationUnit.id===""?"新增组织":`修改组织:${organizationUnit.displayName}`;
  return (
    <Modal onOk={handleOk} destroyOnClose title={title} visible={visible} onCancel={handleOncancel}>
      <Form  form={form}  name="create_or_update_form" layout="vertical">
        <Form.Item label="名称"
          rules={[{
            required: true,
            message: '名称不能为空!'
          }]}
          name="displayName">
          <Input />
        </Form.Item>
      </Form>
    </Modal>
  )

}

export default CreateOrUpdateForm;
