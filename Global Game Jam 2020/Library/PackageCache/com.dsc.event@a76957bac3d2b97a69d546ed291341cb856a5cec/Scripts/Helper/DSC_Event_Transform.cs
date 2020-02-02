using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DSC.Event.Helper
{
    public class DSC_Event_Transform : MonoBehaviour
    {
        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] protected Vector3 m_vPosition;
        [SerializeField] protected Vector3 m_vRotation;
        [SerializeField] protected Vector3 m_vScale = Vector3.one;
        [SerializeField] protected GameObject m_hTargetParent;

#pragma warning restore 0649
        #endregion

        #endregion

        #region Events

        public void SetGameObjectFromPosition(GameObject hGameObject)
        {
            if (hGameObject == null)
                return;

            hGameObject.transform.position = m_vPosition;
        }

        public void SetPositionFromGameObject(GameObject hGameObject)
        {
            if (hGameObject == null)
            {
                m_vPosition = Vector3.zero;
                return;
            }

            m_vPosition = hGameObject.transform.position;
        }

        public void SetGameObjectFromRotation(GameObject hGameObject)
        {
            if (hGameObject == null)
                return;

            hGameObject.transform.localEulerAngles = m_vRotation;
        }

        public void SetRotationFromGameObject(GameObject hGameObject)
        {
            if (hGameObject == null)
            {
                m_vRotation = Vector3.zero;
                return;
            }

            m_vRotation = hGameObject.transform.localEulerAngles;
        }

        public void SetGameObjectFromScale(GameObject hGameObject)
        {
            if (hGameObject == null)
                return;

            hGameObject.transform.localScale = m_vScale;
        }

        public void SetScaleFromGameObject(GameObject hGameObject)
        {
            if (hGameObject == null)
            {
                m_vScale = Vector3.one;
                return;
            }

            m_vScale = hGameObject.transform.localScale;
        }

        public void CopyTransformFormGameObject(GameObject hGameObject)
        {
            if(hGameObject == null)
            {
                m_vPosition = Vector3.zero;
                m_vRotation = Vector3.zero;
                m_vScale = Vector3.one;
                return;
            }

            m_vPosition = hGameObject.transform.position;
            m_vRotation = hGameObject.transform.localEulerAngles;
            m_vScale = hGameObject.transform.localScale;
        }

        public void SetGameObjectParent(GameObject hGameObject)
        {
            if (hGameObject == null)
                return;

            if(m_hTargetParent)
            {
                hGameObject.transform.SetParent(m_hTargetParent.transform);
            }
            else
            {
                hGameObject.transform.SetParent(transform);
            }
        }

        public void SetTargetParent(GameObject hTargetParent)
        {
            m_hTargetParent = hTargetParent;
        }

        public void SetPosition(Vector3 vPosition)
        {
            m_vPosition = vPosition;
        }

        public void ResetPosition()
        {
            m_vPosition = Vector3.zero;
        }

        public void SetPositionX(float fPositionX)
        {
            m_vPosition.x = fPositionX;
        }

        public void SetPositionY(float fPositionY)
        {
            m_vPosition.y = fPositionY;
        }

        public void SetPositionZ(float fPositionZ)
        {
            m_vPosition.z = fPositionZ;
        }

        public void SetRotation(Vector3 vRotation)
        {
            m_vRotation = vRotation;
        }

        public void SetRotation(Quaternion qRotation)
        {
            m_vRotation = qRotation.eulerAngles;
        }

        public void ResetRotation()
        {
            m_vRotation = Vector3.zero;
        }

        public void SetRotationX(float fRotationX)
        {
            m_vRotation.x = fRotationX;
        }

        public void SetRotationY(float fRotationY)
        {
            m_vRotation.y = fRotationY;
        }

        public void SetRotationZ(float fRotationZ)
        {
            m_vRotation.z = fRotationZ;
        }

        public void SetScale(Vector3 vScale)
        {
            m_vScale = vScale;
        }

        public void ResetScale()
        {
            m_vScale = Vector3.one;
        }

        public void SetScaleX(float fScaleX)
        {
            m_vScale.x = fScaleX;
        }

        public void SetScaleY(float fScaleY)
        {
            m_vScale.y = fScaleY;
        }

        public void SetScaleZ(float fScaleZ)
        {
            m_vScale.z = fScaleZ;
        }

        #endregion
    }
}