using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SLIDDES.Networking.WebGL
{
    /// <summary>
    /// The base class to inherit for send / handling logic
    /// </summary>
    public abstract class NetworkMessageHandler
    {
        /// <summary>
        /// The function name in the server.js file
        /// </summary>
        public abstract string FunctionName { get; }
        /// <summary>
        /// The corresponding network message type to this network message
        /// </summary>
        public abstract NetworkMessageType Type { get; }
        /// <summary>
        /// The data that gets send
        /// </summary>
        /// <example>
        /// public override Data DataSend => dataSend;
        /// private DataS dataSend = new DataS();
        ///
        /// private class DataS : Data { // The data you want to send }
        /// </example>
        public abstract Data DataSend { get; }

        /// <summary>
        /// Interal Send Method
        /// </summary>
        public virtual void Send()
        {
#pragma warning disable CS0618 // Type or member is obsolete
            string d = JsonUtility.ToJson(DataSend);
            Debug.Log(string.Format("[WebGL] Send: {0} With data: {1}", FunctionName, d));
            Application.ExternalCall("socket.emit", FunctionName, d);
#pragma warning restore CS0618 // Type or member is obsolete
        }

        /// <summary>
        /// If the network message has a return function
        /// </summary>
        /// <param name="data"></param>
        public virtual void Handle(string data) { }

        /// <summary>
        /// The base class for data that gets send with which you need to inherit from
        /// </summary>
        /// <example>
        /// DataS = DataSend
        /// DataR = DataReceive
        /// </example>
        public abstract class Data { }
    }
}
