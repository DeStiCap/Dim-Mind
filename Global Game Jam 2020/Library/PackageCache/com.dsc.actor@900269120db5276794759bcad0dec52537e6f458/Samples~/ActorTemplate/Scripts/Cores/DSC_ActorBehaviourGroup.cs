using System.Collections;
using System.Collections.Generic;
using DSC.Actor;
using UnityEngine;

namespace DSC.Template.Actor.Default
{
    [CreateAssetMenu(fileName = "BehaviourGroup", menuName = "DSC/Actor/Behaviour Group")]
    public class DSC_ActorBehaviourGroup : DSC_ActorBehaviour
    {
        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] protected DSC_ActorBehaviour[] m_arrBehaviour;

#pragma warning restore 0649
        #endregion

        #endregion

        #region Base - Override

        public override void OnCreateBehaviour(ActorData hActorData, List<IActorBehaviourData> lstBehaviourData)
        {
            base.OnCreateBehaviour(hActorData, lstBehaviourData);

            if (!HasBehaviourInArray())
                return;

            for(int i = 0; i < m_arrBehaviour.Length; i++)
            {
                m_arrBehaviour[i]?.OnCreateBehaviour(hActorData, lstBehaviourData);                
            }
        }

        public override void OnDestroyBehaviour(ActorData hActorData, List<IActorBehaviourData> lstBehaviourData)
        {
            base.OnDestroyBehaviour(hActorData, lstBehaviourData);

            if (!HasBehaviourInArray())
                return;

            for(int i = 0; i < m_arrBehaviour.Length; i++)
            {
                m_arrBehaviour[i]?.OnDestroyBehaviour(hActorData, lstBehaviourData);
            }
        }

        public override void OnStartBehaviour(ActorData hActorData, List<IActorBehaviourData> lstBehaviourData)
        {
            if (isRunning)
                return;

            isRunning = true;

            if (!HasBehaviourInArray())
                return;

            for(int i = 0; i < m_arrBehaviour.Length; i++)
            {
                var hBehaviour = m_arrBehaviour[i];
                if (hBehaviour != null && hBehaviour.PassCondition(hActorData,lstBehaviourData))
                    hBehaviour.OnStartBehaviour(hActorData, lstBehaviourData);
            }
        }

        public override void OnStopBehaviour(ActorData hActorData, List<IActorBehaviourData> lstBehaviourData)
        {
            if (!isRunning)
                return;

            isRunning = false;

            if (!HasBehaviourInArray())
                return;

            for(int i = 0; i < m_arrBehaviour.Length; i++)
            {
                m_arrBehaviour[i]?.OnStopBehaviour(hActorData, lstBehaviourData);
            }
        }

        public override void OnUpdateBehaviour(ActorData hActorData, List<IActorBehaviourData> lstBehaviourData)
        {
            base.OnUpdateBehaviour(hActorData, lstBehaviourData);

            if (!HasBehaviourInArray())
                return;

            for(int i = 0; i < m_arrBehaviour.Length; i++)
            {
                var hBehaviour = m_arrBehaviour[i];
                if (hBehaviour == null)
                    continue;

                bool bRunning = false;

                if (hBehaviour.isRunning)
                {
                    if (!hBehaviour.PassCondition(hActorData, lstBehaviourData))
                        hBehaviour.OnStopBehaviour(hActorData, lstBehaviourData);
                    else
                        isRunning = true;
                }
                else if(hBehaviour.PassCondition(hActorData,lstBehaviourData))
                {
                    hBehaviour.OnStartBehaviour(hActorData, lstBehaviourData);
                    isRunning = true;
                }

                if(bRunning)
                    hBehaviour.OnUpdateBehaviour(hActorData, lstBehaviourData);
            }
        }

        public override void OnFixedUpdateBehaviour(ActorData hActorData, List<IActorBehaviourData> lstBehaviourData)
        {
            base.OnFixedUpdateBehaviour(hActorData, lstBehaviourData);

            if (!HasBehaviourInArray())
                return;

            for(int i = 0; i < m_arrBehaviour.Length; i++)
            {
                var hBehaviour = m_arrBehaviour[i];
                if(hBehaviour != null && hBehaviour.isRunning)
                    hBehaviour.OnFixedUpdateBehaviour(hActorData, lstBehaviourData);
            }
        }

        public override void OnLateUpdateBehaviour(ActorData hActorData, List<IActorBehaviourData> lstBehaviourData)
        {
            base.OnLateUpdateBehaviour(hActorData, lstBehaviourData);

            if (!HasBehaviourInArray())
                return;

            for(int i = 0; i < m_arrBehaviour.Length; i++)
            {
                var hBehaviour = m_arrBehaviour[i];
                if (hBehaviour != null && hBehaviour.isRunning)
                    hBehaviour.OnLateUpdateBehaviour(hActorData, lstBehaviourData);
            }
        }

        #endregion

        #region Helper

        protected bool HasBehaviourInArray()
        {
            return (m_arrBehaviour != null && m_arrBehaviour.Length > 0);
        }

        #endregion
    }
}