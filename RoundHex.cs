using UnityEngine;
using System.Collections;

public class RoundHex : MonoBehaviour
{
    public int HexSize;
    public int sizeX;
    public int sizeY;
    
    void Start()
    {
        sizeX = HexSize * 2 + 1;
        sizeY = HexSize * 2 + 1;

        HexMap(HexSize, 0);


    }

    void HexMap(int size, int z)
    {
        //int numHex = 1;

        //for (int i=1; i< size+1; i++)
        //{
        //    numHex += 6 * i;
        //}
        int numHex = size * 2 + 1;
        for (int i=0; i < size; i++)
        {
            numHex += 2 * (size + 1 + i);
        }
        Debug.Log(numHex);

        int numVerts = 7 * numHex;        
        int numTriangles = 3 * 6 * numHex;
        

        Mesh __mesh = new Mesh();                   // Создаем Mesh который мы будем передавать далее

        Vector3[] vertices = new Vector3[numVerts]; // Создаем переменную для хранения вершин
        Vector3[] normals = new Vector3[numVerts];  // Создаем переменную для хранения нормалей
        Vector2[] uv = new Vector2[numVerts];       // Создаем переменную для хранения UV
        int[] triangles = new int[numTriangles];    // Создаем переменную для хранения треугольников

        //#region CenterHex
        
        //#region verts

        ////positions vertices of the hexagon to make a normal hexagon      

        //vertices = new Vector3[]
        //        {
        //            /*0*/new Vector3( 0.50f, z,  0.50f),
        //            /*1*/new Vector3( 0.50f, z,  1.00f),
        //            /*2*/new Vector3( 1.00f, z,  0.75f),
        //            /*3*/new Vector3( 1.00f, z,  0.25f),
        //            /*4*/new Vector3( 0.50f, z,  0.00f),
        //            /*5*/new Vector3( 0.00f, z,  0.25f),
        //            /*6*/new Vector3( 0.00f, z,  0.75f)                    
        //        };
        //#endregion
        //#region Normals

        ////positions vertices of the hexagon to make a normal hexagon      

        //normals = new Vector3[]
        //        {
        //            /*0*/Vector3.up,
        //            /*1*/Vector3.up,
        //            /*2*/Vector3.up,
        //            /*3*/Vector3.up,
        //            /*4*/Vector3.up,
        //            /*5*/Vector3.up,
        //            /*6*/Vector3.up
        //        };
        //#endregion
        //#region triangles
        ////triangles connecting the verts

        //triangles = new int[]
        //        {
        //            0,1,2,
        //            0,2,3,
        //            0,3,4,
        //            0,4,5,
        //            0,5,6,
        //            0,6,1
        //        };
        //#endregion

        //#region uv
        ////uv mappping
        //uv = new Vector2[]
        //        {
        //            /*0*/new Vector2( 0.50f, 0.50f),
        //            /*1*/new Vector2( 0.50f, 1.00f),
        //            /*2*/new Vector2( 1.00f, 0.75f),
        //            /*3*/new Vector2( 1.00f, 0.25f),
        //            /*4*/new Vector2( 0.50f, 0.00f),
        //            /*5*/new Vector2( 0.00f, 0.25f),
        //            /*6*/new Vector2( 0.00f, 0.75f)
        //        };
        //#endregion

        //#endregion

        int num_Hex = 0;
        int num_verts = 0;
        int num_triangles = 0;
        bool notOdd;
        //bool __isHex;

        for (int ly=0; ly < size * 2 + 1; ly++)
        {
            int _dx = 0;
            int _dy = 0;

            if (size % 2 == 0)
            {
                _dx = Mathf.Abs(size - ly) / 2;
                _dy = size * 2 - (ly + 2) % 2 - _dx;
            }
            else
            {
                _dx = Mathf.Abs(size - ly) / 2;
                _dy = size * 2 - (ly + 3) % 2 - _dx;
            }

            for (int lx=_dx; lx < _dy + 1; lx++)
            {
                //if (ly<size && lx > (size / 2) - ly || ly < size && lx > (size * 2) - ly)
                //if (ly < size && lx > (size / 2) - ly)
                //    { continue; }
                //else
                //    { __isHex = true; }

                //if (ly>size && lx > Mathf.Abs (ly/2 - size ) || ly > size && lx > Mathf.Abs(ly * 2 - size))
                //    { continue; }
                //else
                //{ __isHex = true; }

                num_Hex++; //Count of сurrent HEX

                #region Vertices and UV
                //determine if we are in an odd row; if so we need to offset the hexagons

                notOdd = ((ly % 2) == 0);
                if (size % 2 != 0)
                {
                    notOdd = !notOdd;
                }
                

                //Можно уменьшить кол-во строк, если сделать:
                //
                //  if (notOdd != true) {__dx=0.50f; __dy=0.75f; __ly=ly-1;}
                //  else {__dx=0; __dy=0; __ly=ly;}
                //
                //и запись вершинт в следующем виде:
                //
                //  vertices[0 + num_verts] = new Vector3(0.0f + __dx + lx, z, 0.00f + __dy + 0.75f * __ly);
                //
                if (notOdd == true)
                {
                    //if (ly < size && lx < (size/2)-(ly/2) || ly < size && lx > size*2 -2 + ly/2)   //(size / 2) - ly
                    //{ continue; }
                    //if (ly > size && lx < -(size / 2) + (ly / 2) || ly > size && lx > size*2 - Mathf.CeilToInt( ly/size))   //(size / 2) - ly
                    //{ continue; }
                    //if (lx<Mathf.Abs(size/2 - (ly / 2)) || lx> Mathf.Abs((ly-size) / 2 + size*2) ) { continue; }
                    //generate the hexagons in the normal positioning for this row
                    /*0*/
                    vertices[0 + num_verts] = new Vector3(0.0f + lx, z, 0.00f + 0.75f * ly);
                    uv[0 + num_verts] = new Vector2((0.0f + lx) / sizeX, (0.00f + 0.75f * ly) / sizeY);

                    /*1*/
                    vertices[1 + num_verts] = new Vector3(0.0f + lx, z, 0.50f + 0.75f * ly);
                    uv[1 + num_verts] = new Vector2((0.0f + lx) / sizeX, (0.50f + 0.75f * ly) / sizeY);

                    /*2*/
                    vertices[2 + num_verts] = new Vector3(0.5f + lx, z, 0.25f + 0.75f * ly);
                    uv[2 + num_verts] = new Vector2((0.5f + lx) / sizeX, (0.25f + 0.75f * ly) / sizeY);

                    /*3*/
                    vertices[3 + num_verts] = new Vector3(0.5f + lx, z, -0.25f + 0.75f * ly);
                    uv[3 + num_verts] = new Vector2((0.5f + lx) / sizeX, (-0.25f + 0.75f * ly) / sizeY);

                    /*4*/
                    vertices[4 + num_verts] = new Vector3(0.0f + lx, z, -0.50f + 0.75f * ly);
                    uv[4 + num_verts] = new Vector2((0.0f + lx) / sizeX, (-0.50f + 0.75f * ly) / sizeY);

                    /*5*/
                    vertices[5 + num_verts] = new Vector3(-0.5f + lx, z, -0.25f + 0.75f * ly);
                    uv[5 + num_verts] = new Vector2((-0.5f + lx ) / sizeX, (-0.25f + 0.75f * ly) / sizeY);

                    /*6*/
                    vertices[6 + num_verts] = new Vector3(-0.5f + lx, z, 0.25f + 0.75f * ly);
                    uv[6 + num_verts] = new Vector2((-0.5f + lx) / sizeX, (0.25f + 0.75f * ly) / sizeY);
                }
                else
                {
                    //if (lx == size * 2) { continue; }
                    //if (ly < size && lx < (size / 2) - (ly ) || ly < size && lx > size * 2 - 2 + ly / 2)   //(size / 2) - ly
                    //{ continue; }
                    //if (ly > size && lx < ly-size* Mathf.CeilToInt((ly+1) / size))//(size / 2) - 2 * Mathf.CeilToInt(ly / size))// || ly > size && lx > size * 2 - Mathf.CeilToInt(ly / size))   //(size / 2) - ly
                    //{ continue; }
                    //generate the hexagons in the offset positioning for this row
                    /*0*/
                    vertices[0 + num_verts] = new Vector3(0.5f + lx, z, 0.75f + 0.75f * (ly - 1));
                    uv[0 + num_verts] = new Vector2((0.5f + lx ) / sizeX, (0.75f + 0.75f * (ly - 1)) / sizeY);

                    /*1*/
                    vertices[1 + num_verts] = new Vector3(0.5f + lx, z, 1.25f + 0.75f * (ly - 1));
                    uv[1 + num_verts] = new Vector2((0.5f + lx) / sizeX, (1.25f + 0.75f * (ly - 1)) / sizeY);

                    /*2*/
                    vertices[2 + num_verts] = new Vector3(1.0f + lx, z, 1.00f + 0.75f * (ly - 1));
                    uv[2 + num_verts] = new Vector2((1.0f + lx) / sizeX, (1.00f + 0.75f * (ly - 1)) / sizeY);

                    /*3*/
                    vertices[3 + num_verts] = new Vector3(1.0f + lx, z, 0.50f + 0.75f * (ly - 1));
                    uv[3 + num_verts] = new Vector2((1.0f + lx) / sizeX, (0.50f + 0.75f * (ly - 1)) / sizeY);

                    /*4*/
                    vertices[4 + num_verts] = new Vector3(0.5f + lx, z, 0.25f + 0.75f * (ly - 1));
                    uv[4 + num_verts] = new Vector2((0.5f + lx ) / sizeX, (0.25f + 0.75f * (ly - 1)) / sizeY);

                    /*5*/
                    vertices[5 + num_verts] = new Vector3(0.0f + lx, z, 0.50f + 0.75f * (ly - 1));
                    uv[5 + num_verts] = new Vector2((0.0f + lx) / sizeX, (0.50f + 0.75f * (ly - 1)) / sizeY);

                    /*6*/
                    vertices[6 + num_verts] = new Vector3(0.0f + lx, z, 1.00f + 0.75f * (ly - 1));
                    uv[6 + num_verts] = new Vector2((0.0f + lx) / sizeX, (1.00f + 0.75f * (ly - 1)) / sizeY);
                }

                #endregion

                #region Normals //equal Vertices count
                for (int i = 0; i < 7; i++)
                {
                    normals[i + num_verts] = Vector3.up;
                }
                #endregion

                #region Triangles

                for (int i = 0; i < 6; i++)
                {
                    triangles[0 + i * 3 + num_triangles] = 0 + num_verts;
                    triangles[1 + i * 3 + num_triangles] = 1 + i + num_verts;

                    if (i == 5)
                    {
                        triangles[2 + i * 3 + num_triangles] = -4 + i + num_verts;
                    }
                    else
                    {
                        triangles[2 + i * 3 + num_triangles] = 2 + i + num_verts;
                    }
                }

                #endregion

                num_verts += 7;     //Save count of Vertices after loop
                num_triangles += 18; //Save count of Triangles after loop
            }
        }
        //Create new Mesh and populated data

        __mesh.vertices = vertices;
        __mesh.normals = normals;
        //__mesh.RecalculateNormals();
        __mesh.uv = uv;
        __mesh.triangles = triangles;

        MeshFilter mesh_filter = GetComponent<MeshFilter>();
        mesh_filter.mesh = __mesh;
        mesh_filter.name = "Hex Map";
        mesh_filter.mesh.name = "Hex";
    }

    Vector3[] CalculateVert(float x, float y, float z, int __sizeX, int __sizeY)  //x,y - center of Hex
    {
        Vector3[] __vert = new Vector3[7];

        //generate the hexagons in the normal positioning for this row
        /*0*/__vert[0] = new Vector3( (0.50f + x) / __sizeX, z, 0.00f + y);
        /*1*/__vert[1] = new Vector3( (0.00f + x) / __sizeX, z, 0.25f + y);
        /*2*/__vert[2] = new Vector3( (0.00f + x) / __sizeX, z, 0.75f + y);
        /*3*/__vert[3] = new Vector3( (0.50f + x) / __sizeX, z, 1.00f + y);
        /*4*/__vert[4] = new Vector3( (1.00f + x) / __sizeX, z, 0.75f + y);
        /*5*/__vert[5] = new Vector3( (1.00f + x) / __sizeX, z, 0.25f + y);
        /*6*/__vert[6] = new Vector3( (0.50f + x) / __sizeX, z, 0.00f + y);

        return __vert; 
    }

    }
