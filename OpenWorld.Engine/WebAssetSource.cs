using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace OpenWorld.Engine
{
	/// <summary>
	/// Defines an asset source that loads from a uri.
	/// </summary>
	public sealed class WebAssetSource : AssetSource
	{
		string uri;
		WebClient client;

		/// <summary>
		/// Creates a new web asset source.
		/// </summary>
		public WebAssetSource(string uri)
		{
			this.client = new WebClient();
			this.client.Proxy = null;
			this.uri = uri;
			if (!this.uri.EndsWith("/"))
				this.uri += "/";
		}

		/// <summary>
		/// Opens a stream for an asset.
		/// </summary>
		/// <param name="assetName">The path to the asset with extension.</param>
		/// <returns>Stream that contains the asset.</returns>
		protected internal override System.IO.Stream Open(string assetName)
		{
			return this.client.OpenRead(GetUri(assetName));
		}

		/// <summary>
		/// Checks if an asset exists.
		/// </summary>
		/// <param name="assetName">The path to the asset with extension.</param>
		/// <returns>True if the asset exists.</returns>
		protected internal override bool Exists(string assetName)
		{
			try
			{
				//Creating the HttpWebRequest
				HttpWebRequest request = WebRequest.Create(GetUri(assetName)) as HttpWebRequest;
				//Setting the Request method HEAD, you can also use GET too.
				request.Method = "HEAD";
				//Getting the Web Response.
				HttpWebResponse response = request.GetResponse() as HttpWebResponse;
				//Returns TRUE if the Status code == 200
				return (response.StatusCode == HttpStatusCode.OK);
			}
			catch
			{
				//Any exception will returns false.
				return false;
			}
		}

		private string GetUri(string assetName)
		{
			return this.uri + assetName;
		}

		/// <summary>
		/// Gets the web client that is used.
		/// </summary>
		public WebClient Client
		{
			get { return client; }
		}

		/// <summary>
		/// Gets or sets the base uri.
		/// </summary>
		public string Uri
		{
			get { return uri; }
			set { uri = value; }
		}
	}
}
