import { Modal, Form, Input, Checkbox, message, Button } from 'antd';
import React from 'react';
import { useRequest } from '@umijs/hooks';
import { IdentityRoleDto } from '../data';
import { createRole, updateRole } from '../service';

interface CreateOrUpdateFormProps {
  visible: boolean;
  onCancel: () => void;
  editRole?: IdentityRoleDto;
}
const CreateOrUpdateForm: React.FC<CreateOrUpdateFormProps> = props => {
  const { visible, onCancel, editRole } = props;
  const { run: create } = useRequest(createRole, {
    manual: true,
    onSuccess: async () => {
      message.success("操作成功!")
    }
  })
  const { run: update } = useRequest(updateRole, {
    manual: true,
    onSuccess: async () => {
      message.success("保存成功!")
    }
  })
  const formFinish = (values: any) => {
    if (editRole) {
      update(editRole.id, { ...values as any, concurrencyStamp: editRole.concurrencyStamp });
    } else {
      create(values as any)
    }
    onCancel();
  }
  return (
    <Modal footer={null} destroyOnClose onCancel={onCancel} title={editRole ? "编辑角色" : "新增角色"} visible={visible}>
      <Form name="role-form" onFinish={formFinish} initialValues={editRole} layout="vertical" >
        <Form.Item name="name" label="名称"
          rules={[{
            required: true,
            message: "名称不能为空!"
          }]} >
          <Input />
        </Form.Item>
        <Form.Item valuePropName="checked" style={{ marginBottom: 10 }} name="isDefault">
          <Checkbox>默认</Checkbox>
        </Form.Item>
        <Form.Item valuePropName="checked" name="isPublic" >
          <Checkbox>公开</Checkbox>
        </Form.Item>
        <Form.Item style={{ textAlign: 'right' }}>
          <Button onClick={onCancel} style={{marginRight:8}} type="default" >
            取消
          </Button>
          <Button type="primary" htmlType="submit">
            提交
          </Button>
        </Form.Item>
      </Form>
    </Modal>
  );
};
export default CreateOrUpdateForm;
