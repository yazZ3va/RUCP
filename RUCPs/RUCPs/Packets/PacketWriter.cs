﻿/* BSD 3-Clause License
 *
 * Copyright (c) 2020, Vyacheslav Busel (yazZ3va)
 * All rights reserved. */

using System;
using System.Collections.Generic;
using System.Text;

namespace RUCPs.Packets
{
    public partial class PacketData
    {
        unsafe private void WriteValue(void* value, int len)
        {
            fixed (byte* d = Data)
            { Buffer.MemoryCopy(value, d + index, len, len); }
            Length = index += len;
        }
        /// <summary>
        /// Записывает byte[]  в заранее определнный массив данных на отправку.
        /// Если размер данных для записи превышает размер массива возврощает exception
        /// </summary>
        public void WriteBytes(byte[] bytes)
        {
            WriteShort((short)bytes.Length);
            Array.Copy(bytes, 0, Data, index, bytes.Length);
            Length = index += bytes.Length;
        }
        unsafe public void WriteFloat(float value)
        {
            WriteValue(&value, 4);
        }

        unsafe public void WriteInt(int value)
        {
            WriteValue(&value, 4);
        }
        unsafe public void WriteLong(long value)
        {
            WriteValue(&value, 8);
        }

        unsafe public void WriteShort(short value)
        {
            WriteValue(&value, 2);
        }

        public void WriteByte(byte value)
        {
            Data[index++] = value;
            Length = index;
        }

        public void WriteBool(bool value)
        {
            Data[index++] = (byte)(value ? 1 : 0);
            Length = index;
        }

        public void WriteString(string value)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(value);
            WriteBytes(bytes);
        }

      /*  public void Write(float f) => WriteFloat(f);
        public void Write(int i) => WriteInt(i);
        public void Write(long l) => WriteLong(l);
        public void Write(short s) => WriteShort(s);
        public void Write(byte b) => WriteByte(b);
        public void Write(bool b) => WriteBool(b);
        public void Write(string s) => WriteString(s);*/

    }
}
