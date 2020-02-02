using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DSC.Dialogue
{
    [CreateAssetMenu(fileName = "DialogueEvent_StartDialogue", menuName = "DSC/Dialogue/Events/Start Dialogue")]
    public class DialogueEvent_StartDialogue : DialogueEvent
    {
        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] protected DialogueData m_hDialogue;

#pragma warning restore 0649
        #endregion

        #endregion

        #region Base - Override

        public override void OnStart(List<IDialogueEventData> lstData)
        {
            base.OnStart(lstData);

            if (!lstData.TryGetData(out DialogueEventData_MonoBehaviour<DSC_Dialogue_DataController> hDataController))
                return;

            hDataController.m_hMono.SetAndStartNewDialogueData(m_hDialogue);
        }

        #endregion
    }
}