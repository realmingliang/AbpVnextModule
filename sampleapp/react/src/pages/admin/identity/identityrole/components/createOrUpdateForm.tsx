import { Modal, Form, Input, Checkbox, Button } from 'antd';
import React from 'react';
import { IdentityRoleDto } from '../data';
import { Dispatch } from 'redux';
import { connect } from 'umi';

interface CreateOrUpdateFormProps  {
  visible: boolean;
  onCancel: () => void;
  editRole?: IdentityRoleDto;
  dispatch?:Dispatch
}
const CreateOrUpdateForm: React.FC<CreateOrUpdateFormProps> = props => {
  const { visible, onCancel, editRole,dispatch } = props;
  const formFinish = (values: any) => {
    if (editRole) {
      dispatch!({
        type: 'identityRole/updateRole',
        payload:{id:editRole.id,data:{...values, concurrencyStamp: editRole.concurrencyStamp}}
      })
    } else {
      dispatch!({
        type: 'identityRole/createRole',
        payload: values
      })
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
export default connect()(CreateOrUpdateForm);
