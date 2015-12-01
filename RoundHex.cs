using UnityEngine;
using System.Collections;

public class RoundHex : MonoBehaviour
{
    public int HexSize;
    public int sizeX;
    public int sizeY;
    
    void Start()
    {
        sizeX = HexSize * 2 + 1;                     // Необходимо для вычисления правильного наложения UV, при конвертации
        sizeY = Mathf.CeilToInt(sizeX * 0.75f);      // квадратной текстуры в гексагональный вид. ВЫСОТА - 0,75 х ШИРИНЫ

        HexMap(HexSize, 0);
        Camera camera = Camera.main;
        camera.transform.position = new Vector3(HexSize, 10, HexSize * 0.75f); // Устанавливаем камеру по центру гексагональной сетки

    }

    void HexMap(int size, int z)
    {

        int numHex = size * 2 + 1;                  // Вычисляем кол-во гексов,
        for (int i=0; i < size; i++)                // где size - диаметр гексагональной карты.
        {
            numHex += 2 * (size + 1 + i);           // для  size=1: numHex=1+6; для  size=2: numHex=1+6+12; для  size=3: numHex=1+6+12+18;
        }
        Debug.Log(numHex);

        int numVerts = 7 * numHex;                  // Вычисляем кол-во вершин, для каждого гекса = 7
        int numTriangles = 3 * 6 * numHex;          // Вычисляем кол-во вершин треугольников для каждого треугольника в каждом гексе. 


        Mesh __mesh = new Mesh();                   // Создаем Mesh который мы будем передавать далее

        Vector3[] vertices = new Vector3[numVerts]; // Создаем переменную для хранения вершин
        Vector3[] normals = new Vector3[numVerts];  // Создаем переменную для хранения нормалей
        Vector2[] uv = new Vector2[numVerts];       // Создаем переменную для хранения UV
        int[] triangles = new int[numTriangles];    // Создаем переменную для хранения треугольников

        int num_Hex = 0;
        int num_verts = 0;
        int num_triangles = 0;
        bool notOdd;

        for (int ly=0; ly < size * 2 + 1; ly++)         // Запускаем цикл постройки гексагональной карты по Y-Высоте
        {
            int _dx = Mathf.Abs(size - ly) / 2; ;       // координаты центра первого гекса в ряду
            int _dy = size * 2 - (ly + 3) % 2 - _dx;    // координаты центра последнего гекса в ряду (для нечетных значений size)

            if (size % 2 == 0)                          // Если размер кратный 2 ( 1 - центральный ряд - 1)
            {               
                _dy = size * 2 - (ly + 2) % 2 - _dx;    // Вычисляем координаты центра последнего гекса вряду (тек.ряд+2)
            }

            for (int lx=_dx; lx < _dy + 1; lx++)        //запускаем цикл постройки по X-Ширине
            {
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
                    //            /*0*/new Vector3( 0.50f, z,  0.50f),         1
                    //            /*1*/new Vector3( 0.50f, z,  1.00f),       /   \
                    //            /*2*/new Vector3( 1.00f, z,  0.75f),      6     2
                    //            /*3*/new Vector3( 1.00f, z,  0.25f),      |  0  |
                    //            /*4*/new Vector3( 0.50f, z,  0.00f),      5     3
                    //            /*5*/new Vector3( 0.00f, z,  0.25f),       \   /
                    //            /*6*/new Vector3( 0.00f, z,  0.75f)          4    
                            
                    //generate the hexagons in the normal positioning for this row
                    /*0*/
                    vertices[0 + num_verts] =   new Vector3( 0.50f + lx,            z,  0.50f + 0.75f * ly);
                    uv[0 + num_verts] =         new Vector2((0.50f + lx) / sizeX,      (0.50f + 0.75f * ly) / sizeY);

                    /*1*/
                    vertices[1 + num_verts] =   new Vector3( 0.50f + lx,            z,  1.00f + 0.75f * ly);
                    uv[1 + num_verts] =         new Vector2((0.50f + lx) / sizeX,      (1.00f + 0.75f * ly) / sizeY);

                    /*2*/
                    vertices[2 + num_verts] =   new Vector3( 1.00f + lx,            z,  0.75f + 0.75f * ly);
                    uv[2 + num_verts] =         new Vector2((1.00f + lx) / sizeX,      (0.75f + 0.75f * ly) / sizeY);

                    /*3*/
                    vertices[3 + num_verts] =   new Vector3( 1.00f + lx,            z,  0.25f + 0.75f * ly);
                    uv[3 + num_verts] =         new Vector2((1.00f + lx) / sizeX,      (0.25f + 0.75f * ly) / sizeY);

                    /*4*/
                    vertices[4 + num_verts] =   new Vector3( 0.50f + lx,            z,  0.00f + 0.75f * ly);
                    uv[4 + num_verts] =         new Vector2((0.50f + lx) / sizeX,      (0.00f + 0.75f * ly) / sizeY);

                    /*5*/
                    vertices[5 + num_verts] =   new Vector3( 0.00f + lx,            z,  0.25f + 0.75f * ly);
                    uv[5 + num_verts] =         new Vector2((0.00f + lx ) / sizeX,     (0.25f + 0.75f * ly) / sizeY);

                    /*6*/
                    vertices[6 + num_verts] =   new Vector3( 0.00f + lx,            z,  0.75f + 0.75f * ly);
                    uv[6 + num_verts] =         new Vector2((0.00f + lx) / sizeX,      (0.75f + 0.75f * ly) / sizeY);
                }
                else
                {

                    //generate the hexagons in the offset positioning for this row
                    /*0*/
                    vertices[0 + num_verts] =   new Vector3( 1.00f + lx,            z,  1.25f + 0.75f * (ly - 1));
                    uv[0 + num_verts] =         new Vector2((1.00f + lx ) / sizeX,     (1.25f + 0.75f * (ly - 1)) / sizeY);

                    /*1*/
                    vertices[1 + num_verts] =   new Vector3( 1.00f + lx,            z,  1.75f + 0.75f * (ly - 1));
                    uv[1 + num_verts] =         new Vector2((1.00f + lx) / sizeX,      (1.75f + 0.75f * (ly - 1)) / sizeY);

                    /*2*/
                    vertices[2 + num_verts] =   new Vector3( 1.50f + lx,            z,  1.50f + 0.75f * (ly - 1));
                    uv[2 + num_verts] =         new Vector2((1.50f + lx) / sizeX,      (1.50f + 0.75f * (ly - 1)) / sizeY);

                    /*3*/
                    vertices[3 + num_verts] =   new Vector3( 1.50f + lx,            z,  1.00f + 0.75f * (ly - 1));
                    uv[3 + num_verts] =         new Vector2((1.50f + lx) / sizeX,      (1.00f + 0.75f * (ly - 1)) / sizeY);

                    /*4*/
                    vertices[4 + num_verts] =   new Vector3( 1.00f + lx,            z,  0.75f + 0.75f * (ly - 1));
                    uv[4 + num_verts] =         new Vector2((1.00f + lx ) / sizeX,     (0.75f + 0.75f * (ly - 1)) / sizeY);

                    /*5*/
                    vertices[5 + num_verts] =   new Vector3( 0.50f + lx,            z,  1.00f + 0.75f * (ly - 1));
                    uv[5 + num_verts] =         new Vector2((0.50f + lx) / sizeX,      (1.00f + 0.75f * (ly - 1)) / sizeY);

                    /*6*/
                    vertices[6 + num_verts] =   new Vector3( 0.50f + lx,            z,  1.50f + 0.75f * (ly - 1));
                    uv[6 + num_verts] =         new Vector2((0.50f + lx) / sizeX,      (1.50f + 0.75f * (ly - 1)) / sizeY);
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

                num_verts += 7;         //Save count of Vertices after loop
                num_triangles += 18;    //Save count of Triangles after loop
            }
        }
        
        //Create new Mesh and populated data
        __mesh.vertices     = vertices;
        __mesh.normals      = normals;
        __mesh.uv           = uv;
        __mesh.triangles    = triangles;

        MeshFilter mesh_filter = GetComponent<MeshFilter>();
        mesh_filter.mesh = __mesh;
        mesh_filter.name = "Hex Map";
        mesh_filter.mesh.name = "Hex";
    }

    //Vector3[] CalculateVert(float x, float y, float z, int __sizeX, int __sizeY)  //x,y - center of Hex
    //{
    //    Vector3[] __vert = new Vector3[7];

    //    //generate the hexagons in the normal positioning for this row
    //    /*0*/__vert[0] = new Vector3( (0.50f + x) / __sizeX, z, 0.00f + y);
    //    /*1*/__vert[1] = new Vector3( (0.00f + x) / __sizeX, z, 0.25f + y);
    //    /*2*/__vert[2] = new Vector3( (0.00f + x) / __sizeX, z, 0.75f + y);
    //    /*3*/__vert[3] = new Vector3( (0.50f + x) / __sizeX, z, 1.00f + y);
    //    /*4*/__vert[4] = new Vector3( (1.00f + x) / __sizeX, z, 0.75f + y);
    //    /*5*/__vert[5] = new Vector3( (1.00f + x) / __sizeX, z, 0.25f + y);
    //    /*6*/__vert[6] = new Vector3( (0.50f + x) / __sizeX, z, 0.00f + y);

    //    return __vert; 
    //}

    }
