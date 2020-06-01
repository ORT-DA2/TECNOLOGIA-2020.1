using System;
using System.Collections.Generic;
using Sort.Interface;

namespace Sort
{
    public class BubbleSort : ISort
    {
        public void Sort(ref List<int> elements)
        {
            Console.WriteLine("Ordenando por BubbleSort");
            int temp = 0;

            for (int write = 0; write < elements.Count; write++)
            {
                for (int sort = 0; sort < elements.Count - 1; sort++)
                {
                    if (elements[sort] > elements[sort + 1])
                    {
                        temp = elements[sort + 1];
                        elements[sort + 1] = elements[sort];
                        elements[sort] = temp;
                    }
                }
            }
        }
    }
}
