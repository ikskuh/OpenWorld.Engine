using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine
{
	/// <summary>
	/// Represents a post processing stage.
	/// </summary>
	public sealed class PostProcessingStage
	{
		/// <summary>
		/// Instantiates an empty post processing stage.
		/// </summary>
		public PostProcessingStage()
		{
			this.Enabled = true;
		}

		/// <summary>
		/// Instantiates a new post processing stage with an effect.
		/// </summary>
		/// <param name="effect">The effect for this stage.</param>
		public PostProcessingStage(PostProcessingShader effect)
			: this()
		{
			this.Effect = effect;
		}

		/// <summary>
		/// Instantiates a new post processing stage with an effect.
		/// </summary>
		/// <param name="effect">The effect for this stage.</param>
		/// <param name="enabled">Determines if the post processing stage is enabled.</param>
		public PostProcessingStage(PostProcessingShader effect, bool enabled)
		{
			this.Effect = effect;
			this.Enabled = enabled;
		}

		/// <summary>
		/// Gets or sets the post processing effect.
		/// </summary>
		public PostProcessingShader Effect { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether the post processing stage is used or not.
		/// </summary>
		public bool Enabled { get; set; }
	}
}
