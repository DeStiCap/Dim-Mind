using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DSC.Core;
using DSC.Actor;
using Spine.Unity;

namespace GGJ2020
{
    public class Actor_SpineAnimationController : MonoBehaviour
    {
        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] protected DSC_ActorController m_hActorController;

        [Header("Spine")]
        [SpineAnimation]
        public string m_sIdleAnimation;

        [SpineAnimation]
        public string m_sRunAnimationName;

        [SpineAnimation]
        public string m_sJumpingAnimation;

        [SpineAnimation]
        public string m_sJumpStartAnimation;

        [SpineAnimation]
        public string m_sLandingAnimation;

        [SpineAnimation]
        public string m_sAttackAnimation;

        [SpineAnimation]
        public string m_sDamageAnimation;

        [SpineAnimation]
        public string m_sGroundPoundAnimation;

        [SpineAnimation]
        public string m_sGroundPoundCastingAnimation;

        [SpineAnimation]
        public string m_sClimbAnimation;

        public bool m_bPlayer;

        // Spine.AnimationState and Spine.Skeleton are not Unity-serialized objects. You will not see them as fields in the inspector.
        public Spine.AnimationState m_hSpineAnimationState;
        public Spine.Skeleton m_hSkeleton;

#pragma warning restore 0649
        #endregion

        #region Variable - Property

        #endregion

        SkeletonAnimation m_hSkeletonAnimation;

        #endregion

        #region Base - Mono

        private void Start()
        {
            m_hActorController.actorData.m_hAnimation = this;
            // Make sure you get these AnimationState and Skeleton references in Start or Later.
            // Getting and using them in Awake is not guaranteed by default execution order.
            m_hSkeletonAnimation = GetComponent<SkeletonAnimation>();
            m_hSpineAnimationState = m_hSkeletonAnimation.AnimationState;
            m_hSkeleton = m_hSkeletonAnimation.Skeleton;
        }

        #endregion

        #region Helper

        #endregion
    }
}