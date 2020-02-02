using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DSC.Dialogue
{
    [CreateAssetMenu(fileName = "DialogueEvent_SceneLoad", menuName = "DSC/Dialogue/Events/Scene Load")]
    public class DialogueEvent_SceneLoad : DialogueEvent
    {
        #region Enum

        protected enum LoadBy
        {
            Index,
            Name
        }

        #endregion

        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] protected LoadBy m_eLoadBy;
        [SerializeField] protected int m_nSceneIndex;
        [SerializeField] protected string m_sSceneName;

        [Header("Option")]
        [SerializeField] protected bool m_bLoadAsyne = true;

#pragma warning restore 0649
        #endregion

        #endregion

        #region Base - Override

        public override void OnStart(List<IDialogueEventData> lstData)
        {
            base.OnStart(lstData);

            if (!lstData.TryGetData(out DialogueEventData_MonoBehaviour<DSC_Dialogue_DataController> hDataController))
                return;

            hDataController.m_hMono.loadingScene = true;

            switch (m_eLoadBy)
            {
                case LoadBy.Index:
                    if (m_bLoadAsyne)
                        SceneManager.LoadSceneAsync(m_nSceneIndex);
                    else
                        SceneManager.LoadScene(m_nSceneIndex);
                    break;

                case LoadBy.Name:
                    if (m_bLoadAsyne)
                        SceneManager.LoadSceneAsync(m_sSceneName);
                    else
                        SceneManager.LoadScene(m_sSceneName);
                    break;
            }
            
        }

        #endregion
    }
}