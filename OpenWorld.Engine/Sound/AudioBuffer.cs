using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK.Audio.OpenAL;
using OpenTK.Audio;
using System.Runtime.InteropServices;

namespace OpenWorld.Engine.Sound
{
    /// <summary>
    /// Represents a buffer of raw audio data.
    /// </summary>
    public class AudioBuffer : IALResource
    {
        int id;

        /// <summary>
        /// Instantiates a new Audio Buffer.
        /// </summary>
        public AudioBuffer()
        {
            id = AL.GenBuffer();
        }
        
        public ~AudioBuffer()
        {
            Dispose();
        }

        public AudioBuffer(AudioData data) : this()
        {
            SetData(data);
        }

        private AudioBuffer(int sndID)
        {
            id = sndID;
        }

        /// <summary>
        /// Sets the buffers data.
        /// </summary>
        /// <param name="data">The data to set</param>
        public void SetData(AudioData data)
        {
            if (this.id == 0) throw new InvalidOperationException("Buffer was disposed");
            if (data == null) throw new ArgumentNullException("data is null");

            IntPtr buffer = Marshal.AllocHGlobal(data.Buffer.Length);
            Marshal.Copy(data.Buffer,0,buffer,data.Buffer.Length);

            AL.BufferData(id, data.Format, buffer, data.Buffer.Length, data.Frequency);
            Marshal.FreeHGlobal(buffer);
        }

        public void Dispose()
        {
            if(this.id != 0)
            {
                AL.DeleteBuffer(id);
                this.id = 0; 
            }

            GC.SuppressFinalize(this);
        }

        public static AudioBuffer CreateFromNative(int sndID)
        {
            return new AudioBuffer(sndID);
        }
       
        public int Id { get { return this.id; } }
    }
}
