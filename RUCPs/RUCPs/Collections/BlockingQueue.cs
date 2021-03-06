﻿/* BSD 3-Clause License
 *
 * Copyright (c) 2020, Vyacheslav Busel (yazZ3va)
 * All rights reserved. */

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace RUCPs.Collections
{
    public class BlockingQueue<T> where T : IDelayed, IComparable
    {
        private PriorityQueue<T> container = new PriorityQueue<T>();


        public void Add(T t)
        {
            lock (container)
            {
                container.Enqueue(t);
                Monitor.Pulse(container);
            }
        }


        public T Take()
        {
            lock (container)
            {
                while (true)
                {
                    if (container.Count == 0)
                    { Monitor.Wait(container); continue; }

                    long time = container.Peek().GetDelay();
                    if (time > 0) Monitor.Wait(container, (int)time);

                    else return container.Dequeue();
                }

            }
        }

    }
}