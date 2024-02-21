using P90Ez.Twitch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp.Server;

namespace P90Ez.ChannelpointPlayer
{
    public class Server
    {
        private WebSocketServer ws;
        internal Logger Logger { get; private set; }

        public Server(int Port, Logger Logger)
        {
            this.Logger = Logger;
            ws = new WebSocketServer(Port);
            ws.AddWebSocketService<CPBroadcaster>("/", () => new CPBroadcaster(this));
            ws.Start();
        }

        private List<WebSocketBehavior> CPBroadcasterConnections = new List<WebSocketBehavior>();

        /// <summary>
        /// Adds a connection to the broadcasting list
        /// </summary>
        internal void AddBroadcasterConnection(WebSocketBehavior instance)
        {
            CPBroadcasterConnections.Add(instance);
        }

        /// <summary>
        /// Removes a connection from the broadcasting list
        /// </summary>
        internal void RemoveBroadcasterConnection(WebSocketBehavior instance)
        {
            CPBroadcasterConnections.Remove(instance);
        }

        /// <summary>
        /// Sends a message to all active connections.
        /// </summary>
        public void Broadcast(string Message)
        {
            foreach(var connection in CPBroadcasterConnections)
            {
                connection.Context.WebSocket.Send(Message);
            }
        }

        internal class CPBroadcaster : WebSocketBehavior
        {
            /// <summary>
            /// CTOR for saving a parent reference for registering connections.
            /// </summary>
            public CPBroadcaster(Server Parent) 
            {
                this.Parent = Parent;
            }

            private Server Parent { get; }
            protected override void OnOpen()
            {
                base.OnOpen();

                Parent.AddBroadcasterConnection(this); //register connection
            }

            protected override void OnError(WebSocketSharp.ErrorEventArgs e)
            {
                base.OnError(e);

                Parent.Logger.Log("WebsocketServer: " + e.Message, ILogger.Severety.Warning);
            }

            protected override void OnClose(WebSocketSharp.CloseEventArgs e)
            {
                Parent.RemoveBroadcasterConnection(this); //remove closing connection

                base.OnClose(e);
            }
        }
    }
}
