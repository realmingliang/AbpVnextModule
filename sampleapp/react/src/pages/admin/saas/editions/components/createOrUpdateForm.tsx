import { Modal, Form, Input, message } from "antd";
import React from "react";
import { useRequest } from "@umijs/hooks";
import { SaasEditionDto, EditionUpdateDto, EditionCreateDto } from "../data";
import { updateEdition, createEdition } from "../service";

interface CreateOrUpdateFormProps {
  visible: boolean;
  onCancel: () => void;
  onSubmit: () => void;
  editEdition: SaasEditionDto;
}

const CreateOrUpdateForm: React.FC<CreateOrUpdateFormProps> = props => {
  const { visible, onCancel, editEdition, onSubmit } = props;
  const [form] = Form.useForm();
  const title = editEdition === null ? "新增版本" : "编辑版本";
  const { run: doUpdateEdition } = useRequest(updateEdition, {
    manual: true,
    onSuccess: () => {
      message.success("操作成功！");
      onCancel();
      onSubmit();
    }
  });
  const { run: doCreateEdition } = useRequest(createEdition, {
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
      if (editEdition.id !== "") {
        doUpdateEdition(
          editEdition?.id,
          values as EditionUpdateDto,
        )
      } else {
        doCreateEdition(
          values as EditionCreateDto,
        )
      }
    })
  }
  const handleCancel=()=>{
    form.resetFields();
    onCancel();
  }
  form.setFieldsValue({...editEdition})
  return (
    <Modal destroyOnClose onOk={handleOk} title={title} visible={visible} onCancel={handleCancel}>
      <Form form={form} layout="vertical"  name="create_or_update_form">
        <Form.Item
          label="名称"
          name="displayName"
          rules={[{
            required: true,
            message: "名称不能为空!"
          }]}>
          <Input />
        </Form.Item>
      </Form>
    </Modal>
  )
};
export default CreateOrUpdateForm;
