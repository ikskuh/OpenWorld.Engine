using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK.Audio.OpenAL;
using OpenTK;

namespace OpenWorld.Engine.Sound
{
    /// <summary>
    /// Represents the listener in the scene (Singleton)
    /// </summary>
    public class AudioListener : Singleton<AudioListener>
    {
        /// <summary>
        /// Current Position of the listener
        /// </summary>
        public Vector3 Position
        {
            get
            {
                Vector3 ret;
                AL.GetListener(ALListener3f.Position, out ret);
                return ret;
            }
            set
            {
                AL.Listener(ALListener3f.Position, ref value);
            }
        }

        public Vector3 Velocity
        {
            get
            {
                Vector3 ret;
                AL.GetListener(ALListener3f.Velocity, out ret);
                return ret;
            }
            set
            {
                AL.Listener(ALListener3f.Velocity, ref value);
            }
        }

        public Vector3 LookAt
        {
            get
            {
                Vector3 temp;
                Vector3 at;
                AL.GetListener(ALListenerfv.Orientation, out at, out temp);
                return at;
                
            }
            set
            {
                Vector3 temp;
                Vector3 up;
                AL.GetListener(ALListenerfv.Orientation,out temp, out up);
                AL.Listener(ALListenerfv.Orientation, ref value, ref up);
            }
        }

        public Vector3 Up
        {
            get
            {
                Vector3 temp;
                Vector3 up;
                AL.GetListener(ALListenerfv.Orientation, out temp, out up);
                return up;
            }
            set
            {
                Vector3 temp;
                Vector3 at;
                AL.GetListener(ALListenerfv.Orientation, out at, out temp);
                AL.Listener(ALListenerfv.Orientation, ref at, ref value);
            }
        }

        public float Gain
        {
            get
            {
                float ret;
                AL.GetListener(ALListenerf.Gain, out ret);
                return ret;
            }
            set
            {
                AL.Listener(ALListenerf.Gain, value);
            }
        }
    }
}
