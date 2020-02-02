using System.Collections.Generic;

namespace DSC.Dialogue
{
    public static class Extention_IDialogueEventData
    {
        /// <summary>
        /// Try get dialogue data from list.
        /// </summary>
        /// <typeparam name="Data">Data type</typeparam>
        /// <param name="listData">List contain data.</param>
        /// <param name="data">Get data</param>
        public static bool TryGetData<Data>(this List<IDialogueEventData> listData, out Data data) where Data : struct, IDialogueEventData
        {
            data = default;

            if (listData != null && listData.Count > 0)
            {
                for (int i = 0; i < listData.Count; i++)
                {
                    if (listData[i] is Data)
                    {
                        data = (Data)listData[i];
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Try get dialogue data from list.
        /// </summary>
        /// <typeparam name="Data">Data type</typeparam>
        /// <param name="listData">List contain data.</param>
        /// <param name="indexData">Get index data</param>
        public static bool TryGetData<Data>(this List<IDialogueEventData> listData, out int indexData) where Data : struct, IDialogueEventData
        {
            indexData = -1;

            if (listData != null && listData.Count > 0)
            {
                for (int i = 0; i < listData.Count; i++)
                {
                    if (listData[i] is Data)
                    {
                        indexData = i;
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Try get dialogue data from list.
        /// </summary>
        /// <typeparam name="Data">Data type</typeparam>
        /// <param name="listData">List contain data.</param>
        /// <param name="data">Get data</param>
        /// <param name="indexData">Index get data in list.</param>
        public static bool TryGetData<Data>(this List<IDialogueEventData> listData, out Data data, out int indexData) where Data : struct, IDialogueEventData
        {
            data = default;
            indexData = -1;

            if (listData != null && listData.Count > 0)
            {
                for (int i = 0; i < listData.Count; i++)
                {
                    if (listData[i] is Data)
                    {
                        data = (Data)listData[i];
                        indexData = i;
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Remove this dialogue data type from list.
        /// </summary>
        /// <typeparam name="Data">Data type.</typeparam>
        /// <param name="listData">Dialogue data list.</param>
        public static void Remove<Data>(this List<IDialogueEventData> listData) where Data : struct, IDialogueEventData
        {
            if (listData.TryGetData(out Data outData, out int outIndex))
                listData.RemoveAt(outIndex);
        }
    }
}