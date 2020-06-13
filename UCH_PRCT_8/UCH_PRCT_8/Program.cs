/*
 Эйлеров граф задан матрицей смежности. Найти в нем какой-либо эйлеров цикл.
 */
using System;
using System.Collections.Generic;

namespace Practice_8
{
    // Класс для задания тестовых данных
    //    Граф задается матрицей смежности: 
    //      число строк матрицы равно числу столбцов и равно количеству вершин в графе,
    //      в случае неориентированного графа информация в матрице избыточна, матрица симметрична относительно диагонали,
    //      значение в ячейке с индексами [i,j] равно 1, если между i-й и j-й вершиной существует ребро, иначе - 0,
    //      тип значений в массиве можно было сделать bit, но оставлен int для расширения функциональности для ориентированного графа (если ребро из вершины i в j, то 1, если из j в i, то -1).	
    static class TestData
    {

        // результат - матрица смежности, представленная двумерным массивом, параметр - имена вершин для удобства пользователя
        public static int[,] CreateTestGraph1(out List<string> names)
        {
            // имена вершин
            names = new List<string>();

            names.Add("x1");
            names.Add("x2");
            names.Add("x3");
            names.Add("x4");
            names.Add("x5");

            // матрица смежности
            return new int[,] {
                { 0, 1, 0, 1, 0 },
                { 1, 0, 1, 1, 1 },
                { 0, 1, 0, 1, 0 },
                { 1, 1, 1, 0, 1 },
                { 0, 1, 0, 1, 0 }
            };


        }

        // Аналог CreateTestGraph1 с другим графом (неэйлеровым)
        public static int[,] CreateTestGraph2(out List<string> names)
        {
            names = new List<string>();

            names.Add("x1");
            names.Add("x2");
            names.Add("x3");
            names.Add("x4");
            names.Add("x5");

            return new int[,] {
                { 1, 1, 1, 1, 1 },
                { 1, 0, 1, 1, 1 },
                { 1, 1, 1, 1, 1 },
                { 1, 0, 1, 1, 1 },
                { 1, 1, 1, 1, 1 }
            };

        }

    }

    static class EulerGraph
    {

        // Вычисляет степень i-й вершины в графе, заданном матрицей смежности arr.
        // Степень вершины - число инцидентных ей рёбер.
        private static int GetNodeDegree(int[,] arr, int i)
        {
            var n = arr.GetLength(0);

            var degree = 0;
            for (int j = 0; j <= n - 1; j++)
            {
                degree = degree + arr[i, j];
            }

            return degree;
        }


        // Проверяет является ли граф, заданный матрицей смежности arr Эйлеровым, использую теорему
        // связанный граф является Эйлеровым тогда и только тогда, когда все его вершины имеют чётную степень.
        public static bool CheckGraphIsEuler(int[,] arr)
        {
            var n = arr.GetLength(0);

            for (int i = 0; i <= n - 1; i++)
            {
                if ((GetNodeDegree(arr, i) % 2) != 0) // если степень вершины нечетна
                    return false;
            }

            return true;
        }


        // Рекурсивная процедура поиска эйлерова цикла 
        public static void FindEulerCycle(int[,] arr, List<string> names, int i)
        {
            var n = arr.GetLength(0);

            for (int j = 0; j <= n - 1; j++)
            {
                if (arr[i, j] == 1)
                {
                    // 1.  удаляем ребро из графа
                    arr[i, j] = 0;
                    arr[j, i] = 0;

                    //2. рекурсивный вызов
                    FindEulerCycle(arr, names, j);
                }
            }

            // Включаем вершину в путь						
            Console.Write(names[i] + " ");
        }

    }

    class Program
    {
        public static void Main(string[] args)
        {

            // 1. Задаем тестовые данные
            var node_names = new List<string>();
            int[,] arr = TestData.CreateTestGraph1(out node_names);

            // 2. Проверяем граф на Эйлеровость
            if (EulerGraph.CheckGraphIsEuler(arr))
                EulerGraph.FindEulerCycle(arr, node_names, 0); // 3. Ищем Эйлеров цикл
            else
                Console.Write("Заданный граф не является эйлеровым. ");

            Console.WriteLine();
            Console.Write("Для выхода нажмите любую клавишу . . . ");
            Console.ReadKey(true);
        }
    }
}