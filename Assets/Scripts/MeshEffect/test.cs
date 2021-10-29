using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    void Start()
    {
        Mesh mesh = new Mesh();

        Vector3[] vertices = new Vector3[4];//Mesh的頂點座標
        Vector2[] uv = new Vector2[4];//uv數量
        int[] triangles = new int[6];//三角形MESH的頂點

        //頂點順時鐘成形
        vertices[0] = new Vector3(0, 0);
        vertices[1] = new Vector3(0, 100);
        vertices[2] = new Vector3(100, 100);
        vertices[3] = new Vector3(100, 0);

        uv[0] = new Vector2(0, 0);//vertices normalized後的值, texture的座標
        uv[1] = new Vector2(0, 1);
        uv[2] = new Vector2(1, 1);
        uv[3] = new Vector2(1, 0);

        triangles[0] = 0;//對應頂點的位置 0 = vertices[0]. 依此類推
        triangles[1] = 1;
        triangles[2] = 2;
        triangles[3] = 0;
        triangles[4] = 2;
        triangles[5] = 3;

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
        GetComponent<MeshFilter>().mesh = mesh;
    }

}
