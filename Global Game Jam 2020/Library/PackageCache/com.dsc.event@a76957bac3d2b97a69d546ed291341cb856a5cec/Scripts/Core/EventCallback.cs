using System.Collections.Generic;
using UnityEngine.Events;

#region  Enum

public enum EventOrder
{
    Normal,
    PreNormal,
    PostNormal,
    PreEarly,
    Early,
    PostEalry,
    PreLate,
    Late,
    PostLate,
}

#endregion

namespace DSC.Event
{
    public class EventCallback<EventKey>
    {
        Dictionary<EventKey, Dictionary<EventOrder, UnityAction>> m_dicActData;

        /// <summary>
        /// Add callback to this event.
        /// </summary>
        public void Add(EventKey eventKey, UnityAction action)
        {
            MainAdd(eventKey, action);
        }

        /// <summary>
        /// Add callback to this event.
        /// </summary>
        public void Add(EventKey eventKey, UnityAction action, EventOrder orderType)
        {
            MainAdd(eventKey, action, orderType);
        }

        void MainAdd(EventKey eventKey, UnityAction action, EventOrder eOrderType = EventOrder.Normal)
        {
            CreateDictionaryDataIfNull();

            if (m_dicActData.TryGetValue(eventKey, out Dictionary<EventOrder, UnityAction> hOutData))
            {
                if (hOutData.TryGetValue(eOrderType, out UnityAction hOutAction))
                {
                    hOutAction += action;
                    hOutData[eOrderType] = hOutAction;
                }
                else
                {
                    hOutData.Add(eOrderType, action);
                }

                m_dicActData[eventKey] = hOutData;
            }
            else
            {
                var hNewData = new Dictionary<EventOrder, UnityAction>();
                hNewData.Add(eOrderType, action);
                m_dicActData.Add(eventKey, hNewData);
            }
        }

        /// <summary>
        /// Remove callback from this event.
        /// </summary>
        public void Remove(EventKey eventKey, UnityAction action)
        {
            MainRemove(eventKey, action);
        }

        /// <summary>
        /// Remove callback from this event.
        /// </summary>
        public void Remove(EventKey eventKey, UnityAction action, EventOrder orderType)
        {
            MainRemove(eventKey, action, orderType);
        }

        void MainRemove(EventKey eventKey, UnityAction action, EventOrder eOrderType = EventOrder.Normal)
        {
            if (!HasKeyInData(eventKey))
                return;

            var hData = m_dicActData[eventKey];
            if (!hData.ContainsKey(eOrderType))
                return;

            var hAction = hData[eOrderType];
            hAction -= action;
            hData[eOrderType] = hAction;

            m_dicActData[eventKey] = hData;
        }

        /// <summary>
        /// Clear all event callback in data.
        /// </summary>
        public void ClearAll()
        {
            if (m_dicActData == null || m_dicActData.Count <= 0)
                return;

            m_dicActData.Clear();
        }

        /// <summary>
        /// Clear all callback in this event.
        /// </summary>
        public void ClearEvent(EventKey eventKey)
        {
            if (!HasKeyInData(eventKey))
                return;

            var hData = m_dicActData[eventKey];
            hData.Clear();
            m_dicActData[eventKey] = hData;
        }

        /// <summary>
        /// Run event callback.
        /// </summary>
        public void Run(EventKey eventKey)
        {
            if (!HasKeyInData(eventKey))
                return;

            var hData = m_dicActData[eventKey];

            if (hData.TryGetValue(EventOrder.PreEarly, out UnityAction hPreEarlyAction))
            {
                hPreEarlyAction?.Invoke();
            }

            if (hData.TryGetValue(EventOrder.Early, out UnityAction hEarlyAction))
            {
                hEarlyAction?.Invoke();
            }

            if (hData.TryGetValue(EventOrder.PostEalry, out UnityAction hPostEarlyAction))
            {
                hPostEarlyAction?.Invoke();
            }

            if (hData.TryGetValue(EventOrder.PreNormal, out UnityAction hPreNormalAction))
            {
                hPreNormalAction?.Invoke();
            }

            if (hData.TryGetValue(EventOrder.Normal, out UnityAction hNormalAction))
            {
                hNormalAction?.Invoke();
            }

            if (hData.TryGetValue(EventOrder.PostNormal, out UnityAction hPostNormalAction))
            {
                hPostNormalAction?.Invoke();
            }

            if (hData.TryGetValue(EventOrder.PreLate, out UnityAction hPreLateAction))
            {
                hPreLateAction?.Invoke();
            }

            if (hData.TryGetValue(EventOrder.Late, out UnityAction hLateAction))
            {
                hLateAction?.Invoke();
            }

            if (hData.TryGetValue(EventOrder.PostLate, out UnityAction hPostLateAction))
            {
                hPostLateAction?.Invoke();
            }
        }

        #region Helper

        void CreateDictionaryDataIfNull()
        {
            if (m_dicActData == null)
                m_dicActData = new Dictionary<EventKey, Dictionary<EventOrder, UnityAction>>();
        }

        bool HasKeyInData(EventKey eEventKey)
        {
            if (m_dicActData == null || m_dicActData.Count <= 0 || !m_dicActData.ContainsKey(eEventKey))
                return false;

            return true;
        }

        #endregion
    }

    public class EventCallback<EventKey, T0>
    {
        Dictionary<EventKey, Dictionary<EventOrder, UnityAction<T0>>> m_dicActData;

        /// <summary>
        /// Add callback to this event.
        /// </summary>
        public void Add(EventKey eventKey, UnityAction<T0> action)
        {
            MainAdd(eventKey, action);
        }

        /// <summary>
        /// Add callback to this event.
        /// </summary>
        public void Add(EventKey eventKey, UnityAction<T0> action, EventOrder orderType)
        {
            MainAdd(eventKey, action, orderType);
        }

        void MainAdd(EventKey eventKey, UnityAction<T0> action, EventOrder eOrderType = EventOrder.Normal)
        {
            CreateDictionaryDataIfNull();

            if (m_dicActData.TryGetValue(eventKey, out Dictionary<EventOrder, UnityAction<T0>> hOutData))
            {
                if (hOutData.TryGetValue(eOrderType, out UnityAction<T0> hOutAction))
                {
                    hOutAction += action;
                    hOutData[eOrderType] = hOutAction;
                }
                else
                {
                    hOutData.Add(eOrderType, action);
                }

                m_dicActData[eventKey] = hOutData;
            }
            else
            {
                var hNewData = new Dictionary<EventOrder, UnityAction<T0>>();
                hNewData.Add(eOrderType, action);
                m_dicActData.Add(eventKey, hNewData);
            }
        }

        /// <summary>
        /// Remove callback from this event.
        /// </summary>
        public void Remove(EventKey eventKey, UnityAction<T0> action)
        {
            MainRemove(eventKey, action);
        }

        /// <summary>
        /// Remove callback from this event.
        /// </summary>
        public void Remove(EventKey eventKey, UnityAction<T0> action, EventOrder orderType)
        {
            MainRemove(eventKey, action, orderType);
        }

        void MainRemove(EventKey eventKey, UnityAction<T0> action, EventOrder eOrderType = EventOrder.Normal)
        {
            if (!HasKeyInData(eventKey))
                return;

            var hData = m_dicActData[eventKey];
            if (!hData.ContainsKey(eOrderType))
                return;

            var hAction = hData[eOrderType];
            hAction -= action;
            hData[eOrderType] = hAction;

            m_dicActData[eventKey] = hData;
        }

        /// <summary>
        /// Clear all event callback in data.
        /// </summary>
        public void ClearAll()
        {
            if (m_dicActData == null || m_dicActData.Count <= 0)
                return;

            m_dicActData.Clear();
        }

        /// <summary>
        /// Clear all callback in this event.
        /// </summary>
        public void ClearEvent(EventKey eventKey)
        {
            if (!HasKeyInData(eventKey))
                return;

            var hData = m_dicActData[eventKey];
            hData.Clear();
            m_dicActData[eventKey] = hData;
        }

        /// <summary>
        /// Run event callback.
        /// </summary>
        public void Run(EventKey eventKey, T0 arg0)
        {
            if (!HasKeyInData(eventKey))
                return;

            var hData = m_dicActData[eventKey];

            if (hData.TryGetValue(EventOrder.PreEarly, out UnityAction<T0> hPreEarlyAction))
            {
                hPreEarlyAction?.Invoke(arg0);
            }

            if (hData.TryGetValue(EventOrder.Early, out UnityAction<T0> hEarlyAction))
            {
                hEarlyAction?.Invoke(arg0);
            }

            if (hData.TryGetValue(EventOrder.PostEalry, out UnityAction<T0> hPostEarlyAction))
            {
                hPostEarlyAction?.Invoke(arg0);
            }

            if (hData.TryGetValue(EventOrder.PreNormal, out UnityAction<T0> hPreNormalAction))
            {
                hPreNormalAction?.Invoke(arg0);
            }

            if (hData.TryGetValue(EventOrder.Normal, out UnityAction<T0> hNormalAction))
            {
                hNormalAction?.Invoke(arg0);
            }

            if (hData.TryGetValue(EventOrder.PostNormal, out UnityAction<T0> hPostNormalAction))
            {
                hPostNormalAction?.Invoke(arg0);
            }

            if (hData.TryGetValue(EventOrder.PreLate, out UnityAction<T0> hPreLateAction))
            {
                hPreLateAction?.Invoke(arg0);
            }

            if (hData.TryGetValue(EventOrder.Late, out UnityAction<T0> hLateAction))
            {
                hLateAction?.Invoke(arg0);
            }

            if (hData.TryGetValue(EventOrder.PostLate, out UnityAction<T0> hPostLateAction))
            {
                hPostLateAction?.Invoke(arg0);
            }
        }

        #region Helper

        void CreateDictionaryDataIfNull()
        {
            if (m_dicActData == null)
                m_dicActData = new Dictionary<EventKey, Dictionary<EventOrder, UnityAction<T0>>>();
        }

        bool HasKeyInData(EventKey eEventKey)
        {
            if (m_dicActData == null || m_dicActData.Count <= 0 || !m_dicActData.ContainsKey(eEventKey))
                return false;

            return true;
        }

        #endregion
    }

    public class EventCallback<EventKey, T0, T1>
    {
        Dictionary<EventKey, Dictionary<EventOrder, UnityAction<T0, T1>>> m_dicActData;

        /// <summary>
        /// Add callback to this event.
        /// </summary>
        public void Add(EventKey eventKey, UnityAction<T0, T1> action)
        {
            MainAdd(eventKey, action);
        }

        /// <summary>
        /// Add callback to this event.
        /// </summary>
        public void Add(EventKey eventKey, UnityAction<T0, T1> action, EventOrder orderType)
        {
            MainAdd(eventKey, action, orderType);
        }

        void MainAdd(EventKey eventKey, UnityAction<T0, T1> action, EventOrder eOrderType = EventOrder.Normal)
        {
            CreateDictionaryDataIfNull();

            if (m_dicActData.TryGetValue(eventKey, out Dictionary<EventOrder, UnityAction<T0, T1>> hOutData))
            {
                if (hOutData.TryGetValue(eOrderType, out UnityAction<T0, T1> hOutAction))
                {
                    hOutAction += action;
                    hOutData[eOrderType] = hOutAction;
                }
                else
                {
                    hOutData.Add(eOrderType, action);
                }

                m_dicActData[eventKey] = hOutData;
            }
            else
            {
                var hNewData = new Dictionary<EventOrder, UnityAction<T0, T1>>();
                hNewData.Add(eOrderType, action);
                m_dicActData.Add(eventKey, hNewData);
            }
        }

        /// <summary>
        /// Remove callback from this event.
        /// </summary>
        public void Remove(EventKey eventKey, UnityAction<T0, T1> action)
        {
            MainRemove(eventKey, action);
        }

        /// <summary>
        /// Remove callback from this event.
        /// </summary>
        public void Remove(EventKey eventKey, UnityAction<T0, T1> action, EventOrder orderType)
        {
            MainRemove(eventKey, action, orderType);
        }

        void MainRemove(EventKey eventKey, UnityAction<T0, T1> action, EventOrder eOrderType = EventOrder.Normal)
        {
            if (!HasKeyInData(eventKey))
                return;

            var hData = m_dicActData[eventKey];
            if (!hData.ContainsKey(eOrderType))
                return;

            var hAction = hData[eOrderType];
            hAction -= action;
            hData[eOrderType] = hAction;

            m_dicActData[eventKey] = hData;
        }

        /// <summary>
        /// Clear all event callback in data.
        /// </summary>
        public void ClearAll()
        {
            if (m_dicActData == null || m_dicActData.Count <= 0)
                return;

            m_dicActData.Clear();
        }

        /// <summary>
        /// Clear all callback in this event.
        /// </summary>
        public void ClearEvent(EventKey eventKey)
        {
            if (!HasKeyInData(eventKey))
                return;

            var hData = m_dicActData[eventKey];
            hData.Clear();
            m_dicActData[eventKey] = hData;
        }

        /// <summary>
        /// Run event callback.
        /// </summary>
        public void Run(EventKey eventKey, T0 arg0, T1 arg1)
        {
            if (!HasKeyInData(eventKey))
                return;

            var hData = m_dicActData[eventKey];

            if (hData.TryGetValue(EventOrder.PreEarly, out UnityAction<T0, T1> hPreEarlyAction))
            {
                hPreEarlyAction?.Invoke(arg0, arg1);
            }

            if (hData.TryGetValue(EventOrder.Early, out UnityAction<T0, T1> hEarlyAction))
            {
                hEarlyAction?.Invoke(arg0, arg1);
            }

            if (hData.TryGetValue(EventOrder.PostEalry, out UnityAction<T0, T1> hPostEarlyAction))
            {
                hPostEarlyAction?.Invoke(arg0, arg1);
            }

            if (hData.TryGetValue(EventOrder.PreNormal, out UnityAction<T0, T1> hPreNormalAction))
            {
                hPreNormalAction?.Invoke(arg0, arg1);
            }

            if (hData.TryGetValue(EventOrder.Normal, out UnityAction<T0, T1> hNormalAction))
            {
                hNormalAction?.Invoke(arg0, arg1);
            }

            if (hData.TryGetValue(EventOrder.PostNormal, out UnityAction<T0, T1> hPostNormalAction))
            {
                hPostNormalAction?.Invoke(arg0, arg1);
            }

            if (hData.TryGetValue(EventOrder.PreLate, out UnityAction<T0, T1> hPreLateAction))
            {
                hPreLateAction?.Invoke(arg0, arg1);
            }

            if (hData.TryGetValue(EventOrder.Late, out UnityAction<T0, T1> hLateAction))
            {
                hLateAction?.Invoke(arg0, arg1);
            }

            if (hData.TryGetValue(EventOrder.PostLate, out UnityAction<T0, T1> hPostLateAction))
            {
                hPostLateAction?.Invoke(arg0, arg1);
            }
        }

        #region Helper

        void CreateDictionaryDataIfNull()
        {
            if (m_dicActData == null)
                m_dicActData = new Dictionary<EventKey, Dictionary<EventOrder, UnityAction<T0, T1>>>();
        }

        bool HasKeyInData(EventKey eEventKey)
        {
            if (m_dicActData == null || m_dicActData.Count <= 0 || !m_dicActData.ContainsKey(eEventKey))
                return false;

            return true;
        }

        #endregion
    }

    public class EventCallback<EventKey, T0, T1, T2>
    {
        Dictionary<EventKey, Dictionary<EventOrder, UnityAction<T0, T1, T2>>> m_dicActData;

        /// <summary>
        /// Add callback to this event.
        /// </summary>
        public void Add(EventKey eventKey, UnityAction<T0, T1, T2> action)
        {
            MainAdd(eventKey, action);
        }

        /// <summary>
        /// Add callback to this event.
        /// </summary>
        public void Add(EventKey eventKey, UnityAction<T0, T1, T2> action, EventOrder orderType)
        {
            MainAdd(eventKey, action, orderType);
        }

        void MainAdd(EventKey eventKey, UnityAction<T0, T1, T2> action, EventOrder eOrderType = EventOrder.Normal)
        {
            CreateDictionaryDataIfNull();

            if (m_dicActData.TryGetValue(eventKey, out Dictionary<EventOrder, UnityAction<T0, T1, T2>> hOutData))
            {
                if (hOutData.TryGetValue(eOrderType, out UnityAction<T0, T1, T2> hOutAction))
                {
                    hOutAction += action;
                    hOutData[eOrderType] = hOutAction;
                }
                else
                {
                    hOutData.Add(eOrderType, action);
                }

                m_dicActData[eventKey] = hOutData;
            }
            else
            {
                var hNewData = new Dictionary<EventOrder, UnityAction<T0, T1, T2>>();
                hNewData.Add(eOrderType, action);
                m_dicActData.Add(eventKey, hNewData);
            }
        }

        /// <summary>
        /// Remove callback from this event.
        /// </summary>
        public void Remove(EventKey eventKey, UnityAction<T0, T1, T2> action)
        {
            MainRemove(eventKey, action);
        }

        /// <summary>
        /// Remove callback from this event.
        /// </summary>
        public void Remove(EventKey eventKey, UnityAction<T0, T1, T2> action, EventOrder orderType)
        {
            MainRemove(eventKey, action, orderType);
        }

        void MainRemove(EventKey eventKey, UnityAction<T0, T1, T2> action, EventOrder eOrderType = EventOrder.Normal)
        {
            if (!HasKeyInData(eventKey))
                return;

            var hData = m_dicActData[eventKey];
            if (!hData.ContainsKey(eOrderType))
                return;

            var hAction = hData[eOrderType];
            hAction -= action;
            hData[eOrderType] = hAction;

            m_dicActData[eventKey] = hData;
        }

        /// <summary>
        /// Clear all event callback in data.
        /// </summary>
        public void ClearAll()
        {
            if (m_dicActData == null || m_dicActData.Count <= 0)
                return;

            m_dicActData.Clear();
        }

        /// <summary>
        /// Clear all callback in this event.
        /// </summary>
        public void ClearEvent(EventKey eventKey)
        {
            if (!HasKeyInData(eventKey))
                return;

            var hData = m_dicActData[eventKey];
            hData.Clear();
            m_dicActData[eventKey] = hData;
        }

        /// <summary>
        /// Run event callback.
        /// </summary>
        public void Run(EventKey eventKey, T0 arg0, T1 arg1, T2 arg2)
        {
            if (!HasKeyInData(eventKey))
                return;

            var hData = m_dicActData[eventKey];

            if (hData.TryGetValue(EventOrder.PreEarly, out UnityAction<T0, T1, T2> hPreEarlyAction))
            {
                hPreEarlyAction?.Invoke(arg0, arg1, arg2);
            }

            if (hData.TryGetValue(EventOrder.Early, out UnityAction<T0, T1, T2> hEarlyAction))
            {
                hEarlyAction?.Invoke(arg0, arg1, arg2);
            }

            if (hData.TryGetValue(EventOrder.PostEalry, out UnityAction<T0, T1, T2> hPostEarlyAction))
            {
                hPostEarlyAction?.Invoke(arg0, arg1, arg2);
            }

            if (hData.TryGetValue(EventOrder.PreNormal, out UnityAction<T0, T1, T2> hPreNormalAction))
            {
                hPreNormalAction?.Invoke(arg0, arg1, arg2);
            }

            if (hData.TryGetValue(EventOrder.Normal, out UnityAction<T0, T1, T2> hNormalAction))
            {
                hNormalAction?.Invoke(arg0, arg1, arg2);
            }

            if (hData.TryGetValue(EventOrder.PostNormal, out UnityAction<T0, T1, T2> hPostNormalAction))
            {
                hPostNormalAction?.Invoke(arg0, arg1, arg2);
            }

            if (hData.TryGetValue(EventOrder.PreLate, out UnityAction<T0, T1, T2> hPreLateAction))
            {
                hPreLateAction?.Invoke(arg0, arg1, arg2);
            }

            if (hData.TryGetValue(EventOrder.Late, out UnityAction<T0, T1, T2> hLateAction))
            {
                hLateAction?.Invoke(arg0, arg1, arg2);
            }

            if (hData.TryGetValue(EventOrder.PostLate, out UnityAction<T0, T1, T2> hPostLateAction))
            {
                hPostLateAction?.Invoke(arg0, arg1, arg2);
            }
        }

        #region Helper

        void CreateDictionaryDataIfNull()
        {
            if (m_dicActData == null)
                m_dicActData = new Dictionary<EventKey, Dictionary<EventOrder, UnityAction<T0, T1, T2>>>();
        }

        bool HasKeyInData(EventKey eEventKey)
        {
            if (m_dicActData == null || m_dicActData.Count <= 0 || !m_dicActData.ContainsKey(eEventKey))
                return false;

            return true;
        }

        #endregion
    }
}