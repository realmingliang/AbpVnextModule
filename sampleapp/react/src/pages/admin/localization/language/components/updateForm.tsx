import React from "react";
import { Modal, Form, Input, Select } from "antd";
import { useLocale, Dispatch, connect } from "umi";
import { Flags } from "../culture";
import { LanguageDto } from "../data";
const { Option } = Select;
interface UpdateFormProps  {
  language:LanguageDto;
  visible: boolean;
  dispatch?:Dispatch
  onCancel: () => void;
}
const UpdateForm: React.FC<UpdateFormProps> = props => {
  const { visible, onCancel,dispatch,language } = props;
  const [form] = Form.useForm();
  const intl = useLocale("LanguageManagement");
  const handleOk=()=>{
    form.validateFields().then(values=>{
      form.resetFields();
      dispatch!({
        type:"language/updateLanguage",
        payload:{
          id:language.id,
          data:values
        },
      })
      onCancel();
    })
  }
  form.setFieldsValue({...language})
  return (
    <Modal onOk={handleOk} okText={intl("Save")}  cancelText={intl("Close")} onCancel={onCancel} visible={visible} title="修改Identityuser">
      <Form  form={form} layout="vertical">
        <Form.Item name="displayName" label={intl("DisplayName")}
          rules={[{
            required: true,
            message: intl("ThisFieldIsRequired")
          }]} >
          <Input />
        </Form.Item>
        <Form.Item name="flagIcon" label={intl("FlagIcon")}
        rules={[{
          required: true,
          message: intl("ThisFieldIsRequired")
        }]} >
        <Select >
          {Flags.map(item => <Option key={item} value={item}>{item}</Option>)}
        </Select>
      </Form.Item>
      </Form>
    </Modal>
  )
}

export default connect()(UpdateForm);
