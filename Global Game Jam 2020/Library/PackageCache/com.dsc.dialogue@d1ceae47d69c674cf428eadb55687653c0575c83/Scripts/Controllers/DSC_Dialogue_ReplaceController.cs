using System.Collections.Generic;
using UnityEngine;

namespace DSC.Dialogue
{
    public class DSC_Dialogue_ReplaceController : BaseDialoguePreProcessing
    {
        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] protected BaseDialogueReplace[] m_arrReplace;

#pragma warning restore 0649
        #endregion

        #endregion

        #region Events

        public override void PreProcessingDialogue(ref Dialogue hDialogue)
        {
            CheckReplaceWord(ref hDialogue);
        }

        #endregion

        #region Main

        protected virtual void CheckReplaceWord(ref Dialogue hDialogue)
        {
            if (m_arrReplace == null || m_arrReplace.Length <= 0)
                return;

            for(int i = 0; i < m_arrReplace.Length; i++)
            {
                var hReplace = m_arrReplace[i];
                if (hReplace == null)
                    continue;

                bool bReplaceDialogue = true;
                bool bReplace = true;
                switch (hReplace.ignoreType)
                {
                    case IgnoreReplaceType.Dialogue:
                        bReplaceDialogue = false;
                        break;

                    case IgnoreReplaceType.Talker:
                        bReplace = false;
                        break;
                }

                bool bColorDialogue = true;
                bool bColorTalker = true;
                switch (hReplace.ignoreColor)
                {
                    case IgnoreColorType.Dialogue:
                        bColorDialogue = false;
                        break;

                    case IgnoreColorType.Talker:
                        bColorTalker = false;
                        break;
                }

                if (bReplaceDialogue && hDialogue.m_sDialogue.Contains(hReplace.id))
                {
                    switch (hReplace.replaceType)
                    {
                        case ReplaceEventType.Replace:
                            ReplaceWord(ref hDialogue.m_sDialogue, hReplace);
                            break;

                        case ReplaceEventType.Color:
                            if(bColorDialogue)
                                ReplaceColor(ref hDialogue.m_sDialogue, hReplace);
                            break;

                        case ReplaceEventType.ReplaceColor:
                            if(bColorDialogue)
                                ReplaceColor(ref hDialogue.m_sDialogue, hReplace);
                            ReplaceWord(ref hDialogue.m_sDialogue, hReplace);
                            break;
                    }
                }

                if (bReplace && hDialogue.m_sTalker.Contains(hReplace.id))
                {
                    switch (hReplace.replaceType)
                    {
                        case ReplaceEventType.Replace:
                            ReplaceWord(ref hDialogue.m_sTalker, hReplace);
                            break;

                        case ReplaceEventType.Color:
                            if(bColorTalker)
                                ReplaceColor(ref hDialogue.m_sTalker, hReplace);
                            break;

                        case ReplaceEventType.ReplaceColor:
                            if(bColorTalker)
                                ReplaceColor(ref hDialogue.m_sTalker, hReplace);
                            ReplaceWord(ref hDialogue.m_sTalker, hReplace);
                            break;
                    }
                }
            }
        }

        protected void ReplaceWord(ref string sOriginal, BaseDialogueReplace hReplace)
        {
            var sReplaceID = hReplace.id;

            if (!TryGetReplaceData(hReplace, out var hData))
            {
                Debug.LogWarning("Don't have replace word '" + sReplaceID + "' in data.", gameObject);
                return;
            }
          
            sOriginal = sOriginal.Replace(sReplaceID, hData.replaceWord);
        }

        protected void ReplaceColor(ref string sOriginal, BaseDialogueReplace hReplace)
        {
            string sColorWord = "<color=#" + hReplace.replaceColor + ">" + hReplace.id + "</color>";
            sOriginal = sOriginal.Replace(hReplace.id, sColorWord);
        }
        
        #endregion

        #region Helper

        protected BaseDialogueReplaceData GetReplaceData(BaseDialogueReplace hReplace)
        {
            return hReplace.replaceData;
        }

        protected bool TryGetReplaceData(BaseDialogueReplace hReplace,out BaseDialogueReplaceData hData)
        {
            hData = GetReplaceData(hReplace);
            return hData != null;
        }

        #endregion
    }
}