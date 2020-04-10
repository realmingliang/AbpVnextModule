# ABPVnext 模块

[![NuGet](https://img.shields.io/nuget/v/Tudou.Abp.AuditLogging.Application.svg?style=flat-square)](https://www.nuget.org/packages/Tudou.Abp.AuditLogging.Application)



### 模块列表

- AuditLogging审计日志 

- SettingManagement设置管理 

- Saas租户版本管理

- identity 组织机构管理

### 插件列表

- umi-plugin-abp-setting 设置


         启用方式:有src/setting.ts时启用
         使用:    import { useSetting } from 'umi'
                  const setting= useSetting();
                  const value=setting[settingName];

- umi-plugin-abp-feature 功能管理

         启用方式:有src/feature.ts时启用
         使用:    import { useFeature,Feature} from 'umi'
                  const feature= useFeature();
  
 


- umi-plugin-abp-react  react插件集

            包含:umi-plugin-abp-setting 
                 umi-plugin-abp-feature
                 @umijs/preset-react




