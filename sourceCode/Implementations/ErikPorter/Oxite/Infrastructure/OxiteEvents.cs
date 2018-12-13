//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;

namespace Oxite.Infrastructure
{
    public class OxiteEvents : IOxiteEvents
    {
        private List<OxiteEvent> events;

        public OxiteEvents()
        {
            events = new List<OxiteEvent>(10);
        }

        #region IOxiteEvents Members

        public void Add(string eventName, Action<object> method)
        {
            OxiteEvent evnt = GetEvent(eventName);

            if (evnt == null)
            {
                evnt = new OxiteEvent(eventName);

                events.Add(evnt);
            }

            evnt.Methods.Add(method);
        }

        public void Remove(string eventName, Action<object> method)
        {
            OxiteEvent evnt = GetEvent(eventName);

            if (evnt != null)
            {
                evnt.Methods.Remove(method);

                if (evnt.Methods.Count == 0)
                    events.Remove(evnt);
            }
        }

        public IList<OxiteEvent> GetEvents()
        {
            return events;
        }

        public OxiteEvent GetEvent(string eventName)
        {
            OxiteEvent foundEvent = null;

            foreach (OxiteEvent evnt in events)
            {
                if (string.Compare(evnt.Name, eventName, true) == 0)
                {
                    foundEvent = evnt;

                    break;
                }
            }

            return foundEvent;
        }

        public void FireEvent(string eventName, object state)
        {
            OxiteEvent foundEvent = GetEvent(eventName);

            if (foundEvent != null)
                foreach (Action<object> method in foundEvent.Methods)
                    method(state);
        }

        #endregion
    }
}
