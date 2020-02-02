using System;
using UnityEngine;
using UnityEngine.Events;

namespace DSC.Core
{
    [Serializable]
    public class EventGameObject : UnityEvent<GameObject> { }

    [Serializable]
    public class EventString : UnityEvent<string> { }

    [SerializeField]
    public class EventInt : UnityEvent<int> { }

    [SerializeField]
    public class EventFloat : UnityEvent<float> { }

    [SerializeField]
    public class EventBool : UnityEvent<bool> { }

    [Serializable]
    public class EventUnityEvent : UnityEvent<UnityEvent> { }
}