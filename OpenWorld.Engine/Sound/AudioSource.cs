using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK.Audio.OpenAL;
using OpenTK;

namespace OpenWorld.Engine.Sound
{
    /// <summary>
    /// Represents an audio source
    /// </summary>
    public class AudioSource : IALResource
    {
        int id;

        public AudioSource()
        {
            id = AL.GenSource();
        }

        public AudioSource(AudioBuffer buffer) : this()
        {
            Sound = buffer;
        }

        public ~AudioSource()
        {
            Dispose();
        }
        
        public AudioBuffer Sound
        {
            get
            {
                int sndID;
                AL.GetSource(id, ALGetSourcei.Buffer, out sndID);
                return AudioBuffer.CreateFromNative(sndID);
            }
            set
            {
                CheckSourceExists();
                AL.Source(id, ALSourcei.Buffer, value.Id);
            }
        }

        public bool Looping
        {
            get 
            {
                bool ret;
                AL.GetSource(id, ALSourceb.Looping, out ret);
                return ret;
            }
            set 
            { 
                CheckSourceExists();
                AL.Source(id, ALSourceb.Looping, value);
            }
        }

        public Vector3 Position
        {
            get 
            {
                Vector3 ret;
                AL.GetSource(id, ALSource3f.Position, out ret);
                return ret;
            }
            set
            {
                CheckSourceExists();
                AL.Source(id, ALSource3f.Position, ref value);
            }
        }

        public Vector3 Direction
        {
            get 
            {
                Vector3 ret;
                AL.GetSource(id, ALSource3f.Direction, out ret);
                return ret;
            }
            set
            {
                CheckSourceExists();
                AL.Source(id, ALSource3f.Direction, ref value);
            }
        }

        public Vector3 Velocity
        {
            get 
            {
                Vector3 ret;
                AL.GetSource(id, ALSource3f.Velocity, out ret);
                return ret;
            }
            set
            {
                CheckSourceExists();
                AL.Source(id, ALSource3f.Velocity, ref value);
            }
        }

        public float ConeInnerAngle
        {
            get 
            {
                float ret;
                AL.GetSource(id, ALSourcef.ConeInnerAngle, out ret);
                return ret;
            }
            set
            {
                CheckSourceExists();
                AL.Source(id, ALSourcef.ConeInnerAngle, value);
            }
        }

        public float ConeOuterAngle
        {
            get 
            {
                float ret;
                AL.GetSource(id, ALSourcef.ConeOuterAngle, out ret);
                return ret;
            }
            set
            {
                CheckSourceExists();
                AL.Source(id, ALSourcef.ConeOuterAngle, value);
            }
        }

        public float Pitch
        {
            get 
            {
                float ret;
                AL.GetSource(id, ALSourcef.Pitch, out ret);
                return ret;
            }
            set
            {
                CheckSourceExists();
                AL.Source(id, ALSourcef.Pitch, value);
            }
        }

        public float Gain
        {
            get 
            {
                float ret;
                AL.GetSource(id, ALSourcef.Gain, out ret);
                return ret;
            }
            set
            {
                CheckSourceExists();
                AL.Source(id, ALSourcef.Gain, value);
            }
        }

     
        public float MinGain
        {
            get
            {
                float ret;
                AL.GetSource(id, ALSourcef.MinGain, out ret);
                return ret;
            }
            set
            {
                CheckSourceExists();
                AL.Source(id, ALSourcef.MinGain, value);
            }
        }

        public float MaxGain
        {
            get
            {
                float ret;
                AL.GetSource(id, ALSourcef.MaxGain, out ret);
                return ret;
            }

            set
            {
                CheckSourceExists();
                AL.Source(id, ALSourcef.MaxGain, value);
            }
        }

        public float ReferenceDistance
        {
            get 
            {
                float ret;
                AL.GetSource(id, ALSourcef.ReferenceDistance, out ret);
                return ret;
            }
            set
            {
                CheckSourceExists();
                AL.Source(id, ALSourcef.ReferenceDistance, value);
            }
        }

        public float RolloffFactor
        {
            get 
            {
                float ret;
                AL.GetSource(id, ALSourcef.RolloffFactor, out ret);
                return ret;
            }
            set
            {
                CheckSourceExists();
                AL.Source(id, ALSourcef.RolloffFactor, value);
            }
        }

        public float ConeOuterGain
        {
            get
            {
                float ret;
                AL.GetSource(id, ALSourcef.ConeOuterGain, out ret);
                return ret;
            }

            set
            {
                CheckSourceExists();
                AL.Source(id, ALSourcef.ConeOuterGain, value);
            }
        }

        public float MaxDistance
        {
            get
            {
                float ret;
                AL.GetSource(id, ALSourcef.MaxDistance, out ret);
                return ret;
            }
            set
            {
                CheckSourceExists();
                AL.Source(id, ALSourcef.MaxDistance, value);
            }
        }

        public float SecOffset
        {
            set
            {
                CheckSourceExists();
                AL.Source(id, ALSourcef.SecOffset, value);
            }
        }
       
        public void GetOffsets(out float secs, out int bytes, out int samples)
        {
            AL.GetSource(id, ALSourcef.SecOffset, out secs);
            AL.GetSource(id, ALGetSourcei.SampleOffset, out samples);
            AL.GetSource(id, ALGetSourcei.ByteOffset, out bytes);
        }

        public ALSourceType SourceType
        {
            get
            {
                int ret;
                AL.GetSource(id, ALGetSourcei.SourceType, out ret);
                return (ALSourceType)ret;
            }
            set
            {
                CheckSourceExists();
                AL.Source(id, ALSourcei.SourceType, (int)value);
            }
        }

        public ALSourceState SourceState
        {
            get
            {
                return AL.GetSourceState(id);
            }
        }

        public void Play()
        {
            AL.SourcePlay(id);
        }

        public void Pause()
        {
            AL.SourcePause(id);
        }

        public void Stop()
        {
            AL.SourceStop(id);
        }

        public void Rewind()
        {
            AL.SourceRewind(id);
        }

        private void CheckSourceExists()
        {
            if (this.id == 0)
            {
                throw new InvalidOperationException("Source is disposed.");
            }
        }

        public void Dispose()
        {
            if (id != 0)
            {
                AL.DeleteSource(id);
                id = 0;
            }
            GC.SuppressFinalize(this);
        }

        public int Id { get { return this.id; } }
    }
}
