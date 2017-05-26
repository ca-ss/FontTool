
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace readFontlib
{
    /// <summary>
    /// Ϊ MatrixFont ���ṩһЩ�����ľ�̬�����ࡣ
    /// </summary>
    public class MatrixHelp
    {
        #region ����

        /// <summary>
        /// �ж�ColorA��RGBֵ�Ƿ���ColorB����ȡ�
        /// </summary>
        /// <param name="ColorA">ColorA��</param>
        /// <param name="ColorB">ColorB��</param>
        /// <returns>�������ɫ��RGBֵ��ȣ�����true�����򷵻�false��</returns>
        public static bool ColorEquals(Color ColorA, Color ColorB)
        {
            return ((ColorA.R == ColorB.R) & (ColorA.G == ColorB.G) & (ColorA.B == ColorB.B));
        }

        /// <summary>
        /// ͨ���ֽ�����ת����ָ����ʽ�������ʽ�����ַ�������ķ�����
        /// </summary>
        /// <param name="bytes">Ҫת�����ֽ����顣</param>
        /// <param name="width">ÿ���ַ������ĵģ���ָ�����ֽ�����ģ��ֽڸ�����</param>
        /// <returns>�ַ�������</returns>
        public static string BytesToString(Byte[] bytes, int width)
        {
            //��Ч�Լ�顣
            if (width < 1)
            {
                MessageBox.Show("ת����ȱ������ 1 !", "��ʾ",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }

            StringBuilder sb = new StringBuilder();

            try
            {
                int indexArray = 0;
                //�����ֽ����飬��ʮ�����Ƹ�ʽת�����ַ�����
                foreach (byte bt in bytes)
                {
                    //sb.AppendFormat("0x{0,2:X}H\t", bt);
                    if (bt <= 0x0F)
                    {
                        sb.AppendFormat("0x0{0:X}H, ", bt);
                    }
                    else
                    {
                        sb.AppendFormat("0x{0:X}H, ", bt);
                    }
                    indexArray++;
                    if (indexArray % width == 0)
                    {
                        sb.AppendLine("");
                    }
                }
            }
            catch (Exception ecp)       //�쳣����
            {
                throw ecp;
            }
            //���ؽ����
            return sb.ToString();
        }

        /// <summary>
        /// ����һ��Image�������������ض���ʼ��ΪColor.Black��
        /// </summary>
        /// <param name="width">Image����Ŀ�ȡ�</param>
        /// <param name="height">Image����ĸ߶ȡ�</param>
        /// <returns></returns>
        public static Image BlackImage(int width, int height)
        {
            //�����Ч�ԡ�
            if ((width <= 0) || (height <= 0))
            {
                return null;
            }
            //����Image����DIBλͼ����
            Bitmap bmp = new Bitmap(width, height);
            //����ͼ�񣬽�ÿһ���ظ�ֵ��Color.Black��
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    bmp.SetPixel(j, i, Color.Black);
                }
            }
            //���ؽ����
            return bmp;
        }

        #endregion ����
    }
}