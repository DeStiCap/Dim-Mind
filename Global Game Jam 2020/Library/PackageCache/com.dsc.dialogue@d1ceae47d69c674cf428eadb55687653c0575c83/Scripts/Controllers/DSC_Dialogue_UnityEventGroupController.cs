using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DSC.Core;

namespace DSC.Dialogue
{
    public class DSC_Dialogue_UnityEventGroupController : BaseComponentGroupController
    {
        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] protected int m_nGroupID;
        [SerializeField] protected DSC_Dialogue_DataController m_hDataController;
        [SerializeField] protected DSC_Dialogue_UnityEventController[] m_arrEventController;

#pragma warning restore 0649
        #endregion

        #region Variable - Property

        public virtual int groupID { get { return m_nGroupID; } }

        #endregion

        #endregion

        #region Base - Mono

        protected virtual void Awake()
        {
            if (m_hDataController && m_hDataController.dialogueEventDataList != null)
            {
                if (m_hDataController.dialogueEventDataList.TryGetData(out DialogueEventData_GroupController<DSC_Dialogue_UnityEventGroupController> hOutData, out int nOutIndex))
                {
                    hOutData.m_lstGroupController.Add(this);
                    m_hDataController.dialogueEventDataList[nOutIndex] = hOutData;
                }
                else
                {
                    List<DSC_Dialogue_UnityEventGroupController> lstGroup = new List<DSC_Dialogue_UnityEventGroupController>();
                    lstGroup.Add(this);

                    m_hDataController.dialogueEventDataList.Add(new DialogueEventData_GroupController<DSC_Dialogue_UnityEventGroupController>
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
                if (m_hDataController.dialogueEventDataList.TryGetData(out DialogueEventData_GroupController<DSC_Dialogue_UnityEventGroupController> hOutData, out int nOutIndex))
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

        #region Base - Override

        public override void SetAllEnable(bool bEnable)
        {

        }

        public override void SetEnable(int nIndex, bool bEnable)
        {

        }

        #endregion

        #region Main

        public void RunEvent(int nIndex,float fDelayTime)
        {
            if (!m_arrEventController.TryGetData(nIndex, out var hController))
                return;

            hController.RunEvent(fDelayTime);
        }

        #endregion
    }
}