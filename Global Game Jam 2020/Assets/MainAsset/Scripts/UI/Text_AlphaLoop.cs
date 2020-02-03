using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DSC.Core;
using TMPro;

namespace GGJ2020
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class Text_AlphaLoop : MonoBehaviour
    {
        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

        [Min(0)]
        [SerializeField] protected float m_fSpeed = 1;

#pragma warning restore 0649
        #endregion

        #region Variable - Property

        #endregion

        TextMeshProUGUI m_hText;

        bool m_bFading;

        #endregion

        #region Base - Mono

        private void Awake()
        {
            m_hText = GetComponent<TextMeshProUGUI>();
        }

        private void Update()
        {
            var hColor = m_hText.color;

        StartAgain:
            ;

            if (!m_bFading)
            {
                if(hColor.a >= 1)
                {
                    m_bFading = true;
                    goto StartAgain;
                }

                hColor.a += Time.deltaTime * m_fSpeed;
            }
            else
            {
                if(hColor.a <= 0)
                {
                    m_bFading = false;
                    goto StartAgain;
                }

                hColor.a -= Time.deltaTime * m_fSpeed;
            }

            m_hText.color = hColor;
        }

        #endregion

        #region Helper

        #endregion
    }
}