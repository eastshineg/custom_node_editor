using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

namespace BTNode
{
    public class MouseEvent
    {
        public Vector2 CurPos { get { return this._cur_pos; } }
        public Node SelectNode { get { return this._select_node; } }
        public Node RightSelectNode { get { return this._right_select_node; } }
        public bool ContextMenuEvent { get { return this._show_context_menu; } }

        Vector2 _cur_pos;
        Node _select_node;
        Node _right_select_node;
        bool _show_context_menu;

        public void EventHandle(List<Node> node_list)
        {
            var mouse_event = Event.current;

            if (mouse_event.isMouse == true)
            {
                this._cur_pos = mouse_event.mousePosition;

                if (EventType.MouseDown == mouse_event.type)
                {
                    this._select_node = null;
                    this._right_select_node = null;

                    if (mouse_event.button == 0 || mouse_event.button == 1)
                    {
                        for(int i = 0; i < node_list.Count; ++i)
                        {
                            var node = node_list[i];
                            if(node.Data.WindowRect.Contains(this._cur_pos) == true)
                            {
                                if(mouse_event.button == 0)
                                {
                                    this._select_node = node;
                                }
                                else if(mouse_event.button == 1)
                                {
                                    this._right_select_node = node;
                                }
                            }
                        }
                    }

                    this._show_context_menu = false;
                    if (this._right_select_node == null && mouse_event.button == 1)
                    {
                        this._show_context_menu = true;
                    }
                }

                mouse_event.Use();
            }
        }
    }

    public enum ContextMenuType
    {
        ADD_NODE,
        MAX,
    }

    public struct NodeAddEventData
    {
        Vector2 Pos;
    }

    public class NodeEditorWindow : EditorWindow
    {
        MouseEvent _mouse_event;

        List<Node> _node_list = new List<Node>();

        [MenuItem("Window/Node_Editor_Window")]
        static void Init()
        {
            var window = EditorWindow.GetWindow<NodeEditorWindow>();
        }

        void OnGUI()
        {
            _mouse_event.EventHandle(this._node_list);

            if(_mouse_event.ContextMenuEvent == true)
            {
                var context_menu = new GenericMenu();
                context_menu.AddItem(new GUIContent("new node"), false, ContextMenuSelectCallback, ContextMenuType.ADD_NODE);
            }

            BeginWindows();

            EndWindows();
        }

        void ContextMenuSelectCallback(object obj)
        {
            var context_menu_type = (ContextMenuType)obj;

            switch (context_menu_type)
            {
                case ContextMenuType.ADD_NODE:
                    {
                        var node = new Node();
                        node.Data.WindowRect = new Rect(this._mouse_event.CurPos.x, this._mouse_event.CurPos.y, node.Data.WindowRect.width, node.Data.WindowRect.height);
                        this._node_list.Add(node);
                    }
                    break;
            }
        }
    }

}