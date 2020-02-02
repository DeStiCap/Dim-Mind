using UnityEngine;

namespace DSC.Core
{
    public static class InputUtility
    {
        /// <summary>
        /// Calculate new input axis to same old axis behaviour as old input.
        /// </summary>
        /// <param name="fAxis">Current Axis</param>
        /// <param name="fRawAxis">Raw Axis that receive from new input.</param>
        /// <param name="fSensitivity">Value axis incease in 1 second when press input.</param>
        /// <param name="fGravity">Value axis decrease in 1 second to 0 when not press input.</param>
        /// <param name="fDeltaTime">Time deltatime</param>
        public static void CalculateAxis(ref float fAxis, float fRawAxis, float fSensitivity, float fGravity, float fDeltaTime)
        {
            if (fRawAxis != 0)
            {
                if ((fRawAxis > 0 && fAxis < 0) || (fRawAxis < 0 && fAxis > 0))
                    fAxis = 0;

                fAxis += fRawAxis * fSensitivity * fDeltaTime;
                fAxis = Mathf.Clamp(fAxis, -1, 1);
            }
            else if (fAxis != 0)
            {
                float fReduce = fGravity * fDeltaTime;
                if (fAxis < 0)
                    fReduce = -fReduce;

                float fNewAxis = fAxis - fReduce;
                if ((fNewAxis > 0 && fAxis < 0) || (fNewAxis < 0 && fAxis > 0))
                    fNewAxis = 0;

                fAxis = fNewAxis;
            }
        }

        /// <summary>
        /// Convert raw float value from new input to GetInputType.
        /// </summary>
        /// <param name="ePreviousGetType">Previous get type.</param>
        /// <param name="fRawValue">Raw value from new input.</param>
        /// <returns>Convert GetInputType</returns>
        public static GetInputType ConvertRawValueToGetType(GetInputType ePreviousGetType,float fRawValue)
        {
            var eResult = GetInputType.None;

            switch (ePreviousGetType)
            {
                case GetInputType.None:
                    if (fRawValue > 0)
                        eResult = GetInputType.Down;
                    break;

                case GetInputType.Down:
                case GetInputType.Hold:
                    if (fRawValue > 0)
                        eResult = GetInputType.HoldEnd;
                    else
                        eResult = GetInputType.Up;
                    break;

                case GetInputType.HoldEnd:
                    if (fRawValue > 0)
                        eResult = GetInputType.Down;
                    else
                        eResult = GetInputType.None;
                    break;

                case GetInputType.Up:
                    if (fRawValue > 0)
                        eResult = GetInputType.Down;
                    else
                        eResult = GetInputType.None;
                    break;
            }

            return eResult;
        }
    }
}