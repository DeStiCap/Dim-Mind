using UnityEngine;

namespace DSC.Event.Helper
{
    public class DSC_Event_Rigidbody : MonoBehaviour
    {
        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] protected Vector3 m_vVelocity;
        [Min(0)]
        [SerializeField] protected float m_fMass = 1;
        [Min(0)]
        [SerializeField] protected float m_fDrag = 0;
        [Min(0)]
        [SerializeField] protected float m_fAngularDrag = 0.05f;

#pragma warning restore 0649
        #endregion

        #endregion

        #region Events

        public void SetGameObjectVelocity(GameObject hGameObject)
        {
            if (!TryGetRigidbody(hGameObject, out var hRigid))
                return;

            hRigid.velocity = m_vVelocity;
        }

        public void ResetGameObjectVelocity(GameObject hGameObject)
        {
            if (!TryGetRigidbody(hGameObject, out var hRigid))
                return;

            hRigid.velocity = Vector3.zero;
        }

        
        public void EnableGravity(GameObject hGameObject)
        {
            if (!TryGetRigidbody(hGameObject, out var hRigid))
                return;

            hRigid.useGravity = true;
        }

        public void DisableGravity(GameObject hGameObject)
        {
            if (!TryGetRigidbody(hGameObject, out var hRigid))
                return;

            hRigid.useGravity = false;
        }

        public void EnableKinematic(GameObject hGameObject)
        {
            if (!TryGetRigidbody(hGameObject, out var hRigid))
                return;

            hRigid.isKinematic = true;
        }

        public void DisableKinematic(GameObject hGameObject)
        {
            if (!TryGetRigidbody(hGameObject, out var hRigid))
                return;

            hRigid.isKinematic = false;
        }

        public void SetVelocity(Vector3 vVelocity)
        {
            m_vVelocity = vVelocity;
        }

        public void ResetVelocity()
        {
            m_vVelocity = Vector3.zero;
        }

        public void SetVelocityX(float fVelocityX)
        {
            m_vVelocity.x = fVelocityX;
        }

        public void SetVelocityY(float fVelocityY)
        {
            m_vVelocity.y = fVelocityY;
        }

        public void SetVelocityZ(float fVelocityZ)
        {
            m_vVelocity.z = fVelocityZ;
        }

        public void SetGameObjectMass(GameObject hGameObject)
        {
            if (!TryGetRigidbody(hGameObject, out var hRigid))
                return;

            hRigid.mass = m_fMass;
        }

        public void SetMass(float fMass)
        {
            if (fMass < 0.01f)
                fMass = 0.01f;

            m_fMass = fMass;
        }

        public void SetGameObjectDrag(GameObject hGameObject)
        {
            if (!TryGetRigidbody(hGameObject, out var hRigid))
                return;

            hRigid.drag = m_fDrag;
        }

        public void SetDrag(float fDrag)
        {
            if (fDrag < 0)
                fDrag = 0;

            m_fDrag = fDrag;
        }

        public void SetGameObjectAngularDrag(GameObject hGameObject)
        {
            if (!TryGetRigidbody(hGameObject, out var hRigid))
                return;

            hRigid.angularDrag = m_fAngularDrag;
        }

        public void SetAngularDrag(float fAngularDrag)
        {
            if (fAngularDrag < 0)
                fAngularDrag = 0;

            m_fAngularDrag = fAngularDrag;
        }

        #endregion

        #region Helper

        protected Rigidbody GetRigidBody(GameObject hGameObject)
        {
            if (hGameObject == null)
                return null;

            return hGameObject.GetComponent<Rigidbody>();
        }

        protected bool TryGetRigidbody(GameObject hGameObject,out Rigidbody hOutRigid)
        {
            hOutRigid = GetRigidBody(hGameObject);
            return hOutRigid != null;
        }

        #endregion
    }
}