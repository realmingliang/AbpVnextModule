import React from "react";
import { Modal, Form, Input } from "antd";
import { useLocale, Dispatch, connect } from "umi";
import { LanguageTextDto } from "../data";
const { TextArea } = Input;
interface UpdateFormProps {
  languageText: LanguageTextDto;
  visible: boolean;
  dispatch?: Dispatch
  onCancel: () => void;
  onSubmit:()=>void;
}
const UpdateForm: React.FC<UpdateFormProps> = props => {
  const { visible, onCancel, dispatch,onSubmit, languageText } = props;
  const [form] = Form.useForm();
  const intl = useLocale("LanguageManagement");
  const handleOk = () => {
    form.validateFields().then(async values => {
      form.resetFields();
      await dispatch!({
        type: "languageText/updateLanguageText",
        payload: {
          params: {...languageText},
          value:values.value,
        },
      })
      await onSubmit();
      onCancel();

    })
  }
  form.setFieldsValue({ ...languageText })
  return (
    <Modal onOk={handleOk} okText={intl("Save")} cancelText={intl("Close")} onCancel={onCancel} visible={visible} title="修改Identityuser">
      <Form form={form} layout="vertical">
        <Form.Item name="name" label={intl("Key")}
          rules={[{
            required: true,
            message: intl("ThisFieldIsRequired")
          }]} >
          <Input disabled />
        </Form.Item>
        <Form.Item name="baseValue" label={intl("BaseValue")} >
          <TextArea disabled />
        </Form.Item>
        <Form.Item name="value" label={intl("TargetValue")} >
          <TextArea />
        </Form.Item>
      </Form>
    </Modal>
  )
}

export default connect()(UpdateForm);
