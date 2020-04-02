import { UploadOutlined } from '@ant-design/icons';
// import '@ant-design/compatible/assets/index.css';
import { Button, Input, Upload, Form } from 'antd';
import React, { Component, Fragment } from 'react';
import { Dispatch } from 'redux';
import { connect } from 'dva';
import styles from './ChangePassowrd.less';

const FormItem = Form.Item;

// 头像组件 方便以后独立，增加裁剪之类的功能
const AvatarView = () => (
  <Fragment>
    <div className={styles.avatar_title}>Logo</div>
    <div className={styles.avatar}>
      <img src="https://gw.alipayobjects.com/zos/rmsportal/BiazfanxmamNRoxxVxka.png" alt="avatar" />
    </div>
    <Upload showUploadList={false}>
      <div className={styles.button_view}>
        <Button>
          <UploadOutlined />
          上传头像
        </Button>
      </div>
    </Upload>
  </Fragment>
);

interface ChangePasswordViewViewProps {
  dispatch: Dispatch;
}

class ChangePasswordView extends Component<ChangePasswordViewViewProps> {
  view: HTMLDivElement | undefined = undefined;

  getViewDom = (ref: HTMLDivElement) => {
    this.view = ref;
  };

  onFinish = (values: any) => {
    const { dispatch } = this.props;
    dispatch({
      type: 'accountSettings/changePassword',
      payload: values,
    });
  };

  render() {
    return (
      <div className={styles.baseView} ref={this.getViewDom}>
        <div className={styles.left}>
          <Form layout="vertical" hideRequiredMark onFinish={this.onFinish}>
            <Form.Item
              label="当前密码"
              name="currentPassword"
              rules={[
                {
                  required: true,
                  message: '当前密码不能为空!',
                },
              ]}
            >
              <Input type="password" />
            </Form.Item>
            <FormItem
              label="新密码"
              hasFeedback
              name="newPassword"
              rules={[
                {
                  required: true,
                  message: '新密码不能为空！',
                },
              ]}
            >
              <Input type="password" />
            </FormItem>
            <FormItem
              label="确认密码"
              name="confirmPassword"
              dependencies={['newPassword']}
              hasFeedback
              rules={[
                {
                  required: true,
                  message: '确认密码不能为空！',
                },
                ({ getFieldValue }) => ({
                  validator(rule, value) {
                    if (!value || getFieldValue('newPassword') === value) {
                      return Promise.resolve();
                    }
                    // eslint-disable-next-line prefer-promise-reject-errors
                    return Promise.reject('两次密码输入不一致!');
                  },
                }),
              ]}
            >
              <Input type="password" />
            </FormItem>
            <Form.Item>
              <Button type="primary" htmlType="submit">
                保存设置
              </Button>
            </Form.Item>
          </Form>
        </div>
        <div className={styles.right}>
          <AvatarView />
        </div>
      </div>
    );
  }
}

export default connect()(ChangePasswordView);
