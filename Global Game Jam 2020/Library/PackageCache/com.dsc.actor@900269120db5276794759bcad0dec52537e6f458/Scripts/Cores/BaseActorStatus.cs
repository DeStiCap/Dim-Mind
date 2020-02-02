using UnityEngine;

namespace DSC.Actor
{
    public abstract class BaseActorStatus<ActorStatus> : MonoBehaviour where ActorStatus : struct
    {
        public abstract ActorStatus status { get; set; }
    }
}