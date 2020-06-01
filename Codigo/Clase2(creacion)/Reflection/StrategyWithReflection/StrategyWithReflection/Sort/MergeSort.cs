using System;
using System.Collections.Generic;
using System.Linq;
using Sort.Interface;

namespace Sort
{
    public class MergeSort : ISort
    {
        public void Sort(ref List<int> elements)
        {
            Console.WriteLine("Ordenando por Merge sort2");
            elements = this.MergingSort(elements);
        }

        private List<int> MergingSort(List<int> elements)
        {
            if (elements.Count <= 1)
                return elements;

            List<int> left = new List<int>();
            List<int> right = new List<int>();

            int middle = elements.Count / 2;
            for (int i = 0; i < middle; i++)  //Dividing the unsorted list
            {
                left.Add(elements[i]);
            }
            for (int i = middle; i < elements.Count; i++)
            {
                right.Add(elements[i]);
            }

            left = this.MergingSort(left);
            right = this.MergingSort(right);

            return this.Merge(left, right);
        }

        private List<int> Merge(List<int> left, List<int> right)
        {
            List<int> result = new List<int>();

            while (left.Count > 0 || right.Count > 0)
            {
                if (left.Count > 0 && right.Count > 0)
                {
                    if (left.First() <= right.First())  //Comparing First two elements to see which is smaller
                    {
                        result.Add(left.First());
                        left.Remove(left.First());      //Rest of the list minus the first element
                    }
                    else
                    {
                        result.Add(right.First());
                        right.Remove(right.First());
                    }
                }
                else if (left.Count > 0)
                {
                    result.Add(left.First());
                    left.Remove(left.First());
                }
                else if (right.Count > 0)
                {
                    result.Add(right.First());

                    right.Remove(right.First());
                }
            }
            return result;
        }
    }
}