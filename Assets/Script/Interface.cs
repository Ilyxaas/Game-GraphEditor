using System;
using System.Collections.Generic;
using TopRel;
using GraphRel;

namespace Interface
{
    public interface TopInterface
    {
        bool ExistenceEdge(int EdgeIndex); // существует ли ребро в одном направлении
        void Print(bool Weight); // вывести вершину
        int Gegree(); //степень вершины
        bool IsRing(); //является ли вершина кольцом true - да  False - нет 
    }

    public interface GraphInterface
    {
       //public Weighted_Graph ReadGraph(string _FileName); //считать граф из файла
        void Save(string _FileName); //Сохранить вершину в файл
        bool Existence(int Index); //Существует ли
        void Print(); //вывести граф
        bool IsFull();// проверка на полноту графа
    }
}
