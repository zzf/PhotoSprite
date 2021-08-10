using System;
using System.Drawing;
/***
 CMYK色彩模型：
1.CMY=RGB？

大家在小的时候美术课上，老师都说过原色（其实这里应该叫做颜料原色）是红、黄、蓝三种。
一般在绘画上所指三原色的红是品红（也叫曙红，英文：Magenta）、黄是柠檬黄（Yellow）、蓝是湖蓝，也叫做青（Cyan）。

光的三原色和颜料三原色不同，光的三原色上文已经讲过了叫做RGB；
这里我们说说颜料的三原色青（C）、品（M）、黄（Y），关于CMY的混色规律上文已经提到了，从上面的色相环图形上大家也应该能看出来，在理想的条件下，CMY色域应该全等于RGB。
也就是说，理想状况下：CMY与RGB的关系应该满足每像素点下：

C=255-R
M=255-G
Y=255-B

但是由于我们现代工艺所局限，我们从提取的CMY三色油墨不是很纯；
如果用大家尝试过强行将打印机里的CMY三色油墨等比例混合并且不残水，它是得不到纯净的黑色的，而是会形成一种参杂大量灰度绛红色；
如果你想要用CMY三色油墨强行调节成纯黑色只有降一点C油墨的含量，这往往不好控制；
而我们一般用油墨打印什么的几乎都是印黑白的比较多，为了节约成本，我们用碳啊煤之类的（我不是学化学的确实不知道 = =）提炼了纯黑色油墨，
在打印机内部多增加了一位黑色油墨的通道，这才有了传说中的CMYK色彩模式，K是黑色（Black）的意思。
 */

namespace PhotoSprite.ColorSpace
{
    /// <summary>
    /// CMYK 色彩空间结构体
    /// </summary>
    public struct CMYK
    {
        byte c, m, y, k;

        /// <summary>
        /// 获取或设置 C 分量
        /// </summary>
        public byte C
        {
            get
            {
                return c;
            }
            set
            {
                c = value;
            }
        }

        /// <summary>
        /// 获取或设置 M 分量
        /// </summary>
        public byte M
        {
            get
            {
                return m;
            }
            set
            {
                m = value;
            }
        }

        /// <summary>
        /// 获取或设置 Y 分量
        /// </summary>
        public byte Y
        {
            get
            {
                return y;
            }
            set
            {
                y = value;
            }
        }

        /// <summary>
        /// 获取或设置 K 分量
        /// </summary>
        public byte K
        {
            get
            {
                return k;
            }
            set
            {
                k = value;
            }
        }


        public override bool Equals(object obj)
        {
            return this == (CMYK)obj;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }


        /// <summary>
        /// 判断 CMYK 结构体是否相等
        /// </summary>
        /// <param name="lCmyk">CMYK 结构体 1</param>
        /// <param name="rCmyk">CMYK 结构体 2</param>
        /// <returns></returns>
        public static bool operator ==(CMYK lCmyk, CMYK rCmyk)
        {
            if ((lCmyk.C == rCmyk.C) &&
                (lCmyk.M == rCmyk.M) &&
                (lCmyk.Y == rCmyk.Y) &&
                (lCmyk.K == rCmyk.K))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 判断 CMYK 结构体是否不相等
        /// </summary>
        /// <param name="lCmyk">CMYK 结构体 1</param>
        /// <param name="rCmyk">CMYK 结构体 2</param>
        /// <returns></returns>
        public static bool operator !=(CMYK lCmyk, CMYK rCmyk)
        {
            return !(lCmyk == rCmyk);
        }


        /// <summary>
        /// 根据 (c, m, y, k) 分量建立 PhotoSprite.ColorSpace.CMYK 结构体
        /// </summary>
        /// <param name="c">C 分量</param>
        /// <param name="m">M 分量</param>
        /// <param name="y">Y 分量</param>
        /// <param name="k">K 分量</param>
        public static CMYK FromCmyk(byte c, byte m, byte y, byte k)
        {
            CMYK cmyk = new CMYK();

            cmyk.c = c;
            cmyk.m = m;
            cmyk.y = y;
            cmyk.k = k;

            return cmyk;
        } // end of FromCmyk


        /// <summary>
        /// 根据 Color 结构体建立 PhotoSprite.ColorSpace.CMYK 结构体
        /// </summary>
        /// <param name="color">RGB 颜色结构体</param>
        /// <returns></returns>
        public static CMYK FromColor(Color color)
        {
            return FromRgb((byte)color.R, (byte)color.G, (byte)color.B);
        } // end of FromColor


        /// <summary>
        /// 根据 (red, green, blue) 颜色分量建立 PhotoSprite.ColorSpace.CMYK 结构体
        /// </summary>
        /// <param name="red">red 分量</param>
        /// <param name="green">green 分量</param>
        /// <param name="blue">blue 分量</param>
        /// <returns></returns>
        public static CMYK FromRgb(byte red, byte green, byte blue)
        {
            byte c, m, y, k;

            c = (byte)(255 - red);
            m = (byte)(255 - green);
            y = (byte)(255 - blue);

            k = Math.Min(c, Math.Min(m, y));
            c -= k;
            m -= k;
            y -= k;

            return FromCmyk(c, m, y, k);
        } // end of FromRgb


        /// <summary>
        /// 获取 RGB 颜色值
        /// </summary>
        /// <returns></returns>
        public Color ToRgb()
        {
            int R, G, B;

            R = 255 - c - k;
            G = 255 - m - k;
            B = 255 - y - k;

            return Color.FromArgb(R, G, B);
        } // end of ToRgb


        /// <summary>
        /// 获取 RGB 结构中 red 分量值
        /// </summary>
        /// <returns></returns>
        public byte GetRed()
        {
            return (byte)ToRgb().R;
        }


        /// <summary>
        /// 获取 RGB 结构中 green 分量值
        /// </summary>
        /// <returns></returns>
        public byte GetGreen()
        {
            return (byte)ToRgb().G;
        }


        /// <summary>
        /// 获取 RGB 结构中 blue 分量值
        /// </summary>
        /// <returns></returns>
        public byte GetBlue()
        {
            return (byte)ToRgb().B;
        }


    }
}
