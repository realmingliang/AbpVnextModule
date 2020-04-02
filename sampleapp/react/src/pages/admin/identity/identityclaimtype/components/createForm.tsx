import React from "react";
import { Modal, Form, Input, Switch, Select, message } from "antd";
import { useRequest } from '@umijs/hooks';
import { createClaimType } from "../service";
import { IdentityClaimTypeCreateDto } from "../data";

interface CreateFormProps {
  visible: boolean;
  onCancel: () => void;
  onSubmit: () => void;
}
const valueType = ["String", "Int", "Boolean", "DateTime"];
const { Option } = Select;
const CreateForm: React.FC<CreateFormProps> = props => {
  const { visible, onCancel, onSubmit } = props;
  const [form] = Form.useForm();
  const { run: doCreateClaimType } = useRequest(createClaimType, {
    manual: true,
    onSuccess: () => {
      message.success("操作成功!");
      onSubmit();
      onCancel();
    }
  })
  // 表单提交
  const handleOk = () => {
    form.validateFields().then(values => {
      form.resetFields();
      doCreateClaimType({ ...values as IdentityClaimTypeCreateDto })
    })
  }
  return (
    <Modal onOk={handleOk} onCancel={onCancel} visible={visible} title="新声明类型">
      <Form form={form} layout="vertical">
        <Form.Item rules={[{
          required: true,
          message: "名称不能为空!"
        }]} label="名称" name="name">
          <Input />
        </Form.Item>
        <Form.Item rules={[{
          required: true,
          message: "正则不能为空!"
        }]} label="正则" name="regex">
          <Input />
        </Form.Item>
        <Form.Item label="正则描述" name="regexDescription">
          <Input />
        </Form.Item>
        <Form.Item label="描述" name="description">
          <Input />
        </Form.Item>
        <Form.Item label="数据类型" name="valueType">
          <Select defaultValue={[valueType[0]]}>
            {
              valueType.map(item => <Option value={item} >{item}</Option>)
            }
          </Select>
        </Form.Item>
        <Form.Item name="required">
          <Switch checkedChildren="必须" unCheckedChildren="可选" />
        </Form.Item>
      </Form>
    </Modal>
  )
}

export default CreateForm;
