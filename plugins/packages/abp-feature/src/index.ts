import { IApi } from 'umi';
import { join } from 'path';
import getContextContent from './utils/getContextContent';
import getFeatrueProviderContent from './utils/getFeatureProviderContent';
import getFeatureContent from './utils/getContextContent';
import getRootContainerContent from './utils/getRootContainerContent';
import { checkIfHasDefaultExporting } from './utils';

const feature_DIR = 'plugin-abp-feature'; // plugin-feature 插件创建临时文件的专有文件夹

export default function(api: IApi) {
  const umiTmpDir = api.paths.absTmpPath;
  const srcDir = api.paths.absSrcPath;
  const settingFilePath = api.utils.winPath(join(srcDir!, 'feature'));

  api.onGenerateFiles(() => {
    // 判断 feature 工厂函数存在并且 default 暴露了一个函数
    if (checkIfHasDefaultExporting(settingFilePath)) {
      // 创建 feature 的 context 以便跨组件传递 feature 实例
      api.writeTmpFile({
        path: `${feature_DIR}/context.ts`,
        content: getContextContent(),
      });

      // 创建 featureProvider，1. 生成 feature 实例; 2. 遍历修改 routes; 3. 传给 context 的 Provider
      api.writeTmpFile({
        path: `${feature_DIR}/featureProvider.ts`,
        content: getFeatrueProviderContent(api.utils),
      });

      // 创建 feature 的 hook
      api.writeTmpFile({
        path: `${feature_DIR}/feature.tsx`,
        content: getFeatureContent(),
      });

      // 生成 rootContainer 运行时配置
      api.writeTmpFile({
        path: `${feature_DIR}/rootContainer.ts`,
        content: getRootContainerContent(),
      });
    }
  });

  if (checkIfHasDefaultExporting(settingFilePath)) {
    // 增加 rootContainer 运行时配置
    api.addRuntimePlugin(() =>
      api.utils.winPath(join(umiTmpDir!, feature_DIR, 'rootContainer.ts')),
    );

    api.addUmiExports(() => [
      {
        exportAll: true,
        source: `../${feature_DIR}/feature`,
      },
    ]);

    api.addTmpGenerateWatcherPaths(() => [
      `${settingFilePath}.ts`,
      `${settingFilePath}.js`,
    ]);
  }
}
