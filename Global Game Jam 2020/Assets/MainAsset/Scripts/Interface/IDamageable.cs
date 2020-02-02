using UnityEngine;

namespace GGJ2020
{
    public interface IDamageable
    {
        void DoDamage(int nDamage);
        void DoDamage(int nDamage, GameObject hAttacker);
        void DoDamage(int nDamage, Vector3 vPosition);
        void DoDamage(int nDamage, float fFreezeDuration);
    }
}