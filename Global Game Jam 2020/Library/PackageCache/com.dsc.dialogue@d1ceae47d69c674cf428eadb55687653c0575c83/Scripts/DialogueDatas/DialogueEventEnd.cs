using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DSC.Dialogue
{
    [CreateAssetMenu(fileName = "DialogueEventEnd", menuName = "DSC/Dialogue/Event End")]
    public class DialogueEventEnd : DialogueEvent
    {
        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] protected DialogueEvent[] m_arrEvent;

#pragma warning restore 0649
        #endregion

        #endregion

        #region Base - Override

        public override void OnEnd(List<IDialogueEventData> lstData)
        {
            if(m_arrEvent != null && m_arrEvent.Length > 0)
            {
                for(int i = 0; i < m_arrEvent.Length; i++)
                {
                    var hEvent = m_arrEvent[i];
                    if(hEvent != null)
                    {
                        hEvent.OnStart(lstData);
                        hEvent.OnEnd(lstData);
                    }
                }
            }

            base.OnEnd(lstData);
        }

        #endregion
    }
}