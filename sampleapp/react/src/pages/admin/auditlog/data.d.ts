
import { ApplicationConfiguration } from '@/models/global';
import { PageRequestDto } from "../../../services/data.d";

export enum HttpStateCode {
  100, 101, 102, 103, 200, 201, 202, 203, 204, 205, 206, 207, 208, 226, 300, 300, 301, 301, 302, 302, 303, 303, 304, 305, 306, 307, 307, 308, 400, 401, 402, 403, 404, 405, 406, 407, 408, 409, 410, 411, 412, 413, 414, 415, 416, 417, 421, 422, 423, 424, 426, 428, 429, 431, 451, 500, 501, 502, 503, 504, 505, 506, 507, 508, 510, 511
}
export interface GetAuditLogsInput extends PageRequestDto {
  sorting?: string;
  url?: string;
  userName?: string;
  applicationName?: string;
  correlationId?: string;
  httpMethod?: string;
  httpStatusCode?: number;
  maxExecutionDuration?: number;
  minExecutionDuration?: number;
  hasException?: boolean;
}

export interface AuditLogDto {
  applicationName?: string;
  userId?: string;
  userName?: string;
  tenantId?: string;
  tenantName?: string;
  impersonatorUserId?: string;
  impersonatorTenantId?: string;
  executionTime?: Date;
  executionDuration?: number;
  clientIpAddress?: string;
  clientName?: string;
  clientId?: string;
  correlationId?: string;
  browserInfo?: string;
  httpMethod?: string;
  url?: string;
  exceptions?: string;
  comments?: string;
  httpStatusCode?: HttpStateCode;
  extraProperties?: ApplicationConfiguration.Dictionary<string>[];
  entityChanges?: EntityChangeDto[];
  actions?: AuditLogActionDto[];
  id?: string;
}

export interface AuditLogActionDto {
  tenantId: string;
  auditLogId: string;
  serviceName: string;
  methodName: string;
  parameters: string;
  executionTime: Date;
  executionDuration: number;
  extraProperties: ApplicationConfiguration.Dictionary<string>[];
  id: string;
}

export interface EntityChangeDto {
  auditLogId: string;
  tenantId: string;
  changeTime: Date;
  changeType: number;
  entityTenantId: string;
  entityId: string;
  entityTypeFullName: string;
  propertyChanges: EntityPropertyChangeDto[];
  extraProperties: ApplicationConfiguration.Dictionary<string>[];
  id: string;
}
export interface EntityPropertyChangeDto {
  tenantId: string;
  entityChangeId: string;
  newValue: string;
  originalValue: string;
  propertyName: string;
  propertyTypeFullName: string;
  id: string;

}
