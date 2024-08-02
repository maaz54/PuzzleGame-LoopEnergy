using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnergyLoop.Game.Tiles
{
    [System.Serializable]
    public class Node
    {
        // Array representing connections [Up, Right, Down, Left]
        public int[] connections = new int[4];

        // 0 = no connection, 1 = connection
        public Node(int up, int right, int down, int left)
        {
            connections[0] = up;
            connections[1] = right;
            connections[2] = down;
            connections[3] = left;
        }

        // Rotate the node by 90 degrees clockwise
        public void RotateClockwise()
        {
            int temp = connections[3];
            for (int i = 3; i > 0; i--)
            {
                connections[i] = connections[i - 1];
            }
            connections[0] = temp;
        }
    }
}
