import { LanguageTextDto, GetLanguageTextInput } from "./data";
import React, { useRef, useState, useEffect } from "react";
import ProTable, { ActionType, ProColumns } from "@ant-design/pro-table";
import { useLocale, useAccess, ConnectProps, connect, useModel } from "umi";
import LanguageManagement from "./permissionName";
import { SettingOutlined, DownOutlined } from "@ant-design/icons";
import { Button, Dropdown, Menu, Card, Form, Row, Col, Input, Select } from "antd";
import UpdateForm from './components/updateForm';
import { ModalState } from "./model";
import _ from 'lodash';
import { PagedResultDto } from "@/services/data";
import { PaginationConfig } from "antd/lib/pagination";


const { Option } = Select;
const { Search } = Input;
interface LanguageTextProps extends ConnectProps {
  LanguageTexts: PagedResultDto<LanguageTextDto>;
}


const LanguageText: React.FC<LanguageTextProps> = ({ dispatch, LanguageTexts }) => {
  const actionRef = useRef<ActionType>();
  const access = useAccess();
  const intl = useLocale("LanguageManagement");
  const [updateModalVisible, handleUpdateModalVisible] = useState<boolean>(false);
  const [languageText, setLanguageText] = useState<LanguageTextDto>();
  const { initialState } = useModel("@@initialState");
  const { localization } = initialState!;
  const [form] = Form.useForm();
  const defaultRequestParams: GetLanguageTextInput = {
    skipCount: 0,
    maxResultCount: 10,
    resourceName: _.keys(localization.values)[0],
    baseCultureName: localization.languages[0].cultureName,
    targetCultureName: localization.languages[1].cultureName,
    getOnlyEmptyValues: false,
  };
  const [requestParams, setRequestParams] = useState<GetLanguageTextInput>(defaultRequestParams);
  const getData = async () => {
    await dispatch!({
      type: "languageText/getLanguageTexts",
      payload: requestParams
    })
  }
  useEffect(() => {
    getData();
  }, [requestParams])
  const columns: ProColumns<LanguageTextDto>[] = [
    {
      title: intl("Actions"),
      render: (_, record) =>
        <Dropdown
          overlay={
            <Menu
              selectedKeys={[]}
            >
              {
                access[LanguageManagement.Edit] ?
                  <Menu.Item key="edit"
                    onClick={() => handleEdit(record)}>{intl("Edit")}
                  </Menu.Item> : null
              }
            </Menu>
          }
        >
          <Button type="primary">
            <SettingOutlined /> {intl("Actions")} <DownOutlined />
          </Button>
        </Dropdown>
    }, {
      title: intl("Key"),
      dataIndex: 'name',
      ellipsis: true
    }, {
      title: intl("BaseValue"),
      dataIndex: 'baseValue',
      ellipsis: true
    }, {
      title: intl("Value"),
      dataIndex: 'value',
      width: 100
    }, {
      title: intl("ResourceName"),
      dataIndex: 'resourceName',
    }
  ]
  const handleEdit = async (record: LanguageTextDto) => {
    await setLanguageText(record);
    await handleUpdateModalVisible(true);
  }

  const handleSearch = async (value: string) => {
    await setRequestParams({
      filter: value,
      ...form.getFieldsValue(),
    })
  }
  const handleTableChange = async (params: PaginationConfig) => {
    await setRequestParams(
      { ...requestParams, skipCount: (params.current! - 1) * params.pageSize!, maxResultCount: params.pageSize }
    )

  }
  return (
    <>
      <Card style={{ marginBottom: 10 }}>
        <Form initialValues={requestParams} form={form} layout="vertical">
          <Row gutter={24}>
            <Col span={6}>
              <Form.Item name="baseCultureName" label={intl("BaseCultureName")}>
                <Select>
                  {
                    localization.languages.map(item =>
                      <Option key={item.cultureName} value={item.cultureName}>{item.displayName}</Option>)
                  }
                </Select>
              </Form.Item>
            </Col>
            <Col span={6}>
              <Form.Item name="targetCultureName" label={intl("TargetCultureName")}>
                <Select>
                  {
                    localization.languages.map(item =>
                      <Option key={item.cultureName} value={item.cultureName}>{item.displayName}</Option>)
                  }
                </Select>
              </Form.Item>
            </Col>
            <Col span={6}>
              <Form.Item name="resourceName" label={intl("ResourceName")}>
                <Select>
                  {
                    _.keys(localization.values).map(item =>
                      <Option key={item} value={item}>{item}</Option>)
                  }
                </Select>
              </Form.Item>
            </Col>
            <Col span={6}>
              <Form.Item name="getOnlyEmptyValues" label={intl("TargetValue")}>
                <Select>
                  <Option key="true" value="true">{intl("OnlyEmptyValues")}</Option>
                  <Option key="false" value="false">{intl("All")}</Option>
                </Select>
              </Form.Item>
            </Col>
            <Col span={24}>
              <Search
                placeholder={intl("Filter")}
                enterButton={intl("Refresh")}
                onSearch={handleSearch}
              />
            </Col>
          </Row>
        </Form>
      </Card>
      <ProTable<LanguageTextDto>
        headerTitle={intl("Languages")}
        actionRef={actionRef}
        rowKey={record => record.name}
        search={false}
        onChange={handleTableChange}
        pagination={{ total: LanguageTexts.totalCount, pageSize: 10 }}
        dataSource={LanguageTexts.items}
        columns={columns}
      />
      <UpdateForm
        languageText={languageText!}
        visible={updateModalVisible}
        onSubmit={()=>getData()}
        onCancel={() => handleUpdateModalVisible(false)} />
    </>
  )
}
export default connect(
  ({
    languageText: { LanguageTexts },
    loading,
  }: {
    languageText: ModalState;
    loading: {
      effects: {
        [key: string]: boolean;
      };
    };
  }) => ({
    LanguageTexts,
  }),
)(LanguageText);
