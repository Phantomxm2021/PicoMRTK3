using System;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.XR.PXR
{
    public class PXR_EnterpriseTools : MonoBehaviour
    {
        public struct NoDelayedQueueItem
        {
            public Action action;
        }

        private List<NoDelayedQueueItem> _actions = new List<NoDelayedQueueItem>();
        List<NoDelayedQueueItem> _currentActions = new List<NoDelayedQueueItem>();
        private static PXR_EnterpriseTools instance;

        public static PXR_EnterpriseTools Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<PXR_EnterpriseTools>();
                }

                if (instance == null)
                {
                    GameObject obj = new GameObject("PXR_EnterpriseTools");
                    instance = obj.AddComponent<PXR_EnterpriseTools>();
                    DontDestroyOnLoad(obj);
                }

                return instance;
            }
        }
        private void Awake()
        {
            instance = this;
        }

        public void QueueOnMainThread(Action taction)
        {
            lock (instance._actions)
            {
                instance._actions.Add(new NoDelayedQueueItem { action = taction });
            }
        }

        void OnDisable()
        {
            if (instance == this)
            {
                instance = null;
            }
        }

        void Update()
        {
            if (_actions.Count > 0)
            {
                lock (_actions)
                {
                    _currentActions.Clear();
                    _currentActions.AddRange(_actions);
                    _actions.Clear();
                }

                for (int i = 0; i < _currentActions.Count; i++)
                {
                    _currentActions[i].action();
                }
            }
        }
    }
}