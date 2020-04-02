import { Modal, Form, Input, message, Select } from "antd";
import React from "react";
import { useRequest } from "@umijs/hooks";
import { SaasTenantDto } from "../data";
import { updateTenant } from "../service";
import { SaasEditionDto } from "../../editions/data";

const { Option } = Select;
interface EditFormProps {
  visible: boolean;
  onCancel: () => void;
  onSubmit: () => void;
  editionOptions: SaasEditionDto[];
  editTenant: SaasTenantDto;
}
const EditForm: React.FC<EditFormProps> = props => {
  const { visible, onCancel, onSubmit, editTenant, editionOptions } = props;
  const [form] = Form.useForm();
  const { run: doUpdateTenant } = useRequest(updateTenant, {
    manual: true,
    onSuccess: () => {
      message.success("操作成功！");
      onCancel();
      onSubmit();
    }
  });
  const handleOk = () => {
    form.validateFields().then(values => {
      form.resetFields();
      doUpdateTenant(editTenant?.id, {
        name: values.name,
        editionId: values.editionId
      })
    })
  }
  form.setFieldsValue({...editTenant})
  return (
    <Modal onOk={handleOk} title="编辑租户" onCancel={onCancel} visible={visible}>
      <Form form={form} layout="vertical"  name="edit_tenant">
        <Form.Item
          label="租户名称"
          name="name"
          rules={[{
            required: true,
            message: "租户名称不能为空!"
          }]}>
          <Input />
        </Form.Item>
        <Form.Item
          label="版本"
          name="editionId">
          <Select>
            {
              editionOptions.map(item => <Option key={item.id} value={item.id}>{item.displayName}</Option>)
            }
          </Select>
        </Form.Item>
      </Form>
    </Modal>
  )
}

export default EditForm;
