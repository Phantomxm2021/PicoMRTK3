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

namespace Pico.Platform.Models
{
    using System;
    using System.Runtime.InteropServices;

    public sealed class Packet : IDisposable
    {
        private readonly ulong size;
        private readonly IntPtr handler;

        public Packet(IntPtr handler)
        {
            this.handler = handler;
            this.size = (ulong) CLIB.ppf_Packet_GetSize(handler);
        }

        public ulong GetBytes(byte[] dest)
        {
            if ((ulong) dest.LongLength >= size)
            {
                Marshal.Copy(CLIB.ppf_Packet_GetBytes(handler), dest, 0, (int) size);
                return size;
            }
            else
            {
                throw new ArgumentException($"Dest array can't  hold {size} bytes");
            }
        }

        public string GetBytes()
        {
            if (size > 0)
            {
                byte[] bytes = new byte[size];
                Marshal.Copy(CLIB.ppf_Packet_GetBytes(handler), bytes, 0, (int) size);
                return System.Text.Encoding.UTF8.GetString(bytes);
            }
            else
            {
                return string.Empty;
            }
        }

        public string SenderId
        {
            get { return CLIB.ppf_Packet_GetSenderID(handler); }
        }

        public ulong Size
        {
            get { return size; }
        }


        #region IDisposable

        ~Packet()
        {
            Dispose();
        }


        public void Dispose()
        {
            CLIB.ppf_Packet_Free(handler);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
#endif