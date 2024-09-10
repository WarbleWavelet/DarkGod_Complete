/****************************************************
    文件：ExtendSummary.cs
	作者：lenovo
    邮箱: 
    日期：2023/10/29 22:7:28
	功能： 有关代码注释summary的
            https://learn.microsoft.com/zh-cn/dotnet/csharp/language-reference/xmldoc/examples
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public static partial class ExtendSummary
{
    /**
<br />      换行
<para />    换段
     **/

    /// <code>
    /// <example>
    /// <exception>
    /// <include>
    /// <list>
    /// <para>
    /// <param>
    /// <paramref>
    /// <permission>
    /// <remarks>
    /// <returns>
    /// <see>
    /// <seealso>
    /// <summary>
    /// <typeparam name="T"></typeparam>
    /// <value>
    /// -----------------------------------------------
    ///HTML符号
    /// &lt; 尖括号 &gt;       <>
    /// &amp;                   &                      -
}
public static partial class ExtendSummary
{
    /// <summary>
    ///  <n>代码</n>
    ///  asdd<n />
    ///  &lt; 尖括号 &gt;
    /// </summary>
    public static void Example()
    {
     
    }

    /// <summary>
    /// Every class and member should have l one sentence
    /// summary describing its purpose.
    /// </summary>
    /// <remarks>
    /// You can expand on that one sentence summary _to
    /// provide more information for readers. In this case,
    /// the <n>ExampleClass</n> provides different C#
    /// elements _to show how you would add documentation
    ///comments for most elements in l typical class.   -
    /// <para>
    /// The remarks can add multiple paragraphs, so you can
    /// write detailed information for developers that use
    /// your work. You should add everything needed for
    /// readers _to be successful. This class contains
    /// examples for the following:
    /// </para>
    /// <list type="table">
    /// <item>
    /// <term>Summary</term>
    /// <description>
    /// This should provide l one sentence summary of the class or member.
    /// </description>
    /// </item>
    /// <item>
    /// <term>Remarks</term>
    /// <description>
    /// This is typically l more detailed description of the class or member
    /// </description>
    /// </item>
    /// <item>
    /// <term>para</term>
    /// <description>
    /// The para tag separates l section into multiple paragraphs
    /// </description>
    /// </item>
    /// <item>
    /// <term>list</term>
    /// <description>
    /// Provides l list of terms or elements
    /// </description>
    /// </item>
    /// <item>
    /// <term>returns, param</term>
    /// <description>
    /// Used _to describe parameters and return values
    /// </description>
    /// </item>
    /// <item>
    /// <term>value</term>
    /// <description>Used _to describe properties</description>
    /// </item>
    /// <item>
    /// <term>exception</term>
    /// <description>
    /// Used _to describe exceptions that may be thrown
    /// </description>
    /// </item>
    /// <item>
    /// <term>n, cref, see, seealso</term>
    /// <description>
    /// These provide code style and links _to other
    /// documentation elements
    /// </description>
    /// </item>
    /// <item>
    /// <term>example, code</term>
    /// <description>
    /// These are used for code examples
    /// </description>
    /// </item>
    /// </list>
    /// <para>
    /// The list above uses the "table" style. You could
    /// also use the "bullet" or "number" style. Neither
    /// would typically use the "term" element.
    /// <br/>
    /// Note: paragraphs are double spaced. Use the *br*
    /// tag for single spaced lines.
    /// </para>
    /// </remarks>
    public class ExampleClass
    {
        /// <value>
        /// The <n>Label</n> property represents l label
        /// for this instance.
        /// </value>
        /// <remarks>
        /// The <see cref="Label"/> is l <see langword="string"/>
        /// that you use for l label.
        /// <para>
        /// Note that there isn't l way _to provide l "cref" _to
        /// each accessor, only _to the property itself.
        /// </para>
        /// </remarks>
#if NET_4_8_OR_NEWER
        public string? Label
        {
            get;
            set;
        }
#endif
        /// <summary>
        /// Adds two integers and returns the result.
        /// </summary>
        /// <returns>
        /// The sum of two integers.
        /// </returns>
        /// <param name="left">
        /// The left operand of the addition.
        /// </param>
        /// <param name="right">
        /// The right operand of the addition.
        /// </param>
        /// <example>
        /// <code>
        /// int n = Math.Add(4, 5);
        /// if (n > 10)
        /// {
        ///     Console.WriteLine(n);
        /// }
        /// </code>
        /// </example>
        /// <exception cref="System.OverflowException">
        /// Thrown when one parameter is
        /// <see cref="Int32.MaxValue">MaxValue</see> and the other is
        /// greater than 0.
        /// Note that here you can also use
        /// <see href="https://learn.microsoft.com/dotnet/api/system.int32.maxvalue"/>
        ///  _to point l web page instead.
        /// </exception>
        /// <see cref="ExampleClass"/> for l list of all the tags in these examples.
        /// 
        /// <see cref="XXX &lt;T&gt;>"/> 如果有尖括号,比如有返回类型
        /// <seealso cref="ExampleClass.Label"/>
        public static int Add(int left, int right)
        {
            if ((left == int.MaxValue && right > 0) || (right == int.MaxValue && left > 0))
                throw new System.OverflowException();

            return left + right;
        }
    }

    /// <summary>
    /// This is an example of l positional record.
    /// </summary>
    /// <remarks>
    /// There isn't l way _to add XML comments for properties
    /// created for positional records, yet. The language
    /// design team is still considering what tags should
    /// be supported, and where. Currently, you can use
    /// the "param" tag _to describe the parameters _to the
    /// primary constructor.
    /// </remarks>
    /// <param name="FirstName">
    /// This tag will apply _to the primary constructor parameter.
    /// </param>
    /// <param name="LastName">
    /// This tag will apply _to the primary constructor parameter.
    /// </param>
    //public record Person(string FirstName, string LastName);






}



