using System.Collections.Generic;

namespace DSC.Actor
{
    public static class Extention_IActorBehaviourData
    {
        /// <summary>
        /// Try get behaviour data from list.
        /// </summary>
        /// <typeparam name="Data">Data type</typeparam>
        /// <param name="lstData">List contain data.</param>
        /// <param name="hOutData">Get data</param>
        /// <param name="nOutIndex">Index of get data in list.</param>
        /// <returns>Return true when get data success.</returns>
        public static bool TryGetData<Data>(this List<IActorBehaviourData> lstData, out Data hOutData, out int nOutIndex) where Data : struct, IActorBehaviourData
        {
            hOutData = default;
            nOutIndex = -1;

            if (lstData != null && lstData.Count > 0)
            {
                for (int i = 0; i < lstData.Count; i++)
                {
                    if (lstData[i] is Data)
                    {
                        hOutData = (Data)lstData[i];
                        nOutIndex = i;
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Try get behaviour data index from list.
        /// </summary>
        /// <typeparam name="Data">Data type</typeparam>
        /// <param name="lstData">List contain data.</param>
        /// <param name="hOutData">Get data</param>
        /// <returns>Return true when get data success.</returns>
        public static bool TryGetData<Data>(this List<IActorBehaviourData> lstData, out Data hOutData) where Data : struct, IActorBehaviourData
        {
            return lstData.TryGetData(out hOutData, out int nOutIndex);
        }

        /// <summary>
        /// Try get behaviour data index from list.
        /// </summary>
        /// <typeparam name="Data">Data type</typeparam>
        /// <param name="lstData">List contain data.</param>
        /// <param name="nOutIndex">Index of get data in list.</param>
        /// <returns>Return true when get data success.</returns>
        public static bool TryGetData<Data>(this List<IActorBehaviourData> lstData, out int nOutIndex) where Data : struct, IActorBehaviourData
        {
            return lstData.TryGetData(out Data hOutData, out nOutIndex);
        }

        /// <summary>
        /// Remove this behaviour data type from list.
        /// </summary>
        /// <typeparam name="Data">Data type.</typeparam>
        /// <param name="listData">Actor behaviour data list.</param>
        public static void Remove<Data>(this List<IActorBehaviourData> listData) where Data : struct, IActorBehaviourData
        {
            if (listData.TryGetData(out Data outData, out int outIndex))
                listData.RemoveAt(outIndex);
        }
    }
}