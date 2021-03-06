﻿/* BSD 3-Clause License
 *
 * Copyright (c) 2020, Vyacheslav Busel (yazZ3va)
 * All rights reserved. */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RUCPc.Network
{
   public class ServerInfo
    {
        /// <summary>
        /// Полученные пакеты от сервера
        /// </summary>
        internal int received = 0;
        /// <summary>
        /// Количество обработанных пакетов
        /// </summary>
        internal int proccesed = 0;

        public int AcceptedPackets
        {
            get => proccesed;
        }
        public int DroppedPackets
        {
            get => received - proccesed;
        }

    }
}
