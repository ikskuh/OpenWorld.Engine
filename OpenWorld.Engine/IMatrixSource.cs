using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine
{
	/// <summary>
	/// Provides a view matrix.
	/// </summary>
	public interface IViewMatrixSource
	{
		/// <summary>
		/// Gets the view matrix.
		/// </summary>
		/// <returns></returns>
		Matrix4 GetViewMatrix(Camera camera);
	}

	/// <summary>
	/// Provides a projection matrix.
	/// </summary>
	public interface IProjectionMatrixSource
	{
		/// <summary>
		/// Gets the projection matrix.
		/// </summary>
		/// <returns></returns>
		Matrix4 GetProjectionMatrix(Camera camera);
	}
}
