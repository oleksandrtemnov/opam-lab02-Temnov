using System;

namespace Lab02
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            int n = 25;
            Random rnd = new Random(n);

            int[] mainArray = GenerateArray(16, 0, 5, rnd);
            PrintArray(mainArray);

            Task1_Aggregate(mainArray);

            int[] filteredArray = Task2_Filter(mainArray);
            PrintArray(filteredArray);

            Task3_FindLongestSeries(mainArray);

            int[] arrayToReverse = CopyArray(mainArray);
            Task4_ReverseInPlace(arrayToReverse);
            PrintArray(arrayToReverse);

            int[,] matrix = GenerateMatrix(5, 4, 0, 5, rnd);
            Task5_ProcessMatrix(matrix);

            RunEdgeCasesTests();
        }

        static int[] GenerateArray(int size, int minVal, int maxVal, Random rnd)
        {
            int[] arr = new int[size];
            for (int i = 0; i < arr.Length; i++)
                arr[i] = rnd.Next(minVal, maxVal + 1);
            return arr;
        }

        static int[,] GenerateMatrix(int rows, int cols, int minVal, int maxVal, Random rnd)
        {
            int[,] mat = new int[rows, cols];
            for (int i = 0; i < mat.GetLength(0); i++)
                for (int j = 0; j < mat.GetLength(1); j++)
                    mat[i, j] = rnd.Next(minVal, maxVal + 1);
            return mat;
        }

        static void PrintArray(int[] arr)
        {
            if (arr == null || arr.Length == 0)
            {
                Console.WriteLine("[]");
                return;
            }
            Console.Write("[");
            for (int i = 0; i < arr.Length; i++)
                Console.Write(arr[i] + (i < arr.Length - 1 ? ", " : ""));
            Console.WriteLine("]");
        }

        static int[] CopyArray(int[] arr)
        {
            if (arr == null) return null;
            int[] copy = new int[arr.Length];
            for (int i = 0; i < arr.Length; i++)
                copy[i] = arr[i];
            return copy;
        }

        public static void Task1_Aggregate(int[] arr)
        {
            if (arr == null || arr.Length == 0)
            {
                Console.WriteLine("Результати не визначені");
                return;
            }

            int sum = 0, zeroCount = 0;
            int minVal = arr[0], minIndex = 0;
            int maxVal = arr[0], maxIndex = 0;

            for (int i = 0; i < arr.Length; i++)
            {
                int val = arr[i];
                sum += val;

                if (val == 0) zeroCount++;

                if (val < minVal)
                {
                    minVal = val;
                    minIndex = i;
                }

                if (val > maxVal)
                {
                    maxVal = val;
                    maxIndex = i;
                }
            }

            double average = (double)sum / arr.Length;
            Console.WriteLine($"Сума: {sum}, Середнє: {average:F2}, Min: {minVal} ({minIndex}), Max: {maxVal} ({maxIndex}), Нулів: {zeroCount}");
        }

        public static int[] Task2_Filter(int[] arr)
        {
            if (arr == null || arr.Length == 0) return new int[0];

            int count = 0;
            for (int i = 0; i < arr.Length; i++)
                if (arr[i] % 5 == 0) count++;

            int[] result = new int[count];
            int index = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] % 5 == 0)
                {
                    result[index] = arr[i];
                    index++;
                }
            }

            return result;
        }

        public static void Task3_FindLongestSeries(int[] arr)
        {
            if (arr == null || arr.Length == 0)
            {
                Console.WriteLine("Серії відсутні");
                return;
            }

            int bestVal = arr[0], bestLength = 1, bestStart = 0;
            int currentVal = arr[0], currentLength = 1, currentStart = 0;

            for (int i = 1; i < arr.Length; i++)
            {
                if (arr[i] == currentVal)
                {
                    currentLength++;
                }
                else
                {
                    if (currentLength > bestLength)
                    {
                        bestLength = currentLength;
                        bestVal = currentVal;
                        bestStart = currentStart;
                    }
                    currentVal = arr[i];
                    currentLength = 1;
                    currentStart = i;
                }
            }

            if (currentLength > bestLength)
            {
                bestLength = currentLength;
                bestVal = currentVal;
                bestStart = currentStart;
            }

            Console.WriteLine($"Серія: Значення = {bestVal}, Довжина = {bestLength}, Індекс = {bestStart}");
        }

        public static void Task4_ReverseInPlace(int[] arr)
        {
            if (arr == null || arr.Length <= 1) return;

            int left = 0, right = arr.Length - 1;
            while (left < right)
            {
                int temp = arr[left];
                arr[left] = arr[right];
                arr[right] = temp;
                left++;
                right--;
            }
        }

        public static void Task5_ProcessMatrix(int[,] matrix)
        {
            if (matrix == null || matrix.GetLength(0) == 0 || matrix.GetLength(1) == 0) return;

            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                    Console.Write($"{matrix[i, j],4}");
                Console.WriteLine();
            }

            int maxRowSum = int.MinValue, maxRowIndex = 0;

            for (int i = 0; i < rows; i++)
            {
                int currentSum = 0;
                for (int j = 0; j < cols; j++)
                    currentSum += matrix[i, j];

                Console.WriteLine($"Сума рядка {i}: {currentSum}");

                if (currentSum > maxRowSum)
                {
                    maxRowSum = currentSum;
                    maxRowIndex = i;
                }
            }

            Console.WriteLine($"Рядок з max сумою: {maxRowIndex} ({maxRowSum})");

            for (int j = 0; j < cols; j++)
            {
                int maxColVal = matrix[0, j];
                for (int i = 1; i < rows; i++)
                {
                    if (matrix[i, j] > maxColVal)
                        maxColVal = matrix[i, j];
                }
                Console.WriteLine($"Max стовпця {j}: {maxColVal}");
            }
        }

        static void RunEdgeCasesTests()
        {
            int[] emptyArray = new int[0];
            Task1_Aggregate(emptyArray);
            PrintArray(Task2_Filter(emptyArray));
            Task3_FindLongestSeries(emptyArray);
            Task4_ReverseInPlace(emptyArray);

            int[] singleElement = new int[] { 7 };
            Task1_Aggregate(singleElement);
            PrintArray(Task2_Filter(singleElement));
            Task3_FindLongestSeries(singleElement);
            Task4_ReverseInPlace(singleElement);

            int[] sameElements = new int[] { 5, 5, 5, 5 };
            Task1_Aggregate(sameElements);
            Task3_FindLongestSeries(sameElements);
        }
    }
}