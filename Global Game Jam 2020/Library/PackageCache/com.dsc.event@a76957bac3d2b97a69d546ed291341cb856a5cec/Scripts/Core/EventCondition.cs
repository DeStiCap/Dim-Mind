using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DSC.Event
{
    #region Data

    public struct EventConditionData : IEventConditionData
    {
        public Transform m_hTransform;

        public EventConditionData(Transform hTransform)
        {
            m_hTransform = hTransform;
        }
    }

    #endregion

    /// <summary>
    /// Will remove in Unity 2020.1
    /// </summary>
    public abstract class EventCondition : BaseEventCondition<EventConditionData>
    {

    }
}