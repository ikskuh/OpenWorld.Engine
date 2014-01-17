using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWorld.Engine.Sound
{
    /// <summary>
    /// Defines an OpenAL resource.
    /// </summary>
    interface IALResource : IDisposable
    {
        /// <summary>
        /// Gets the resource identifier of the OpenAL resource.
        /// </summary>
        int Id { get; }

       
    }
}
