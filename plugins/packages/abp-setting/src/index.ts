import { IApi } from 'umi';
import { join } from 'path';
import getContextContent from './utils/getContextContent';
import getSettingProviderContent from './utils/getSettingProviderContent';
import getSettingContent from './utils/getSettingContent';
import getRootContainerContent from './utils/getRootContainerContent';
import { checkIfHasDefaultExporting } from './utils';

const setting_DIR = 'plugin-abp-setting'; // plugin-setting 插件创建临时文件的专有文件夹

export default function(api: IApi) {
  const umiTmpDir = api.paths.absTmpPath;
  const srcDir = api.paths.absSrcPath;
  const settingFilePath = api.utils.winPath(join(srcDir!, 'setting'));
  api.describe({
    key: "abpsetting",
    enableBy: api.EnableBy.register,
  })
  api.onGenerateFiles(() => {
    // 判断 setting 工厂函数存在并且 default 暴露了一个函数
    if (checkIfHasDefaultExporting(settingFilePath)) {
      // 创建 setting 的 context 以便跨组件传递 setting 实例
      api.writeTmpFile({
        path: `${setting_DIR}/context.ts`,
        content: getContextContent(),
      });

      // 创建 settingProvider，1. 生成 setting 实例; 2. 传给 context 的 Provider
      api.writeTmpFile({
        path: `${setting_DIR}/settingProvider.ts`,
        content: getSettingProviderContent(api.utils),
      });

      // 创建 setting 的 hook
      api.writeTmpFile({
        path: `${setting_DIR}/setting.tsx`,
        content: getSettingContent(),
      });

      // 生成 rootContainer 运行时配置
      api.writeTmpFile({
        path: `${setting_DIR}/rootContainer.ts`,
        content: getRootContainerContent(),
      });
    }
  });

  if (checkIfHasDefaultExporting(settingFilePath)) {
    // 增加 rootContainer 运行时配置
    api.addRuntimePlugin(() =>
      api.utils.winPath(join(umiTmpDir!, setting_DIR, 'rootContainer.ts')),
    );

    api.addUmiExports(() => [
      {
        exportAll: true,
        source: `../${setting_DIR}/setting`,
      },
    ]);

    api.addTmpGenerateWatcherPaths(() => [
      `${settingFilePath}.ts`,
      `${settingFilePath}.js`,
    ]);
  }
}
