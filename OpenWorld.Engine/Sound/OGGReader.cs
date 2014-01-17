using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using OpenTK.Audio.OpenAL;

namespace OpenWorld.Engine.Sound
{


    /// <summary>
    /// Reader which reads from an oggvorbis file.
    /// </summary>
    public class OGGReader : AudioReader
    {
        public OGGReader(Stream input) : base(input) { }
        public OGGReader(Stream input, Encoding encoding) : base(input, encoding) { }

        /// <summary>
        /// Reads the audio data in the oggvorbis stream.
        /// TODO: Read from stream instead of temp file.
        /// </summary>
        /// <returns>Raw Audio Buffer</returns>
        public AudioData ReadAudioData()
        {

            ALFormat format = 0;
            int frequency = 0;
            AudioData data;
            using (MemoryStream mStream = new MemoryStream())
            {

                vorbis_info info;
                OggVorbis_File file = new OggVorbis_File();


                int bitstream = 0;
                long bytes;
                byte[] array = new byte[32768];


                string filePath = CreateTempSoundFile();

                NativeMethods.ov_fopen(filePath, ref file);
                info = (vorbis_info)Marshal.PtrToStructure(NativeMethods.ov_info(ref file, -1), typeof(vorbis_info));

                if (info.channels == 1)
                    format = ALFormat.Mono16;
                else
                    format = ALFormat.Stereo16;
                frequency = info.rate;

                do
                {
                    bytes = NativeMethods.ov_read(ref file, array, array.Length, 0, 2, 1, ref bitstream);
                    mStream.Write(array, 0, (int)bytes);

                } while (bytes > 0);



                NativeMethods.ov_clear(ref file);
                File.Delete(filePath);

                data = new AudioData(mStream.ToArray(), format, frequency);

            }
            return data;
        }

        private string CreateTempSoundFile()
        {
            string path = Path.GetTempFileName();

            using (var fs = File.Open(path,FileMode.Create))
            {
                BaseStream.CopyTo(fs);
                
            }

            return path;
        }
       
    }
}
