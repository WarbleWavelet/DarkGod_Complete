/****************************************************
	文件：StaticPath.cs
	作者：lenovo
	邮箱: 
	日期：2024/7/8 13:15:21
	功能：
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

using Random = UnityEngine.Random;


/// <summary></summary>
public static partial class StaticPath //常用的四个路径
{


    /// <summary>
    /// D:/Data/Projects/Unity/AirCombat_4_20230723_2020.3.23f1c1/Assets
    /// app程序包安装路径，app本身就在这里，此目录是只读的。
    ///<para/>app的独立数据存储目录下有三个文件夹：Documents，Library和tmp。
    ///<br/>01Documents目录，这个目录用于存储需要长期保存的数据，比如我们的热更新内容就写在这里。需要注意的是，iCloud会自动备份此目录，如果此目录下写入的内容较多，审核的可能会被苹果拒掉。
    ///<para/>02Library目录，这个目录下有两个子目录，Caches和Preferences。
    ///<br/>0201Caches是一个相对临时的目录，适合存放下载缓存的临时文件，空间不足时可能会被系统清除，Application.temporaryCachePath返回的就是此路径。我把热更新的临时文件写在这里，等一个版本的所有内容更新完全后，再把内容转移到Documents目录。
    ///<br/>0202Preferences用于应用存储偏好设置，用NSUserDefaults读取或设置。
    ///<para/>03tmp目录，临时目录，存放应用运行时临时使用的数据。
    ///需要注意的是，以上无论临时、缓存或者普通目录，如果不需要的数据，都请删除。不要占用用户的存储空间，像微信就是坏榜样。
    /// </summary>
    public static string Application_DataPath = Application.dataPath;
    public static string Application_AssetsPath = Application_DataPath;
    public static string Application_ProjectPath = Application_AssetsPath.TrimName(TrimNameType.SlashPre);

    /// <summary>
    /// D:/Data/Projects/Unity/AirCombat_4_20230723_2020.3.23f1c1/Assets/StreamingAssets
    /// streamingAssetsPath是dataPath下的Raw目录   
    /// 该文件夹只读，不可写，资源无法更新进去。
    ///  移动端可以将本地的资源（资源MD5值配置表）等一些文件放到StreamingAssets文件夹下
    /// </summary>
    public static string Application_StreamingAssetsPath = Application.streamingAssetsPath;


    /// <summary>
    /// C:/Users/lenovo/AppData/LocalLow/plane/plane
    /// <br/>热更的重要路径，该文件夹可读可写，在移动端唯一一个可读写操作的文件夹
    /// <br/>通过Copy到persistentDataPath下与服务器的版本文件配置表作比对，完成资源的热更。  
    /// <br/>该文件夹是apk安装以后，才会形成的一个文件夹，无法提前创建。  
    /// <br/>该文件夹是安装完apk以后形成，里面的数据持久存在。
    /// </summary>
    public static string Application_PersistentDataPath = Application.persistentDataPath; //


    /// <summary>
    /// C:/Users/lenovo/AppData/Local/Temp/plane/plane
    /// </summary>
    public static string Application_TemporaryCachePath = Application.temporaryCachePath;








}
public static partial class StaticPath //一些文
{ 



    //Android
    //Application.temporaryCachePath
    //Application.persistentDataPath
    //Application.temporaryCachePath/persistentDataPath返回空字符串。这其实因为权限的原因，app没有声明访问外部存储空间的权限，但是Application.temporaryCachePath/ ApplicationpersistentDataPath却想返回外部存储的路径。
    //经反复测试发现，有【外置SD卡】的设备上，如果声明读/写外部存储设备的权限，会返回外部存储路径，不声明则会返回内部存储路径，这样不会有问题。
    //而在【无外置SD卡】的设备上，不管是否声明读/写外部存储设备的权限，Application.temporaryCachePath/persistentDataPath都返回外部存储路径，但是又没有权限，就可能会导致返回null了，
    //之所以说可能是因为这个bug不是必现，如果出现了设备重启之后就好了，怀疑是linux设备mount问题。但是出了问题，我们不能跟用户说你重启一下手机就好了。
    //<uses-permission android:name="android.permission.WRITE_EXTERNAL_STORAGE" />
    //
    //iOS:
    //Application.dataPath /var/containers/Bundle/Application/app sandbox/xxx.app/Data
    //Application.streamingAssetsPath /var/containers/Bundle/Application/app sandbox/test.app/Data/Raw
    //Application.temporaryCachePath /var/mobile/Containers/Data/Application/app sandbox/Library/Caches
    //Application.persistentDataPath /var/mobile/Containers/Data/Application/app sandbox/Documents
    //iOS和Mac OS X不同于Windows，app都是在一个沙盒空间中运行，每个app也有一个独立的数据存储空间，各app彼此不能互相访问、打扰。
    //
    //下面是各路径对应的OC访问方法
    //app安装路径: [[NSBundle mainBundle] resourcePath]
    //app数据沙盒存储根目录: NSHomeDirectory()
    //Documents: NSSearchPathForDirectoriesInDomains(NSDocumentDirectory, NSUserDomainMask, YES)
    //Library:     NSSearchPathForDirectoriesInDomains(NSLibraryDirectory, NSUserDomainMask, YES)
    //Caches:     NSSearchPathForDirectoriesInDomains(NSCachesDirectory, NSUserDomainMask, YES)
    //tmp:        NSTemporaryDirectory()
    //Android:
    //Application.dataPath            /data/app/package name-1/base.apk
    //Application.streamingAssetsPath jar:file:///data/app/package name-1/base.apk!/assets
    //Application.temporaryCachePath /storage/emulated/0/Android/data/package name/cache
    //Application.persistentDataPath /storage/emulated/0/Android/data/package name/files
    //Android的几个目录是apk程序包、内存存储(InternalStorage) 和外部存储(ExternalStorage) 目录。
    //apk程序包目录: apk的安装路径，/data/app/package name-n/base.apk，dataPath就是返回此目录。
    //内部存储目录: /data/data/package name-n/，用户自己或其它app都不能访问该目录。打开会发现里面有4个目录（需要root）
    //cache 缓存目录，类似于iOS的Cache目录
    //databases 数据库文件目录
    //files 类似于iOS的Documents目录
    //shared_prefs 类似于iOS的Preferences目录，用于存放常用设置，比如Unity3D的PlayerPrefs就存放于此
    //外部存储目录: 在内置或外插的sd上，用户或其它app都可以访问，外部存储目录又分私有和公有目录。
    //公有目录是像DCIM、Music、Movies、Download这样系统创建的公共目录，当然你也可以像微信那样直接在sd卡根目录创建一个文件夹。好处嘛，就是卸载app数据依旧存在。
    //私有目录在/storage/emulated/n/Android/data/package name/，打开可以看到里面有两个文件夹cache和files。为什么跟内部存储目录重复了？这是为了更大的存储空间，以防内存存储空间较小。推荐把不需要隐私的、较大的数据存在这里，而需要隐私的或较小的数据存在内部存储空间。
    //
    //下面是各路径对应的Java访问方法：
    //apk包内: AssetManager.open(String filename)
    //内部存储: context.getFilesDir().getPath() or context.getCacheDir().getPath()
    //外部存储: context.getExternalFilesDir(null).getPath() or context.getExternalCacheDir().getPath()
    //


    //
    //Windows:
    //Application.dataPath:            应用的appname_Data/
    //Application.streamingAssetsPath: 应用的appname_Data/StreamingAssets
    //Application.temporaryCachePath: C:\Users\username\AppData\Local\Temp\company name\product name
    //Application.persistentDataPath:   C:\Users\username\AppData\LocalLow\company name\product name
    //
    //PlayerPrefs路径(补充)
    //Android: /data/data/pkg-name/shared_prefs/pkg-name.v2.playerprefs.xml
    //iOS:/Library/Preferences/[bundle identifier].plist
    //Windows:HKEY_CURRENT_USER/Software/CompanyName/ProductName
    //Mac:~/Library/Preferences/com.CompanyName.ProductName.plist

}



