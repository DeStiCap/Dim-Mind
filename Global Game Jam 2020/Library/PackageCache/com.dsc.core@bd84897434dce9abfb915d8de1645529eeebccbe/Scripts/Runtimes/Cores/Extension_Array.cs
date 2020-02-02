using System;
using UnityEngine;

namespace DSC.Core
{
    public static class Extension_Array 
    {
        /// <summary>
        /// Check array not null and length more than zero.
        /// </summary>
        /// <typeparam name="Data">Array Data type</typeparam>
        /// <param name="array">Array</param>
        /// <returns>Return true if not null and length more than zero.</returns>
        public static bool HasData<Data>(this Data[] array)
        {
            return (array != null && array.Length > 0);
        }

        /// <summary>
        /// Try get data in array.
        /// </summary>
        /// <typeparam name="Data">Data type.</typeparam>
        /// <param name="array">Data array</param>
        /// <param name="index">Index</param>
        /// <param name="data">Get data</param>
        /// <returns>Return true success get data.</returns>
        public static bool TryGetData<Data>(this Data[] array,int index, out Data data) where Data : class
        {
            if (index < 0 || array == null || array.Length <= index)
            {
                data = default;
                return false;
            }

            data = (Data)array.GetValue(index);
            return data != null;
        }
    }
}