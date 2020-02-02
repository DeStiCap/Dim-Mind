using System.Collections.Generic;
using UnityEngine;
using DSC.Core;

namespace DSC.Dialogue
{
    public class DSC_Dialogue_AudioSourceGroupController : BaseComponentGroupController
    {
        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] protected int m_nGroupID;
        [SerializeField] protected DSC_Dialogue_DataController m_hDataController;
        [SerializeField] protected AudioSource[] m_arrAudioSource;

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
                if (m_hDataController.dialogueEventDataList.TryGetData(out DialogueEventData_GroupController<DSC_Dialogue_AudioSourceGroupController> hOutData, out int nOutIndex))
                {
                    hOutData.m_lstGroupController.Add(this);
                    m_hDataController.dialogueEventDataList[nOutIndex] = hOutData;
                }
                else
                {
                    List<DSC_Dialogue_AudioSourceGroupController> lstGroup = new List<DSC_Dialogue_AudioSourceGroupController>();
                    lstGroup.Add(this);

                    m_hDataController.dialogueEventDataList.Add(new DialogueEventData_GroupController<DSC_Dialogue_AudioSourceGroupController>
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
                if (m_hDataController.dialogueEventDataList.TryGetData(out DialogueEventData_GroupController<DSC_Dialogue_AudioSourceGroupController> hOutData, out int nOutIndex))
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
            if (!TryGetAudioSource(nIndex, out var hAudio))
                return;

            hAudio.enabled = bEnable;
        }

        public override void SetAllEnable(bool bEnable)
        {
            if (m_arrAudioSource == null)
                return;

            for(int i = 0; i < m_arrAudioSource.Length; i++)
            {
                var hAudioSource = m_arrAudioSource[i];
                if (hAudioSource)
                    hAudioSource.enabled = bEnable;
            }
        }

        #endregion

        #region Main

        public virtual void Play(int nIndex)
        {
            if (!TryGetAudioSource(nIndex, out var hAudio))
                return;

            hAudio.Play();
        }

        public virtual void Play(int nIndex, float fDelay)
        {
            if (!TryGetAudioSource(nIndex, out var hAudio))
                return;

            hAudio.PlayDelayed(fDelay);
        }

        public virtual void PlayOneShot(int nIndex,AudioClip hClip)
        {
            if (!TryGetAudioSource(nIndex, out var hAudio))
                return;

            hAudio.PlayOneShot(hClip);
        }

        public virtual void Stop(int nIndex)
        {
            if (!TryGetAudioSource(nIndex, out var hAudio))
                return;

            hAudio.Stop();
        }

        public virtual void SetClip(int nIndex, AudioClip hClip)
        {
            if (!TryGetAudioSource(nIndex, out var hAudio))
                return;

            hAudio.clip = hClip;
        }

        public virtual void SetLoop(int nIndex, bool bLoop)
        {
            if (!TryGetAudioSource(nIndex, out var hAudio))
                return;

            hAudio.loop = bLoop;
        }

        #endregion

        #region Helper

        protected AudioSource GetAudioSource(int nIndex)
        {
            if (nIndex < 0 || m_arrAudioSource == null || m_arrAudioSource.Length <= nIndex)
                return null;

            return m_arrAudioSource[nIndex];
        }

        protected bool TryGetAudioSource(int nIndex,out AudioSource hOutAudio)
        {
            hOutAudio = GetAudioSource(nIndex);
            return hOutAudio != null;
        }

        #endregion
    }
}