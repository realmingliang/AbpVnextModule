export default function() {
  var regex=/[.*+?^${}()|[\]\\]/g;
  var regex2='"\\\\$&"'
  return `\
import { useContext } from 'react';
import LocaleContext from './context';


export const useLocale = (source:string) => {
  const locale = useContext(LocaleContext);
  if(locale[source]===undefined){
    return "SourceNotExist"
  }
  return L;
  function L(key:string,args:any[]=[]){
    var value = locale[source][key];
    if (value == undefined) {
        return key;
    }
    if(args.length>0){
      return formatString(value!,args)!;
    }
    return locale[source][key]!;
  }
};
function formatString(str:string,args:any[]=[]) {
  if (args.length < 1) {
    return null;
  }

  for (var i = 0; i < args.length; i++) {
    var placeHolder = '{' + i + '}';
    str = replaceAll(str, placeHolder, args[i]);
  }
  return str;
};
function replaceAll (str:string, search:string, replacement:string) {
  var fix = search.replace(${regex}, ${regex2});
  return str.replace(new RegExp(fix, 'g'), replacement);
};

`;
}
