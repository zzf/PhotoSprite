using System;
using System.Drawing;

/***
YUV 颜色空间在 PAL，NTSC和 SECAM复合颜色视频标准中使用。
采用YUV色彩空间的重要性是它的亮度信号Y和色度信号U、V是分离的。黑白电视系统只使用亮度信号（Y）；
彩色电视采用YUV空间正是为了用亮度信号Y解决彩色电视机与黑白电视机的兼容问题，色度信号（U，V）以一种特殊的方式加入亮度信号；
这样，黑白电视接收机能够显示正常的黑白图像，而彩色电视接收机能够对对附加的色度信号进行解码从而显示彩色图像。
人眼对色度的敏感程度要低于对亮度的敏感程度。
人类视网膜上的视网膜杆细胞要多于视网膜锥细胞；
说得通俗一些，视网膜杆细胞的作用就是识别亮度，而视网膜锥细胞的作用就是识别色度。
所以，你的眼睛对于亮和暗的分辨要比对颜色的分辨精细一些。
正是因为这个，在我们的视频存储中，没有必要存储全部颜色信号。
所以把YUV分开存储，Y信号是黑白信号，是以全分辨率存储的，而色度信号并不是用全分辨率存储的。

颜色空间是一系列颜色的数学表现形式。
三种最流行的颜色模型是RGB（用于计算机图形）；YIQ，YUV或YCbCr（用于视频系统）和CMYK（用于彩色打印）。
但是，这三种颜色没有一种和我们直觉概念上的色调，饱和度，亮度有直接的联系。
这就使我们暂时去追寻其它的模型，如HIS和HSV，它们能简化编程，处理和终端用户操作。

 */
namespace PhotoSprite.ColorSpace
{
    /// <summary>
    /// YUV 色彩空间结构体
    /// </summary>
    public struct YUV
    {
        byte y, u, v;

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
        /// 获取或设置 U 分量
        /// </summary>
        public byte U
        {
            get
            {
                return u;
            }
            set
            {
                u = value;
            }
        }

        /// <summary>
        /// 获取或设置 V 分量
        /// </summary>
        public byte V
        {
            get
            {
                return v;
            }
            set
            {
                v = value;
            }
        }


        public override bool Equals(object obj)
        {
            return this == (YUV)obj;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }


        /// <summary>
        /// 判断 YUV 结构体是否相等
        /// </summary>
        /// <param name="lYuv">YUV 结构体 1</param>
        /// <param name="rYuv">YUV 结构体 2</param>
        /// <returns></returns>
        public static bool operator ==(YUV lYuv, YUV rYuv)
        {
            if ((lYuv.Y == rYuv.Y) &&
                (lYuv.U == rYuv.U) &&
                (lYuv.V == rYuv.V))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 判断 YUV 结构体是否不相等
        /// </summary>
        /// <param name="lYuv">YUV 结构体 1</param>
        /// <param name="rYuv">YUV 结构体 2</param>
        /// <returns></returns>
        public static bool operator !=(YUV lYuv, YUV rYuv)
        {
            return !(lYuv == rYuv);
        }


        /// <summary>
        /// 根据 (Y, U, V) 分量建立 PhotoSprite.ColorSpace.YUV 结构体
        /// </summary>
        /// <param name="y">Y 分量</param>
        /// <param name="u">U 分量</param>
        /// <param name="v">V 分量</param>
        /// <returns></returns>
        public static YUV FromYuv(byte y, byte u, byte v)
        {
            YUV yuv = new YUV();

            yuv.Y = y;
            yuv.U = u;
            yuv.V = v;

            return yuv;
        } // end of FromYuv


        /// <summary>
        /// 根据 Color 结构体建立 PhotoSprite.ColorSpace.YUV 结构体
        /// </summary>
        /// <param name="color">RGB 颜色结构体</param>
        /// <returns></returns>
        public static YUV FromColor(Color color)
        {
            return FromRgb((byte)color.R, (byte)color.G, (byte)color.B);
        } // end of FromColor


        /// <summary>
        /// 根据 (red, green, blue) 颜色分量建立 PhotoSprite.ColorSpace.YUV 结构体
        /// </summary>
        /// <param name="red">red 分量</param>
        /// <param name="green">green 分量</param>
        /// <param name="blue">blue 分量</param>
        /// <returns></returns>
        public static YUV FromRgb(byte red, byte green, byte blue)
        {
            byte y, u, v;

            y = (byte)(((66 * red + 129 * green + 25 * blue + 128) >> 8) + 16);
            u = (byte)(((-38 * red - 74 * green + 112 * blue + 128) >> 8) + 128);
            v = (byte)(((112 * red - 94 * green - 18 * blue + 128) >> 8) + 128);

            return FromYuv(y, u, v);
        } // end of FromRgb


        /// <summary>
        /// 获取 RGB 颜色值
        /// </summary>
        /// <returns></returns>
        public Color ToRgb()
        {
            int C = y - 16;
            int D = u - 128;
            int E = v - 128;

            int R, G, B;

            R = Math.Max(Math.Min(((298 * C + 409 * E + 128) >> 8), 255), 0);
            G = Math.Max(Math.Min(((298 * C - 100 * D - 208 * E + 128) >> 8), 255), 0);
            B = Math.Max(Math.Min(((298 * C + 516 * D + 128) >> 8), 255), 0);

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
