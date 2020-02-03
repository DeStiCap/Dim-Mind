using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DSC.Core;
using DSC.Event;
using DSC.Actor;

namespace GGJ2020
{
    public struct InputData
    {
        public bool m_bPressInput;
        public float m_fHorizontal;
        public float m_fVertical;
        public InputEventType m_eHoldingInput;
        public EventCallback<(InputEventType, GetInputType), ActorData, List<IActorBehaviourData>> m_hInputCallback;
    }
}