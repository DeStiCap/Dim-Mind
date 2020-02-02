using System.Collections.Generic;
using UnityEngine;
using DSC.UI;

namespace DSC.Dialogue
{
    public class DSC_Dialogue_ButtonGroupController : DSC_UI_ButtonGroupController
    {
        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] protected DSC_Dialogue_DataController m_hDataController;

#pragma warning restore 0649
        #endregion

        #endregion

        #region Base - Mono

        protected virtual void Awake()
        {
            if (m_hDataController && m_hDataController.dialogueEventDataList != null)
            {
                if(m_hDataController.dialogueEventDataList.TryGetData(out DialogueEventData_GroupController<DSC_Dialogue_ButtonGroupController> hOutData,out int nOutIndex))
                {
                    hOutData.m_lstGroupController.Add(this);
                    m_hDataController.dialogueEventDataList[nOutIndex] = hOutData;
                }
                else
                {
                    List<DSC_Dialogue_ButtonGroupController> lstGroup = new List<DSC_Dialogue_ButtonGroupController>();
                    lstGroup.Add(this);

                    m_hDataController.dialogueEventDataList.Add(new DialogueEventData_GroupController<DSC_Dialogue_ButtonGroupController>
                    {
                        m_lstGroupController = lstGroup
                    });
                }
            }
        }

        /*
        protected virtual void OnDestroy()
        {
            if (m_hDataController && m_hDataController.dialogueEventDataList != null)
            {
                if (m_hDataController.dialogueEventDataList.TryGetData(out DialogueEventData_GroupController<DSC_Dialogue_ButtonGroupController> hOutData, out int nOutIndex))
                {
                    hOutData.m_lstGroupController.Remove(this);

                    if (hOutData.m_lstGroupController.Count > 0)
                        m_hDataController.dialogueEventDataList[nOutIndex] = hOutData;
                    else
                        m_hDataController.dialogueEventDataList.RemoveAt(nOutIndex);
                }
            }
        }*/

        #endregion
    }
}