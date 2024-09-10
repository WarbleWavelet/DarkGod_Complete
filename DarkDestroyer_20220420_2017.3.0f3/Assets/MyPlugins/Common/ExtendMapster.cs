/****************************************************
    文件：ExtendMapster.cs
	作者：lenovo
    邮箱: 
    日期：2023/9/11 13:35:5
	功能：对象映射器
01 Mapster(据说性能更加好)
02 AutoMap

报错
# bug[未解决] The type or namespace name 'Mapster' could not be found
[mapster.tool version 8.2.0](https://stackoverflow.com/questions/66409492/mapster-tool-fails-generating-mapper)
## bug 包“Mapster.Tool 8.2.0”具有一个包类型“DotnetTool”，项目“XXX”不支持该类型。
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public static partial class ExtendMapster 
{

    public static void Example()
    {
       // CharacterVersion01 characterVersion01 = new CharacterVersion01("织田信长", 90, 90, 90, 90);
        //CharacterVersion02 characterVersion02 = characterVersion01.Adapt<CharacterVersion02>();
        //Debug.Log(characterVersion02.ToString());

    }
    class CharacterVersion01
    {
        public CharacterVersion01(string name, int command, int force, int politics, int strategy)
        {
            Name = name;
            Command = command;
            Force = force;
            Politics = politics;
            Strategy = strategy;
        }

        public string Name { get; set; }
        public int Command { get; set; }
        public int Force { get; set; }
        public int Politics{ get; set; }
        public int Strategy { get; set; }
    }

    class CharacterVersion02
    {
        public string Name { get; set; }
        public int Command { get; set; }
        public int Force { get; set; }
        public int Politics { get; set; }
        public int Strategy { get; set; }



        public override string ToString()
        {
            string str = "";
            str += "\t" + Name;
            str += "\t" + Command;
            str += "\t" + Force;
            str += "\t" + Politics;
            str += "\t" + Strategy;

            return str;
        }
    }
}



