using System.Collections.Generic;
using DSC.Actor;

namespace GGJ2020
{
    public static class Extension_DSC_ActorBehaviourCondition
    {
        public static bool PassAllCondition(this DSC_ActorBehaviourCondition[] arrCondition, ActorData hActorData, List<IActorBehaviourData> lstBehaviourData)
        {
            if (arrCondition == null || arrCondition.Length <= 0)
                return true;

            for(int i = 0; i < arrCondition.Length; i++)
            {
                var hCondition = arrCondition[i];
                if (hCondition != null && !hCondition.PassCondition(hActorData, lstBehaviourData))
                    return false;
            }

            return true;
        }
    }
}