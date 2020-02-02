using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DSC.Core;

namespace DSC.Event.Helper
{
    public class DSC_Event_GameObject : MonoBehaviour
    {
        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

        [Header("Spawn")]
        [SerializeField] protected List<GameObject> m_lstSpawnPosition;
        [SerializeField] protected EventGameObject m_hSpawnEvent;

#pragma warning restore 0649
        #endregion

        protected List<GameObject> m_lstTempSpawnPosition;

        #endregion

        #region Events

        public void SetSpawnPosition(params GameObject[] arrSpawnPosition)
        {
            if (m_lstSpawnPosition == null)
                m_lstSpawnPosition = new List<GameObject>();
            else
                m_lstSpawnPosition.Clear();
            
            m_lstSpawnPosition.AddRange(arrSpawnPosition);
        }

        public void AddSpawnPosition(params GameObject[] arrSpawnPosition)
        {
            if (m_lstSpawnPosition == null)
                m_lstSpawnPosition = new List<GameObject>();

            m_lstSpawnPosition.AddRange(arrSpawnPosition);
        }

        public void RemoveSpawnPosition(GameObject hSpawnPosition)
        {
            m_lstSpawnPosition.Remove(hSpawnPosition);
        }

        public void RemoveAllSpawnPosition()
        {
            m_lstSpawnPosition.Clear();
        }

        public void SetSpawnEvent(EventGameObject hEvent)
        {
            m_hSpawnEvent = hEvent;
        }

        public void AddSpawnEvent(UnityAction<GameObject> hAction)
        {
            m_hSpawnEvent?.AddListener(hAction);
        }

        public void RemoveSpawnEvent(UnityAction<GameObject> hAction)
        {
            m_hSpawnEvent?.RemoveListener(hAction);
        }

        public void RemoveAllSpawnEvent()
        {
            m_hSpawnEvent?.RemoveAllListeners();
        }

        public void SpawnGameObject(GameObject hGameObject)
        {
            if (hGameObject == null)
            {
                Debug.LogWarning("Don't have any GameObject to spawn.");
                return;
            }
            Vector3 vPos = transform.position;

            if (m_lstSpawnPosition != null && m_lstSpawnPosition.Count > 0)
            {
                if (m_lstTempSpawnPosition == null)
                    m_lstTempSpawnPosition = new List<GameObject>();
                else
                    m_lstTempSpawnPosition.Clear();
                
                m_lstTempSpawnPosition.AddRange(m_lstSpawnPosition);

                do
                {
                    int nRan = Random.Range(0, m_lstTempSpawnPosition.Count);

                    var hSpawnPos = m_lstTempSpawnPosition[nRan];
                    if(hSpawnPos != null)
                    {
                        vPos = hSpawnPos.transform.position;

                        break;
                    }

                    m_lstTempSpawnPosition.RemoveAt(nRan);

                } while (m_lstTempSpawnPosition.Count > 0);
            }


            GameObject hGO = Instantiate(hGameObject, vPos, Quaternion.identity);

            m_hSpawnEvent?.Invoke(hGO);
        }

        public void DestroyGameObject(GameObject hGameObject)
        {
            if(hGameObject.scene.rootCount == 0)
            {
                Debug.LogWarning("Can't destroy prefab.");
                return;
            }

            if(hGameObject != null)
                Destroy(hGameObject);
        }

        public void DestroyThisGameObject()
        {
            Destroy(gameObject);
        }

        public void EnableGameObject(GameObject hGameObject)
        {
            if (hGameObject == null)
                return;

            hGameObject.SetActive(true);
        }

        public void DisableGameObject(GameObject hGameObject)
        {
            if (hGameObject == null)
                return;

            hGameObject.SetActive(false);
        }

        #endregion
    }
}