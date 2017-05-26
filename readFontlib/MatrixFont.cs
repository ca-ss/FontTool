
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Threading;
using System.IO;

namespace readFontlib
{
    /// <summary>
    /// ���ں��ֵ����������ɵ��ࡣ
    /// 
    /// ��Ҫ��
    /// 
    /// 1��ͨ�����캯����ʼ�����еĸ����ֶΣ������ܵ��޸�ʱ������
    ///    DIBChanged ����ʹ����ͬ����ʵ��ϸ����ο��������Զ���
    ///    �� DIBChanged ������ʵ�ֹ��̡�
    /// 2���ڵ����ֽ����������ϣ���Ҫ��ʵ�ַ���Ϊ 
    ///    GetDemoCharMatrixBytes ������������ο��䶨�塣
    /// </summary>
    public class MatrixFont
    {
        #region �ֶ�

        /// <summary>
        /// ����������ʹ�õ�Font��
        /// </summary>
        private Font m_MatFont;

        /// <summary>
        /// ʹ�õ�ʾ���ַ���
        /// </summary>
        private string m_DemoChar;

        /// <summary>
        /// ��ʾ���ɵĵ�������Ŀ�ȣ������Ŀ����
        /// </summary>
        private int m_CharWidth;

        /// <summary>
        /// ��ʾ���ɵĵ�������ĸ߶ȣ������Ŀ����
        /// </summary>
        private int m_CharHeight;

        /// <summary>
        /// ��ʾ���ɵĵ��������ˮƽƫ������
        /// </summary>
        private int m_OffsetX;

        /// <summary>
        /// ��ʾ���ɵĵ�������Ĵ�ֱƫ������
        /// </summary>
        private int m_OffsetY;

        /// <summary>
        /// ʾ���ַ���λͼ���ݣ���DIBλͼ��ʽ�洢����
        /// </summary>
        private Bitmap m_MatBitmap;

        /// <summary>
        /// ����λͼ�ĸ߿��Ƿ���ȡ�
        /// </summary>
        private bool m_IsEqualWH;

        #endregion �ֶ�

        #region ���캯��

        /// <summary>
        /// ���캯����
        /// </summary>
        /// <param name="matFont">����������ʹ�õ�Font��</param>
        /// <param name="demoChar">����ʾ�����ַ���</param>
        /// <param name="charHeight">�ַ��ĸ߶ȡ�</param>
        /// <param name="charWidth">�ַ��Ŀ�ȡ�</param>
        /// <param name="offsetX">�ַ���ˮƽƫ������</param>
        /// <param name="offsetY">�ַ��Ĵ�ֱƫ������</param>
        /// <param name="isEqualWH">����λͼ�ĸ߿��Ƿ���ȣ�ֵ�߿�true��ʾ��ȣ�false��ʾ�߿���ȡ�</param>
        public MatrixFont(Font matFont, Char demoChar, int charWidth, int charHeight, int offsetX, int offsetY, bool isEqualWH)
        {
            this.m_MatFont = matFont;
            this.m_DemoChar = demoChar.ToString();
            this.m_CharWidth = charWidth;
            this.m_CharHeight = charHeight;
            this.m_OffsetX = offsetX;
            this.m_OffsetY = offsetY;
            this.m_IsEqualWH = isEqualWH;

            this.DIBChanged(matFont, demoChar.ToString(), charWidth, charHeight, offsetX, offsetY);
        }

        #endregion

        #region ����

        /// <summary>
        /// ��ȡ�����õ�������ʹ�õ�Font��
        /// </summary>
        public Font MatFont
        {
            get
            {
                return this.m_MatFont;
            }
            set
            {
                if (this.m_MatFont != null)
                {
                    this.m_MatFont.Dispose();
                }
                this.m_MatFont = value;
                this.DIBChanged(value, this.DemoChar, this.CharWidth,
                                this.CharHeight, this.OffsetX, this.OffsetY);
            }
        }

        /// <summary>
        /// ��ȡ������ʾ����ʹ�õ��ַ���
        /// </summary>
        public string DemoChar
        {
            get
            {
                return this.m_DemoChar;
            }
            set
            {
                if (value.Length > 0)
                {
                    this.m_DemoChar = value[0].ToString();
                    this.DIBChanged(this.MatFont, value[0].ToString(), this.CharWidth,
                                this.CharHeight, this.OffsetX, this.OffsetY);
                }
                else
                {
                    this.m_DemoChar = string.Empty;
                    this.DIBChanged(this.MatFont, " ", this.CharWidth,
                                this.CharHeight, this.OffsetX, this.OffsetY);
                }
            }
        }

        /// <summary>
        /// ��ȡ�����õ�������ĸ߶ȣ������Ŀ����
        /// </summary>
        public int CharHeight
        {
            get
            {
                return this.m_CharHeight;
            }
            set
            {
                this.m_CharHeight = value;
                if (this.IsEqualWH)
                {
                    this.m_CharWidth = value;
                    this.DIBChanged(this.MatFont, this.DemoChar, value,
                                value, this.OffsetX, this.OffsetY);
                }
                else
                {
                    this.DIBChanged(this.MatFont, this.DemoChar, this.CharWidth,
                                    value, this.OffsetX, this.OffsetY);
                }
            }
        }

        /// <summary>
        /// ��ȡ�����õ�������Ŀ�ȣ������Ŀ����
        /// </summary>
        public int CharWidth
        {
            get
            {
                return this.m_CharWidth;
            }
            set
            {
                this.m_CharWidth = value;
                if (this.IsEqualWH)
                {
                    this.m_CharHeight = value;
                    this.DIBChanged(this.MatFont, this.DemoChar, value,
                                value, this.OffsetX, this.OffsetY);
                }
                else
                {
                    this.DIBChanged(this.MatFont, this.DemoChar, value,
                                    this.CharHeight, this.OffsetX, this.OffsetY);
                }
            }
        }

        /// <summary>
        /// ��ȡ�����õ��������ˮƽƫ������
        /// </summary>
        public int OffsetX
        {
            get
            {
                return this.m_OffsetX;
            }
            set
            {
                this.m_OffsetX = value;
                this.DIBChanged(this.MatFont, this.DemoChar, this.CharWidth,
                                this.CharHeight, value, this.OffsetY);
            }
        }

        /// <summary>
        /// ��ȡ�����õ�������Ĵ�ֱƫ������
        /// </summary>
        public int OffsetY
        {
            get
            {
                return this.m_OffsetY;
            }
            set
            {
                this.m_OffsetY = value;
                this.DIBChanged(this.MatFont, this.DemoChar, this.CharWidth,
                                this.CharHeight, this.OffsetX, value);
            }
        }

        /// <summary>
        /// ��ȡʾ���ַ���λͼ���ݣ���DIBλͼ��ʽ�洢�����ݣ���
        /// </summary>
        public Bitmap MatBitmap
        {
            get
            {
                return this.m_MatBitmap;
            }
        }

        /// <summary>
        /// ��ȡ�����õ���λͼ�߿��Ƿ���ȣ�ֵ�߿�true��ʾ��ȣ�false��ʾ�߿���ȡ�
        /// </summary>
        public bool IsEqualWH
        {
            get
            {
                return this.m_IsEqualWH;
            }
            set
            {
                this.m_IsEqualWH = value;
                if (value)
                {
                    this.CharHeight = this.CharWidth;
                }
                else
                {
                    this.DIBChanged(this.MatFont, this.DemoChar, this.CharWidth,
                                this.CharHeight, this.OffsetX, this.OffsetY);
                }
            }
        }

        #endregion ����

        #region ����

        /// <summary>
        /// �ֶ�ֵ�ı�ʱ�����¹���ʾ���ַ���λͼ���ݡ�
        /// </summary>
        /// <param name="matFont">����������ʹ�õ�Font��</param>
        /// <param name="demoChar">����ʾ�����ַ���</param>
        /// <param name="charHeight">�ַ��ĸ߶ȡ�</param>
        /// <param name="charWidth">�ַ��Ŀ�ȡ�</param>
        /// <param name="offsetX">�ַ���ˮƽƫ������</param>
        /// <param name="offsetY">�ַ��Ĵ�ֱƫ������</param>
        private void DIBChanged(Font matFont, string demoChar,
            int charWidth, int charHeight, int offsetX, int offsetY)
        {
            try
            {
                if (this.m_MatBitmap != null)
                {
                    this.m_MatBitmap.Dispose();
                }
                //�������ڵ�ǰ�ַ�Ԥ����λͼ�����������ض�ΪColor.Black����
                this.m_MatBitmap = (Bitmap)MatrixHelp.BlackImage(charWidth, charHeight);

                using (Graphics g = Graphics.FromImage(this.m_MatBitmap))
                {
                    //if (!this.IsEqualWH)
                    //{
                    ////�����������ű任������Matrix����
                    //Matrix matrix = new Matrix();
                    //matrix.Scale((float)(charWidth) / (matFont.Height),
                    //             (float)(charHeight) / (matFont.Height));
                    //g.Transform = matrix;
                    //}

                    //ȷ���ַ���ͼ���ϵ������ʼ�㡣
                    Point txtPoint = new Point(offsetX - 2, offsetY);

                    //ȷ������ַ��Ķ��뷽ʽ��Ϊ����룩��
                    StringFormat sf = new StringFormat();
                    sf.Alignment = StringAlignment.Near;

                    //�����ַ���
                    g.DrawString(demoChar, matFont, Brushes.White, txtPoint, sf);

                    //�ͷ���Դ��
                    g.Dispose();
                }
            }
            catch (Exception ecp)   //�쳣����
            {
                throw ecp;
            }
        }

        /// <summary>
        /// ��ȡMatrixFont�Ļ�����Ϣ��
        /// </summary>
        /// <returns>MatrixFont�Ļ�����Ϣ��</returns>
        public string GetMatFontInfo()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("���壺" + this.MatFont.FontFamily.Name);
            sb.AppendLine("�����С��" + this.MatFont.SizeInPoints.ToString("##"));
            sb.AppendLine("������ʽ��" + this.MatFont.Style.ToString());

            return sb.ToString();
        }

        /// <summary>
        /// ��ȡʾ���ַ��ĵ������ݣ��ֽ����飩��
        /// </summary>
        /// <returns>�������ݡ�</returns>
        public Byte[] GetDemoCharMatrixBytes()
        {
            //���������ͼ���һ�е�������������ֽ�����
            int aryW = (int)Math.Ceiling((double)this.MatBitmap.Width / 8);

            //�����洢�������ݵ��ֽ����顣
            Byte[] matrixBytes = new Byte[aryW * this.MatBitmap.Height];
            matrixBytes.Initialize();   //��ʼ���ֽ������ÿһ��Ԫ�ء�

            try
            {
                //����ͼ����ȡ������ȷ����Ӧ�ĵ�������λ��0��1����
                for (int i = 0; i < this.MatBitmap.Height; i++)
                {
                    for (int j = 0; j < this.MatBitmap.Width; j++)
                    {
                        if (!MatrixHelp.ColorEquals(this.MatBitmap.GetPixel(j, i), Color.Black))
                        {
                            int iArray = i * aryW + (int)Math.Floor((double)j / 8);
                            matrixBytes[iArray] += (Byte)(1 << (7 - (j % 8)));
                        }
                    }
                }
            }
            catch (Exception ecp)  //�쳣����
            {
                throw ecp;
            }
            //���ؽ����
            return matrixBytes;
        }

        #endregion ����
    }
}