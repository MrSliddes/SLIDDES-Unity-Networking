using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace SLIDDES.Networking.WebGL
{
    /// <summary>
    /// For sending messages through WebGL with client.js
    /// </summary>
    /// <remarks>
    /// The way it works:
    /// 1. The SLIDDES.Networking.WebGL.NetworkWebGL class communicates with the server.js file
    /// 2. The server.js sends it back to client.js and client.js sends it back to 
    /// the referenced object name with the corrisponding method name.
    /// In this case
    /// NetworkWebGL -> server.js -> client.js -> NetworkWebGL.Handle
    /// </remarks>
    public class NetworkWebGL : MonoBehaviour
    {
        public static NetworkWebGL Instance
        {
            get
            {
                if(instance == null) return new GameObject(gameObjectName).AddComponent<NetworkWebGL>();
                return instance;
            }
            private set { }
        }

        /// <summary>
        /// The dictonary that handles the networkMessageType with the correspondent NetworkWebGLHandler Method
        /// </summary>
        public static Dictionary<NetworkMessageType, NetworkMessageHandler> networkMessageHandlers;
        /// <summary>
        /// Called when a network message is send
        /// </summary>
        public static DelegateSend OnSend;

        /// <summary>
        /// Internal reference, the objectName used in client.js unityInstance to send data 
        /// back to unity via unityInstance.SendMessage(objectName, methodName, value);
        /// </summary>
        private static readonly string gameObjectName = "[SLIDDES Network WebGL]"; // IMPORTANT Dont change this unless you know what you are doing
        private static NetworkWebGL instance;

        [Header("Configuration")]
        [Tooltip("The handle class that tells the NetworkWebGL what to do")]
        [SerializeField] private NetworkHandler handler;
        [Tooltip("UnityEngine.Debug.Log all data being send")]
        public bool debugLogSend = true;

        /// <summary>
        /// Called when a networkmessagehandler is send with Send()
        /// </summary>
        /// <param name="networkMessage"></param>
        public delegate void DelegateSend(NetworkMessageHandler networkMessage);
        /// <summary>
        /// A stopwatch to use for diagnostics purposes
        /// </summary>
        public System.Diagnostics.Stopwatch stopwatchTest_0;

        private void Awake()
        {
            if(instance != null)
            {
                Debug.LogWarning("[NetworkWebGL] instance already set. Make sure you only have 1 WebGL component in your scene!");
                Destroy(this);
                return;
            }
            instance = this;
            DontDestroyOnLoad(gameObject);
            Connect();
        }

        private void Start()
        {
            networkMessageHandlers = handler.NetworkMessageHandlers;
        }

        /// <summary>
        /// Connect to the server, auto run on start
        /// </summary>
        public static void Connect()
        {
#pragma warning disable CS0618 // Type or member is obsolete
            Application.ExternalEval("socket.isReady = true;");
#pragma warning restore CS0618 // Type or member is obsolete
        }

        /// <summary>
        /// Send a message to the server.js
        /// </summary>
        /// <param name="type">The network message type</param>
        /// <param name="data">Containing the nessesary json formatted data</param>
        public static void Send(NetworkMessageHandler networkMessage)
        {
            if(Instance.debugLogSend) Debug.Log("[NetworkWebGL] Send: " + networkMessage.FunctionName);
            networkMessage.Send();

            OnSend?.Invoke(networkMessage);
        }

        /// <summary>
        /// Handles data from client.js
        /// </summary>
        /// <param name="data">The JsonHandleBaseData string</param>
        /// <example>
        /// (server.js script)
        /// var data = { result: -1 };
        /// socket.emit("HANDLE", JSON.stringify({ type: "SERVER_LOGIN", data: JSON.stringify(data) }));
        /// socket.emit("Handle", JSON.stringify(handleData));
        /// </example>
        public void Handle(string data)
        {
            Debug.Log("[NetworkWebGL] Handle data raw: " + data);
            var d = JsonUtility.FromJson<HandleData>(data);
            if(!Enum.TryParse(d.type, out NetworkMessageType type))
            {
                UnityEngine.Debug.LogWarning("[NetworkWebGL] Enum name not recognised. Did you mistype it?");
                return;
            }

            // Check for message
            if(networkMessageHandlers.ContainsKey(type))
            {
                // Handle message
                Debug.Log("[NetworkWebGL] Handle networkMessageType: " + type + " with data: " + d.data);
                networkMessageHandlers[type].Handle(d.data);
            }
            else
            {
                // Didn't recognise message
                UnityEngine.Debug.LogWarning("[NetworkWebGL] Unsupported message type received: " + type + ". Did you forget to add it to the handler class dictonary?");
            }
        }

        /// <summary>
        /// Called by the .html file when the user tries to close the window
        /// </summary>
        public void OnWebApplicationQuit()
        {
            // Tell each gameobject to execute OnApplicationQuit
            GameObject[] gameObjects = FindObjectsOfType<GameObject>();
            foreach(GameObject item in gameObjects)
            {
                item.SendMessage("OnApplicationQuit", SendMessageOptions.DontRequireReceiver);
            }
        }

        /// <summary>
        /// Class for getting the networkMessageType first from the data
        /// </summary>
        private class HandleData
        {
            /// <summary>
            /// The type of networkmessage the data is
            /// </summary>
            public string type;
            /// <summary>
            /// The data belonging to the NetworkMessageHandler
            /// </summary>
            public string data;
        }
    }
}
