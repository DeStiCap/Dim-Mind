using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DSC.Core;
using DSC.Actor;

namespace GGJ2020
{
    [CreateAssetMenu(fileName = "Behaviour_Boss_Attack", menuName = "DSC/Actor/Behaviour/Boss Attack")]
    public class DSC_ActorBehaviour_Boss_Attack : DSC_ActorBehaviour
    {
        #region Data

        [System.Serializable]
        protected struct AttackData
        {
            public int m_nEventIndex;
            public float m_fAttackEndDelay;
        }

        #endregion

        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

        [Header("Attack")]
        [SerializeField] protected AttackData[] m_arrAttackData;

#pragma warning restore 0649
        #endregion

        #region Variable - Property

        #endregion

        #endregion

        #region Base - Override

        public override void OnUpdateBehaviour(ActorData hActorData, List<IActorBehaviourData> lstBehaviourData)
        {
            base.OnUpdateBehaviour(hActorData, lstBehaviourData);

            if (hActorData.m_hAiData.m_bPatterning)
                return;

            if (m_arrAttackData == null || m_arrAttackData.Length <= 0)
                return;

            int nAttack = Random.Range(0, m_arrAttackData.Length);

            StartAttack(hActorData, lstBehaviourData,nAttack);
        }

        #endregion

        #region Helper

        protected virtual void StartAttack(ActorData hActorData, List<IActorBehaviourData> lstBehaviourData,int nAttack)
        {
            if(lstBehaviourData.TryGetData(out ActorMonoData < Actor_EventController > hEventController))
            {
                hEventController.m_hMono.RunEvent(m_arrAttackData[nAttack].m_nEventIndex);
            }

            hActorData.m_hAiData.m_fPatternLastTime = Time.time;
            hActorData.m_hAiData.m_fPatternEndCoolDown = m_arrAttackData[nAttack].m_fAttackEndDelay;
            hActorData.m_hAiData.m_bPatterning = true;
        }

        #endregion
    }
}