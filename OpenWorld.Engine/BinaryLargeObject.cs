using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWorld.Engine
{
	/// <summary>
	/// Asset type for unspecified binary data.
	/// </summary>
	[AssetExtension(".dat")]
	public sealed class BinaryLargeObject : IAsset
	{
		byte[] data;

		/// <summary>
		/// Instantiates a new BLOB.
		/// </summary>
		public BinaryLargeObject()
		{
			this.data = new byte[0];
		}

		/// <summary>
		/// Instantiates a new BLOB with data.
		/// </summary>
		/// <param name="source">The data of the BLOB</param>
		public BinaryLargeObject(byte[] source)
		{
			this.data = source;
		}

		/// <summary>
		/// Instantiates a new BLOB with a stream as data source.
		/// </summary>
		/// <param name="stream">The stream that should be copied to the BLOB.</param>
		public BinaryLargeObject(Stream stream)
		{
			((IAsset)this).Load(null, stream, null);
		}

		void IAsset.Load(AssetLoadContext manager, Stream stream, string extensionHint)
		{
			if (stream == null)
				throw new ArgumentNullException("stream");
			
			using (var memory = new MemoryStream())
			{
				stream.CopyTo(memory);
				this.data = memory.ToArray();
			}
		}

		/// <summary>
		/// Gets a stream that contains the BLOB data.
		/// </summary>
		/// <returns></returns>
		public MemoryStream GetStream()
		{
			return new MemoryStream(this.data);
		}

		/// <summary>
		/// Gets a byte from the BLOB.
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public byte this[int index]
		{
			get { return this.data[index]; }
		}

		/// <summary>
		/// The length of the BLOB in bytes.
		/// </summary>
		public int Length
		{
			get { return this.data.Length; }
		}
	}
}
