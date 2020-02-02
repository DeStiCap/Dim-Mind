using UnityEngine;

namespace DSC.Core
{
    public abstract class BaseComponentGroupController : MonoBehaviour
    {
        public abstract void SetEnable(int nIndex, bool bEnable);
        public abstract void SetAllEnable(bool bEnable);
    }
}