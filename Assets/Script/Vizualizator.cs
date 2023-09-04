using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GraphRel;
using TopRel;
using System;

public class Vizualizator : MonoBehaviour
{
    [Header("Цвет исходящих ребер")]
    [SerializeField] Color FromEdgeColor;  // цвет для ребер исходящей из вершины
    [Header("Цвет входящих ребер")]
    [SerializeField] Color InEdgeColor;  // цвет для ребер исходящей из вершины
    [Header("Цвет ребер двустороннего направления ребер")]
    [SerializeField] Color FulEdgeColor;  // цвет для ребер исходящей из вершины
    [Header("Цвет ребер вершины")]
    [SerializeField] Color TopColor;  // цвет для ребер исходящей из вершины
    [Header("Минимальное расстояние между вершинами")]
    [SerializeField] int MinDist;

    [Space]

    [Header("Модель ребра")]
    [SerializeField] GameObject EdgeModerl;
    [Header("Модель вершины")]
    [SerializeField] GameObject TopModerl;

    [Space]

    [Header("Название файла для считывание")]
    public string FileName;

    Weighted_Graph Graph = new Weighted_Graph();

    [Space]

    public List<GameObject> Edgel;
    public List<GameObject> Topl;
    // Start is called before the first frame update
    void Start()
    {
        Graph = Graph.ReadGraph(FileName);
        //print(Graph.List_Top.Count);
        SpawnGraph();

      
           
    }

   
    private void SpawnGraph()
    {
      if (Graph.List_Top.Count > 0)
        foreach (Weighted_Top i in Graph.List_Top) //  отрисовываем графы
        {
            i.Pos = new Vector3(UnityEngine.Random.Range(MinDist, MinDist + 5f), UnityEngine.Random.Range(MinDist, MinDist + 5f), UnityEngine.Random.Range(MinDist, MinDist + 5f));
            GameObject NewObj = Instantiate(TopModerl, i.Pos, Quaternion.identity);
            NewObj.GetComponent<MeshRenderer>().material.color = TopColor;
            Topl.Add(NewObj);
        }

      if(Graph.List_Top.Count >0)
        foreach (Weighted_Top i in Graph.List_Top) //проходимся по всем вершинам графа
        {
           if(i.Edge.Count > 0)
            foreach (Pair j in i.Edge) //проходимся по всем вершинам графа
            {
                Vector3 tp = Graph.FindTop(j.Edge).Pos; // позиция вершины в которую надо отправить ребро
                Vector3 Rot = i.Pos - tp;
                        float Scale = Vector3.Distance(i.Pos, tp) * 100 / 2;
                        //GetVector(i.Pos,tp);
                //print(Scale);                
                GameObject NewObj = Instantiate(EdgeModerl, i.Pos, Quaternion.identity);
                        NewObj.transform.localScale = new Vector3(10f,10f,Scale);
                        NewObj.transform.Rotate(GetVector(i.Pos, tp));
                Edgel.Add(NewObj);
            }
        }
    }
    Vector3 GetVector(Vector3 start,Vector3 end)
    {
        //print(start);
        //print(end);
        
        double DistXZ = Math.Sqrt((start.x - end.x) * (start.x - end.x) + (start.z - end.z) * (start.z - end.z));
        double distYZ = Math.Sqrt((start.y - end.y) * (start.y - end.y) + (start.z - end.z) * (start.z - end.z));
        //print("Dist   " + DistXZ );
        double Yrot = 0;
        double Xrot = 0;

        //
        //Для Yrot
        //

        if (start.x < end.x && start.z < end.z)
        {
            print("1");
            Yrot = Math.Asin((end.x - start.x) / DistXZ) * 180f / Math.PI;
        }

        if (start.x > end.x && start.z < end.z)
        {
            print("12");
            Yrot = -1 * Math.Asin((start.x - end.x) / DistXZ) * 180f / Math.PI; // верно
        }


        if (start.x < end.x && start.z > end.z)
        {
            print("13");
            Yrot = 90 + Math.Asin((start.z - end.z) / DistXZ) * 180f / Math.PI;
        }

        if (start.x > end.x && start.z > end.z)
        {
            print("14");
            Yrot = 180 + Math.Asin((start.x - end.x) / DistXZ) * 180f / Math.PI;
        }

        //
        // для Xrot
        //

        if (start.y < end.y && start.z < end.z)
        {
            print("111");
            Xrot = Math.Asin((end.y - start.y) / DistXZ) * 180f / Math.PI;
        }
        if (start.y > end.y && start.z < end.z)
        {
            print("1222");
            Xrot = -1 *Math.Asin((start.y - end.y) / DistXZ) * 180f / Math.PI;
        }
        if (start.y < end.y && start.z > end.z)
        {
            print("2221");
            Xrot = 90 + Math.Asin((start.z - end.z) / DistXZ) * 180f / Math.PI;
        }
        if (start.y > end.y && start.z > end.z)
        {
            print("2221");
            Xrot = 180 + Math.Asin((start.y - end.y) / DistXZ) * 180f / Math.PI;
        }
        print(Yrot);
        print(Xrot);
        return new Vector3(-(float)Xrot,(float)Yrot,0);
    }
   
}
