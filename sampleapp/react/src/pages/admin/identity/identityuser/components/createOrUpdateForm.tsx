import React from "react";
import Modal from "antd/lib/modal/Modal";
import { Tabs, Form, Checkbox, Input, Button } from "antd";
import { connect } from "dva";
import { IdentityUserCreateOrUpdateDto } from "../data";
import { IdentityRoleDto } from "../../identityrole/data";
import { Dispatch, useLocale } from "umi";

const { TabPane } = Tabs;
interface CreateOrUpdateFormProps {
  onCancel: () => void;
  modalVisible: boolean;
  formValues?: IdentityUserCreateOrUpdateDto;
  allRoles: IdentityRoleDto[];
  onSubmit: () => void;
  dispatch?: Dispatch
}

const CreateOrUpdateForm: React.FC<CreateOrUpdateFormProps> = props => {
  const { modalVisible, onCancel, formValues, allRoles, dispatch, onSubmit } = props;
  const options = allRoles?.map(item => ({ label: item.name, value: item.name }))
  const intl = useLocale("AbpIdentity");

  /**
   * modal确认按钮处理
   */
  const handleFormonFinish = async (values: any) => {
    if (formValues?.id !== undefined) {
      await dispatch!({
        type: 'identityUser/updateUser',
        payload: {
          ...values,
          id: formValues.id,
        }
      })
    } else {
      await dispatch!({
        type: 'identityUser/createUser',
        payload: values,
      })
    }
    onSubmit();
    onCancel();
  }

  return (
    <Modal
      title={formValues?.id === undefined ? intl("NewUser") : `${intl("Edit")}:${formValues.name}`}
      onCancel={onCancel}
      destroyOnClose
      footer={null}
      visible={modalVisible}>
      <Form
        layout="vertical"
        onFinish={handleFormonFinish}
        initialValues={formValues}
      ><Tabs>
          <TabPane tab={intl("Users")} key='User'>
            <Form.Item name="userName" label="用户名"
              rules={[{
                required: true,
                message: '用户名不能为空！',
              }]} required>
              <Input placeholder="请输入" />
            </Form.Item>
            <Form.Item name="name" label="名称">
              <Input placeholder="请输入" />
            </Form.Item>
            <Form.Item name="sureName" label="完整名称">
              <Input placeholder="请输入" />
            </Form.Item>
            {
              formValues?.id === undefined ? (<Form.Item name="password" label="密码"
                rules={[{
                  required: true,
                  message: '密码不能为空！',
                }]} required>
                <Input type="password" placeholder="请输入" />
              </Form.Item>) : null
            }
            <Form.Item name="email" label="邮箱地址" rules={[{
              required: true,
              message: '邮箱地址不能为空！',
            }, {
              type: 'email',
              message: '请输入正确格式的邮箱地址！'
            }]} required>
              <Input placeholder="请输入" />
            </Form.Item>
            <Form.Item name="phoneNumber" label="手机号">
              <Input placeholder="请输入" />
            </Form.Item>
            <Form.Item label="" valuePropName="checked" name="lockoutEnabled">
              <Checkbox >登录失败后锁定帐户</Checkbox>
            </Form.Item>
            <Form.Item label="" valuePropName="checked" name="twoFactorEnabled" >
              <Checkbox >双重身份认证</Checkbox>
            </Form.Item>
          </TabPane>
          <TabPane tab='角色' key='Role'>
            <Form.Item label="" name="roleNames" >
              <Checkbox.Group options={options} />
            </Form.Item>
          </TabPane>
        </Tabs>
        <Form.Item style={{ textAlign: 'right' }}>
          <Button onClick={onCancel} style={{ marginRight: 8 }} type="default" >
            取消
         </Button>
          <Button type="primary" htmlType="submit">
            提交
        </Button>
        </Form.Item>
      </Form>
    </Modal>
  )

}
export default connect()(CreateOrUpdateForm);
