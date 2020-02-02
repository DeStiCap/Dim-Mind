using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DSC.Core;
using DSC.Event;
using DSC.Actor;

namespace DSC.Template.Actor.SideScrolling2D
{
    public struct InputData
    {
        public float m_fHorizontal;
        public float m_fVertical;
        public InputEventType m_eHoldingInput;
        public EventCallback<(InputEventType, GetInputType), ActorData, List<IActorBehaviourData>> m_hInputCallback;
    }
}