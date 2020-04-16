import { parse } from 'querystring';
import pathRegexp from 'path-to-regexp';
import { Route } from '@/models/connect';
import _ from 'lodash';
import { ThemeSettingsDto, ApplicationConfiguration } from '@/services/data';
import SettingNames from './settingNames';
/* eslint no-useless-escape:0 import/prefer-default-export:0 */
const reg = /(((^https?:(?:\/\/)?)(?:[-;:&=\+\$,\w]+@)?[A-Za-z0-9.-]+(?::\d+)?|(?:www.|[-;:&=\+\$,\w]+@)[A-Za-z0-9.-]+)((?:\/[\+~%\/.\w-_]*)?\??(?:[-\+=&;%@.\w_]*)#?(?:[\w]*))?)$/;

export const isUrl = (path: string): boolean => reg.test(path);

export const isAntDesignPro = (): boolean => {
  if (ANT_DESIGN_PRO_ONLY_DO_NOT_USE_IN_YOUR_PRODUCTION === 'site') {
    return true;
  }
  return window.location.hostname === 'preview.pro.ant.design';
};
const iconConfig={
 cs:"icon-meiguo",
 "zh-Hans":"icon-zhongguo",
 "zh-Hant":"icon-zhongguo",
  en:"icon-yingguo",
 "pt-BR":"icon-baxi",
 tr:"icon-tuerqi"
}
export  const formatterFlagIcon=(name:string)=>{
   return iconConfig[name];
}
// 给官方演示站点用，用于关闭真实开发环境不需要使用的特性
export const isAntDesignProOrDev = (): boolean => {
  const { NODE_ENV } = process.env;
  if (NODE_ENV === 'development') {
    return true;
  }
  return isAntDesignPro();
};
const themeConfig = {
  daybreak: 'dark',
  '#1890ff': 'daybreak',
  '#F5222D': 'dust',
  '#FA541C': 'volcano',
  '#FAAD14': 'sunset',
  '#13C2C2': 'cyan',
  '#52C41A': 'green',
  '#2F54EB': 'geekblue',
  '#722ED1': 'purple',
};

const InitPrimaryColor = (input: string="daybreak") => {
  const color= input && themeConfig[input] ? themeConfig[input] : input;
  let styleLink = document.getElementById("theme-style") as HTMLLinkElement;;
  if (styleLink) { // 假如存在id为theme-style 的link标签，直接修改其href
    styleLink.href = `/theme/${color}.css`;
  } else {
    styleLink = document.createElement('link');
    styleLink.type = 'text/css';
    styleLink.rel = 'stylesheet';
    styleLink.id = 'theme-style';
    styleLink.href = `/theme/${color}.css`;
    document.body.append(styleLink);
  }
}

export const InitThemeSettings = (values: ApplicationConfiguration.Dictionary<string>[]): ThemeSettingsDto => {
  const themeSettings= {};
  _.forOwn(SettingNames.Themes, (value, key) => {
    themeSettings[key] = TryStringToBool(values[value]);
  });
  const themesSettings = themeSettings as ThemeSettingsDto

  InitPrimaryColor(themesSettings.primaryColor);
  return themesSettings;
}

export const getPageQuery = () => parse(window.location.href.split('?')[1]);

/**
 * props.route.routes
 * @param router [{}]
 * @param pathname string
 */
export const getAuthorityFromRouter = <T extends Route>(
  router: T[] = [],
  pathname: string,
): T | undefined => {
  const authority = router.find(
    ({ routes, path = '/' }) =>
      (path && pathRegexp(path).exec(pathname)) ||
      (routes && getAuthorityFromRouter(routes, pathname)),
  );
  if (authority) return authority;
  return undefined;
};

export const getRouteAuthority = (path: string, routeData: Route[]) => {
  let authorities: string[] | string | undefined;
  routeData.forEach(route => {
    // match prefix
    if (pathRegexp(`${route.path}/(.*)`).test(`${path}/`)) {
      if (route.authority) {
        authorities = route.authority;
      }
      // exact match
      if (route.path === path) {
        authorities = route.authority || authorities;
      }
      // get children authority recursively
      if (route.routes) {
        authorities = getRouteAuthority(path, route.routes) || authorities;
      }
    }
  });
  return authorities;
};

export function TryStringToBool(value: string | boolean): boolean | string {
  if (typeof value === 'string') {
    const str = value.toLowerCase();
    if (str === "true") {
      return true;
    } if (str === "false") {
      return false;
    }
    return value;
  }
  if (typeof value === "boolean") {
    return value;
  }
  return false;
}

const colorNameFormat = {
  '#1890ff': '拂晓蓝',
  '#F5222D': '薄暮',
  '#FA541C': '火山',
  '#FAAD14': '日暮',
  '#13C2C2': '明青',
  '#52C41A': '极光绿',
  '#2F54EB': '极客蓝',
  '#722ED1': '酱紫',
};
/**
 * #1890ff -> daybreak
 * @param val
 */
export function genThemeToString(val?: string): string {
  return val && colorNameFormat[val] ? colorNameFormat[val] : val;
}

export function createTree(array: any[], parentIdProperty: string,
  idProperty: string, parentIdValue: number | null, childrenProperty: string,
  fieldMappings: any[]): any[] {
  const tree: any = [];

  const nodes = _.filter(array, [parentIdProperty, parentIdValue]);

  _.forEach(nodes, node => {
    const newNode = {
      data: node,
    };

    mapFields(node, newNode, fieldMappings);

    newNode[childrenProperty] = createTree(
      array,
      parentIdProperty,
      idProperty,
      node[idProperty],
      childrenProperty,
      fieldMappings,
    );

    tree.push(newNode);
  });

  return tree;
}
function mapFields(node: any, newNode: any, fieldMappings: any): void {
  _.forEach(fieldMappings, fieldMapping => {
    if (!fieldMapping.target) {
      return;
    }

    if (fieldMapping.hasOwnProperty('value')) {
      newNode[fieldMapping.target] = fieldMapping.value;
    } else if (fieldMapping.source) {
      newNode[fieldMapping.target] = node[fieldMapping.source];
    } else if (fieldMapping.targetFunction) {
      newNode[fieldMapping.target] = fieldMapping.targetFunction(node);
    }
  });
}
