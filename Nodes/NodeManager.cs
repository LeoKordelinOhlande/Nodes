using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace Nodes
{

    public class NodeManager
    {
        public Random random = new();

        private volatile bool shouldRun;

        private Task task;

        public readonly Dictionary<ulong, Node> nodes = new();
        public void AddNode(Node node)
        {
            lock (node)
            {
                lock (nodes)
                {
                    nodes.Add(node.id, node);
                    node.Initialize();
                    //throw new NotImplementedException();
                }
            }
        }
        public void ConnectNodes(ulong node1, int output, int input, ulong node2)
        {
            lock (nodes[node1])
            {
                lock (nodes[node2])
                {
                    nodes[node1].outputs[output].Connect(nodes[node2].inputs[input]);
                    throw new NotImplementedException();
                }
            }
        }
        public void DisconnectNodes(ulong node1,int output, int input, ulong node2)
        {
            lock (nodes[node1])
            {
                lock (nodes[node2])
                {
                    nodes[node1].outputs[output].Disconnect(nodes[node2]);
                }
            }
            throw new NotImplementedException();
        }
        public void RemoveNode(ulong node)
        {
            lock (nodes[node])
            {
                
                for (int i = 0; i < nodes[node].outputs.Length; i++)
                {

                    nodes[node].inputs[i].Disconnect();
                }
                
            }
            throw new NotImplementedException();
        }
        public bool IdExists(ulong id)
        {
            return nodes.ContainsKey(id);
        }
        public NodeManager()
        {
            shouldRun = true;
        }

        public void Start()
        {
            task = Task.Run(Run);
        }
        public void Run()
        {
            while (shouldRun)
            {
                Console.WriteLine($"NodeManager: {task.Id}\nState: {task.Status}\nNode Count: {nodes.Count}");
                
                
                
            }
        }

        public void Shutdown() => shouldRun = false;
    }
}
