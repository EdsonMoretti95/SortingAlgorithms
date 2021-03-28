using System;

namespace SortingAlgorithms
{
    class Program
    {
        /// <summary>
        ///     Insertion sort has O(n2) time complexity in the worst case scenario but it is adaptive.
        ///     If the date is nearly sorted it will iterate less times because of the flag "correctPos" check.
        /// </summary>
        static int[] insertionSort(int[] arr)
        {
            // outer loop from 1 to length -1
            for (int i = 1; i < arr.Length; i++)
            {
                var correctPos = false;
                // go through array from i to 0 
                for (int k = i; k >= 1 && !correctPos;)
                {
                    // check if the number in k is lower than number in k - 1
                    if (arr[k] < arr[k - 1])
                    {
                        // swap the array positions using a tuple
                        (arr[k], arr[k - 1]) = (arr[k - 1], arr[k]);
                        k--;
                    }
                    else
                    {
                        // if the number in k is not lower than in k - 1 there's no reason to compare further back so stop the loop
                        correctPos = true;
                    }

                }
            }

            return arr;
        }

        /// <summary>
        ///     Selection sort always has worst case scenario time complexity O(n2) but it has the lowest number of swaps.
        ///     Selection sort is mainly used for sorting things where the cost of the swap is too heavy.
        /// </summary>
        static int[] selectionSort(int[] arr)
        {
            // outer loop from 1 to length -1
            for (int i = 0; i < arr.Length; i++)
            {
                // hold the smallest number's position
                int smallestPos = i;
                // inner loop from i + 1 to length -1
                for (int j = i + 1; j < arr.Length; j++)
                {
                    // check if smaller than smallest and hold that position
                    if (arr[j] < arr[smallestPos])
                    {
                        smallestPos = j;
                    }
                }
                // set the smallest number found in i
                (arr[i], arr[smallestPos]) = (arr[smallestPos], arr[i]);
            }

            return arr;
        }

        /// <summary>
        ///     Bubble sort is similar to insertion sort it has complexity O(n2) but it does more unecessary steps with nearly sorted data.
        /// </summary>
        static int[] bubbleSort(int[] arr)
        {
            // outer loop from 0 to length - 2
            for (int i = 0; i <= arr.Length - 2; i++)
            {
                // inner loop from 0 to length - 2
                for (int j = 0; j <= arr.Length - 2; j++)
                {
                    // compare and swap if greater than the next
                    if (arr[j] > arr[j + 1])
                    {
                        (arr[j + 1], arr[j]) = (arr[j], arr[j + 1]);
                    }
                }
            }

            return arr;
        }

        /// <summary>
        ///     Merge sort divides the array into two equal size arrays and recursively sorts these two arrays before merging them again.
        ///     The arrays are split into two until they have size 1, then the merge logic is applied.
        ///     Merge sort is good when memory is not a concern as it creates multiple arrays in the process.
        /// </summary>
        static int[] mergeSort(int[] arr)
        {
            int[] merge(int[] left, int[] right)
            {
                int resultLength = right.Length + left.Length;
                int[] result = new int[resultLength];
                
                int indexLeft = 0, indexRight = 0, indexResult = 0;

                //while the indexes are still lower than the size of the arrays
                while (indexLeft < left.Length || indexRight < right.Length)
                {
                    //if both indexes are still lower than the size of the arrays
                    if (indexLeft < left.Length && indexRight < right.Length)
                    {
                        //If item on left array is less than item on right array, add that item to the result array and increment left index
                        if (left[indexLeft] <= right[indexRight])
                        {
                            result[indexResult] = left[indexLeft];
                            indexLeft++;
                            indexResult++;
                        }
                        // else the item in the right array wll be added to the results array and increment right index
                        else
                        {
                            result[indexResult] = right[indexRight];
                            indexRight++;
                            indexResult++;
                        }
                    }
                    //if only the left array still has elements, add all its items to the results array and increment left index
                    else if (indexLeft < left.Length)
                    {
                        result[indexResult] = left[indexLeft];
                        indexLeft++;
                        indexResult++;
                    }
                    //if only the right array still has elements, add all its items to the results array and increment right index
                    else if (indexRight < right.Length)
                    {
                        result[indexResult] = right[indexRight];
                        indexRight++;
                        indexResult++;
                    }
                }
                return result;
            }


            int[] left;
            int[] right;
            int[] result = new int[arr.Length];

            // check if length <= 1 to avoid infinite recursion
            if (arr.Length <= 1)
                return arr;

            // The exact midpoint of our array  (int division will ignore the rest)
            int midPoint = arr.Length / 2;
            //Will represent our 'left' array
            left = new int[midPoint];

            //if array has an even number of elements, the left and right array will have the same number of elements
            if (arr.Length % 2 == 0)
                right = new int[midPoint];
            //if array has an odd number of elements, the right array will have one more element than left
            else
                right = new int[midPoint + 1];

            //populate left array
            for (int i = 0; i < midPoint; i++)
                left[i] = arr[i];
            //populate right array   
            int x = 0;
            //We start our index from the midpoint, as we have already populated the left array from 0 to midpont
            for (int i = midPoint; i < arr.Length; i++)
            {
                right[x] = arr[i];
                x++;
            }
            //Recursively sort the left array
            left = mergeSort(left);
            //Recursively sort the right array
            right = mergeSort(right);
            //Merge our two sorted arrays
            result = merge(left, right);

            return result;
        }

        /// <summary>
        ///     QuickSort is a gret general purpose sorting algorithm where the time is typically O(n log n) unless the items are not unique, in this case it can go up to O(n2).
        ///     The arrays are split into two sets and everything smaller than a pivot goes into the lower set followed by the pivot and the rest. 
        ///     The new position of the pivot is then stored and the two sets are sorted again recursively.
        /// </summary>
        static int[] quickSort(int[] arr)
        {
            int partition(int[] arr, int low, int high)
            {
                // store the pivot, always the highest position passed
                int pivot = arr[high];
                // store the starting index of elements that are smaller than the pivot (initialize with the lower bound given)
                int smallThanPivot = low; 

                for (int j = low; j < high; j++)
                {
                    // if current index value is less than the pivot
                    if (arr[j] < pivot)
                    {
                        // swap current item to the top of small than pivot set                    
                        (arr[smallThanPivot], arr[j]) = (arr[j], arr[smallThanPivot]);
                        // increment the top of small than pivot set
                        smallThanPivot++;
                    }
                }
                // Increment smallThanPivot to use it as the index for the new position of the pivot
                smallThanPivot++;

                // swap the pivot with smallThanPivot so we have everything that is smaller than the pivot followed directly by the pivot
                (arr[smallThanPivot], arr[high]) = (arr[high], arr[smallThanPivot]);

                // return the new index of the pivot
                return smallThanPivot;
            }

            void sort(int[] arr, int low, int high)
            {
                if (low < high)
                {
                    // here we partition the array to have a list where the items are [[lowerThanPivot], Pivot, [higherThanPivot]] and return the index of the pivot
                    int pivot = partition(arr, low, high);

                    // recursively sort the lowerThanPivot set
                    sort(arr, low, pivot - 1);
                    // recursively sort the higherThanPivot set
                    sort(arr, pivot + 1, high);
                }
            }

            sort(arr, 0, arr.Length - 1);

            return arr;
        }

        static void Main(string[] args)
        {
            int[] array1 = { 9, 5, 2, 7, 52, 63, 14, 24, 19, 67, 8, 83, 97, 51, 15, 3, 1, 10 };

            //array1 = insertionSort(array1);
            //array1 = selectionSort(array1);
            //array1 = bubbleSort(array1);
            //array1 = mergeSort(array1);
            //array1 = quickSort(array1);

            Console.WriteLine("Sorted Array is:");
            foreach (var item in array1)
            {
                Console.Write($"{item} ");
            }
        }
    }
}
