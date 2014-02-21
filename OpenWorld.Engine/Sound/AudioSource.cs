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
	public sealed class AudioSource : IALResource
	{
		int id;

		/// <summary>
		/// Creates an Audio Source
		/// </summary>
		public AudioSource()
		{
			Game.Current.InvokeOpenAL(() =>
				{
					id = AL.GenSource();
				});
		}

		/// <summary>
		/// Creates an Audio Source from an AudioBuffer
		/// </summary>
		/// <param name="buffer">Buffer which holds the data</param>
		public AudioSource(AudioBuffer buffer)
			: this()
		{
			Sound = buffer;
		}

		/// <summary>
		/// 
		/// </summary>
		~AudioSource()
		{
			Dispose();
		}

		/// <summary>
		/// 
		/// </summary>
		public AudioBuffer Sound
		{
			get
			{
				int sndID = default(int); Game.Current.InvokeOpenAL(() =>
					 {
						 AL.GetSource(id, ALGetSourcei.Buffer, out sndID);
					 });
				return AudioBuffer.CreateFromNative(sndID);
			}
			set
			{
				CheckSourceExists();
				Game.Current.InvokeOpenAL(() =>
					 {
						 AL.Source(id, ALSourcei.Buffer, value.Id);
					 });
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public bool Looping
		{
			get
			{
				bool ret = default(bool);
				Game.Current.InvokeOpenAL(() =>
					 {
						 AL.GetSource(id, ALSourceb.Looping, out ret);
					 });
				return ret;
			}
			set
			{
				CheckSourceExists(); Game.Current.InvokeOpenAL(() =>
					 {
						 AL.Source(id, ALSourceb.Looping, value);
					 });
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public Vector3 Position
		{
			get
			{
				Vector3 ret = default(Vector3);
				Game.Current.InvokeOpenAL(() =>
					{
						AL.GetSource(id, ALSource3f.Position, out ret);
					});
				return ret;
			}
			set
			{
				CheckSourceExists();
				Game.Current.InvokeOpenAL(() =>
					 {
						 AL.Source(id, ALSource3f.Position, ref value);
					 });
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public Vector3 Direction
		{
			get
			{
				Vector3 ret = default(Vector3);
				Game.Current.InvokeOpenAL(() =>
					 {
						 AL.GetSource(id, ALSource3f.Direction, out ret);
					 });
				return ret;
			}
			set
			{
				CheckSourceExists();
				Game.Current.InvokeOpenAL(() =>
					 {
						 AL.Source(id, ALSource3f.Direction, ref value);
					 });
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public Vector3 Velocity
		{
			get
			{
				Vector3 ret = default(Vector3);
				Game.Current.InvokeOpenAL(() =>
					{
						AL.GetSource(id, ALSource3f.Velocity, out ret);
					});
				return ret;
			}
			set
			{
				CheckSourceExists();
				Game.Current.InvokeOpenAL(() =>
					{
						AL.Source(id, ALSource3f.Velocity, ref value);
					});
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public float ConeInnerAngle
		{
			get
			{
				float ret = default(float);
				Game.Current.InvokeOpenAL(() =>
					{
						AL.GetSource(id, ALSourcef.ConeInnerAngle, out ret);
					});
				return ret;
			}
			set
			{
				CheckSourceExists();
				Game.Current.InvokeOpenAL(() =>
					{
						AL.Source(id, ALSourcef.ConeInnerAngle, value);
					});
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public float ConeOuterAngle
		{
			get
			{
				float ret = default(float);
				Game.Current.InvokeOpenAL(() =>
					{
						AL.GetSource(id, ALSourcef.ConeOuterAngle, out ret);
					});
				return ret;
			}
			set
			{
				CheckSourceExists();
				Game.Current.InvokeOpenAL(() =>
					{
						AL.Source(id, ALSourcef.ConeOuterAngle, value);
					});
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public float Pitch
		{
			get
			{
				float ret = default(float);
				Game.Current.InvokeOpenAL(() =>
					{
						AL.GetSource(id, ALSourcef.Pitch, out ret);
					});
				return ret;
			}
			set
			{
				CheckSourceExists();
				Game.Current.InvokeOpenAL(() =>
					{
						AL.Source(id, ALSourcef.Pitch, value);
					});
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public float Gain
		{
			get
			{
				float ret = default(float);
				Game.Current.InvokeOpenAL(() =>
					{
						AL.GetSource(id, ALSourcef.Gain, out ret);
					});
				return ret;
			}
			set
			{
				CheckSourceExists();
				Game.Current.InvokeOpenAL(() =>
					{
						AL.Source(id, ALSourcef.Gain, value);
					});
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public float MinGain
		{
			get
			{
				float ret = default(float);
				Game.Current.InvokeOpenAL(() =>
					{
						AL.GetSource(id, ALSourcef.MinGain, out ret);
					});
				return ret;
			}
			set
			{
				CheckSourceExists();
				Game.Current.InvokeOpenAL(() =>
					{
						AL.Source(id, ALSourcef.MinGain, value);
					});
			}
		}
		
		/// <summary>
		/// 
		/// </summary>
		public float MaxGain
		{
			get
			{
				float ret = default(float);
				Game.Current.InvokeOpenAL(() =>
					{
						AL.GetSource(id, ALSourcef.MaxGain, out ret);
					});
				return ret;
			}

			set
			{
				CheckSourceExists();
				Game.Current.InvokeOpenAL(() =>
					{
						AL.Source(id, ALSourcef.MaxGain, value);
					});
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public float ReferenceDistance
		{
			get
			{
				float ret = default(float);
				Game.Current.InvokeOpenAL(() =>
					{
						AL.GetSource(id, ALSourcef.ReferenceDistance, out ret);
					});
				return ret;
			}
			set
			{
				CheckSourceExists();
				Game.Current.InvokeOpenAL(() =>
					{
						AL.Source(id, ALSourcef.ReferenceDistance, value);
					});
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public float RolloffFactor
		{
			get
			{
				float ret = default(float);
				Game.Current.InvokeOpenAL(() =>
					{
						AL.GetSource(id, ALSourcef.RolloffFactor, out ret);
					});
				return ret;
			}
			set
			{
				CheckSourceExists();
				Game.Current.InvokeOpenAL(() =>
					{
						AL.Source(id, ALSourcef.RolloffFactor, value);
					});
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public float ConeOuterGain
		{
			get
			{
				float ret = default(float);
				Game.Current.InvokeOpenAL(() =>
					{
						AL.GetSource(id, ALSourcef.ConeOuterGain, out ret);
					});
				return ret;
			}

			set
			{
				CheckSourceExists();
				Game.Current.InvokeOpenAL(() =>
					{
						AL.Source(id, ALSourcef.ConeOuterGain, value);
					});
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public float MaxDistance
		{
			get
			{
				float ret = default(float);
				Game.Current.InvokeOpenAL(() =>
					{
						AL.GetSource(id, ALSourcef.MaxDistance, out ret);
					});
				return ret;
			}
			set
			{
				CheckSourceExists();
				Game.Current.InvokeOpenAL(() =>
					{
						AL.Source(id, ALSourcef.MaxDistance, value);
					});
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public float SecOffset
		{
			set
			{
				CheckSourceExists();
				Game.Current.InvokeOpenAL(() =>
					{
						AL.Source(id, ALSourcef.SecOffset, value);
					});
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="secs"></param>
		/// <param name="bytes"></param>
		/// <param name="samples"></param>
		public void GetOffsets(out float secs, out int bytes, out int samples)
		{
			float s = default(float);
			int b = default(int), sm = default(int);
			Game.Current.InvokeOpenAL(() =>
				{
					AL.GetSource(id, ALSourcef.SecOffset, out s);
					AL.GetSource(id, ALGetSourcei.SampleOffset, out sm);
					AL.GetSource(id, ALGetSourcei.ByteOffset, out b);
				});
			secs = s;
			bytes = b;
			samples = sm;
		}

		/// <summary>
		/// 
		/// </summary>
		public ALSourceType SourceType
		{
			get
			{
				int ret = default(int);
				Game.Current.InvokeOpenAL(() =>
					{
						AL.GetSource(id, ALGetSourcei.SourceType, out ret);
					});
				return (ALSourceType)ret;
			}
			set
			{
				CheckSourceExists();
				Game.Current.InvokeOpenAL(() =>
					{
						AL.Source(id, ALSourcei.SourceType, (int)value);
					});
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public ALSourceState SourceState
		{
			get
			{
				ALSourceState state = default(ALSourceState);
				Game.Current.InvokeOpenAL(() =>
					{
						state = AL.GetSourceState(id);
					});
				return state;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public void Play()
		{
			CheckSourceExists();
			Game.Current.InvokeOpenAL(() =>
				{
					AL.SourcePlay(id);
				});
		}

		/// <summary>
		/// 
		/// </summary>
		public void Pause()
		{
			CheckSourceExists();
			Game.Current.InvokeOpenAL(() =>
				{
					AL.SourcePause(id);
				});
		}

		/// <summary>
		/// 
		/// </summary>
		public void Stop()
		{
			CheckSourceExists();
			Game.Current.InvokeOpenAL(() =>
				{
					AL.SourceStop(id);
				});
		}

		/// <summary>
		/// 
		/// </summary>
		public void Rewind()
		{
			CheckSourceExists();
			Game.Current.InvokeOpenAL(() =>
				{
					AL.SourceRewind(id);
				});
		}

		/// <summary>
		/// 
		/// </summary>
		private void CheckSourceExists()
		{
			if (this.id == 0)
			{
				throw new InvalidOperationException("Source is disposed.");
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public void Dispose()
		{
			if (id != 0)
			{
				Game.Current.InvokeOpenAL(() =>
					{
						AL.DeleteSource(id);
					});
				id = 0;
			}
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// 
		/// </summary>
		public int Id { get { return this.id; } }
	}
}
