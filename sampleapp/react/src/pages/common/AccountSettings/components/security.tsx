
import React, { Component, Fragment } from 'react';

import { Form, Input, Row, Col, Button, Spin, message } from 'antd';
import { connect } from 'dva';
import { ConnectState } from '@/models/connect';
import { Dispatch } from 'redux';
import { ProfileDto } from '../data';

interface SecurityViewProps {
  dispatch: Dispatch;
  myProfile: ProfileDto;
  loading?: boolean;
}

class SecurityView extends Component<SecurityViewProps> {

  submitHandle = (values: any) => {
    const { dispatch } = this.props;
    dispatch({
      type: 'accountSettings/updateMyProfile',
      payload: values,
    })
    message.success('操作成功！');
  }

  render() {
    const { myProfile, loading } = this.props;
    return (
      <Fragment>
        <Spin spinning={loading}>
          <Form layout='vertical'
            initialValues={myProfile}
            onFinish={this.submitHandle}>
            <Form.Item label="用户名" name='userName'
              rules={[{
                required: true,
                message: '用户名不能为空！',
              }]}>
              <Input />
            </Form.Item>
            <Row gutter={24}>
              <Col span={12}>
                <Form.Item label="名称" name='name'>
                  <Input />
                </Form.Item>
              </Col>
              <Col span={12}>
                <Form.Item label="完整名称" name='sureName'>
                  <Input />
                </Form.Item>
              </Col>
            </Row>
            <Form.Item label="邮箱地址" name='email'
              rules={[{
                required: true,
                message: '邮箱地址不能为空！',
              }]}>
              <Input />
            </Form.Item>
            <Form.Item label="手机号码" name='phoneNumber'>
              <Input />
            </Form.Item>
            <Form.Item>
              <Button type="primary" htmlType="submit">
                保存设置
              </Button>
            </Form.Item>
          </Form>
        </Spin>
      </Fragment>
    );
  }
}

export default connect(({ accountSettings, loading }: ConnectState) => ({
  myProfile: accountSettings.myProfile,
  loading: loading.effects['accountSettings/getMyProfile'],
}))(SecurityView);
