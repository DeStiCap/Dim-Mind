using UnityEngine;

namespace DSC.Dialogue
{
    public class DSC_Dialogue_EventController : MonoBehaviour
    {
        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] DSC_Dialogue_DataController m_hDataController;

#pragma warning restore 0649
        #endregion

        #endregion

        #region Event

        public virtual void StartDialogueEvent(BaseDialogueEvent hEvent)
        {
            if (hEvent == null)
                return;

            var lstData = m_hDataController?.dialogueEventDataList;

            hEvent.OnStart(lstData);
        }

        #endregion
    }
}