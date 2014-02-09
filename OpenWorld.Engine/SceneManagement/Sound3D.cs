using OpenWorld.Engine.Sound;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine.SceneManagement
{
	/// <summary>
	/// Defines a 3D sound source.
	/// </summary>
	public sealed class Sound3D : Component
	{
		AudioSource source;

		/// <summary>
		/// Starts the sound
		/// </summary>
		/// <param name="time"></param>
		protected override void OnStart(GameTime time)
		{
			base.OnStart(time);
			this.source = new AudioSource(this.Sound);
			this.source.Position = this.Node.Transform.WorldPosition;
			this.source.Looping = this.IsLooped;
			if (this.AutoPlay)
				this.source.Play();
		}

		/// <summary>
		/// Updates the sounds position
		/// </summary>
		/// <param name="time"></param>
		protected override void OnUpdate(GameTime time)
		{
			base.OnUpdate(time);
			this.source.Position = this.Node.Transform.WorldPosition;
		}

		/// <summary>
		/// Stops the sound
		/// </summary>
		/// <param name="time"></param>
		protected override void OnStop(GameTime time)
		{
			base.OnStop(time);

			this.source.Dispose();
			this.source = null;
		}

		/// <summary>
		/// Gets or sets the audio buffer that should be played by this 3d sound source.
		/// </summary>
		public AudioBuffer Sound { get; set; }

		/// <summary>
		/// Gets or sets a value that indicates wheather the sound starts right after start or not.
		/// </summary>
		public bool AutoPlay { get; set; }

		/// <summary>
		/// Gets or sets a value that indicates wheather the sound is looped or not.
		/// </summary>
		public bool IsLooped { get; set; }
	}
}
