using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DSC.Core;

namespace DSC.Event.Helper
{
    public abstract class BaseRunEventByOverlap : BaseRunEventByPhysic
    {
        #region Variable - Property

        public abstract Transform overlapPosition { get; set; }
        protected abstract EventCondition[] eventConditionArray { get; set; }
        protected abstract UnityEvent hitEvent { get; set; }
        protected abstract UnityEvent notHitEvent { get; set; }
        protected abstract EventGameObject hitEventGameObject { get; set; }

        #endregion

        #region Base - Override

        protected override void CheckPhysic()
        {
            CheckOverlap();
        }

        #endregion

        #region Events

        public void SetOverlapPosition(Transform hOverlapPosition)
        {
            overlapPosition = hOverlapPosition;
        }

        public void SetCheckUpdate(bool bCheck)
        {
            checkUpdate = bCheck;
        }

        public void SetCondition(params EventCondition[] arrCondition)
        {
            eventConditionArray = arrCondition;
        }

        public void SetIgnoreTargegt(params GameObject[] arrIgnore)
        {
            ignoreTargetArray = arrIgnore;
        }

        public void SetFoundEvent(UnityEvent hEvent)
        {
            hitEvent = hEvent;
        }

        public void SetNotFoundEvent(UnityEvent hEvent)
        {
            notHitEvent = hEvent;
        }

        public void ChangeUpdateType(UpdateType eType)
        {
            updateType = eType;
        }

        public abstract void CheckOverlap();

        #endregion
    }
}