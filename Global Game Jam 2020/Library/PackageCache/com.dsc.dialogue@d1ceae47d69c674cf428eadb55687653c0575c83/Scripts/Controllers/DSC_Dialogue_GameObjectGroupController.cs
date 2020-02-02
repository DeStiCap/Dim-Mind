using System.Collections.Generic;
using UnityEngine;
using DSC.Core;

namespace DSC.Dialogue
{
    public class DSC_Dialogue_GameObjectGroupController : BaseComponentGroupController
    {
        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] protected int m_nGroupID;
        [SerializeField] protected DSC_Dialogue_DataController m_hDataController;
        [SerializeField] protected GameObject[] m_arrGameObject;

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
                if (m_hDataController.dialogueEventDataList.TryGetData(out DialogueEventData_GroupController<DSC_Dialogue_GameObjectGroupController> hOutData, out int nOutIndex))
                {
                    hOutData.m_lstGroupController.Add(this);
                    m_hDataController.dialogueEventDataList[nOutIndex] = hOutData;
                }
                else
                {
                    List<DSC_Dialogue_GameObjectGroupController> lstGroup = new List<DSC_Dialogue_GameObjectGroupController>();
                    lstGroup.Add(this);

                    m_hDataController.dialogueEventDataList.Add(new DialogueEventData_GroupController<DSC_Dialogue_GameObjectGroupController>
                    {
                        m_lstGroupController = lstGroup
                    });
                }
            }
        }

        /*
        protected virtual void OnDestroy()
        {
            if (m_hDataController != null && m_hDataController.dialogueEventDataList != null)
            {
                if (m_hDataController.dialogueEventDataList.TryGetData(out DialogueEventData_GroupController<DSC_Dialogue_GameObjectGroupController> hOutData, out int nOutIndex))
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

        public override void SetEnable(int nIndex, bool bEnable)
        {
            var hGameObject = GetGameObject(nIndex);
            if(hGameObject)
                hGameObject.SetActive(bEnable);
        }

        public override void SetAllEnable(bool bEnable)
        {
            for(int i = 0; i < m_arrGameObject.Length; i++)
            {
                var hGO = m_arrGameObject[i];
                if(hGO)
                    hGO.SetActive(bEnable);
            }
        }


        #endregion

        #region Helper

        protected GameObject GetGameObject(int nIndex)
        {
            if (nIndex < 0 || m_arrGameObject == null || m_arrGameObject.Length <= nIndex)
                return null;

            return m_arrGameObject[nIndex];
        }

       
        #endregion
    }
}