using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DSC.Dialogue
{
    [CreateAssetMenu(fileName = "DialogueEvent_UnityEventRun", menuName = "DSC/Dialogue/Events/Unity Event Run")]
    public class DialogueEvent_UnityEventRun : DialogueEvent
    {
        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] protected int m_nGroupID;
        [SerializeField] protected int m_nIndex;

        [Header("Option")]
        [Min(0)]
        [SerializeField] protected float m_fDelayTime;

#pragma warning restore 0649
        #endregion

        #endregion

        #region Main

        public override void OnStart(List<IDialogueEventData> lstData)
        {
            base.OnStart(lstData);

            if (!lstData.TryGetData(out DialogueEventData_GroupController<DSC_Dialogue_UnityEventGroupController> hGroupController))
                return;

            var lstGroup = hGroupController.m_lstGroupController;
            if (lstGroup == null || lstGroup.Count <= 0)
                return;

            for(int i = 0; i < lstGroup.Count; i++)
            {
                var hGroup = lstGroup[i];
                if(hGroup != null && hGroup.groupID == m_nGroupID)
                {
                    hGroup.RunEvent(m_nIndex,m_fDelayTime);
                    break;
                }
            }
        }

        #endregion
    }
}