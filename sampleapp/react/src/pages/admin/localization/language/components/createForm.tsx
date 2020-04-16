import React from "react";
import { Modal, Form, Input, Select, Checkbox } from "antd";
import { useLocale, connect, ConnectProps } from "umi";
import { Flags,CultureNames,UiCultureNames} from "../culture";

interface CreateFormProps extends ConnectProps {
  visible: boolean;
  onCancel: () => void;
}
const { Option } = Select;
const CreateForm: React.FC<CreateFormProps> = props => {
  const { visible, onCancel,dispatch } = props;
  const [form] = Form.useForm();
  const intl = useLocale("LanguageManagement");
  const handleOk=()=>{
    form.validateFields().then(values=>{
      form.resetFields();
      dispatch!({
        type:"language/createLanguage",
        payload:values,
      })
      onCancel();
    })
  }
  return (
    <Modal onOk={handleOk} okText={intl("Save")} cancelText={intl("Close")} onCancel={onCancel} visible={visible} title={intl("CreateNewLanguage")}>
      <Form form={form} layout="vertical">
        <Form.Item name="cultureName" label={intl("CultureName")}
          rules={[{
            required: true,
            message: intl("ThisFieldIsRequired")
          }]} >
          <Select >
            {CultureNames.map(item => <Option key={item} value={item}>{item}</Option>)}
          </Select>
        </Form.Item>
        <Form.Item name="uiCultureName" label={intl("UiCultureName")}
          rules={[{
            required: true,
            message: intl("ThisFieldIsRequired")
          }]} >
          <Select >
            {UiCultureNames.map(item => <Option key={item} value={item}>{item}</Option>)}
          </Select>
        </Form.Item>
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
        <Form.Item name="isEnabled">
          <Checkbox /> {intl("IsEnabled")}
        </Form.Item>
      </Form>
    </Modal>
  )
}

export default connect()(CreateForm);
