using System.Collections.Generic;
using UnityEngine;

namespace DSC.Actor
{
    public abstract class BaseActorController<ActorData,Behaviour> : MonoBehaviour where Behaviour : BaseActorBehaviour<ActorData>
    {
        /// <summary>
        /// Actor Data in this controller.
        /// </summary>
        public abstract ActorData actorData { get; }

        /// <summary>
        /// List of Behaviour in this controller.
        /// </summary>
        protected abstract List<Behaviour> listBehaviour { get; set; }

        /// <summary>
        /// List of Behaviour data in this controller.
        /// </summary>
        public abstract List<IActorBehaviourData> listBehaviourData { get; protected set; }

        /// <summary>
        /// Get behaviour from data.
        /// </summary>
        /// <typeparam name="T">Behaviour</typeparam>
        /// <returns>Return get behaviour</returns>
        public virtual T GetBehaviour<T>() where T : Behaviour
        {
            if (!HasBehaviourIsList())
                return null;

            for(int i = 0; i < listBehaviour.Count; i++)
            {
                if (listBehaviour[i] is T)
                    return listBehaviour[i] as T;
            }

            return null;
        }

        /// <summary>
        /// Try Get behaviour from data.
        /// </summary>
        /// <typeparam name="T">Behaviour</typeparam>
        /// <param name="hOutBehaviour"></param>
        /// <returns>Return true if get success.</returns>
        public virtual bool TryGetBehaviour<T>(out T hOutBehaviour) where T : Behaviour
        {
            hOutBehaviour = GetBehaviour<T>();
            return hOutBehaviour != null;
        }

        /// <summary>
        /// Add behaviour to data.
        /// </summary>
        /// <param name="hBehaviour">Behaviour want to add to data.</param>
        public virtual void AddBehaviour(Behaviour hBehaviour)
        {
            if (hBehaviour == null)
                return;

            if (listBehaviour == null)
                listBehaviour = new List<Behaviour>();

            listBehaviour.Add(hBehaviour);
            CreateBehaviour(hBehaviour);
        }

        /// <summary>
        /// Add list behaviour to data.
        /// </summary>
        /// <param name="lstBehaviour">List of behaviour that want to add to data.</param>
        public virtual void AddBehaviour(List<Behaviour> lstBehaviour)
        {
            if (lstBehaviour == null || lstBehaviour.Count <= 0)
                return;

            for(int i = 0; i < lstBehaviour.Count; i++)
            {
                AddBehaviour(lstBehaviour[i]);
            }
        }

        /// <summary>
        /// Remove behaviour from data. (Remove first one that found.)
        /// </summary>
        /// <typeparam name="T">Behaviour that want to remove.</typeparam>
        public virtual void RemoveBehaviour<T>() where T : Behaviour
        {
            if (!HasBehaviourIsList())
                return;

            for(int i = 0; i < listBehaviour.Count; i++)
            {
                var hBehaviour = listBehaviour[i];
                if(hBehaviour != null && hBehaviour is T)
                {
                    StopBehaviour(hBehaviour);
                    DestroyBehaviour(hBehaviour);
                    listBehaviour.RemoveAt(i);
                    break;
                }
            }
        }

        /// <summary>
        /// Remove behaviour from data. (Remove first one that found.)
        /// </summary>
        /// <param name="nBehaviourTypeID">ID of behaviour type that want to remove.</param>
        public virtual void RemoveBehaviour(int nBehaviourTypeID)
        {
            if (!HasBehaviourIsList())
                return;

            for (int i = 0; i < listBehaviour.Count; i++)
            {
                var hBehaviour = listBehaviour[i];
                if (hBehaviour != null && hBehaviour.behaviourTypeID == nBehaviourTypeID)
                {
                    StopBehaviour(hBehaviour);
                    DestroyBehaviour(hBehaviour);
                    listBehaviour.RemoveAt(i);
                    break;
                }
            }
        }

        /// <summary>
        /// Remove all behaviour from data.
        /// </summary>
        public virtual void RemoveAllBehaviour()
        {
            if (!HasBehaviourIsList())
                return;

            for(int i = 0; i < listBehaviour.Count; i++)
            {
                var hBehaviour = listBehaviour[i];
                StopBehaviour(hBehaviour);
                DestroyBehaviour(hBehaviour);
            }

            listBehaviour.Clear();
        }

        /// <summary>
        /// Remove this behaviour type in data and add new one to data instead.
        /// </summary>
        /// <typeparam name="T">Behaviour type that want to change.(Remove first one that found.)</typeparam>
        /// <param name="hNewBehaviour">>New behaviour for replace old one.</param>
        public virtual void ChangeBehaviour<T>(Behaviour hNewBehaviour) where T : Behaviour
        {
            RemoveBehaviour<T>();
            AddBehaviour(hNewBehaviour);
        }

        /// <summary>
        /// Remove this behaviour type ID in data and add new one to data instead.
        /// </summary>
        /// <param name="hNewBehaviour">New behaviour for replace old one.</param>
        public virtual void ChangeBehaviour(Behaviour hNewBehaviour)
        {
            RemoveBehaviour(hNewBehaviour.behaviourTypeID);
            AddBehaviour(hNewBehaviour);
        }

        /// <summary>
        /// Try to get this behaviour data type in this controller.
        /// </summary>
        /// <typeparam name="Data">Data type</typeparam>
        /// <param name="hData">Get data.</param>
        public virtual bool TryGetBehaviourData<Data>(out Data hData) where Data : struct, IActorBehaviourData
        {
            return listBehaviourData.TryGetData(out hData, out int outIndex);
        }

        /// <summary>
        /// Try to get this behaviour data type in this controller.
        /// </summary>
        /// <typeparam name="Data">Data type</typeparam>
        /// <param name="hOutData">Get data.</param>
        /// <param name="nOutIndex">Get data index.</param>
        public virtual bool TryGetBehaviourData<Data>(out Data hOutData,out int nOutIndex) where Data : struct, IActorBehaviourData
        {
            return listBehaviourData.TryGetData(out hOutData, out nOutIndex);
        }

        /// <summary>
        /// Set behaviour data to data list. It will replace first same data type if has.
        /// But if not, It will add this as new data instead.
        /// </summary>
        /// <typeparam name="Data">Data type</typeparam>
        /// <param name="hData">Data that want to set.</param>
        public virtual void SetBehaviourData<Data>(Data hData) where Data : struct, IActorBehaviourData
        {
            if (listBehaviourData == null)
                listBehaviourData = new List<IActorBehaviourData>();

            bool hasData = false;

            for(int i = 0; i < listBehaviourData.Count; i++)
            {
                var behaviourData = listBehaviourData[i];
                if(behaviourData is Data)
                {
                    hasData = true;
                    listBehaviourData[i] = hData;
                    break;
                }
            }

            if (!hasData)
                listBehaviourData.Add(hData);
        }

        /// <summary>
        /// Set behaviour data to data list at this index.
        /// </summary>
        /// <typeparam name="Data">Data type</typeparam>
        /// <param name="hData">Data that want to set.</param>
        /// <param name="nDataIndex">Index in list to set data.</param>
        public virtual void SetBehaviourData<Data>(Data hData,int nDataIndex) where Data : struct, IActorBehaviourData
        {
            if (listBehaviourData == null || listBehaviourData.Count <= nDataIndex)
                return;

            listBehaviourData[nDataIndex] = hData;
        }

        /// <summary>
        /// Add new behaviour data to data list.
        /// </summary>
        /// <typeparam name="Data">Data type</typeparam>
        /// <param name="hData">New data that want to add.</param>
        public virtual void AddBehaviourData<Data>(Data hData) where Data : struct, IActorBehaviourData
        {
            if (listBehaviourData == null)
                listBehaviourData = new List<IActorBehaviourData>();

            listBehaviourData.Add(hData);
        }

        /// <summary>
        /// Remove this behaviour data type from data list.
        /// </summary>
        /// <typeparam name="Data">Data type</typeparam>
        public virtual void RemoveBehaviourData<Data>() where Data : struct, IActorBehaviourData
        {
            if (listBehaviourData == null || listBehaviourData.Count <= 0)
                return;

            for(int i = 0; i < listBehaviourData.Count; i++)
            {
                var behaviourData = listBehaviourData[i];
                if(behaviourData is Data)
                {
                    listBehaviourData.RemoveAt(i);
                    break;
                }
            }
        }

        /// <summary>
        /// Remove behaviour data in this index from data list.
        /// </summary>
        public virtual void RemoveBehaviourData(int nDataIndex)
        {
            if (listBehaviourData == null || listBehaviourData.Count <= nDataIndex)
                return;

            listBehaviourData.RemoveAt(nDataIndex);
        }

        #region Behaviour - Helper

        protected bool HasBehaviourIsList()
        {
            return (listBehaviour != null && listBehaviour.Count > 0);
        }

        /// <summary>
        /// Check behaviour condition. Return true if pass, return false if not pass.
        /// </summary>
        /// <param name="hBehaviour">Behaviour want to check.</param>
        protected virtual bool PassBehaviourCondition(Behaviour hBehaviour)
        {
            if (hBehaviour == null)
                return false;

            return hBehaviour.PassCondition(actorData, listBehaviourData);
        }

        /// <summary>
        /// Call create function in this behaviour.
        /// </summary>
        /// <param name="hBehaviour">Behaviour want to call create.</param>
        protected virtual void CreateBehaviour(Behaviour hBehaviour)
        {
            hBehaviour?.OnCreateBehaviour(actorData, listBehaviourData);
        }

        /// <summary>
        /// Call create function from all behaviour.
        /// </summary>
        protected virtual void CreateAllBehaviour()
        {
            if (!HasBehaviourIsList())
                return;

            for (int i = 0; i < listBehaviour.Count; i++)
            {
                CreateBehaviour(listBehaviour[i]);
            }
        }

        /// <summary>
        /// Call destroy function in this behaviour.
        /// </summary>
        /// <param name="hBehaviour">Behaviour want to call destroy.</param>
        protected virtual void DestroyBehaviour(Behaviour hBehaviour)
        {
            hBehaviour?.OnDestroyBehaviour(actorData, listBehaviourData);
        }

        /// <summary>
        /// Call destroy function from all behaviour.
        /// </summary>
        protected virtual void DestroyAllBehaviour()
        {
            if (!HasBehaviourIsList())
                return;

            for (int i = 0; i < listBehaviour.Count; i++)
            {
                DestroyBehaviour(listBehaviour[i]);
            }
        }

        /// <summary>
        /// Call start function in this behaviour.
        /// </summary>
        /// <param name="hBehaviour">Behaviour want to call start.</param>
        protected virtual void StartBehaviour(Behaviour hBehaviour)
        {
            if(hBehaviour != null && hBehaviour.PassCondition(actorData,listBehaviourData))
                hBehaviour.OnStartBehaviour(actorData, listBehaviourData);
        }

        /// <summary>
        /// Call start function from all behaviour.
        /// </summary>
        protected virtual void StartAllBehaviour()
        {
            if (!HasBehaviourIsList())
                return;

            for (int i = 0; i < listBehaviour.Count; i++)
            {
                StartBehaviour(listBehaviour[i]);
            }
        }

        /// <summary>
        /// Call stop function in this behaviour.
        /// </summary>
        /// <param name="hBehaviour">Behaviour want to call stop.</param>
        protected virtual void StopBehaviour(Behaviour hBehaviour)
        {
            hBehaviour?.OnStopBehaviour(actorData, listBehaviourData);
        }

        /// <summary>
        /// Call stop function from all behaviour.
        /// </summary>
        protected virtual void StopAllBehaviour()
        {
            if (!HasBehaviourIsList())
                return;

            for (int i = 0; i < listBehaviour.Count; i++)
            {
                StopBehaviour(listBehaviour[i]);
            }
        }

        /// <summary>
        /// Call update function in this behaviour.
        /// </summary>
        /// <param name="behaviour">Behaviour want to call update.</param>
        protected virtual void UpdateBehaviour(Behaviour behaviour)
        {
            behaviour?.OnUpdateBehaviour(actorData, listBehaviourData);
        }

        /// <summary>
        /// Call fix update function in this behaviour.
        /// </summary>
        /// <param name="behaviour">Behaviour want to call fixed update.</param>
        protected virtual void FixedUpdateBehaviour(Behaviour behaviour)
        {
            behaviour?.OnFixedUpdateBehaviour(actorData, listBehaviourData);
        }

        /// <summary>
        /// Call late update function in this behaviour.
        /// </summary>
        /// <param name="behaviour">Behaviour want to call late update.</param>
        protected virtual void LateUpdateBehaviour(Behaviour behaviour)
        {
            behaviour?.OnLateUpdateBehaviour(actorData, listBehaviourData);
        }

        protected virtual void ExecuteBehaviour()
        {
            for (int i = 0; i < listBehaviour.Count; i++)
            {
                var hBehaviour = listBehaviour[i];
                if (hBehaviour == null)
                    continue;

                bool bRunning = false;

                if (hBehaviour.isRunning)
                {
                    if (!PassBehaviourCondition(hBehaviour))
                        StopBehaviour(hBehaviour);
                    else
                        bRunning = true;
                }
                else if (PassBehaviourCondition(hBehaviour))
                {
                    StartBehaviour(hBehaviour);
                    bRunning = true;
                }

                if (bRunning)
                    UpdateBehaviour(hBehaviour);
            }
        }

        protected virtual void FixedExecuteBehaviour()
        {
            for (int i = 0; i < listBehaviour.Count; i++)
            {
                var hBehaviour = listBehaviour[i];
                if (hBehaviour != null && hBehaviour.isRunning)
                    FixedUpdateBehaviour(hBehaviour);
            }
        }

        protected virtual void LateExecuteBehaviour()
        {
            for (int i = 0; i < listBehaviour.Count; i++)
            {
                var hBehaviour = listBehaviour[i];
                if (hBehaviour != null && hBehaviour.isRunning)
                    LateUpdateBehaviour(hBehaviour);
            }
        }

        #endregion
    }
}