/****************************************************
    文件：StrongCfg.cs
	作者：lenovo
    邮箱: 
    日期：2024/9/7 22:26:11
	功能：
*****************************************************/

using UnityEngine;

#region  玩家突破
/**
	<item ID = "1" >

        < pos > 0 </ pos >
        < starlv > 1 </ starlv >
        < addhp > 20 </ addhp >
        < addhurt > 25 </ addhurt >
        < adddef > 18 </ adddef >
        < minlv > 1 </ minlv >
        < coin > 150 </ coin >
        < crystal > 5 </ crystal >
    </ item >
    **/
public class StrongCfg : BaseData<StrongCfg>
{
    public int pos;
    public int starlv;
    public int addhp;
    public int addhurt;
    public int adddef;
    public int minlv;
    public int coin;
    public int crystal;
}


#endregion