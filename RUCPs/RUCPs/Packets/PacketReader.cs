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
        public long ReadLong()
        {
            long ret = BitConverter.ToInt64(Data, index);
            index += 8;
            return ret;
        }
        public int ReadInt()
        {
            int ret = BitConverter.ToInt32(Data, index);
            index += 4;
            return ret;
        }

        public short ReadShort()
        {
            short ret = BitConverter.ToInt16(Data, index);
            index += 2;
            return ret;
        }

        public byte[] ReadBytes()
        {
            int length = ReadShort();
            byte[] array = new byte[length];
            Array.Copy(Data, index, array, 0, length);
            index += length;
            return array;
        }

        public int ReadByte()
        {
            return Data[index++];
        }

        public bool ReadBool()
        {
            return Data[index++] != 0;
        }

        public float ReadFloat()
        {
            float val = BitConverter.ToSingle(Data, index);
            index += 4;
            return val;
        }

        public String ReadString()
        {
            return Encoding.UTF8.GetString(ReadBytes()); 
        }


    }
}
