import { Modal, Input, Form, Select, Switch, message } from "antd";
import React from "react";
import { useRequest } from "@umijs/hooks";
import { IdentityClaimTypeUpdateDto } from "../data";
import { updateClaimType } from "../service";

interface EditFormProps {
  visible: boolean;
  onCancel: () => void;
  onSubmit: () => void;
  itemId:string;
  editClaimTypeItem:IdentityClaimTypeUpdateDto
}
const valueType = ["String", "Int", "Boolean", "DateTime"];
const { Option } = Select;
const EditForm: React.FC<EditFormProps> = props => {
  const { visible, onCancel, onSubmit,editClaimTypeItem,itemId } = props;
  const[form] = Form.useForm();
  const {run:doUpdateClaimType} = useRequest(updateClaimType,{
    manual:true,
    onSuccess:()=>{
      message.success("操作成功!");
      onSubmit();
      onCancel();
    }
  })
   // 表单提交
   const handleOk = () => {
    form.validateFields().then(values => {
      form.resetFields();
      doUpdateClaimType(itemId,{ ...values as IdentityClaimTypeUpdateDto })
    })
  }
  const valueFormat=editClaimTypeItem===undefined?"":valueType[editClaimTypeItem.valueType]
  form.setFieldsValue({...editClaimTypeItem,valueType:valueFormat});
  return (
    <Modal onOk={handleOk} visible={visible} onCancel={onCancel}>
    <Form form={form} layout="vertical">
      <Form.Item  rules={[{
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
        <Select>
          {
            valueType.map(item => <Option value={item} >{item}</Option>)
          }
        </Select>
      </Form.Item>
      <Form.Item label="" name="required">
        <Switch checkedChildren="必须" unCheckedChildren="可选" />
      </Form.Item>
      </Form>
    </Modal>
  )
}

export default EditForm;
