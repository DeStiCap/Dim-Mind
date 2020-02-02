using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DSC.Core;

namespace GGJ2020
{
    public class Event_StoneDrop : MonoBehaviour
    {
        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] protected Transform[] m_arrStonePrefab;
        [SerializeField] protected Transform[] m_arrPosition;
        [Min(0)]
        [SerializeField] protected int m_nMinStone;
        [Min(0)]
        [SerializeField] protected int m_nMaxStone;
        [Min(0)]
        [SerializeField] protected float m_fDelayBetweenSpawn = 0.15f;

        [Header("Option")]
        [SerializeField] protected bool m_bSetToRootOnAwake;

#pragma warning restore 0649
        #endregion

        #region Variable - Property

        #endregion

        Coroutine m_crtSpawn;

        WaitForSeconds m_wfsSpawn;

        #endregion

        #region Base - Mono

        private void Awake()
        {
            m_wfsSpawn = new WaitForSeconds(m_fDelayBetweenSpawn);

            if (m_bSetToRootOnAwake)
                transform.SetParent(null);
        }

        protected virtual void OnValidate()
        {
            if (m_nMaxStone < m_nMinStone)
                m_nMaxStone = m_nMinStone;

            m_wfsSpawn = new WaitForSeconds(m_fDelayBetweenSpawn);
        }

        #endregion

        #region Events

        public void DropStone()
        {
            if (m_arrStonePrefab == null || m_arrStonePrefab.Length <= 0 || m_arrPosition == null || m_arrPosition.Length <= 1)
                return;


            if (m_crtSpawn != null)
                StopCoroutine(m_crtSpawn);

            m_crtSpawn = StartCoroutine(SpawnLoop());
        }

        IEnumerator SpawnLoop()
        {
            int nSpawn = Random.Range(m_nMinStone, m_nMaxStone);

            for (int i = 0; i < nSpawn; i++)
            {

                var hStone = m_arrStonePrefab[Random.Range(0, m_arrStonePrefab.Length)];

                Vector2 vPosition1 = m_arrPosition[0].position;
                Vector2 vPosition2 = m_arrPosition[1].position;

                Vector2 vMin = Vector2.zero;
                Vector2 vMax = Vector2.zero;
                vMin.x = vPosition1.x < vPosition2.x ? vPosition1.x : vPosition2.x;
                vMax.x = vPosition1.x >= vPosition2.x ? vPosition1.x : vPosition2.x;
                vMin.y = vPosition1.y < vPosition2.y ? vPosition1.y : vPosition2.y;
                vMax.y = vPosition1.y >= vPosition2.y ? vPosition1.y : vPosition2.y;


                Vector2 vPos = new Vector2(Random.Range(vMin.x, vMax.x), Random.Range(vMin.y, vMax.y));

                Instantiate(hStone, vPos, Quaternion.identity);

                yield return m_wfsSpawn;
            }

            yield return null;
        }

        #endregion

        #region Helper

        #endregion
    }
}