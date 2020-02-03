using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DSC.Core;

namespace GGJ2020
{
    public class KnifeFlyingController : FlyingController
    {
        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] protected bool m_bCanBreakRock;
        [SerializeField] protected Transform m_hHitParticalPrefab;

        [Header("Events")]
        [SerializeField] protected UnityEvent m_hHitEvent;

#pragma warning restore 0649
        #endregion

        #region Variable - Property

        public DSC_ActorController owner { get; set; }
        public bool canBreakRock { get { return m_bCanBreakRock; } set { m_bCanBreakRock = value; } }

        #endregion

        #endregion

        #region Base - Mono

        private void OnBecameInvisible()
        {
            gameObject.SetActive(false);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag(TagUtility.Name.player))
            {
                if (canBreakRock && collision.CompareTag(TagUtility.Name.rock))
                {
                    var hDestroyable = collision.GetComponent<IDestroyable>();
                    if(hDestroyable != null)
                    {
                        hDestroyable.Destroy();
                    }
                }

                if (owner) {
                    var hDamageable = collision.GetComponent<IDamageable>();
                    if (hDamageable != null)
                    {
                        hDamageable.DoDamage(owner.actorData.m_hStatus.status.m_nAttack);
                    }
                }

                if (m_hHitParticalPrefab)
                {
                    var hPartical = Instantiate(m_hHitParticalPrefab, transform.position, Quaternion.identity);
                    
                }

                m_hHitEvent?.Invoke();
                gameObject.SetActive(false);
            }
        }

        #endregion

        #region Helper

        #endregion
    }
}