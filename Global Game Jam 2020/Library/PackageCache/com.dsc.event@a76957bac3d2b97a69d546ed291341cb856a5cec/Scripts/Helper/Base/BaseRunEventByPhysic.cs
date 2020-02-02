using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DSC.Core;

namespace DSC.Event.Helper
{
    public abstract class BaseRunEventByPhysic : MonoBehaviour
    {
        #region Variable

        #region Variable - Property

        protected abstract EventConditionData eventConditionData { get; set; }
        protected abstract GameObject[] ignoreTargetArray { get; set; }
        protected abstract bool checkUpdate { get; set; }
        protected abstract UpdateType updateType { get; set; }

        #endregion

        #endregion

        #region Base - Mono

        protected virtual void Awake()
        {
            eventConditionData = new EventConditionData(transform);
        }

        protected virtual void Update()
        {
            if (checkUpdate && updateType == UpdateType.Update)
                CheckPhysic();
        }

        protected virtual void FixedUpdate()
        {
            if (checkUpdate && updateType == UpdateType.FixedUpdate)
                CheckPhysic();
        }

        protected virtual void LateUpdate()
        {
            if (checkUpdate && updateType == UpdateType.LateUpdate)
                CheckPhysic();
        }

        #endregion

        #region Main

        protected abstract void CheckPhysic();

        #endregion

        #region Helper

        protected bool CheckNotIgnoreTarget(GameObject hCheckTarget)
        {
            if (ignoreTargetArray == null || ignoreTargetArray.Length <= 0)
                return true;

            for (int i = 0; i < ignoreTargetArray.Length; i++)
            {
                if (ignoreTargetArray[i] == hCheckTarget)
                    return false;
            }

            return true;
        }

        #endregion
    }
}