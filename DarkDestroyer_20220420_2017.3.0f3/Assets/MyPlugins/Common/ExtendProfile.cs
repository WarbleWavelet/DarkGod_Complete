/****************************************************
    文件：ExtendProfile.cs
	作者：lenovo
    邮箱: 
    日期：2023/7/26 15:34:43
	功能：记录一些性能优化
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public static partial class ExtendOptimization
{       //Drawcall就是一个命令，由CPU发起GPU接受，告诉GPU开始一个渲染过程。
        //为什么要减少Drawcall？因为CPU如果把精力全都放在发起drawcall上，就没空去设置渲染状态了，

    //CPU,设置Drawcall
    //渲染过程在加载到游戏中的资源和对象等
    //CPU需要计算其顶点相关的矩阵
    //游戏中很多物体的材质和贴图都不一样，此时CPU的主要工作就是设置这些物体的渲染状态。
    //而GPU的渲染效率是要比CPU设置状态高很多的，如果大量的drawcall挤占了CPU的运行，那么CPU就会变成性能的瓶颈
}
public static partial class ExtendOptimization//动态图集 
{
    /*
        在运行时，Unity 动态图集会根据游戏需要动态地生成纹理，并将它们打包到一个图集中，减少游戏中需要加载的纹理数量，从而提高游戏性能。
        自动管理纹理的打包和加载
    //
     Unity 动态图集的缺点包括：
    生成图集需要一定的计算资源：在生成动态图集时需要一定的计算资源，如果游戏中的纹理较多，可能会导致游戏卡顿或者运行缓慢。
    需要调整纹理尺寸：在使用动态图集时，需要将不同尺寸的纹理进行调整，否则可能会导致纹理失真或者变形。
    需要一定的配置和调试：使用动态图集需要一定的配置和调试，需要对游戏的纹理进行分类和打包，以便在运行时生成动态图集。

    */
    //https://blog.csdn.net/qq_33808037/article/details/130018125

 
    public class DynamicAtlasManager : MonoBehaviour
    {

        #region 字属


        public int AtlasSize = 2048;
        public TextureFormat TextureFormat = TextureFormat.RGBA32;
        public bool UseMipmaps = false;
        /// <summary>图集名与图集的字典</summary>
        private Dictionary<string, Texture2D> _atlasDic;
        private Dictionary<string, Rect> _spriteRects;    
        /// <summary>图片名与图片的字典</summary> 
        private Dictionary<string, Sprite> _originalSpritesCache;


        #region 单例
        private static DynamicAtlasManager _instance;
        public static DynamicAtlasManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject go = new GameObject("DynamicAtlasManager");
                    _instance = go.AddComponent<DynamicAtlasManager>();
                }

                return _instance;
            }
        }
        #endregion
        #endregion  



        void Awake()
        {
            _atlasDic = new Dictionary<string, Texture2D>();
            _spriteRects = new Dictionary<string, Rect>();
            _originalSpritesCache = new Dictionary<string, Sprite>();
        }



        #region 辅助
        public void AddSpritesToDynamicAtlas(string atlasName, Sprite[] sprites)
        {
            if (sprites == null || sprites.Length == 0) return;

            Texture2D atlas;
            if (_atlasDic.ContainsKey(atlasName))
            {
                atlas = _atlasDic[atlasName];
            }
            else
            {
                atlas = new Texture2D(AtlasSize, AtlasSize, TextureFormat, UseMipmaps);
                atlas.filterMode = FilterMode.Bilinear;
                _atlasDic.Add(atlasName, atlas);
            }

            for (int i = 0; i < sprites.Length; i++)
            {
                if (!_originalSpritesCache.ContainsKey(sprites[i].name))
                {
                    _originalSpritesCache.Add(sprites[i].name, sprites[i]);
                }
            }

            int xOffset = 0;
            int yOffset = 0;
            int maxHeight = 0;

            for (int i = 0; i < sprites.Length; i++)
            {
                Sprite sprite = sprites[i];
                Texture2D spriteTexture = sprite.texture;

                if (xOffset + sprite.rect.width > atlas.width)
                {
                    xOffset = 0;
                    yOffset += maxHeight;
                    maxHeight = 0;
                }

                // Copy the texture using CopyTexture method
                Graphics.CopyTexture(spriteTexture, 0, 0
                    , (int)sprite.rect.x
                    , (int)sprite.rect.y
                    , (int)sprite.rect.width
                    , (int)sprite.rect.height
                    , atlas, 0, 0, xOffset, yOffset);

                _spriteRects[sprite.name] = new Rect(xOffset, yOffset, sprite.rect.width, sprite.rect.height);

                xOffset += (int)sprite.rect.width;
                maxHeight = Mathf.Max(maxHeight, (int)sprite.rect.height);
            }
        }


        public Sprite GetSpriteFromDynamicAtlas(string atlasName, string spriteName)
        {
            if (!_atlasDic.ContainsKey(atlasName) || !_spriteRects.ContainsKey(spriteName))
            {
                return null;
            }

            Texture2D atlas = _atlasDic[atlasName];
            Rect spriteRect = _spriteRects[spriteName];

            // GetOut the original sprite
            if (!_originalSpritesCache.ContainsKey(spriteName))
            {
                return null;
            }

            Sprite originalSprite = _originalSpritesCache[spriteName];

            // Calculate the border of the new sprite based on the original sprite's border
            Vector4 border = originalSprite.border;

            // Create the new sprite with the correct border
            return Sprite.Create(atlas, spriteRect, new Vector2(0.5f, 0.5f), originalSprite.pixelsPerUnit, 0, SpriteMeshType.Tight, border);
        }
        #endregion

    }

 
public class DynamicAtlasDemo : MonoBehaviour
{
    public Sprite sprite1;
    public Sprite sprite2;
    public Sprite sprite3;
    public Image image1;
    public Image image2;
    public Image image3;

    private DynamicAtlasManager _dynamicAtlasManager;

    void Start()
    {
        _dynamicAtlasManager = DynamicAtlasManager.Instance;

        // Add sprites _to the dynamic atlas
        _dynamicAtlasManager.AddSpritesToDynamicAtlas("DemoAtlas", new Sprite[] { sprite1, sprite2, sprite3 });

        image1.sprite = _dynamicAtlasManager.GetSpriteFromDynamicAtlas("DemoAtlas", sprite1.name);
        image2.sprite = _dynamicAtlasManager.GetSpriteFromDynamicAtlas("DemoAtlas", sprite2.name);
        image3.sprite = _dynamicAtlasManager.GetSpriteFromDynamicAtlas("DemoAtlas", sprite3.name);
    }
}

}


public static partial class ExtendOptimization//减少DrawCall 
{
    /*
    https://blog.csdn.net/qq_33808037/article/details/129530241
    Draw Call 是 Unity 中的一个概念，指的是 GPU 渲染一个物体所需的调用次数。
    减少 Draw Calls 可以提高游戏的帧率。
    可以通过合并材质、合并网格等方式减少 Draw Calls。  

    */

    /*
    二、可以通过哪些方式优化Draw Call
    主要的优化方式有以下几点：

    合并网格：将多个网格合并成一个网格，可以减少 Draw Call。可以使用 Unity 中的 Mesh.CombineMeshes 方法来实现网格的合并。
    合并材质：将多个使用相同材质的物体合并成一个物体，可以减少 Draw Call。可以使用 Unity 中的 MaterialPropertyBlock 来实现材质的共享。
    使用静态批处理：将多个静态物体合并为一个批次进行渲染，可以减少 Draw Call。可以在 Unity 中开启静态批处理来实现。
    使用动态批处理：将多个动态物体合并为一个批次进行渲染，可以减少 Draw Call。可以在 Unity 中开启动态批处理来实现。
    使用 GPU Instancing：使用 GPU 实例化技术可以将多个相同的物体实例化，减少 Draw Call。可以通过创建 MaterialPropertyBlock 对象并调用 MaterialPropertyBlock.SetVectorArray 方法来实现 GPU Instancing。
    使用 Atlas 贴图：将多个小贴图合并成一个大贴图，可以减少 Draw Call。可以使用 Unity 中的 SpritePacker 工具来实现贴图的合并。
    减少动态物体的数量：动态物体需要每帧重新绘制，因此数量过多会导致 Draw Call 增加。可以通过使用静态物体、使用 LOD 等方式来减少动态物体的数量。
    减少透明物体的数量：透明物体需要额外的渲染步骤，因此数量过多会导致 Draw Call 增加。可以通过使用不透明物体、使用 Alpha Test 等方式来减少透明物体的数量。
    使用 Occlusion Culling：根据摄像机视锥体内的可见 UI 元素，减少需要渲染的 UI 元素数量，从而提高渲染性能。
    */
    /// <summary>合并网格</summary>
    public static void MergeMesh() { }
    /// <summary>合并材质</summary>
    public static void MergeMaterial() { }

    /// <summary>静态合批</summary>       
    public static void StaticBatching() { }
    /// <summary>动态合批</summary>
    public static void DynamicBatching() { }

    /// <summary>GPU实例</summary>
    public static void GPUInstancing() { }

    /// <summary>Atlas图集</summary>        
    public static void AtlasMaps() { }
    /// <summary>减少数量，动态物体</summary>
    public static void ReduceCount_DynamicObject() { }
    /// <summary>减少数量，透明物体</summary>
    public static void ReduceCount_Transparent() { }
    /// <summary>相机视椎体与UI</summary>
    public static void OcclusionCulling() { }
    /*
三、不同优化方式详细介绍
1.  静态批处理：
静态批处理的原理：
静态批处理是将多个静态物体合并为一个网格进行渲染，以减少 Draw Call 的数量。在静态批处理中，首先需要将多个静态物体的网格数据合并为一个大的网格数据，然后将该网格数据传递给渲染引擎进行绘制。通过将多个静态物体合并为一个网格进行渲染，可以避免重复设置渲染状态和切换渲染资源，从而提高渲染效率。

静态批处理对资源的要求：
静态批处理对资源的要求相对较高，需要将多个静态物体合并为一个网格，并在渲染时使用该网格进行绘制。因此，在使用静态批处理时，需要对多个物体进行合并，并在合并后生成新的网格资源。这可能会导致一些资源占用较高，因此需要适当控制合并后网格的大小和数量，以避免资源浪费和性能下降。 

静态批处理对模型的面数的要求：
在使用静态批处理时，多个静态物体会被合并为一个网格进行渲染，因此，合并后的网格的面数应该尽可能地少，以避免在渲染时对性能的影响。通常来说，一个静态网格的面数不应该超过几千个，最好控制在一千个以下。如果面数过多，可能会导致合并后的网格过大，占用过多的资源，并且在渲染时会消耗大量的计算资源，导致性能下降。

 需要注意的是，实际的面数限制取决于硬件和游戏引擎的支持能力。在使用静态批处理和动态批处理时，应该根据硬件和游戏引擎的支持能力，以及游戏的实际需求进行合理的面数限制。

 静态批处理的优缺点：
优点：

减少 Draw Call：将多个静态物体合并为一个批次进行渲染，可以减少 Draw Call，提高游戏性能。
支持动态光照：静态批处理可以支持动态光照，因此适用于需要实时光照的场景。
简单易用：静态批处理的使用比较简单，只需要在材质上勾选“静态”选项即可。
缺点：

不支持动态物体：静态批处理只适用于静态物体，不支持动态物体的合并。
内存占用较高：静态批处理需要在内存中创建新的合并网格对象，因此占用内存较高。 
静态批处理对遮挡剔除的影响：
在使用静态批处理时，多个静态物体被合并为一个批次进行渲染，可能会导致一些被遮挡的物体也被渲染出来，影响遮挡剔除的效果。因此，在使用静态批处理时，需要注意场景的构建和物体的摆放，尽量避免物体之间的遮挡。

 2.  动态批处理：
动态批处理的原理：
动态批处理是将多个动态物体合并为一个网格进行渲染，以减少 Draw Call 的数量。在动态批处理中，每帧都需要重新生成一个网格数据，将多个动态物体的网格数据合并为一个大的网格数据，然后将该网格数据传递给渲染引擎进行绘制。通过将多个动态物体合并为一个网格进行渲染，可以避免重复设置渲染状态和切换渲染资源，从而提高渲染效率。

动态批处理对资源的要求： 
动态批处理对资源的要求相对较低，不需要生成新的网格资源，而是在动态物体的渲染时进行批量绘制。因此，在使用动态批处理时，不需要对多个物体进行合并，可以直接在运行时动态生成和渲染物体。这可以减少对资源的占用，并提高游戏性能。

动态批处理对模型的面数的要求： 
在使用动态批处理时，多个动态物体也会被合并为一个网格进行渲染，但与静态批处理不同的是，动态物体的面数可以较多。通常来说，一个动态网格的面数可以控制在数万面以下。如果面数过多，可能会导致渲染时消耗大量的计算资源，导致性能下降。

需要注意的是，实际的面数限制取决于硬件和游戏引擎的支持能力。在使用静态批处理和动态批处理时，应该根据硬件和游戏引擎的支持能力，以及游戏的实际需求进行合理的面数限制。

 动态批处理的优缺点：
优点：

支持动态物体：动态批处理可以将多个动态物体合并为一个批次进行渲染，因此适用于需要动态生成物体的场景。
内存占用较低：动态批处理不需要创建新的合并网格对象，因此占用内存较低。
支持 GPU Instancing：动态批处理支持 GPU 实例化技术，可以进一步减少 Draw Call。
缺点：

不支持动态光照：动态批处理不支持动态光照，因此在需要实时光照的场景中效果不佳。
对 CPU 性能影响较大：动态批处理需要在 CPU 上进行计算和合并，因此对 CPU 性能影响较大。 
 动态批处理对遮挡剔除的影响：
在使用动态批处理时，多个动态物体被合并为一个批次进行渲染，可以有效减少 Draw Call，但也可能会影响遮挡剔除的效果。如果动态物体数量较多并且密集，可能会导致一些被遮挡的物体也被渲染出来，影响遮挡剔除的效果。因此，在使用动态批处理时，需要根据具体场景和需求选择和应用，并结合其他优化技术来提高遮挡剔除的效果。

3. 打包图集：
打包图集的原理：
 Unity 中的 UI 打包图集（Sprite Atlas）功能，是将多个图片合并成一个大的纹理图集，从而提高渲染性能的一种技术。
其原理是在运行时将多张图片按照指定的规则合并成一张大的纹理图集，然后将 UI 元素使用这张纹理图集来渲染。在使用纹理图集时，Unity 会根据纹理的 UV 坐标来将纹理图集中的一小部分贴到对应的 UI 元素上。

优缺点：
优点：

 减少 Draw Call：将多张图片合并为一张大的纹理图集，可以减少需要绘制的次数，从而降低渲染的开销，提高游戏性能。
减少纹理切换：将多张图片合并为一张大的纹理图集，可以减少在切换纹理时的开销，从而提高游戏性能。
提高内存利用率：将多张图片合并为一张大的纹理图集，可以减少需要加载到内存中的纹理数量，从而提高内存利用率，降低内存使用的开销。
简化资源管理：将多张图片合并为一张大的纹理图集，可以简化资源管理的工作量，降低开发成本，提高开发效率。
方便美术制作：将多张图片合并为一张大的纹理图集，可以方便美术对于多个小图片的处理，减少了美术工作的复杂度。
缺点：

 图集尺寸限制：图集尺寸的大小有一定的限制，如果需要打包的图片数量过多或者尺寸过大，可能会导致图集的尺寸超过限制，从而无法正常打包。
图片变形：由于将多张图片合并成一张大的纹理图集，可能会导致图片在合并过程中被压缩或拉伸，从而导致图片出现变形。
图集的内存消耗：在运行时，需要将整个图集加载到内存中，可能会占用较大的内存空间，需要在制作图集时合理控制图集的尺寸和包含的图片数量。
图片重复：如果多个 UI 元素使用了同一张图片，但是位置和大小不同，就需要将这张图片在图集中重复多次，从而导致图集的大小增加。
打包图集需要注意以下几点：
 合理控制图集的尺寸和包含的图片数量，避免出现尺寸过大或者包含过多图片导致图集无法正常打包的情况。
对于需要保持原图像比例的图片，需要在图集中设置对应的 Packing Tag，以避免图片在合并过程中被压缩或拉伸。
尽量避免使用过多重复的图片，以减小图集的大小。
对于需要频繁切换的图片，不宜打包到图集中，以避免因切换纹理时需要重新渲染图集导致性能下降。
在使用图集的 UI 元素上需要设置正确的纹理坐标，以确保使用正确的图像。


    */

}




