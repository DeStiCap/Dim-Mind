using UnityEngine;

namespace DSC.Event
{
    public abstract class BaseEventCondition<Data> : ScriptableObject where Data : struct,IEventConditionData
    {
        public abstract bool PassCondition(Data hData);
    }
}