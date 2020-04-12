import { IApi } from 'umi';
import getContextContent from './utils/getContextContent';
import getLocaleProviderContent from './utils/getLocaleProviderContent';
import getLocaleContent from './utils/getLocaleContent';
import getRootContainerContent from './utils/getRootContainerContent';
import { join } from 'path';
const locale_DIR = 'plugin-abp-locale'; // plugin-locale 插件创建临时文件的专有文件夹

export default (api: IApi) => {
  const umiTmpDir = api.paths.absTmpPath;

  api.onGenerateFiles(async () => {
    api.writeTmpFile({
      path: `${locale_DIR}/context.ts`,
      content: getContextContent(),
    });
    // 创建 localeProvider. 生成 locale 实例; 2. 遍历修改 routes; 3. 传给 context 的 Provider
    api.writeTmpFile({
      path: `${locale_DIR}/localeProvider.ts`,
      content: getLocaleProviderContent(api.utils),
    });

    // 创建 locale 的 hook
    api.writeTmpFile({
      path: `${locale_DIR}/locale.tsx`,
      content: getLocaleContent(),
    });

    // 生成 rootContainer 运行时配置
    api.writeTmpFile({
      path: `${locale_DIR}/rootContainer.ts`,
      content: getRootContainerContent(),
    });
  });

  api.addRuntimePlugin(() =>
    api.utils.winPath(join(umiTmpDir!, locale_DIR, 'rootContainer.ts')),
  );

  api.addUmiExports(() => [
    {
      exportAll: true,
      source: `../${locale_DIR}/locale`,
    },
  ]);

};
