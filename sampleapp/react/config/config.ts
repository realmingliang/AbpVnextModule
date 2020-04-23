// https://umijs.org/config/
import { defineConfig, utils } from 'umi';
import aliyunTheme from '@ant-design/aliyun-theme';
import proxy from './proxy';
import webpackPlugin from './plugin.config';
const { winPath } = utils; // preview.pro.ant.design only do not use in your production ;
// preview.pro.ant.design 专用环境变量，请不要在你的项目中使用它。

const { ANT_DESIGN_PRO_ONLY_DO_NOT_USE_IN_YOUR_PRODUCTION, REACT_APP_ENV, GA_KEY } = process.env;
export default defineConfig({
  hash: true,
  antd: {},
  analytics: GA_KEY
    ? {
        ga: GA_KEY,
      }
    : false,
  dva: {
    hmr: true,
  },
  dynamicImport: {
    loading: '@/components/PageLoading/index',
  },
  targets: {
    ie: 11,
  },
  // umi routes: https://umijs.org/docs/routing
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
                      authority: ['AbpSaas.Tenants.ss'],
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
                  name: 'language-management',
                  path: '/admin/language-management',
                  authority: ['AbpSaas.Tenants.ss'],
                  icon: 'smile',
                  routes: [
                    {
                      name: 'language',
                      icon: 'smile',
                      path: '/admin/language-management/language',
                      component: './admin/localization/language',
                    },
                    {
                      name: 'languageTexts',
                      icon: 'smile',
                      path: '/admin/language-management/languagetext',
                      component: './admin/localization/languagetext',
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
  lessLoader: {
    javascriptEnabled: true,
  },
  cssLoader: {
    modules: {
      getLocalIdent: (
        context: {
          resourcePath: string;
        },
        _: string,
        localName: string
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
          const arr = winPath(antdProPath)
            .split('/')
            .map((a: string) => a.replace(/([A-Z])/g, '-$1'))
            .map((a: string) => a.toLowerCase());
          return `antd-pro${arr.join('-')}-${localName}`.replace(/--/g, '-');
        }

        return localName;
      },
    },
  },
  manifest: {
    basePath: '/',
  },
  proxy: proxy[REACT_APP_ENV || 'dev'],
  chainWebpack: webpackPlugin,
});
