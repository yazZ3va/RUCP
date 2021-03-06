﻿/* BSD 3-Clause License
 *
 * Copyright (c) 2020, Vyacheslav Busel (yazZ3va)
 * All rights reserved. */

using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace RUCPs.Tools
{
    class SocketInformer
    {
		public static long GetID(byte[] ip, int port)
		{

			long id = 0;

			//IP
			id |= ((long)ip[0] << 56);
			id |= ((long)ip[1] << 48);
			id |= ((long)ip[2] << 40);
			id |= ((long)ip[3] << 32);

			//Port
			id |= (long)port;

			return id;
		}

        internal static long GetID(IPEndPoint address)
        {
			return GetID(address.Address.GetAddressBytes(), address.Port);
        }
    }
}
