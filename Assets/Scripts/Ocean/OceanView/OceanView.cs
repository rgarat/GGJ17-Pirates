using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pirates.Ocean.OceanView
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class OceanView : MonoBehaviour
    {

        MeshRenderer meshRenderer;
        MeshFilter meshFilter;
        Mesh mesh1;
        Mesh mesh2;


        Vector3[] vertices;
        Color32[] colors;
        Vector2[] uvs;
        int[] triangles;

        bool loadedTexture = false;

        bool usingMesh1 = true;

        public Ocean ocean;

        public float cuadSize;

        void Awake()
        {
            this.meshRenderer = this.GetComponent<MeshRenderer>();
            this.meshFilter = this.GetComponent<MeshFilter>();
            this.mesh1 = new Mesh();
            this.mesh1.MarkDynamic();

            this.mesh2 = new Mesh();
            this.mesh2.MarkDynamic();

            this.mesh1.hideFlags = HideFlags.DontSaveInEditor;
            this.mesh2.hideFlags = HideFlags.DontSaveInEditor;
        }


        void LateUpdate()
        {
            var oceanWidth = Mathf.RoundToInt(ocean.size.x / cuadSize);
            var oceanHeight = Mathf.RoundToInt(ocean.size.y / cuadSize);

            int vertexNeeded = oceanWidth * oceanHeight * 3 * 2;


            if (this.vertices == null || vertexNeeded > this.vertices.Length)
            {
                this.CreateTempArrays(vertexNeeded);
            }

            int vertexIndex = 0;
            for (int xIndex = 0; xIndex < oceanWidth; xIndex++)
            {
                for (int zIndex = 0; zIndex < oceanHeight; zIndex++)
                {
                    var bl = new Vector3(xIndex * cuadSize, 0, zIndex * cuadSize);
                    var br = new Vector3((xIndex + 1) * cuadSize, 0, zIndex * cuadSize);
                    var tl = new Vector3(xIndex * cuadSize, 0, (zIndex + 1) * cuadSize);
                    var tr = new Vector3((xIndex + 1) * cuadSize, 0, (zIndex + 1) * cuadSize);

                    bl = ocean.GetWithHeight(bl);
                    br = ocean.GetWithHeight(br);
                    tl = ocean.GetWithHeight(tl);
                    tr = ocean.GetWithHeight(tr);

                    vertices[vertexIndex++] = bl;
                    vertices[vertexIndex++] = tl;
                    vertices[vertexIndex++] = tr;
                    vertices[vertexIndex++] = bl;
                    vertices[vertexIndex++] = tr;
                    vertices[vertexIndex++] = br;
                }
            }

            for (int i = vertexIndex; i < this.vertices.Length; i++)
            {
                vertices[i] = Vector3.zero;
            }

            for (int i = 0; i < this.vertices.Length; i++)
            {
                triangles[i] = i;
                colors[i] = Color.blue;
            }


            var mesh = usingMesh1 ? mesh1 : mesh2;
            mesh.vertices = this.vertices;
            //mesh.uv = this.uvs;
            mesh.colors32 = this.colors;
            mesh.SetTriangles(this.triangles, 0);

            mesh.RecalculateNormals();

            this.meshFilter.mesh = mesh1;
            usingMesh1 = !usingMesh1;
        }



        private void CreateTempArrays(int vertexNeeded)
        {

            this.vertices = new Vector3[vertexNeeded];
            this.colors = new Color32[vertices.Length];
            this.uvs = new Vector2[vertices.Length];
            this.triangles = new int[vertexNeeded];
        }


    }

}