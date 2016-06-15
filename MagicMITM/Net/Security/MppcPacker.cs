using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MagicMITM.Net.Security
{
    public class MppcPacker
    {
        public byte[] decHistory = new byte[0xffff];
        private int i;
        public int j;
        public BitArray srcbinary;

        private int bitstobytes(int bits, bool complete)
        {
            int num = 0;
            if (complete)
            {
                num += Convert.ToInt32(Math.Pow(Convert.ToDouble(2), Convert.ToDouble((int)(bits + 1))));
            }
            for (int i = bits; i >= 0; i--)
            {
                if (this.srcbinary[this.i])
                {
                    num += Convert.ToInt32(Math.Pow(Convert.ToDouble(2), Convert.ToDouble(i)));
                }
                this.i++;
            }
            return num;
        }

        private byte[] BitsToBytesCompressor(BitArray target, int lenght)
        {
            int[] numArray = new int[lenght / 8];
            byte[] buffer = new byte[lenght / 8];
            for (int i = 0; i < (lenght / 8); i++)
            {
                for (int j = 7; j >= 0; j--)
                {
                    if (target[(i * 8) + j])
                    {
                        numArray[i] += Convert.ToInt32(Math.Pow(Convert.ToDouble(2), Convert.ToDouble(j)));
                    }
                }
                buffer[i] = BitConverter.GetBytes(numArray[i])[0];
            }
            return buffer;
        }

        private void Copy(BitArray inputbits, int from, BitArray outputbits, int to, int length)
        {
            for (int i = 0; i < length; i++)
            {
                outputbits[to + i] = inputbits[from + i];
            }
        }

        private void copytuple(int offsettocopy, int lenght)
        {
            int num2;
            if (offsettocopy == 1)
            {
                byte num = this.decHistory[this.j - offsettocopy];
                for (num2 = 0; num2 < lenght; num2++)
                {
                    this.decHistory[this.j] = num;
                    this.j++;
                }
            }
            else
            {
                byte[] buffer;
                if (offsettocopy >= lenght)
                {
                    buffer = new byte[lenght];
                    for (num2 = 0; num2 < lenght; num2++)
                    {
                        buffer[num2] = this.decHistory[(this.j - offsettocopy) + num2];
                    }
                    Array.Copy(buffer, 0, this.decHistory, this.j, lenght);
                    this.j += lenght;
                }
                else if (offsettocopy < lenght)
                {
                    buffer = new byte[lenght];
                    int num3 = 0;
                    for (num2 = 0; num2 < lenght; num2++)
                    {
                        if (num3 == offsettocopy)
                        {
                            num3 = 0;
                        }
                        buffer[num2] = this.decHistory[(this.j - offsettocopy) + num3];
                        num3++;
                    }
                    Array.Copy(buffer, 0, this.decHistory, this.j, lenght);
                    this.j += lenght;
                }
            }
        }

        public byte[] decompresMccp(byte[] src, int packetnumber)
        {
            if (this.j > 0x6d60)
            {
                Array.Copy(this.decHistory, 0x3fff, this.decHistory, 0, 0x3fff);
                this.j -= 0x3fff;
            }
            this.i = 0;
            int j = this.j;
            this.srcbinary = new BitArray(src);
            this.ReOrderBitArray(this.srcbinary);
            while (this.i < this.srcbinary.Length)
            {
                if (!this.srcbinary[this.i])//0xxx
                {
                    try
                    {
                        this.decHistory[this.j] = Convert.ToByte(this.bitstobytes(7, false));
                    }
                    catch
                    {
                    }
                    this.j++;
                }
                else if (this.srcbinary[this.i])//1xxx
                {
                    this.i++;
                    if (!this.srcbinary[this.i])//10xx
                    {
                        this.i++;
                        try
                        {
                            this.decHistory[this.j] = Convert.ToByte(this.bitstobytes(6, true));
                        }
                        catch
                        {
                        }
                        this.j++;
                    }
                    else if (this.srcbinary[this.i])//11xx
                    {
                        int num2;
                        this.i++;
                        if (this.srcbinary[this.i])//111x
                        {
                            this.i++;
                            if (this.srcbinary[this.i])//1111
                            {
                                this.i++;
                                num2 = this.bitstobytes(5, false);
                                if (num2 == 0)
                                {
                                    while ((this.i % 8) != 0)
                                    {
                                        this.i++;
                                    }
                                    if (this.srcbinary.Length == this.i)
                                    {
                                        break;
                                    }
                                }
                                else
                                {
                                    this.copytuple(num2, this.getlenghtofmatch());
                                }
                            }
                            else if (!this.srcbinary[this.i])//1110
                            {
                                this.i++;
                                num2 = this.bitstobytes(7, false) + 0x40;
                                this.copytuple(num2, this.getlenghtofmatch());
                            }
                        }
                        else if (!this.srcbinary[this.i])//110x
                        {
                            this.i++;
                            num2 = this.bitstobytes(12, false) + 320;
                            this.copytuple(num2, this.getlenghtofmatch());
                        }
                    }
                }
            }
            byte[] destinationArray = new byte[this.j - j];
            Array.Copy(this.decHistory, j, destinationArray, 0, this.j - j);
            return destinationArray;
        }

        private int getlenghtofmatch()
        {
            int bits = 0;
            while (this.srcbinary[this.i])
            {
                bits++;
                this.i++;
            }
            if (bits == 0)
            {
                this.i++;
                return 3;
            }
            this.i++;
            return this.bitstobytes(bits, true);
        }

        public int histlen = 0;
        public int debug13mask = 0;

        public byte[] Pack(byte[] src)
        {
            BitArray outputbits = new BitArray((src.Length * 9) + 100);
            int from = 0;
            int to = 0;
            //histlen += src.Length;
            BitArray array2 = new BitArray(src);
            try
            {
                this.ReOrderBitArray(array2);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
            }
            while (from < array2.Length)
            {
                if (histlen < 8192)
                {
                    #region pack bytes
                    if (array2[from])
                    {
                        outputbits[to] = true;// 10XXXXXXX
                        to++;
                        outputbits[to] = false;
                        to++;
                        this.Copy(array2, from + 1, outputbits, to, 7);
                        to += 7;
                        from += 8;
                    }
                    else
                    {
                        this.Copy(array2, from, outputbits, to, 8);//0XXXXXXX
                        from += 8;
                        to += 8;
                    }
                    histlen++;
                    #endregion
                }
                else
                {
                    #region Add finalizer for restart history
                    for (int i = 0; i < 4; i++)
                    {
                        outputbits[to] = true;
                        to++;
                    }
                    for (int i = 0; i < 6; i++)
                    {
                        outputbits[to] = false;
                        to++;
                    }
                    histlen = 0;
                    to += ((8 - (to % 8)) % 8);
                    #endregion
                }
            }
            #region Add finalizer to end
            for (int i = 0; i < 4; i++)
            {
                outputbits[to] = true;
                to++;
            }
            for (int i = 0; i < 6; i++)
            {
                outputbits[to] = false;
                to++;
            }//1111000000
            #endregion
            //outputbits.Length = (((to + 1) % 8) != 0) ? (to + 1) + (8 - ((to + 1) % 8)) : (to + 1);
            //Complete array size to 8
            outputbits.Length = to + ((8 - (to % 8)) % 8);


            this.ReOrderBitArray(outputbits);
            byte[] tmpd = this.BitsToBytesCompressor(outputbits, outputbits.Length);

            return tmpd;
        }

        public void ReOrderBitArray(BitArray src)
        {
            for (int i = 0; i < src.Length; i += 8)
            {
                for (int j = 0; j < 4; j++)
                {
                    bool flag = src[i + j];
                    src[i + j] = src[i + (7 - j)];
                    src[i + (7 - j)] = flag;
                }
            }
        }

        public void resetHistory()
        {
            this.decHistory = new byte[0x1fc4];
            this.j = 0;
        }
    }
}