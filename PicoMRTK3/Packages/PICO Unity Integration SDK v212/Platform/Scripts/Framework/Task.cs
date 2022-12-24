#if PICO_INSTALL

/*******************************************************************************
Copyright © 2015-2022 Pico Technology Co., Ltd.All rights reserved.

NOTICE：All information contained herein is, and remains the property of
Pico Technology Co., Ltd. The intellectual and technical concepts
contained herein are proprietary to Pico Technology Co., Ltd. and may be
covered by patents, patents in process, and are protected by trade secret or
copyright law. Dissemination of this information or reproduction of this
material is strictly forbidden unless prior written permission is obtained from
Pico Technology Co., Ltd.
*******************************************************************************/

using UnityEngine;


namespace Pico.Platform
{
    public class Task
    {
        public readonly ulong TaskId;
        
        public Task(ulong taskId)
        {
            this.TaskId = taskId;
        }

        public Task OnComplete(Message.Handler handler)
        {
            if (handler == null)
            {
                throw new UnityException("call Task.Oncomplete with null handler.");
            }
            Looper.RegisterTaskHandler(TaskId, handler);
            return this;
        }
    }

    public sealed class Task<T> : Task
    {
        public Task(ulong taskId) : base(taskId)
        {
        }

        public Task<T> OnComplete(Message<T>.Handler handler)
        {
            if (handler == null)
            {
                throw new UnityException("call Task.Oncomplete with null handler.");
            }
            Looper.RegisterTaskHandler(TaskId, handler);
            return this;
        }
    }
}

#endif