using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DSC.Core;
using TMPro;

namespace DSC.Dialogue
{
    public class DSC_Dialogue_ChoiceController : BaseComponentController
    {
        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] protected DSC_Dialogue_DataController m_hDataController;
        [SerializeField] protected TextMeshProUGUI m_hChoiceText;
        [SerializeField] protected UnityEvent m_hChooseEvent;

#pragma warning restore 0649
        #endregion

        #region Variable - Property

        public TextMeshProUGUI text
        {
            get
            {
                if (m_hChoiceText == null)
                    m_hChoiceText = GetComponentInChildren<TextMeshProUGUI>();

                return m_hChoiceText;
            }
        }

        protected DSC_Dialogue_DataController dataController
        {
            get
            {
                if (m_hDataController == null)
                    m_hDataController = FindObjectOfType<DSC_Dialogue_DataController>();

                return m_hDataController;
            }
        }

        #endregion

        protected DialogueEvent[] m_arrEvent;

        #endregion

        #region Base - Override

        public override void SetEnable(bool bEnable)
        {
            gameObject.SetActive(bEnable);
        }

        #endregion

        #region Events

        public void SetChoice(Choice hChoice)
        {
            if (text != null)
                m_hChoiceText.text = hChoice.m_sChoice;

            m_arrEvent = hChoice.m_arrEvent;
        }

        public void ChooseThisChoice()
        {
            if(dataController != null && m_arrEvent != null && m_arrEvent.Length > 0)
            {
                for(int i = 0; i < m_arrEvent.Length; i++)
                {
                    var hEvent = m_arrEvent[i];
                    if(hEvent != null)
                    {
                        hEvent.OnStart(m_hDataController.dialogueEventDataList);
                        hEvent.OnEnd(m_hDataController.dialogueEventDataList);
                    }
                }
            }

            m_hChooseEvent?.Invoke();
        }

        #endregion
    }
}