using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using OpenTK.Audio.OpenAL;

namespace OpenWorld.Engine.Sound
{
    /// <summary>
    /// Defines a pack of data to return to the user.
    /// </summary>
    public class AudioData
    {
		private byte[] buffer;
        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="data">The raw audio data</param>
        /// <param name="format">The format of the audio data</param>
        /// <param name="frequency">The frequency of the audio data</param>
        public AudioData(byte[] data, ALFormat format,int frequency)
        {
            this.buffer = data;
            Format = format;
            Frequency = frequency;
        }

		/// <summary>
		/// 
		/// </summary>
		public byte[] GetBuffer()
		{
			return this.buffer;
		}
        
		/// <summary>
		/// 
		/// </summary>
		public ALFormat Format { get; private set; }

		/// <summary>
		/// 
		/// </summary>
        public int Frequency { get; private set; }
    }
    
    /// <summary>
    /// Reader reading audio data from a stream.
    /// </summary>
    public abstract class AudioReader : BinaryReader
    {
		/// <summary>
		/// 
		/// </summary>
		/// <param name="input"></param>
        protected AudioReader(Stream input) : base(input) { }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="input"></param>
		/// <param name="encoding"></param>
        protected AudioReader(Stream input, Encoding encoding) : base(input, encoding) { }

        /// <summary>
        /// Reads the audio data in the stream.
        /// </summary>
        /// <returns>Raw Audio Buffer</returns>
        public abstract AudioData ReadAudioData();

        
    }
}
