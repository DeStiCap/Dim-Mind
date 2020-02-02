using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DSC.Core;

namespace GGJ2020
{
    public class Item_SkillController : MonoBehaviour
    {
        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] protected Skill m_eSkill;
        [SerializeField] protected UnityEvent m_hGetEvent;

#pragma warning restore 0649
        #endregion

        #region Variable - Property

        #endregion

        #endregion

        #region Base - Mono

        private void Start()
        {
            if (FlagUtility.HasFlagUnsafe(Global_GameplayManager.playerSkill, m_eSkill))
            {
                GetSkill();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(TagUtility.Name.player))
            {
                var hPlayerController = Global_GameplayManager.playerController;
                if (hPlayerController != null)
                    hPlayerController.actorData.m_eSkill |= m_eSkill;

                Global_GameplayManager.playerSkill |= m_eSkill;
                Global_GameplayManager.getItemEvent?.Invoke(m_eSkill);

                GetSkill();
            }
        }

        #endregion

        #region Main

        void GetSkill()
        {
            

            m_hGetEvent?.Invoke();
            Destroy(gameObject);
        }

        #endregion

        #region Helper

        #endregion
    }
}