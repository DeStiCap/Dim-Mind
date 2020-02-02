using UnityEngine;

namespace GGJ2020
{
    [System.Serializable]
    public struct ActorStatus
    {
        [Min(0)]
        public int m_nHP;
        [Min(0)]
        public int m_nAttack;
        public float m_fMoveSpeed;
        public float m_fJumpForce;
        public float m_fDoubleJumpForce;
    }
}