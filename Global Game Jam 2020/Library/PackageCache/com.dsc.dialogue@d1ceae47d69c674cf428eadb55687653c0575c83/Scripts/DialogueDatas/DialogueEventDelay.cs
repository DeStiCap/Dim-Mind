using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DSC.Dialogue
{
    [CreateAssetMenu(fileName = "DialogueEventDelay", menuName = "DSC/Dialogue/Event Delay")]
    public class DialogueEventDelay : DialogueEvent
    {
        #region Data

        [System.Serializable]
        public struct EventDelayData
        {
            public DialogueEvent m_hEvent;
            public float m_fDelayTime;
        }

        public struct DelayData
        {
            public DialogueEventDelay m_hKey;
            public List<EventDelayData> m_lstWaitRunEvent;
        }

        public struct DialogueEventData_Delay : IDialogueEventData
        {
            public List<DelayData> m_lstDelayData;

            public Stack<DelayData> m_lstFinishDelayData;
        }

        #endregion

        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] protected EventDelayData[] m_arrDelayEvent;

#pragma warning restore 0649
        #endregion

        #endregion

        #region Base - Override

        public override void OnStart(List<IDialogueEventData> lstData)
        {
            base.OnStart(lstData);

            DelayData hData;

            if(lstData.TryGetData(out DialogueEventData_Delay hDelayData,out var nIndex))
            {
                // Reuse old delay data.
                if(hDelayData.m_lstFinishDelayData != null && hDelayData.m_lstFinishDelayData.Count > 0)
                {
                    hData = hDelayData.m_lstFinishDelayData.Pop();
                    hData.m_hKey = this;
                    hData.m_lstWaitRunEvent.Clear();
                    hData.m_lstWaitRunEvent.AddRange(m_arrDelayEvent);
                }
                else
                {
                    hData = new DelayData
                    {
                        m_hKey = this,
                        m_lstWaitRunEvent = new List<EventDelayData>(m_arrDelayEvent)
                    };
                }

                hDelayData.m_lstDelayData.Add(hData);
                lstData[nIndex] = hDelayData;
            }
            else
            {
                hData = new DelayData
                {
                    m_hKey = this,
                    m_lstWaitRunEvent = new List<EventDelayData>(m_arrDelayEvent)
                };

                var lstDelayData = new List<DelayData>();
                lstDelayData.Add(hData);

                lstData.Add(new DialogueEventData_Delay
                {
                    m_lstDelayData = lstDelayData,
                });
            }
        }

        public override void OnExecute(List<IDialogueEventData> lstData)
        {
            base.OnExecute(lstData);

            if (!lstData.TryGetData(out DialogueEventData_Time hTimeData) || !lstData.TryGetData(out DialogueEventData_Delay hDelayData,out var nOutIndex))
                return;

            if (m_arrDelayEvent == null || m_arrDelayEvent.Length <= 0)
                return;


            int nDelayIndex = -1;
            DelayData hDelay = default;

            for(int i = 0; i < hDelayData.m_lstDelayData.Count; i++)
            {
                if(hDelayData.m_lstDelayData[i].m_hKey == this)
                {
                    hDelay = hDelayData.m_lstDelayData[i];
                    nDelayIndex = i;
                    break;
                }
            }

            if (nDelayIndex < 0)
                return;

            var lstWaitRun = hDelay.m_lstWaitRunEvent;
            for(int i = 0; i < m_arrDelayEvent.Length; i++)
            {
                var hDelayEvent = m_arrDelayEvent[i];
                if (hDelayEvent.m_hEvent == null)
                {
                    if (lstWaitRun.Contains(hDelayEvent))
                    {
                        lstWaitRun.Remove(hDelayEvent);
                    }

                    continue;
                }

                if (!lstWaitRun.Contains(hDelayEvent))
                {
                    hDelayEvent.m_hEvent.OnExecute(lstData);
                    continue;
                }

                if(Time.time >= hTimeData.m_fCurrentDialogueStartTime + hDelayEvent.m_fDelayTime)
                {
                    hDelayEvent.m_hEvent.OnStart(lstData);

                    lstWaitRun.Remove(hDelayEvent);
                }
            }

            hDelay.m_lstWaitRunEvent = lstWaitRun;
            hDelayData.m_lstDelayData[nDelayIndex] = hDelay;
            lstData[nOutIndex] = hDelayData;
        }

        public override void OnEnd(List<IDialogueEventData> lstData)
        {
            if(lstData.TryGetData(out DialogueEventData_Delay hDelayData,out var nOutIndex))
            {
                int nDelayIndex = -1;
                DelayData hDelay = default;

                for (int i = 0; i < hDelayData.m_lstDelayData.Count; i++)
                {
                    if (hDelayData.m_lstDelayData[i].m_hKey == this)
                    {
                        hDelay = hDelayData.m_lstDelayData[i];
                        nDelayIndex = i;
                        break;
                    }
                }

                if (nDelayIndex < 0 || hDelay.m_lstWaitRunEvent.Count <= 0)
                    goto End;

                for (int i = 0; i < hDelay.m_lstWaitRunEvent.Count; i++)
                {
                    var hEvent = hDelay.m_lstWaitRunEvent[i].m_hEvent;
                    if (hEvent != null)
                        hEvent.OnStart(lstData);
                }

                hDelay.m_lstWaitRunEvent.Clear();

                if (hDelayData.m_lstFinishDelayData == null)
                    hDelayData.m_lstFinishDelayData = new Stack<DelayData>();

                hDelayData.m_lstDelayData.RemoveAt(nDelayIndex);
                hDelayData.m_lstFinishDelayData.Push(hDelay);

                lstData[nOutIndex] = hDelayData;
            }

        End:
            for(int i = 0; i < m_arrDelayEvent.Length; i++)
            {
                var hEvent = m_arrDelayEvent[i];
                if (hEvent.m_hEvent != null)
                    hEvent.m_hEvent.OnEnd(lstData);
            }

            ;

            base.OnEnd(lstData);
        }

        #endregion
    }
}