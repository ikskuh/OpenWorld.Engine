using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine.ShaderSystem
{
	/// <summary>
	/// Defines a shader cache that caches different shaders.
	/// </summary>
	public sealed class ShaderCache
	{
		class CacheEntry : IEquatable<CacheEntry>
		{
			public string ClassName { get; set; }

			public ShaderFragment VertexShader { get; set; }

			public ShaderFragment TesselationControlShader { get; set; }

			public ShaderFragment TesselationEvaluationShader { get; set; }

			public ShaderFragment GeometryShader { get; set; }

			public ShaderFragment FragmentShader { get; set; }

			/// <summary>
			/// Compares the object to another object.
			/// </summary>
			/// <param name="obj"></param>
			/// <returns></returns>
			public override bool Equals(object obj)
			{
				return this.Equals(obj as CacheEntry);
			}

			/// <summary>
			/// Compares the cache entry to another cache entry.
			/// </summary>
			/// <param name="other"></param>
			/// <returns></returns>
			public bool Equals(CacheEntry other)
			{
				if (other == null) return false;
				return
					this.ClassName == other.ClassName &&
					this.VertexShader == other.VertexShader &&
					this.TesselationControlShader == other.TesselationControlShader &&
					this.TesselationEvaluationShader == other.TesselationEvaluationShader &&
					this.GeometryShader == this.GeometryShader &&
					this.FragmentShader == this.FragmentShader;
			}

			public override int GetHashCode()
			{
				int hash = 0;
				if (this.ClassName != null) 
					hash ^= this.ClassName.GetHashCode();
				if (this.VertexShader != null) 
					hash ^= this.VertexShader.GetHashCode();
				if (this.TesselationControlShader != null) 
					hash ^= this.TesselationControlShader.GetHashCode();
				if (this.TesselationEvaluationShader != null) 
					hash ^= this.TesselationEvaluationShader.GetHashCode();
				if (this.GeometryShader != null)
					hash ^= this.GeometryShader.GetHashCode();
				if (this.FragmentShader != null)
					hash ^= this.FragmentShader.GetHashCode();
				return hash;
			}
		}

		readonly Shader container;
		Dictionary<CacheEntry, CompiledShader> cache;

		internal event EventHandler<ShaderCacheMissEventArgs> Miss;

		internal ShaderCache(Shader container)
		{
			this.container = container;
			this.cache = new Dictionary<CacheEntry, CompiledShader>();
		}

		/// <summary>
		/// Clears the shader cache.
		/// </summary>
		public void Clear()
		{
			this.cache.Clear();
		}

		/// <summary>
		/// Selects a shader from the cache. If the shader is not found, the Miss event is invoked.
		/// </summary>
		/// <param name="className">Class name of the shader. Can be null for default class</param>
		/// <param name="fragments">Overwritten shader parts.</param>
		/// <returns></returns>
		public CompiledShader Select(string className, params ShaderFragment[] fragments)
		{
			// Sanatize fragments input 
			ShaderFragment vertexShader = null;
			ShaderFragment tessCtrlShader = null;
			ShaderFragment tessEvalShader = null;
			ShaderFragment geometryShader = null;
			ShaderFragment fragmentShader = null;
			for (int i = 0; i < fragments.Length; i++)
			{
				if(fragments[i] == null) continue;
				switch(fragments[i].Type)
				{
					case ShaderType.VertexShader:
						if (vertexShader != null)
							throw new ArgumentException("You can only input one shader per type.", "fragments");
						vertexShader = fragments[i];
						break;
					case ShaderType.TessControlShader:
						if (tessCtrlShader != null)
							throw new ArgumentException("You can only input one shader per type.", "fragments");
						tessCtrlShader = fragments[i];
						break;
					case ShaderType.TessEvaluationShader:
						if (tessEvalShader != null)
							throw new ArgumentException("You can only input one shader per type.", "fragments");
						tessEvalShader = fragments[i];
						break;
					case ShaderType.GeometryShader:
						if (geometryShader != null)
							throw new ArgumentException("You can only input one shader per type.", "fragments");
						geometryShader = fragments[i];
						break;
					case ShaderType.FragmentShader:
						if (fragmentShader != null)
							throw new ArgumentException("You can only input one shader per type.", "fragments");
						fragmentShader = fragments[i];
						break;
					default:
						throw new ArgumentException("A shader fragment contains an unsupported shader type.", "fragments");
				}
			}

			var entry = new CacheEntry()
			{
				ClassName = className,
				VertexShader = vertexShader,
				TesselationControlShader = tessCtrlShader,
				TesselationEvaluationShader = tessEvalShader,
				GeometryShader = geometryShader,
				FragmentShader = fragmentShader
			};

			if (this.cache.ContainsKey(entry))
				return this.cache[entry];

			ShaderCacheMissEventArgs e = new ShaderCacheMissEventArgs(className, fragments);
			if (this.Miss != null)
				this.Miss(this, e);
			else
				throw new InvalidOperationException("Shader cache needs a Miss event assigned.");

			if (e.ResultShader == null)
				throw new InvalidOperationException("Shader cache miss: No resulting shader provided!");

			this.cache.Add(entry, e.ResultShader);

			return e.ResultShader;
		}
	}

	/// <summary>
	/// Provides data for a shader cache miss.
	/// </summary>
	public sealed class ShaderCacheMissEventArgs : EventArgs
	{
		internal ShaderCacheMissEventArgs(string className, ShaderFragment[] fragmens)
		{
			this.ShaderClass = className;
			this.Fragments = fragmens;
		}

		/// <summary>
		/// Gets the class of the shader missing.
		/// </summary>
		/// <remarks>Can be null, then it's the default class.</remarks>
		public string ShaderClass { get; private set; }

		/// <summary>
		/// Gets an array of shader fragments that will be replaced.
		/// </summary>
		public ShaderFragment[] Fragments { get; private set; }

		/// <summary>
		/// Gets or sets the resulting shader.
		/// </summary>
		public CompiledShader ResultShader { get; set; }
	}
}
