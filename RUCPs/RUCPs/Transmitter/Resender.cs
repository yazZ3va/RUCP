﻿/* BSD 3-Clause License
 *
 * Copyright (c) 2020, Vyacheslav Busel (yazZ3va)
 * All rights reserved. */

using RUCPs.Collections;
using RUCPs.Debugger;
using RUCPs.Packets;
using System;
using System.Collections.Concurrent;
using System.Threading;

namespace RUCPs.Transmitter
{
    internal class Resender
    {
        private static Resender instance = null;

        private static BlockingQueue<Packet> elements = new BlockingQueue<Packet>();


        internal static void Add(Packet packet)
        {
            packet.WriteSendTime(); //Время следующей переотправки пакета
            elements.Add(packet);
        }

        internal static void Start()
        {
            if (instance == null)
            {
                lock (elements)
                {
                    instance = new Resender();
                    new Thread(() => instance.Run()) { IsBackground = true }.Start();
                }
            }
        }

        internal void Run()
        {
            while (true)
            {
                try
                {
                    Packet packet = elements.Take();


                    //If the first packet in the queue is confirmed or the client is disconnected, remove it from the queue and go to the next
                    if (packet.ACK || !packet.Client.isConnected()) { packet.Dispose(); continue; }

                    //If the number of attempts to resend the packet exceeds 20, disconnect the client
                    if (packet.SendCicle > 20)
                    {
                        Debug.Log($"Lost connection, remote node does not respond for: {DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - packet.SendTime}ms " +
                                  $"timeout:{packet.Client.GetTimeout()}ms", MsgType.WARNING);

                        packet.Client.CloseConnection();
                        packet.Dispose();
                        continue;
                    }

                    UdpSocket.Send(packet);
                    Resender.Add(packet); //Запись на переотправку

                }
                catch (Exception e)
                {
                    Debug.Log(e);
                }

            }
        }
    }
}
