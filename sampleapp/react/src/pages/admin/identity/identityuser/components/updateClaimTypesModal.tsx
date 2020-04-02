import { Modal, Form, Input, Row, Col, Button } from "antd";
import React from "react";
import { DeleteOutlined, SaveOutlined } from "@ant-design/icons";
import { IdentityUserClaimDto } from "../data";

interface UpdateClaimTypesModalProps {
  visible: boolean;
  onCancel: () => void;
  claimTypes: IdentityUserClaimDto[]
}
const UpdateClaimTypesModal: React.FC<UpdateClaimTypesModalProps> = ({ visible, onCancel, claimTypes }) => {

  return (
    <Modal title="声明" visible={visible} onCancel={onCancel}>
      <Form layout="vertical">
        <Row gutter={12}>
          <Col span={12} >
            <Form.Item label="类型" name="claimType">
              <Input />
            </Form.Item>
          </Col>
          <Col span={12}>
            <Form.Item label="值" name="claimValue">
              <Input />
            </Form.Item>
          </Col>
          <Col span={24}>
            <Form.Item >
              <Button style={{ width: '100%' }} type="primary">添加声明</Button>
            </Form.Item>

          </Col>
        </Row>
        {
          claimTypes.map(item =>
            <Input addonBefore={item.claimType}
              addonAfter={<><Button type="primary" icon={<SaveOutlined />} /><Button type="danger" icon={<DeleteOutlined />}/></>}
              defaultValue={item.claimValue} />)
        }


      </Form>
    </Modal>
  )
}

export default UpdateClaimTypesModal;
