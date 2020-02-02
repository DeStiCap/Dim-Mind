using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DSC.Dialogue
{
    [CreateAssetMenu(fileName = "DialogueEventChoice", menuName = "DSC/Dialogue/Choice")]
    public class DialogueEventChoice : BaseDialogueChoice
    {
        #region Enum

        protected enum RunTimeType
        {
            OnStart,
            OnEnd
        }

        #endregion

        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] protected Choice[] m_arrChoice;

        [Header("Option")]
        [SerializeField] protected RunTimeType m_eRunTimeType = RunTimeType.OnStart;
        

#pragma warning restore 0649
        #endregion

        #region Variable - Property

        public override Choice[] choiceArray { get { return m_arrChoice; } }

        #endregion

        #endregion

        #region Base - Override

        public override void OnStart(List<IDialogueEventData> lstData)
        {
            base.OnStart(lstData);

            if (m_eRunTimeType != RunTimeType.OnStart)
                return;

            if (!lstData.TryGetData(out DialogueEventData_MonoBehaviour<DSC_Dialogue_ChoiceGroupController> hChoiceGroup))
                return;

            hChoiceGroup.m_hMono.SetAndShowChoice(m_arrChoice);
        }

        public override void OnEnd(List<IDialogueEventData> lstData)
        {
            if(m_eRunTimeType == RunTimeType.OnEnd)
            {
                if (lstData.TryGetData(out DialogueEventData_MonoBehaviour<DSC_Dialogue_ChoiceGroupController> hChoiceGroup))
                {
                    hChoiceGroup.m_hMono.SetAndShowChoice(m_arrChoice);
                }
            }

            base.OnEnd(lstData);
        }

        #endregion

    }
}