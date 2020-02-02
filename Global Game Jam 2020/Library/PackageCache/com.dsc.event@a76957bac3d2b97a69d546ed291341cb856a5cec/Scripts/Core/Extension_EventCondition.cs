using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DSC.Event
{
    public static class Extension_EventCondition
    {
        public static bool PassAllCondition(this EventCondition[] arrCondition, EventConditionData hData)
        {
            if (arrCondition == null || arrCondition.Length <= 0)
                return true;

            for(int i = 0; i < arrCondition.Length; i++)
            {
                var hCondition = arrCondition[i];
                if (hCondition == null)
                    continue;

                if (!hCondition.PassCondition(hData))
                    return false;
            }

            return true;
        }
    }
}