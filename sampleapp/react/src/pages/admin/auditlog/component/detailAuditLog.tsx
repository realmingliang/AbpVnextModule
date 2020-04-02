import React from "react";
import { Modal, Tabs, Button, Skeleton, Descriptions, Badge, Typography, Collapse } from "antd";
import { connect } from 'dva';
import { ConnectState } from "@/models/connect";
import _ from 'lodash';
import { AuditLogDto } from "../data.d";

const { Paragraph } = Typography;
const { TabPane } = Tabs;
const { Panel } = Collapse;
interface DetailAuditLogProps {
  modalvislble: boolean;
  onCancel: () => void;
  auditlog?: AuditLogDto;
  loading?: boolean;
}

const DetailAuditLog: React.FC<DetailAuditLogProps> = props => {
  const { modalvislble, onCancel, loading, auditlog } = props;
  let iconColor; let httpColor = '';
  if (auditlog!.httpStatusCode! >= 200 && auditlog!.httpStatusCode! < 300)
    iconColor = '#28a745';
  else if (auditlog!.httpStatusCode! >= 400 && auditlog!.httpStatusCode! < 500)
    iconColor = '#dc3545';
  else
    iconColor = '#2db7f5'
  switch (auditlog!.httpMethod) {
    case 'GET':
      httpColor = '#343a40';
      break;
    case 'POST':
      httpColor = '#28a745';
      break;
    case 'DELETE':
      httpColor = '#dc3545';
      break;
    case 'PUT':
      httpColor = '#ffc107';
      break;
    default:
      httpColor = '#2db7f5';
  }
  return (
    <Modal title='详情'
      visible={modalvislble}
      width={900}
      onCancel={onCancel}
      footer={[
        <Button key="cancel" onClick={onCancel}>
          取消
            </Button>
      ]}>
      <Skeleton loading={loading}>
        <Tabs>
          <TabPane tab='全部信息' key='overall'>
            <Descriptions title="" bordered>
              <Descriptions.Item label="Http状态码"><Badge color={iconColor} text={auditlog!.httpStatusCode} /></Descriptions.Item>
              <Descriptions.Item label="Http方法"><Badge color={httpColor} text={auditlog!.httpMethod} /></Descriptions.Item>
              <Descriptions.Item label="客户端名称">
                {auditlog!.clientName}
              </Descriptions.Item>
              <Descriptions.Item label="IP地址">{auditlog!.clientIpAddress}</Descriptions.Item>

              <Descriptions.Item label="Url" span={2}>{auditlog!.url}</Descriptions.Item>
              <Descriptions.Item label="异常信息" span={3}>
                {auditlog!.exceptions}
              </Descriptions.Item>
              <Descriptions.Item label="用户名"> {auditlog!.userName}</Descriptions.Item>
              <Descriptions.Item label="操作时间"> {auditlog!.executionTime}</Descriptions.Item>
              <Descriptions.Item label="持续时长"> {auditlog!.executionDuration}</Descriptions.Item>
              <Descriptions.Item label="应用名称"> {auditlog!.applicationName}</Descriptions.Item>
              <Descriptions.Item label="关联Id" span={2}> {auditlog!.correlationId}</Descriptions.Item>
              <Descriptions.Item span={3} label="浏览器信息">
                <Paragraph ellipsis={{ rows: 3 }}>{auditlog!.browserInfo}</Paragraph>
              </Descriptions.Item>
              <Descriptions.Item span={3} label="额外的属性">
                {JSON.stringify(auditlog!.extraProperties) === "{}" ? '{}' : (
                  _.forEach(auditlog!.extraProperties, (value, key) => <p>{key}:{value}</p>)
                )}
              </Descriptions.Item>
            </Descriptions>
          </TabPane>
          <TabPane tab='请求' key='action'>
            {auditlog!.actions?.length === 0 ? null : (
              <Collapse >
                {auditlog!.actions?.map(item => (
                  <Panel header={item.serviceName} key={item.id}>
                    <Descriptions title="" >
                      <Descriptions.Item label="持续时间" span={3}> {item.executionDuration}</Descriptions.Item>
                      <Descriptions.Item span={3} label="额外属性">
                        {item.parameters}
                      </Descriptions.Item>
                    </Descriptions>
                  </Panel>
                ))}
              </Collapse>)}
          </TabPane>
          <TabPane tab='实体更改' key='change'>
            {auditlog!.entityChanges?.length === 0 ? '没有实体变更记录！' : (
              <Collapse >
                {auditlog!.entityChanges?.map(item => (
                  <Panel header={item.entityTypeFullName} key={item.id}>
                    <Descriptions title="" >
                      <Descriptions.Item label="更改时间" span={3}> {item.changeTime}</Descriptions.Item>
                      <Descriptions.Item span={3} label="更改内容">
                        {item.propertyChanges.length === 0 ? 'null' : (
                          _.forEach(item.propertyChanges, value => <p>{value.propertyName}:{value.originalValue} = &gt; {value.newValue}</p>)
                        )}
                      </Descriptions.Item>
                      <Descriptions.Item span={3} label="额外属性">
                        {JSON.stringify(item.extraProperties) === "{}" ? 'null' : (
                          _.forEach(item.extraProperties, (value, key) => <p>{key}:{value}</p>)
                        )}
                      </Descriptions.Item>
                    </Descriptions>
                  </Panel>
                ))}
              </Collapse>
            )}
          </TabPane>
        </Tabs>
      </Skeleton>
    </Modal>
  )
}

export default connect(({ auditLog, loading }: ConnectState) => ({
  auditlog: auditLog.auditlog,
  loading: loading.effects['auditLog/getAuditLog']
}))(DetailAuditLog);
