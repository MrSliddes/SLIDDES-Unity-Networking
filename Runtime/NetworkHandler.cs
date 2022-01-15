using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SLIDDES.Networking.WebGL
{
    /// <summary>
    /// The class to inherit with your own ServerHandler version to handle what the server does
    /// </summary>
    public abstract class NetworkHandler : MonoBehaviour
    {
        /// <summary>
        /// The dictonary that handles the networkMessageType with the correspondent NetworkMessageHandler Method
        /// </summary>
        /// <example>
        /// public override Dictionary<NetworkMessageType, NetworkMessageHandler> NetworkMessageHandlers { get { return networkMessageHandlers; }
        /// 
        /// private Dictionary<NetworkMessageType, NetworkMessageHandler> networkMessageHandlers = new Dictionary<NetworkMessageType, NetworkMessageHandler>
        /// {
        ///     { NetworkMessageType.PING, HandlePong }
        /// };
        /// </example>
        public abstract Dictionary<NetworkMessageType, NetworkMessageHandler> NetworkMessageHandlers { get; }
    }
}
