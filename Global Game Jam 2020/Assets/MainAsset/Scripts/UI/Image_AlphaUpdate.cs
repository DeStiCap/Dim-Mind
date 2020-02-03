using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DSC.Core;

namespace GGJ2020
{
    [RequireComponent(typeof(Image))]
    public class Image_AlphaUpdate : MonoBehaviour
    {
        #region Enum

        protected enum SetType
        {
            Up,
            Down
        }

        #endregion

        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] protected SetType m_eSetType;
        [Min(0)]
        [SerializeField] protected float m_fSpeedMultiplier = 1f;

#pragma warning restore 0649
        #endregion

        #region Variable - Property

        #endregion

        protected Image m_hImage;

        #endregion

        #region Base - Mono

        protected virtual void Awake()
        {
            m_hImage = GetComponent<Image>();
        }

        protected virtual void Update()
        {
            var hColor = m_hImage.color;
            switch (m_eSetType)
            {
                case SetType.Up:
                    if (hColor.a >= 1)
                        goto Finish;
                    hColor.a += Time.deltaTime * m_fSpeedMultiplier;
                    break;

                case SetType.Down:
                    if (hColor.a <= 0)
                        goto Finish;
                    hColor.a -= Time.deltaTime * m_fSpeedMultiplier;
                    break;
            }

            m_hImage.color = hColor;

        Finish:

            ;
        }

        #endregion

        #region Helper

        #endregion
    }
}