using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxFlowProblem
{
    class Program
    {
        static int[,] InputMatrix;
        static int[,] FlowMatrix;
        static int size;

        static void Main(string[] args)
        {
            
            InputMatrix = StringToMatrix(File.ReadAllText("input.txt"));

            FlowMatrix = new int[size, size];

            int s;
            int maxFlow = 0;
            int __s = int.MaxValue;
            do
            {
                s = Go(0, new bool[size], ref __s);
                maxFlow += s;
            } while (s > 0);
            

            Console.WriteLine(maxFlow);
            Console.Read();
        }
        

        static int Go(int currentEdge, bool[] visited, ref int min)
        {
            if (currentEdge == size - 1)
                return min;
            

            for (int i = 0; i < size; i++)
            {
                int maxFlow = InputMatrix[currentEdge, i];
                int flow = FlowMatrix[currentEdge, i];

                if (maxFlow == 0)
                    continue;

                if (!visited[i] && flow < maxFlow)
                {
                    visited[currentEdge] = true;
                    if (min > maxFlow - flow && maxFlow != flow)
                        min = maxFlow - flow;
                    int delta = Go(i, visited, ref min);

                    if (delta > 0)
                    {
                        FlowMatrix[currentEdge, i] += delta;

                        return delta;
                    }
                }
            }

            for (int i = 0; i < size; i++)
            {
                int maxFlow = InputMatrix[i, currentEdge];
                int flow = FlowMatrix[i, currentEdge];

                if (!visited[i] && flow > 0)
                {
                    visited[i] = true;
                    if (min > maxFlow - flow && maxFlow != flow)
                        min = maxFlow - flow;
                    int delta = Go(i, visited, ref min);

                    if (delta > 0)
                    {
                        FlowMatrix[i, currentEdge] -= delta;

                        return delta;
                    }
                }
            }

            return 0;
        }


        static int[,] StringToMatrix(string str)
        {
            string[] lines = str.Split(
                new[] { "\r\n", "\r", "\n" },
                StringSplitOptions.None
            );

            size = lines.Length;
            int[,] res = new int[lines.Length, lines.Length];

            for (int i = 0; i < lines.Length; i++)
            {
                string[] items = lines[i].Split(' ');
                for (var j = 0; j < items.Length; j++)
                {
                    res[i, j] = int.Parse(items[j]);
                }
            }

            return res;
        }
    }
}
