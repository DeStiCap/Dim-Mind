using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DSC.Core;

namespace DSC.Dialogue
{
    public class DSC_Dialogue_ChoiceGroupController : BaseComponentGroupController
    {
        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] protected DSC_Dialogue_DataController m_hDataController;
        [SerializeField] protected DSC_Dialogue_ChoiceController[] m_arrChoiceController;

#pragma warning restore 0649
        #endregion

        #endregion

        #region Base - Mono

        protected virtual void Awake()
        {
            if (m_hDataController && m_hDataController.dialogueEventDataList != null)
            {
                m_hDataController.dialogueEventDataList.Add(new DialogueEventData_MonoBehaviour<DSC_Dialogue_ChoiceGroupController>
                {
                    m_hMono = this
                });
            }
        }

        /*
        protected virtual void OnDestroy()
        {
            if (m_hDataController != null && m_hDataController.dialogueEventDataList != null)
            {
                if (m_hDataController.dialogueEventDataList.TryGetData(out DialogueEventData_MonoBehaviour<DSC_Dialogue_ChoiceGroupController> hOutData, out int nOutIndex))
                {
                    if(hOutData.m_hMono == this)
                        m_hDataController.dialogueEventDataList.RemoveAt(nOutIndex);
                }
            }
        }*/

        #endregion

        #region Base - Override

        public override void SetEnable(int nIndex, bool bEnable)
        {
            if (!TryGetChoiceController(nIndex, out var hChoice))
                return;

            hChoice.SetEnable(bEnable);
        }

        public override void SetAllEnable(bool bEnable)
        {
            if (!HasChoiceController())
                return;

            for(int i = 0; i < m_arrChoiceController.Length; i++)
            {
                var hChoice = m_arrChoiceController[i];
                if (hChoice != null)
                    hChoice.SetEnable(bEnable);
            }
        }

        #endregion

        #region Main

        public virtual void SetAndShowChoice(Choice[] arrChoice)
        {
            if (arrChoice == null || arrChoice.Length <= 0)
                return;

            SetAllEnable(false);

            for(int i = 0; i < arrChoice.Length; i++)
            {
                if (!TryGetChoiceController(i, out var hOutChoice))
                    continue;

                hOutChoice.SetChoice(arrChoice[i]);
                SetEnable(i, true);
            }
        }

        #endregion

        #region Helper

        protected bool HasChoiceController()
        {
            return (m_arrChoiceController != null && m_arrChoiceController.Length > 0);
        }

        protected DSC_Dialogue_ChoiceController GetChoiceController(int nIndex)
        {
            if (m_arrChoiceController == null || m_arrChoiceController.Length <= nIndex)
                return null;

            return m_arrChoiceController[nIndex];
        }

        protected bool TryGetChoiceController(int nIndex,out DSC_Dialogue_ChoiceController hOutController)
        {
            hOutController = GetChoiceController(nIndex);
            return hOutController != null;
        }

        #endregion
    }
}