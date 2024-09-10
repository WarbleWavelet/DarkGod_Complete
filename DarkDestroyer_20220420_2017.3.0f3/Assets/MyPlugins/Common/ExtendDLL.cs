/****************************************************
    文件：ExtendDLL.cs
	作者：lenovo
    邮箱: 
    日期：2024/7/21 1:33:46
	功能：
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public static partial class ExtendDLL//

{
#if NET_4_7_OR_NEWER
    public static (string, string, string) Cysharp = ("Cysharp.Threading.Tasks", "", "");
    public static (string, string, string) SharpZipLib = ("ICSharpCode.SharpZipLib", "", "与QuickSheet中也发生过版本冲突,我有ICSharpCode.SharpZipLib_0.85.5.452,QuickSheet的是0.86.0.518,我在后面加上_和版本号,没冲突,也没报错");
    public static (string, string, string) OleDb = ("System.Data.OleDb", "", "");
    public static (string, string, string) Newtonsoft = ("Newtonsoft.Json", "报过版本错误.VS安装包", "Assembly 'Assets/QuickSheet/GDataPlugin/Editor/Google/Google.GData.Client.dll' will not be loaded due to errors:\nGoogle.GData.Client references strong named Newtonsoft.Json Assembly references: 4.0.5.0 Found in project: 13.0.0.0.");
    //
    public static (string, string, string) Enum = ("System.Enum", "报过版本错误不存在,不能用Enum,要用enum,改了没用; 4.7.1-api\mscorlib.dll这个位置有,v4.6\mscorlib.dll没有=>好像需要4.7.3");
    public static (string, string, string) Numerics = ("System.Numerics", "NuGet安装后报过不存在,引用里面也没有");
    public static (string, string, string) Data = ("System.Data", "NuGet安装后报过不存在,引用里面也没有");
#endif
}
public static partial class ExtendDLL//dll冲突
{
    //auto Reference
    //validate References
    //同名没事?看到NewtoJson在UnityPakages下有三个在不同文件夹下的同名,没报错
    //不同名没事?复制了一堆不同版本的,用_版本号加载名字后面,也可以
    //ProjectsSettings/Player/AssemblyVersionValidation
    //
    //有一个情况
    //01 不同版本的,用_版本号加载名字
    //02 设置false auto Reference
    //03 设置false validate References ;设置为 true 以精确匹配强命名程序集。默认情况下，Mono 通过精确版本匹配解析强命名程序集。因此，有时当 Mono 搜索不存在的确切版本时，您可能会遇到错误。禁用此设置以使 Mono 忽略版本匹配
    //04 只有在设置false ProjectsSettings/Player/AssemblyVersionValidation.才不会冲突
}
public static partial class ExtendDLL//dll使用
{
    //NuGet下载引用
    //dll复制到Assets目录下
    //
    //NuGet有时没用,显示已经安装,引用没找到.
    //01 要么复制到Unity的Assets下


}




