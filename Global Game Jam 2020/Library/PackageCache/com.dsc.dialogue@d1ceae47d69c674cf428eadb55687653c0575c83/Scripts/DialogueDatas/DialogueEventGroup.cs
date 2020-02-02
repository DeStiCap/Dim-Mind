using System.Collections.Generic;
using UnityEngine;

namespace DSC.Dialogue
{
    [CreateAssetMenu(fileName = "DialogueEventGroup", menuName = "DSC/Dialogue/Event Group")]
    public class DialogueEventGroup : DialogueEvent
    {
        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] BaseDialogueEvent[] m_arrEvents;

#pragma warning restore 0649
        #endregion

        #endregion

        #region Base - Override

        public override void OnStart(List<IDialogueEventData> lstData)
        {
            base.OnStart(lstData);

            if (!HasEventInArray())
                return;

            for(int i = 0; i < m_arrEvents.Length; i++)
            {
                var hEvent = m_arrEvents[i];
                if (hEvent == null)
                    continue;

                hEvent.OnStart(lstData);
            }
        }

        public override void OnExecute(List<IDialogueEventData> lstData)
        {
            base.OnExecute(lstData);

            if (!HasEventInArray())
                return;

            for (int i = 0; i < m_arrEvents.Length; i++)
            {
                var hEvent = m_arrEvents[i];
                if (hEvent == null)
                    continue;

                hEvent.OnExecute(lstData);
            }
        }

        public override void OnEnd(List<IDialogueEventData> lstData)
        {
            base.OnEnd(lstData);

            if (!HasEventInArray())
                return;

            for (int i = 0; i < m_arrEvents.Length; i++)
            {
                var hEvent = m_arrEvents[i];
                if (hEvent == null)
                    continue;

                hEvent.OnEnd(lstData);
            }
        }

        #endregion

        #region Helper

        protected bool HasEventInArray()
        {
            return (m_arrEvents != null && m_arrEvents.Length > 0);
        }

        #endregion
    }
}