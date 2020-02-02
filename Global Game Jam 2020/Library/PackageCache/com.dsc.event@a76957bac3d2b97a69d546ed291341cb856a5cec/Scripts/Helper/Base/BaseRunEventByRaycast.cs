using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DSC.Core;

namespace DSC.Event.Helper
{
    public abstract class BaseRunEventByRaycast : BaseRunEventByPhysic
    {
        #region Variable

        #region Variable - Property

        protected abstract UnityEvent hitEvent { get; set; }
        protected abstract UnityEvent notHitEvent { get; set; }
        protected abstract EventGameObject hitEventGameObject { get; set; }
        
        #endregion

        #endregion

        #region Base - Override

        protected override void CheckPhysic()
        {
            CheckRaycast();
        }

        #endregion

        #region Events

        public abstract void CheckRaycast();

        public void SetHitEvent(UnityEvent hEvent)
        {
            hitEvent = hEvent;
        }

        public void AddHitEvent(UnityAction hAction)
        {
            hitEvent?.AddListener(hAction);
        }

        public void RemoveHitEvent(UnityAction hAction)
        {
            hitEvent?.RemoveListener(hAction);
        }

        public void RemoveAllHitEvent()
        {
            hitEvent?.RemoveAllListeners();
        }

        public void SetNotHitEvent(UnityEvent hEvent)
        {
            notHitEvent = hEvent;
        }

        public void AddNotHitEvent(UnityAction hAction)
        {
            notHitEvent?.AddListener(hAction);
        }

        public void RemoveNotHitEvent(UnityAction hAction)
        {
            notHitEvent?.RemoveListener(hAction);
        }

        public void RemoveAllNotHitEvent()
        {
            notHitEvent?.RemoveAllListeners();
        }

        public void SetHitEventGameObject(EventGameObject hEvent)
        {
            hitEventGameObject = hEvent;
        }

        public void AddHitEventGameObject(UnityAction<GameObject> hAction)
        {
            hitEventGameObject?.AddListener(hAction);
        }

        public void RemoveHitEventGameObject(UnityAction<GameObject> hAction)
        {
            hitEventGameObject?.RemoveListener(hAction);
        }

        public void RemoveAllHitEventGameObject()
        {
            hitEventGameObject?.RemoveAllListeners();
        }

        #endregion
    }
}