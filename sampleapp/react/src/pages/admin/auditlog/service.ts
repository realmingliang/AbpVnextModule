import request from "@/utils/request";
import { GetAuditLogsInput } from "./data";


export async function queryAuditLogs(params?: GetAuditLogsInput): Promise<any> {
  return request('api/audit-logging/audit-logs', {
    method: 'GET',
    params,
})}

export async function getAuditLog(input?: string): Promise<any> {
  return request(`api/audit-logging/audit-logs/${input}`, {
    method: 'GET',
})}
