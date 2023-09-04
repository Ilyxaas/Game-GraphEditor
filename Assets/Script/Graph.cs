
using System;
using System.Collections.Generic;
using TopRel;
using System.IO;
using Interface;
using UnityEngine;

namespace GraphRel
{
    class Weighted_Graph : GraphInterface //Взвешенный граф
    {
        //
        //поля графа
        //
        
        public List<Weighted_Top> List_Top; //список вершин

        public bool IsOrientation;
        public bool IsWeights;
        //
        //конструкоры графа
        //

        public Weighted_Graph() // создаем пустой граф по умолчанию не взвешенный и не ориентированный
        {

            List_Top = new List<Weighted_Top>();
            IsOrientation = false;
            IsWeights = false;
        }

        public Weighted_Graph(bool _Orientation, bool _Weights) // создаем пустой граф
        {
            List_Top = new List<Weighted_Top>();
            IsOrientation = _Orientation;
            IsWeights = _Weights;
        }
        public Weighted_Graph(int _Orientation, int _Weights) // создаем пустой граф
        {
            bool BLOrientation;
            bool BLWeights;

            if (_Orientation == 1)
                BLOrientation = true;
            else
                BLOrientation = false;


            if (_Weights == 1)
                BLWeights = true;
            else
                BLWeights = false;

            List_Top = new List<Weighted_Top>();
            IsOrientation = BLOrientation;
            IsWeights = BLWeights;
        }

        public Weighted_Graph(Weighted_Graph Gh)
        {
            List_Top = new List<Weighted_Top>(Gh.List_Top);
        }

        public Weighted_Graph(string _FileName) //Считываем граф из файла
        {
            List_Top = new List<Weighted_Top>();
            ReadGraph(_FileName);
        }

        //
        //методы графа
        //

        public bool Existence(int Index) // возвращает false если вершины нет и true если вершина есть
        {
            if (List_Top.Count != 0)//если есть хотя бы одна вершина
                foreach (Weighted_Top i in List_Top)
                    if (i.TopIndex == Index)
                        return true;
            return false;
        }

        public void Print() //вывести граф
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("----------------------------------");
            Console.WriteLine("----------------------------------");
            Console.WriteLine();
            Console.WriteLine();
            int Ring = 0;  //Кол-во колец

            if (List_Top.Count > 0)//если есть хотя бы одна вершина
                foreach (Weighted_Top i in List_Top)//cчитаем кольца
                    if (i.IsRing())
                        Ring++;

            if (IsWeights)
            {
                if (List_Top.Count > 0)//если есть хотя бы одна вершина
                {
                    foreach (Weighted_Top i in List_Top) // вывести все вершины с весами
                    {
                        i.Print();
                    }
                }
            }
            else
             if (List_Top.Count > 0)//если есть хотя бы одна вершина
                foreach (Weighted_Top i in List_Top) // вывести все вершины без весов
                {
                    i.Print(false);
                }


            Console.WriteLine();
            Console.WriteLine("----------------------------------");
            Console.WriteLine();
            Console.WriteLine("Гарактеристики графа");
            Console.WriteLine();
            Console.WriteLine("----------------------------------");
            Console.WriteLine("Ориентированный : " + IsOrientation);
            Console.WriteLine("Взвешенный : " + IsWeights);
            Console.WriteLine("Кол-во вершин : " + List_Top.Count);
            Console.WriteLine("Кол-во Петель в графе : " + Ring);
           // Console.WriteLine("Полный : " + IsFull());

        }

        public bool IsFull()// проверка на полноту графа
        {
            int Top = List_Top.Count;
            int Edge = 0;

            if (List_Top.Count > 0)//если есть хотя бы одна вершина
                foreach (Weighted_Top i in List_Top)//проходимся по всем вершинам
                    Edge += i.Edge.Count;

            if (Convert.ToInt32(Top) == Convert.ToInt32(Edge * Convert.ToDouble(Top - 1) / 2f))
                return true;
            else
                return false;
        }

        public bool Add(int Index,int WeightTop =0) //  true если вершина добавленна и false если вершина с таким номером уже есть
        {
            if (!Existence(Index))//если вершины с таким номером нет то добавляем
            {               
                List_Top.Add(new Weighted_Top(Index, WeightTop));
                return true;
            }
            else
                return false;

        }

        public bool Add(int Index,int _Weight, List<Pair> _Edge) //  true если вершина добавленна и false если вершина с таким номером уже есть
        {
            if (!Existence(Index))//если вершины с таким номером нет то добавляем
            {
                Weighted_Top NewTop = new Weighted_Top(Index, _Weight, _Edge);
                List_Top.Add(NewTop);
                return true;
            }
            else
                return false;

        }

        public Weighted_Top FindTop(int _TopIndex) // возвращает вершину по индексу null в случае отсутствия вершины
        {
            if (List_Top.Count > 0)//если есть хотя бы одна вершина
                foreach (Weighted_Top i in List_Top)
                    if (i.TopIndex == _TopIndex)
                        return i;
            return null;
        }

        public Pair FindPair(Weighted_Top Top, int _EdgeIndex) //Возвращает пару вес индекс по вершине и ID
        {
            if (Top.Edge.Count > 0)//если есть хотя бы одна вершина
                foreach (Pair i in Top.Edge)
                {
                    if (i.Edge == _EdgeIndex)
                        return i;
                }
            return null;
        }

        public bool Delete(int Index)
        {
            if (Existence(Index))//если такая вершина есть
            {
                List_Top.Remove(FindTop(Index)); //удаляем вершину
                //теперь если есть другие вершины которые связанны с данной удалим связи у них
                if (List_Top.Count > 0)//если есть хотя бы одна вершина
                    foreach (Weighted_Top i in List_Top)//проходимся по всем вершинам
                        if (i.ExistenceEdge(Index))
                            i.Edge.Remove(FindPair(i,Index));
                return true;
            }
            else
                return false;

        }

        public bool DeleteDirectedEdge(int _TopIndex, int DeleteEdge) //удалить существующее ребро у существующей вершины
        {
           if(!IsWeights)
            if (Existence(_TopIndex))
                if (FindTop(_TopIndex).ExistenceEdge(DeleteEdge))
                {
                    FindTop(_TopIndex).Edge.Remove(FindPair(FindTop(_TopIndex),DeleteEdge));
                    return true;
                }
           else
            if (Existence(_TopIndex))
                if (FindTop(_TopIndex).ExistenceEdge(DeleteEdge))
                {
                    FindTop(_TopIndex).Edge.Remove(FindPair(FindTop(_TopIndex), DeleteEdge));
                    FindTop(DeleteEdge).Edge.Remove(FindPair(FindTop(DeleteEdge), _TopIndex));
                            return true;
                }

            return false;
        }

        public int Step_of_the_approach(int Index)
        {
            int Count = 0;
            if (List_Top.Count > 0)//если есть хотя бы одна вершина
                foreach (Weighted_Top i in List_Top)
                {
                    //if (i.TopIndex != Index)
                    {
                        foreach (Pair j in i.Edge)
                            if (j.Edge == Index)
                                Count++;
                    }
                }
                    return Count;
        }

        public Weighted_Graph ReadGraph(string _FileName) //считать граф из файла 
        {

            FileStream Fstream = File.OpenRead(_FileName + ".txt");
            byte[] array = new byte[Fstream.Length];
            Fstream.Read(array, 0, array.Length);

            string TextFromFile = System.Text.Encoding.Default.GetString(array);
            TextFromFile = TextFromFile.Replace("\n", "");
            //Console.WriteLine(TextFromFile.Length);
                    


            Console.WriteLine(TextFromFile);
            int _IsWeights = Convert.ToInt32(TextFromFile.Substring(0, 1));
            int _IsOrientation = Convert.ToInt32(TextFromFile.Substring(1, 1));
            TextFromFile = TextFromFile.Remove(0,2);
            Console.WriteLine(TextFromFile);
            Weighted_Graph gr = new Weighted_Graph(_IsOrientation, _IsWeights);
            while (TextFromFile.Length > 0)//пока файл не равен 0
            {
                int index = TextFromFile.IndexOf(',', 0);
                if (index != -1)
                {
                    int Last = TextFromFile.IndexOf(';', 0);
                    int ID = Convert.ToInt32(TextFromFile.Substring(0, index));
                    TextFromFile = TextFromFile.Remove(0, index + 1);
                    Console.WriteLine("ID -  " + ID);

                    index = TextFromFile.IndexOf(',', 0);
                    Last = TextFromFile.IndexOf(';', 0);

                    Console.WriteLine(TextFromFile);
                    List<Pair> l = new List<Pair>();

                    if (Last < index) //значит есть связи иначе вершина вложенная
                    {
                        TextFromFile = TextFromFile.Remove(0, 1);
                    }
                    else
                    {
                        bool fl = true;
                        while (fl)
                        {

                            int index1 = TextFromFile.IndexOf(',', 0);
                            int Last1 = TextFromFile.IndexOf(';', 0);
                            Console.WriteLine("STOP -   " + Last1);
                            
                            if (Last1 == 0)
                            {
                                fl = false;
                                TextFromFile = TextFromFile.Remove(0, 1);
                                break;
                            }
                            if (index1 != -1)
                            {
                                int Edge = Convert.ToInt32(TextFromFile.Substring(0, index1));
                                Console.WriteLine("Edge -  " + Edge);
                                TextFromFile = TextFromFile.Remove(0, index1 + 1);
                                index1 = TextFromFile.IndexOf(',', 0);

                                int Weight = Convert.ToInt32(TextFromFile.Substring(0, index1));
                                Console.WriteLine("Weight -  " + Weight);
                                TextFromFile = TextFromFile.Remove(0, index1 + 1);
                                l.Add(new Pair(Edge, Weight));
                                Console.WriteLine(TextFromFile);
                            }
                        }
                    }
                    gr.Add(ID, 0, l);
                }
            }

            return gr;
        }



        public void Save(string _FileName)
        {
            FileStream fileStream = new FileStream(_FileName + ".txt", FileMode.Append);
            byte[] NewLine = System.Text.Encoding.Default.GetBytes("\n");
            if (fileStream != null)
            {
                //
                // сначала запишем параметры графа взвешенность и ориентированность 0-1
                //
                string STop = "";
                STop += Convert.ToInt32(IsWeights).ToString() + "\n";
                STop += Convert.ToInt32(IsOrientation).ToString() + "\n";
                byte[] arr = System.Text.Encoding.Default.GetBytes(STop);
                fileStream.Write(arr, 0, arr.Length);

                if (List_Top.Count > 0)//если есть хотя бы одна вершина
                    foreach (Weighted_Top i in List_Top)
                    {
                        STop = i.TopIndex.ToString();
                        if (i.Edge.Count > 0)
                            for (int j = 0; j < i.Edge.Count; j++)
                                STop += "," + i.Edge[j].Edge.ToString()+ ","+ i.Edge[j].WeightEdge.ToString() + ",";


                        STop += ",;";
                        byte[] array = System.Text.Encoding.Default.GetBytes(STop);
                        fileStream.Write(array, 0, array.Length);
                        fileStream.Write(NewLine, 0, NewLine.Length);
                    }
            }

            fileStream.Close();

        }        
    }
}
