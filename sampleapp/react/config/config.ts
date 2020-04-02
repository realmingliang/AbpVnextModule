import { IConfig, IPlugin } from 'umi-types';
import defaultSettings from './defaultSettings'; // https://umijs.org/config/

import aliyunTheme from '@ant-design/aliyun-theme';
import slash from 'slash2';
import themePluginConfig from './themePluginConfig';
import proxy from './proxy';
import webpackPlugin from './plugin.config';
const { pwa } = defaultSettings; // preview.pro.ant.design only do not use in your production ;
// preview.pro.ant.design 专用环境变量，请不要在你的项目中使用它。

const { ANT_DESIGN_PRO_ONLY_DO_NOT_USE_IN_YOUR_PRODUCTION, REACT_APP_ENV } = process.env;
const isAntDesignProPreview = ANT_DESIGN_PRO_ONLY_DO_NOT_USE_IN_YOUR_PRODUCTION === 'site';
const plugins: IPlugin[] = [
  ['umi-plugin-antd-icon-config', {}],
  [
    'umi-plugin-react',
    {
      antd: true,
      dva: {
        hmr: true,
      },
      locale: {
        // default false
        enable: true,
        // default zh-CN
        default: 'zh-CN',
        // default true, when it is true, will use `navigator.language` overwrite default
        baseNavigator: true,
      },
      dynamicImport: {
        loadingComponent: './components/PageLoading/index',
        webpackChunkName: true,
        level: 3,
      },
      pwa: pwa
        ? {
            workboxPluginMode: 'InjectManifest',
            workboxOptions: {
              importWorkboxFrom: 'local',
            },
          }
        : false, // default close dll, because issue https://github.com/ant-design/ant-design-pro/issues/4665
      // dll features https://webpack.js.org/plugins/dll-plugin/
      // dll: {
      //   include: ['dva', 'dva/router', 'dva/saga', 'dva/fetch'],
      //   exclude: ['@babel/runtime', 'netlify-lambda'],
      // },
    },
  ],
  [
    'umi-plugin-pro-block',
    {
      moveMock: false,
      moveService: false,
      modifyRequest: true,
      autoAddMenu: true,
    },
  ],
];

if (isAntDesignProPreview) {
  // 针对 preview.pro.ant.design 的 GA 统计代码
  plugins.push([
    'umi-plugin-ga',
    {
      code: 'UA-72788897-6',
    },
  ]);
  plugins.push([
    'umi-plugin-pro',
    {
      serverUrl: 'https://ant-design-pro.netlify.com',
    },
  ]);
  plugins.push(['umi-plugin-antd-theme', themePluginConfig]);
}
export default {
  plugins,
  hash: true,
  targets: {
    ie: 11,
  },
  // umi routes: https://umijs.org/zh/guide/router.html
  routes: [
    {
      path: '/account',
      component: '../layouts/UserLayout',
      routes: [
        {
          name: 'login',
          path: '/account/login',
          component: './account/login',
        },
      ],
    },
    {
      path: '/',
      component: '../layouts/SecurityLayout',
      routes: [
        {
          path: '/',
          component: '../layouts/BasicLayout',
          routes: [
            {
              path: '/',
              redirect: '/welcome',
            },
            {
              path: '/welcome',
              name: '首页',
              icon: 'smile',
              component: './Welcome',
            },
            {
              path: '/admin',
              name: '管理',
              icon: 'tool',
              routes: [
                {
                  path: '/admin/saas',
                  name: 'Saas',
                  authority: ['AbpSaas.Tenants'],
                  icon: 'idcard',
                  routes: [
                    {
                      path: '/admin/saas/tenants',
                      name: '租户',
                      authority: ['AbpSaas.Tenants'],
                      icon: 'schedule',
                      component: './admin/saas/tenants',
                    },
                    {
                      path: '/admin/saas/editions',
                      name: '版本',
                      authority: ['AbpSaas.Editions'],
                      icon: 'cloud',
                      component: './admin/saas/editions',
                    },
                  ],
                },
                {
                  path: '/admin/identity',
                  name: '身份管理',
                  authority: ['AbpIdentity.Roles', 'AbpIdentity.Users'],
                  icon: 'idcard',
                  routes: [
                    {
                      path: '/admin/identity/user',
                      name: '用户',
                      icon: 'user',
                      authority: ['AbpIdentity.Users'],
                      component: './admin/identity/identityuser',
                    },
                    {
                      path: '/admin/identity/role',
                      name: '角色',
                      authority: ['AbpIdentity.Roles'],
                      icon: 'safety',
                      component: './admin/identity/identityrole',
                    },
                    {
                      path: '/admin/identity/identityclaimtype',
                      name: '声明类型',
                      authority: ['AbpIdentity.Roles'],
                      icon: 'safety',
                      component: './admin/identity/identityclaimtype',
                    },
                    {
                      path: '/admin/identity/organizationunit',
                      name: '组织机构',
                      //authority: ['AbpIdentity.Organization'],
                      icon: 'safety',
                      component: './admin/identity/organizationunit',
                    },
                  ],
                },
                {
                  path: '/admin/auditlogging',
                  name: '审计日志',

                  icon: 'audit',
                  component: './admin/auditlog',
                },
                {
                  path: '/admin/settings',
                  name: '设置',
                  authority: ['AbpIdentity.SettingManagement'],
                  icon: 'setting',
                  component: './admin/settings',
                },
              ],
            },
            {
              name: '个人设置',
              icon: 'smile',
              hideInMenu: true,
              path: '/accountsettings',
              component: './common/AccountSettings',
            },

            {
              component: './404',
            },
          ],
        },

        {
          component: './404',
        },
      ],
    },
    {
      component: './404',
    },
  ],
  // Theme for antd: https://ant.design/docs/react/customize-theme-cn
  theme: aliyunTheme,
  define: {
    'process.env.REACT_APP_APP_BASE_URL': process.env.REACT_APP_APP_BASE_URL,
    'process.env.REACT_APP_REMOTE_SERVICE_BASE_URL': process.env.REACT_APP_REMOTE_SERVICE_BASE_URL,
    'process.env.REACT_APP_Grant_Type': process.env.REACT_APP_Grant_Type,
    'process.env.REACT_APP_Client_Id': process.env.REACT_APP_Client_Id,
    'process.env.REACT_APP_Client_Secret': process.env.REACT_APP_Client_Secret,
    'process.env.REACT_APP_Scope': process.env.REACT_APP_Scope,
    REACT_APP_ENV: REACT_APP_ENV || false,
    ANT_DESIGN_PRO_ONLY_DO_NOT_USE_IN_YOUR_PRODUCTION:
    ANT_DESIGN_PRO_ONLY_DO_NOT_USE_IN_YOUR_PRODUCTION || '', // preview.pro.ant.design only do not use in your production ; preview.pro.ant.design 专用环境变量，请不要在你的项目中使用它。
  },
  ignoreMomentLocale: true,
  lessLoaderOptions: {
    javascriptEnabled: true,
  },
  disableRedirectHoist: true,
  cssLoaderOptions: {
    modules: true,
    getLocalIdent: (
      context: {
        resourcePath: string;
      },
      _: string,
      localName: string,
    ) => {
      if (
        context.resourcePath.includes('node_modules') ||
        context.resourcePath.includes('ant.design.pro.less') ||
        context.resourcePath.includes('global.less')
      ) {
        return localName;
      }

      const match = context.resourcePath.match(/src(.*)/);

      if (match && match[1]) {
        const antdProPath = match[1].replace('.less', '');
        const arr = slash(antdProPath)
          .split('/')
          .map((a: string) => a.replace(/([A-Z])/g, '-$1'))
          .map((a: string) => a.toLowerCase());
        return `antd-pro${arr.join('-')}-${localName}`.replace(/--/g, '-');
      }

      return localName;
    },
  },
  manifest: {
    basePath: '/',
  },
  proxy: proxy[REACT_APP_ENV || 'dev'],
  chainWebpack: webpackPlugin,
} as IConfig;
