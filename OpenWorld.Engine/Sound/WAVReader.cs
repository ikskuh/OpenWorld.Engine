using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using OpenTK.Audio.OpenAL;

namespace OpenWorld.Engine.Sound
{
	/// <summary>
	/// 
	/// </summary>
    public class WAVReader : AudioReader
    {
		/// <summary>
		/// 
		/// </summary>
		/// <param name="stream"></param>
        public WAVReader(Stream stream) : base(stream)
        {
        }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="stream"></param>
		/// <param name="encoding"></param>
        public WAVReader(Stream stream, Encoding encoding) : base(stream,encoding)
        {
        }

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
        public override AudioData ReadAudioData()
        {
            string chunkDescriptor = Encoding.ASCII.GetString(ReadBytes(4));
            int chunkSize = BitConverter.ToInt32(ReadBytes(4),0);
            string format = Encoding.Default.GetString(ReadBytes(4));

            string subchunk1id = Encoding.Default.GetString(ReadBytes(4));
            int subchunk1Size = BitConverter.ToInt32(ReadBytes(4), 0);
            short audioFormat = BitConverter.ToInt16(ReadBytes(2), 0);
            short numChannels = BitConverter.ToInt16(ReadBytes(2), 0);
            int sampleRate = BitConverter.ToInt32(ReadBytes(4), 0);
            int byteRate = BitConverter.ToInt32(ReadBytes(4), 0);
            short blockAlign = BitConverter.ToInt16(ReadBytes(2), 0);
            short bitsPerSample = BitConverter.ToInt16(ReadBytes(2), 0);

            string subchunk2id = Encoding.Default.GetString(ReadBytes(4));
            int subchunk2Size = BitConverter.ToInt32(ReadBytes(4), 0);
            byte[] data = ReadBytes(subchunk2Size);

            if(chunkDescriptor != "RIFF")
            {
                throw new FormatException("WAVE File has not RIFF format!");
            }
       
            if(chunkSize != BaseStream.Length - 8)
            {
                throw new FormatException("WAVE File chunk size has not the expected size!");
            }

            if(format != "WAVE")
            {
                throw new FormatException("This is not a WAVE file!");
            }

            if(subchunk1id != "fmt ")
            {
                throw new FormatException("WAVE File: \"fmt \" subchunk not found where expected!");
            }

            if(subchunk1Size != 16)
            {
                throw new FormatException("WAVE File: \"fmt \" subchunk has not the size of 16 (PCM)!");
            }

            if(audioFormat != 1)
            {
                throw new FormatException("WAVE File: This is not a PCM file!");
            }

            if(numChannels > 2)
            {
                throw new NotSupportedException("WAVE File: More channels than 2 are currently not supported!");
            }

            if(byteRate != sampleRate * numChannels * (bitsPerSample/8))
            {
                throw new FormatException("WAVE File: Byte Rate has not the expected value!");
            }

            if(blockAlign != numChannels * (bitsPerSample/8))
            {
                throw new FormatException("WAVE File: Block Align has not the expected value!");
            }

            if(bitsPerSample != 8 && bitsPerSample != 16)
            {
                throw new NotSupportedException("WAVE File: Only 8 or 16 bits per sample supported!");
            }

            if(subchunk2id != "data")
            {
                throw new FormatException("WAVE File: \"data\" subchunk not found where expected!");
            }

            ALFormat fmt = ALFormat.Stereo16;
            if(numChannels == 1 && bitsPerSample == 16)
            {
                fmt = ALFormat.Mono16;
            }
            else if(numChannels == 1 && bitsPerSample == 8)
            {
                fmt = ALFormat.Mono8;
            }
            else if(numChannels == 2 && bitsPerSample == 8)
            {
                fmt = ALFormat.Stereo8;
            }
            else if(numChannels == 2 && bitsPerSample == 16)
            {
                fmt = ALFormat.Stereo16;
            }

            AudioData ret = new AudioData(data, fmt, sampleRate);

            return ret;

        }
    }
}
