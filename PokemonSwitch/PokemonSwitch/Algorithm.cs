using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonSwitch
{
    class Algorithm
    {
        const int SIZE = 4;
        Node[] arrNode; Node[] arrNodeParent; Node[] arrNodeParentSaved;
        Node startNode, endNode;
        bool solved; int nStep;
        int[] dx = { -1, 1, 0, 0 };
        int[] dy = { 0, 0, -1, 1 };
        private bool IsSameColor()
        {
            char c = arrNode[0].data;
            for(int i = 1; i < SIZE * SIZE; i ++)
            {
                if (arrNode[i].data != c) return false;
            }
            return true;
        }
        private void SwitchNodeColor(Node node)
        {
            switch(node.data)
            {
                case 'b':
                    arrNode[node.index].data = 'w'; break;
                case 'w':
                    arrNode[node.index].data = 'b'; break;
                default:
                    break;
            }
        }

        private void SwitchColor(Node node)
        {
            SwitchNodeColor(arrNode[node.index]);
            for(int i = 0; i < 4; i ++)
            {
                int indexX = node.x + dx[i];
                int indexY = node.y + dy[i];
                if(indexX >= 0 && indexX < SIZE && indexY >= 0 && indexY < SIZE)
                {
                    int nextIndex = indexX * SIZE + indexY;
                    SwitchNodeColor(arrNode[nextIndex]);
                }
            }
        }
        private void BackTrack(Node currentNode, int stepCount)
        {
            if(stepCount == nStep)
            {
                if (solved) return;
                if(IsSameColor())
                {
                    for (int i = 0; i < SIZE * SIZE; i++)
                        arrNodeParentSaved[i] = arrNodeParent[i];
                    solved = true;
                    endNode = currentNode;
                }
                return;
            }

            for(int i = currentNode.index + 1; i < SIZE * SIZE; i ++)
            {
                Node nextNode = arrNode[i];
                if(nextNode.isVisited == false)
                {
                    arrNode[nextNode.index].isVisited = true;
                    SwitchColor(arrNode[nextNode.index]);
                    arrNodeParent[nextNode.index] = currentNode;
                    stepCount++;
                    BackTrack(arrNode[nextNode.index], stepCount);
                    stepCount--;
                    SwitchColor(arrNode[nextNode.index]);
                    arrNode[nextNode.index].isVisited = false;
                }
            }
        }

        public List<Node> Solve(bool[] arrMap)
        {
            arrNode = new Node[SIZE * SIZE];
            arrNodeParent = new Node[SIZE * SIZE];
            arrNodeParentSaved = new Node[SIZE * SIZE];
            List<Node> result = new List<Node>();
            for(int i = 0; i < SIZE * SIZE; i ++)
            {
                Node node = new Node();
                node.x = i / SIZE; node.y = i % SIZE;
                node.index = i;
                node.data = arrMap[i] ? 'w' : 'b';
                arrNode[i] = node;
            }

            if (IsSameColor() == false)
            {
                for (int i = 1; i <= 7; i++)
                {
                    nStep = i;
                    solved = false;
                    for (int j = 0; j < SIZE * SIZE; j++)
                    {
                        SwitchColor(arrNode[j]);
                        arrNode[j].isVisited = true;
                        BackTrack(arrNode[j], 1);
                        arrNode[j].isVisited = true;
                        SwitchColor(arrNode[j]);
                        if (solved)
                        {
                            startNode = arrNode[j]; break;
                        }
                        for (int k = 0; k < SIZE * SIZE; k++)
                            arrNode[k].isVisited = false;
                    }
                    if (solved) break;
                }
            }
            else solved = true;

            if(solved && endNode != null)
            {
                Node node = endNode;
                while (true)
                {
                    result.Add(node);
                    if (node.index == startNode.index) break; ;
                    node = arrNodeParentSaved[node.index];
                }
            }
            return result;
        }
    }
    public class Node
    {
        const int SIZE = 4;
        public int index, x, y;
        public char data;
        public bool isVisited;
        public Node()
        {
            x = -1; y = -1; index = -1;
            data = '?';
            isVisited = false;
        }
    }
}
