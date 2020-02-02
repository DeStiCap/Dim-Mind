using System.Collections.Generic;
using UnityEngine;

namespace DSC.Dialogue
{
    [CreateAssetMenu(fileName = "DialogueEvent_RawImageColor", menuName = "DSC/Dialogue/Events/Raw Image Color")]
    public class DialogueEvent_RawImageColor : DialogueEvent
    {
        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] protected int m_nGroupID;
        [SerializeField] protected int m_nIndex;
        [SerializeField] protected Color m_hColor;

#pragma warning restore 0649
        #endregion

        #endregion

        #region Base - Override

        public override void OnStart(List<IDialogueEventData> lstData)
        {
            base.OnStart(lstData);

            if (!lstData.TryGetData(out DialogueEventData_GroupController<DSC_Dialogue_RawImageGroupController> hOutData) || hOutData.m_lstGroupController == null)
                return;

            for(int i = 0; i < hOutData.m_lstGroupController.Count; i++)
            {
                var hGroupController = hOutData.m_lstGroupController[i];
                if (hGroupController == null)
                    continue;

                if(hGroupController.groupID == m_nGroupID)
                {
                    hGroupController.SetColor(m_nIndex, m_hColor);
                    break;
                }
            }
        }

        #endregion
    }
}