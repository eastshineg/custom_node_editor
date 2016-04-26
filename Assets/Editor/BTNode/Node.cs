using UnityEngine;
using UnityEditor;

namespace BTNode
{
    public class Node
    {
        NodeData _node_data;
        public NodeData Data { get { return this._node_data; } }
        public int ID { get { return this._id; } }

        int _id;

        public Node(int id)
        {
            this._node_data = new NodeData();
            this._node_data.WindowRect = new Rect(0, 0, 100, 40);
            this._id = id;
        }
        
        public Node(NodeData node_data, int id)
        {
            this._node_data = node_data;
            this._id = id;
        }

        public void Draw()
        {
            this._node_data.WindowRect = GUI.Window(this._id, this._node_data.WindowRect, DrawWindow, this._node_data.WindowTitle);
        }

        void DrawWindow(int id)
        {

        }
    }
}