using System;
using System.Diagnostics;

namespace Sorting
{
    class Program
    {
        public class SortingArray
        {

            public int[] array; 
            public int[] arrayUP;
            public int[] arrayDOWN;

            public SortingArray(int elements,Random random) //Funcion base
            {
                array = new int[elements];
                arrayUP = new int[elements];
                arrayDOWN = new int[elements];
                for (int i = 0; i < elements; i++)
                {
                    array[i] = random.Next();
                }
                Array.Copy(array, arrayUP, elements);
                Array.Sort(arrayUP);
                Array.Copy(arrayUP, arrayDOWN, elements);
                Array.Reverse(arrayDOWN);
            }

            public void Sort(Action<int[]> NoAction) //Se usa esta funcion para los cuatro tipos de ordenado, asi, cada utilice uno u otro en funcion de lo deseado // ** Se usa "Action" para funciones con retorno, y "NoAction" para funciones que no devuelven nada**
            {   

                Stopwatch time = new Stopwatch();
                int[] temp = new int[array.Length]; //Hacemos una copia para conservar el original
                Array.Copy(array, temp, array.Length);
                Console.WriteLine(NoAction.Method.Name);
                time.Start();

                NoAction(temp); 

                time.Stop();

                Console.WriteLine("Initial:" + time.ElapsedMilliseconds + "ms" + time.ElapsedTicks + "ticks");

                time.Reset();
               
                time.Start();
                NoAction(temp);
                time.Stop();
                Console.WriteLine("Increasing:" + time.ElapsedMilliseconds + "ms" + time.ElapsedTicks + "ticks");
                time.Reset();
                Array.Reverse(temp);

                time.Start();
                NoAction(temp);
                time.Stop();
                Console.WriteLine("Decreasing:" + time.ElapsedMilliseconds + "ms" + time.ElapsedTicks + "ticks");

            }
            
            //BUBBLESORT

            public void BubbleSort(int[] array)
            {
                for (int i = 0; i < array.Length -1; i++)
                {
                    for (int j = 0; j < array.Length-1; j++)
                    {
                        
                        if(array[j]> array[j+1])
                        {
                            int temp = array[j];
                            array[j] = array[j + 1];
                            array[j + 1] = temp;

                        }
                    }
                }

            }
            public void BubbleSortEarlyExit(int[] array)
            {
                bool ordered = true;
                for (int i = 0; i < array.Length - 1; i++)
                {
                    ordered = true;
                    for (int j = 0; j < array.Length - 1; j++)
                    {

                        if (array[j] > array[j + 1])
                        {
                            ordered = false;
                            int temp = array[j];
                            array[j] = array[j + 1];
                            array[j + 1] = temp;

                        }
                    }
                    if (ordered)
                        return;
                }


            }

            //QUICKSORT

            public void QuickSort(int[] array)
            {
                QuickSort(array, 0, array.Length);
            }

            public void QuickSort(int[] array, int left, int right)
            {
                if(left< right)
                {
                    int pivot = QuickSortPivot(array,left,right);
                    QuickSort(array, left, pivot);
                    QuickSort(array, pivot+1, right);
                }
                
            }
            public int QuickSortPivot(int[] array, int left, int right)
            {
                int pivot = array[(left + right) / 2];
                while (true)
                {
                    while (array[left] < pivot)
                    {
                        left++;
                    }
                    while (array[right] > pivot)
                    {
                        right--;
                    }
                    if(left>= right)
                    {
                        return right;
                    }
                    else
                    {
                        int temp = array[left];
                        array[left] = array[right];
                        array[right]=temp;
                        right--; left++;
                    }
                }
                
            }

            //INSERTIONSORT

            public void InsertionSort(int[] array)
            {
                int n = array.Length;
                int val;
                int flag;

                for (var i = 1; i < n; i++)
                {
                    val = array[i];
                    flag = 0;
                    for (var j = i - 1; j >= 0 && flag != 1;)
                    {
                        if (val < array[j])
                        {
                            array[j + 1] = array[j];
                            j--;
                            array[j + 1] = val;
                        }
                        else flag = 1;
                    }
                }
            }

            //MERGESORT

            public void MergeSort(int[] array)
            {
                this.MergeSortAux(array);
                return;
            }
            public int[] MergeSortAux(int[] array)
            {
                int[] left;
                int[] right;
                int[] result = new int[array.Length];
                //Como este es un algoritmo recursivo, necesitamos tener un caso base para
                //evitar una recursividad infinita y por lo tanto un "stackoverflow"
                if (array.Length <= 1)
                    return array;
                //El punto medio exacto de nuestra array
                int midPoint = array.Length / 2;
                // Representará nuestra array "izquierda"
                left = new int[midPoint];

                //Si la array tiene un número par de elementos, la matriz izquierda y derecha tendrán la misma cantidad de elementos
                if (array.Length % 2 == 0)
                    right = new int[midPoint];
                //si la array tiene un número impar de elementos, la matriz derecha tendrá un elemento más que la izquierda
                else
                    right = new int[midPoint + 1];
                //llenamos array izquierda
                for (int i = 0; i < midPoint; i++)
                    left[i] = array[i];
                //llenamos array izquierda  
                int x = 0;
                //Comenzamos nuestro índice desde el punto medio, ya que hemos llenado la array izquierda de 0 al punto medio
            for (int i = midPoint; i < array.Length; i++)
                {
                    right[x] = array[i];
                    x++;
                }
                //Ordenamos recursivamente la array izquierda
                left = MergeSortAux(left);
                //Ordenamos recursivamente la array derecha
                right = MergeSortAux(right);
                //Combinamos las dos arrays ordenadas
                result = merge(left, right);
                return result;
            }

            private static int[] merge(int[] left, int[] right)
            {
                int resultLength = right.Length + left.Length;
                int[] result = new int[resultLength];
                
                int indexLeft = 0, indexRight = 0, indexResult = 0;
                //mientras que cualquiera de las arrays todavía tenga un elemento
                while (indexLeft < left.Length || indexRight < right.Length)
                {
                    //si las dos arrays tinen un elemento
                    if (indexLeft < left.Length && indexRight < right.Length)
                    {
                        //Si el elemento de la array de la izquierda es menor que el elemento de la array de la derecha, se agrega ese elemento a la array de resultados
                        if (left[indexLeft] <= right[indexRight])
                        {
                            result[indexResult] = left[indexLeft];
                            indexLeft++;
                            indexResult++;
                        }
                        //Sino, el elemento en la array derecha se agregará a la array de resultados
                        else
                        {
                            result[indexResult] = right[indexRight];
                            indexRight++;
                            indexResult++;
                        }
                    }
                    //si solo la array izquierda todavía tiene elementos, se agrega todos sus elementos a la array de resultados
                    else if (indexLeft < left.Length)
                    {
                        result[indexResult] = left[indexLeft];
                        indexLeft++;
                        indexResult++;
                    }
                    //si solo la array correcta todavía tiene elementos, se agrega todos sus elementos a la matriz de resultados

                    else if (indexRight < right.Length)
                    {
                        result[indexResult] = right[indexRight];
                        indexRight++;
                        indexResult++;
                    }
                }
                return result;
            }



        }
        static void Main(string[] args)
        {
            Console.WriteLine("How many elements do you want?"); //Se pide el nimerop de elementos a ordenar
            int elements= int.Parse(Console.ReadLine());
            

            Console.WriteLine("what seet do you want to use?"); //Se pide el seet a usar
            int seed = int.Parse(Console.ReadLine());

            //Se selecciona el metodo a usar para ordenar, por defecto el está el MergeSort

            Random randon = new Random(seed);
            SortingArray array = new SortingArray(elements,randon);
            //array.Sort(array.BubbleSort);
            //array.Sort(array.BubbleSortEarlyExit);
            //array.Sort(array.QuickSort);
            //array.Sort(array.InsertionSort);
            array.Sort(array.MergeSort);
        }
            
    }
}
