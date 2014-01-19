using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using OpenTK.Audio.OpenAL;
using OpenTK.Audio;
using System.Runtime.InteropServices;

namespace OpenWorld.Engine.Sound
{
	/// <summary>
	/// Represents a buffer of raw audio data.
	/// </summary>
    [AssetExtension(".ogg",".wav")]
    public class AudioBuffer : IALResource, IAsset
    {
        int id;

        /// <summary>
        /// Instantiates a new Audio Buffer.
        /// </summary>
        public AudioBuffer()
        {
            id = AL.GenBuffer();
        }
        
        /// <summary>
        /// Destroys and Disposes the Buffer
        /// </summary>
        ~AudioBuffer()
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

        public void Load(AssetLoadContext context, Stream stream, string extension)
        {
            AudioReader reader = null;
            if(extension == ".wav")
            {
                reader = new WAVReader(stream);
            }
            if(extension == ".ogg")
            {
                reader = new OGGReader(stream);
            }
            if(reader == null)
            {
                throw new ArgumentException("Incorrect file ending!");
            }

            SetData(reader.ReadAudioData());
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
