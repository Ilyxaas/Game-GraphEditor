using System;
using System.Collections.Generic;
using Interface;
using UnityEngine;
namespace TopRel
{
    class Weighted_Top  : TopInterface
    {
        //
        // Поля класса
        //
        public Vector3 Pos;
        public int Weigh_Top; //вес вершины
        public int TopIndex; //номер вершины
        public List<Pair> Edge; //список смежных вершин с данной

        //
        // конструкторы
        //

        public Weighted_Top(int _TopIndex,int Weight = 0) // конструктор для изолированной вершины
        {
            Pos = new Vector3(0,0,0);
            Weigh_Top = Weight;
            TopIndex = _TopIndex;
            Edge = new List<Pair>();
        }

        public Weighted_Top(int _TopIndex, List<Pair> _Edge) // конструктор для вершины со списком смежных
        {
            Pos = new Vector3(0, 0, 0);
            Weigh_Top = 0;
            TopIndex = _TopIndex;
            Edge = new List<Pair>(_Edge);
        }
        public Weighted_Top(int _TopIndex,int _Weight, List<Pair> _Edge) // конструктор для вершины со списком смежных и весом вершины
        {
            Pos = new Vector3(0, 0, 0);
            Weigh_Top = _Weight;
            TopIndex = _TopIndex;
            Edge = new List<Pair>(_Edge);
        }


        //
        //Методы
        //

        public void Print(bool Weight = true)
        {
            if (Weight)
            {
                Console.WriteLine("Вершина : " + TopIndex.ToString() + " Вес вершины : " + Weigh_Top.ToString() + " Смежна с :  [ID/Вес] ");
                if (Edge.Count > 0)
                    foreach (Pair i in Edge)
                        i.Print();
                else
                    Console.WriteLine(" (Вершина изолированна)");
                Console.WriteLine("");
            }
            else
            {
                Console.WriteLine("Вершина : " + TopIndex.ToString() +  " Смежна с :  [ID] ");
                if (Edge.Count > 0)
                    foreach (Pair i in Edge)
                        Console.Write(i.Edge.ToString() + " " );
                else
                    Console.WriteLine(" (Вершина изолированна)");
                Console.WriteLine("");


            }

        }

        public Pair ReturnPair(int _Edge) //
        {
            if (Edge.Count > 0)
                foreach (Pair i in Edge)
                    if (i.Edge == _Edge)
                        return i;
            return null;

        }
        public bool IsRing() //является ли вершина кольцом true - да  False - нет 
        {
            if (Edge.Count > 0)
                foreach (Pair i in Edge)
                    if (i.Edge == TopIndex)
                        return true;
            return false;
        }

        public int Gegree() //степень вершины 
        {
            int _Degree = 0;
            if (Edge.Count > 0)
                foreach (Pair i in Edge)
                {
                  if (i.Edge == TopIndex) // еслли есть колько то считаем за 2
                    _Degree += 2;
                  else
                      _Degree++;
                }
            return _Degree;
        }

        public bool ExistenceEdge(int EdgeIndex) // существует ли ребро в одном направлении
        {
            if (Edge.Count > 0) // если ребер больше 0
                foreach (Pair i in Edge) //проходимся по всем
                    if (EdgeIndex == i.Edge)
                        return true;
            return false;

        }

    }

    public class Pair // Пара
    {
        public int Edge; //Скем смежная
        public float WeightEdge; // вес данного ребра

        public Pair(int _Edge, int _WeightEdge = 0)
        {
            Edge = _Edge;
            WeightEdge = _WeightEdge;
        }

        public void Print()
        {
            Console.WriteLine("["+ Edge.ToString() + ", " + WeightEdge.ToString() + "]");
        }

        public override string ToString()
        {
            return "[" + Edge.ToString() + ", " + WeightEdge.ToString() + "]";
        }

    }

}




