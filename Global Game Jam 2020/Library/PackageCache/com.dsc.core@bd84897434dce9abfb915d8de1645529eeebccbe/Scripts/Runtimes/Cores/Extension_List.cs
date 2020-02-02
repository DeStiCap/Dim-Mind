using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DSC.Core
{
    public static class Extension_List
    {
        /// <summary>
        /// Check list not null and count more than zero.
        /// </summary>
        /// <typeparam name="Data">List Data type</typeparam>
        /// <param name="list">List</param>
        /// <returns>Return true if not null and count more than zero.</returns>
        public static bool HasData<Data>(this List<Data> list)
        {
            return (list != null && list.Count > 0);
        }

        /// <summary>
        /// Try get data in list.
        /// </summary>
        /// <typeparam name="Data">Data type.</typeparam>
        /// <param name="list">Data List</param>
        /// <param name="index">Index</param>
        /// <param name="data">Get data</param>
        /// <returns>Return true success get data.</returns>
        public static bool TryGetData<Data>(this List<Data> list,int index, out Data data) where Data : class
        {
            if(index < 0 || list == null || list.Count <= index)
            {
                data = default;
                return false;
            }

            data = list[index];
            return data != null;
        }
    }
}