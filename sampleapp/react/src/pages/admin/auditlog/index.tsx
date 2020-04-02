import { PageHeaderWrapper } from "@ant-design/pro-layout";
import React, { useRef, useState } from "react";
import ProTable, { ActionType, ProColumns } from "@ant-design/pro-table";
import { Button, Tag } from "antd";
import { SearchOutlined } from "@ant-design/icons";
import { connect } from "dva";
import { ConnectState } from "@/models/connect";
import { Dispatch } from "redux";
import { AuditLogDto } from './data.d';
import { queryAuditLogs } from "./service";
import styles from './style.less';
import DetailAuditLog from "./component/detailAuditLog";

const HttpStateCode = {
  '100': '100 - Continue',
  '101': '101 - Switching Protocols',
  '102': '102 - Processing',
  '103': '103 - Early Hints',
  '200': '200 - OK',
  '201': '201 - Created',
  '202': '202 - Accepted',
  '203': '203 - Non-authoritative Information',
  '204': '204 - No Content',
  '205': '205 - Reset Content',
  '400': '400 - Bad Request',
  '401': '401 - Unauthorized',
  '403': '403 - Forbidden',
  '404': '404 - Not Found',
  '500': '500 - Internal Server Error',
  '501': '501 - Not Implemented',
  '502': '502 - Bad Gateway'
}
interface AuditLoggingProps{
  dispatch:Dispatch;
}

const AuditLogging: React.FC<AuditLoggingProps> = props => {
  const { dispatch } = props;
  const actionRef = useRef<ActionType>();
  const [modalVisible, handleModalVisible] = useState<boolean>(false);
  const handleAuditLogDetail=(id:string)=>{
    dispatch({
      type:'auditLog/getAuditLog',
      payload:id,
    })
    handleModalVisible(true)
  }
  const columns: ProColumns<AuditLogDto>[] = [
    {
      title: '操作',
      render: (_,record) => <Button onClick={()=>handleAuditLogDetail(record.id!)} className={styles.detailbtn} icon={<SearchOutlined />} type='primary' />
    }, {
      title: 'Http 请求',
      dataIndex: 'httpStatusCode',
      valueEnum: HttpStateCode,
      width: 450,
      render: (_, record) => {
        let iconColor = "";
        let httpColor = "";
        if (record.httpStatusCode! >= 200 && record.httpStatusCode! < 300)
          iconColor = '#28a745';
        else if (record.httpStatusCode! >= 400 && record.httpStatusCode! < 500)
          iconColor = '#dc3545';
        else
          iconColor = '#2db7f5'

        switch (record.httpMethod) {
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

        return <><Tag className={styles.tag} color={iconColor}>{record.httpStatusCode}</Tag>
          <Tag className={styles.tag} color={httpColor}>{record.httpMethod}</Tag>{record.url}
        </>

      }
    },
    {
      title: '用户名',
      dataIndex: 'userName',
    }, {
      title: 'IP地址',
      dataIndex: 'clientIpAddress',
    }, {
      title: '操作时间',
      dataIndex: 'executionTime',
      valueType: 'date',
    },
    {
      title: '持续时间',
      dataIndex: 'executionDuration',
      hideInSearch: true,
    },
    {
      title: '服务名称',
      dataIndex: 'applicationName',
    },
  ]
  return (
    <PageHeaderWrapper>
      <ProTable<AuditLogDto>
        headerTitle="审计日志"
        actionRef={actionRef}
        rowKey="id"
        request={async (params = {}) => {
          const response = await queryAuditLogs({ skipCount: (params.current! - 1) * params.pageSize!, maxResultCount: params.pageSize });
          const data = response.items;
          return {
            data,
            page: params.current,
            success: true,
            total: response.totalCount,
          }
        }}
        columns={columns}
      />
      <DetailAuditLog
        modalvislble={modalVisible}
        onCancel={() => handleModalVisible(false)} />
    </PageHeaderWrapper>
  )
}

export default connect(({ auditLog, loading }: ConnectState) => ({
  auditlog: auditLog.auditlog,
  loading: loading.effects['auditLog/getAuditLog']
}))(AuditLogging);;
