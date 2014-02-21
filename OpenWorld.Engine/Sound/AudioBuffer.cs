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
	public sealed class AudioBuffer : Asset, IDisposable, IALResource
    {
        int id;

        /// <summary>
        /// Instantiates a new Audio Buffer.
        /// </summary>
        public AudioBuffer()
		{
			Game.Current.InvokeOpenAL(() =>
				{
					id = AL.GenBuffer();
				});
        }
        
        /// <summary>
        /// Destroys and Disposes the Buffer
        /// </summary>
        ~AudioBuffer()
        {
            Dispose();
        }

		/// <summary>
		/// Unloads the sound.
		/// </summary>
		protected override void OnUnload()
		{
			this.Dispose();
		}

		/// <summary>
		/// Creates a new audio buffer with given audio data.
		/// </summary>
		/// <param name="data"></param>
        public AudioBuffer(AudioData data)
			: this()
        {
            SetData(data);
        }

        private AudioBuffer(int sndID)
        {
            id = sndID;
        }

		/// <summary>
		/// Loads the audio buffer.
		/// </summary>
        protected override void Load(AssetLoadContext context, Stream stream, string extensionHint)
        {
            AudioReader reader = null;
			if (extensionHint == ".wav")
            {
                reader = new WAVReader(stream);
            }
			if (extensionHint == ".ogg")
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
            if (data == null) throw new ArgumentNullException("data");

			Game.Current.InvokeOpenAL(() =>
				{
					var buffer = data.GetBuffer();
					AL.BufferData(id, data.Format, buffer, buffer.Length, data.Frequency);
				});
        }

		/// <summary>
		/// Disposes the audio buffer.
		/// </summary>
        public void Dispose()
        {
            if(this.id != 0)
			{
				Game.Current.InvokeOpenAL(() =>
				   {
					   AL.DeleteBuffer(id);
				   });
                this.id = 0; 
            }

            GC.SuppressFinalize(this);
        }

		/// <summary>
		/// Creates an existing audio buffer from a native OpenAL buffer id.
		/// </summary>
		/// <param name="soundId"></param>
		/// <returns></returns>
        public static AudioBuffer CreateFromNative(int soundId)
        {
            return new AudioBuffer(soundId);
        }

		/// <summary>
		/// 
		/// </summary>
        public int Id { get { return this.id; } }
    }
}
