using System.Collections.Generic;
using UnityEngine;
using DSC.Core;

namespace DSC.Actor
{
    public abstract class BaseActorInput<ActorData,InputEventType> : MonoBehaviour where InputEventType : struct, System.Enum, System.IConvertible
    {
        protected abstract ActorData actorData { get; }

        protected abstract Dictionary<InputEventType,GetInputType> previousGetType { get; set; }

        protected abstract void RunEventInput(InputEventType eEventType, GetInputType eGetType);
        protected abstract void SetHoldingInputData(InputEventType eEventType, GetInputType eGetType);

        /// <summary>
        /// On press event process.
        /// </summary>
        /// <param name="eEventType">Input event type.</param>
        /// <param name="fRawValue">Raw value of input.</param>
        /// <returns>Get input type of this input event.</returns>
        public virtual GetInputType OnPressInput(InputEventType eEventType,float fRawValue)
        {
            if (previousGetType == null)
                previousGetType = new Dictionary<InputEventType, GetInputType>();

            if (!previousGetType.ContainsKey(eEventType))
                previousGetType.Add(eEventType, GetInputType.None);

            var eGetType = previousGetType[eEventType];
            eGetType = InputUtility.ConvertRawValueToGetType(eGetType, fRawValue);

            RunEventInput(eEventType, eGetType);

            SetHoldingInputData(eEventType, eGetType);

            previousGetType[eEventType] = eGetType;

            return eGetType;
        }

        
    }
}