using UnityEngine;
using System.Collections;

public class RoundHex : MonoBehaviour
{
    public int hexSize;         //Диаметр гексагональной карты
    public float floorLevel;    //Уровень плоскости гексагональной карты (Y-coordinates in Unity)
    [HideInInspector]
    public int sizeX;
    [HideInInspector]
    public int sizeY;
    [HideInInspector]
    public Mesh __mesh;         // Создаем Mesh который мы будем передавать далее
    private Vector3[] vertices; // Создаем переменную для хранения вершин
    private Vector3[] normals;  // Создаем переменную для хранения нормалей
    private Vector2[] uv;       // Создаем переменную для хранения UV
    private int[] triangles;    // Создаем переменную для хранения треугольников

    void Start()
    {
        HexMap(hexSize, floorLevel);
        Camera camera = Camera.main;
        camera.transform.position = new Vector3(hexSize, 10, hexSize * 0.75f); // Устанавливаем камеру по центру гексагональной сетки
        camera.orthographicSize = (hexSize > 5) ? (int) (hexSize*1.1) : 5;
    }

    void HexMap(int size, float floor)
    {
        sizeX = size * 2 + 1;                       // Необходимо для вычисления правильного наложения UV, при конвертации
        sizeY = Mathf.CeilToInt(sizeX * 0.75f);     // квадратной текстуры в гексагональный вид. ВЫСОТА - 0,75 х ШИРИНЫ

        int numHex = size * 2 + 1;                  // Вычисляем кол-во гексов,
        for (int i=0; i < size; i++)                // где size - диаметр гексагональной карты.
        {
            numHex += 2 * (size + 1 + i);           // для  size=1: numHex=1+6; для  size=2: numHex=1+6+12; для  size=3: numHex=1+6+12+18;
        }

        int numVerts = 7 * numHex;                  // Вычисляем кол-во вершин, для каждого гекса = 7
        int numTriangles = 3 * 6 * numHex;          // Вычисляем кол-во вершин треугольников для каждого треугольника в каждом гексе. 

        vertices = new Vector3[numVerts];           // Записываем в переменную количество вершин
        normals = new Vector3[numVerts];            // Записываем в переменную количество нормалей
        uv = new Vector2[numVerts];                 // Записываем в переменную количество UV
        triangles = new int[numTriangles];          // Записываем в переменную количество треугольников
        
        int num_Hex = 0;
        int num_verts = 0;
        int num_triangles = 0;
        bool notOdd;

        for (int ly=0; ly < size * 2 + 1; ly++)     // Запускаем цикл постройки гексагональной карты по Y-Высоте
        {
            // Координаты центра первого и последнего гекса в ряду
            int _hexFirst = Mathf.Abs(size - ly) / 2;   
            int _hexLast = (size % 2 == 0) ? size * 2 - (ly + 2) % 2 - _hexFirst : size * 2 - (ly + 3) % 2 - _hexFirst; 

            for (int lx= _hexFirst; lx < _hexLast + 1; lx++)    //запускаем цикл постройки по X-Ширине
            {
                num_Hex++; //Count of сurrent HEX

                #region Vertices, Normals and UV                

                notOdd = ((ly % 2) == 0);   //determine if we are in an odd row; if so we need to offset the hexagons

                if (size % 2 != 0)          //Если размерность не кратна 2, порядок рядов меняется на противоположный
                {
                    notOdd = !notOdd;
                }

                float _dx, _dy; //Для расчета смещения гекса в зависимости от четности ряда

                if (notOdd == true) { _dx = lx; _dy = 0.75f * ly; }
                else { _dx = 0.50f + lx; _dy = 0.75f + 0.75f * (ly-1); }

                Vector3[] _vertices = CalculateVert(_dx, _dy, floor);
                Vector2[] _uv = CalculateUV(_dx, _dy, floor, sizeX, sizeY);

                // TEST
                Vector3[] _verticesTemp = _vertices;
                Vector2[] _uvTemp = _uv;

                if (lx == _hexFirst)
                {
                    for (int i = 0; i < 7; i++)
                    {
                        vertices[i + num_verts] = _vertices[i];
                        uv[i + num_verts] = _uv[i];
                        normals[i + num_verts] = Vector3.up;
                        if (i < 6)
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
                    }
                    num_verts += 7;         //Save count of Vertices after loop current row
                    num_triangles += 18;    //Save count of Triangles after loop current row
                }
                else
                {
                    for (int i=0; i < 5; i++)
                    {
                        vertices[i + num_verts] = _vertices[i];
                        uv[i + num_verts] = _uv[i];
                        normals[i + num_verts] = Vector3.up;
                    }
                    for (int i = 0; i < 6; i++)
                    {                         
                        switch (i)
                        {
                            case 0:
                            case 1:
                            case 2:
                                triangles[0 + i * 3 + num_triangles] = 0 + num_verts;
                                triangles[1 + i * 3 + num_triangles] = 1 + i + num_verts;
                                triangles[2 + i * 3 + num_triangles] = 2 + i + num_verts;
                                break;
                            case 3:
                                triangles[0 + i * 3 + num_triangles] = 0 + num_verts;
                                triangles[1 + i * 3 + num_triangles] = 1 + i + num_verts;
                                if (lx>_hexFirst+1)
                                {
                                    triangles[2 + i * 3 + num_triangles] = 2 + i + num_verts-2;
                                }
                                else triangles[2 + i * 3 + num_triangles] = 2 + i + num_verts-4;
                                break;
                            case 4:
                                triangles[0 + i * 3 + num_triangles] = 0 + num_verts;
                                
                                if (lx > _hexFirst + 1)
                                {
                                    triangles[1 + i * 3 + num_triangles] = 1 + i + num_verts - 2;
                                    triangles[2 + i * 3 + num_triangles] = 2 + i + num_verts - 3;
                                }
                                else
                                {                                    
                                    triangles[1 + i * 3 + num_triangles] = 1 + i + num_verts - 4;
                                    triangles[2 + i * 3 + num_triangles] = 2 + i + num_verts - 5;

                                }
                                break;
                            case 5:
                                triangles[0 + i * 3 + num_triangles] = 0 + num_verts;
                                triangles[2 + i * 3 + num_triangles] = 2 + i + num_verts;
                                if (lx > _hexFirst + 1)
                                {
                                    triangles[1 + i * 3 + num_triangles] = 1 + i + num_verts - 3;
                                }
                                else triangles[1 + i * 3 + num_triangles] = 1 + i + num_verts - 5;
                                break;


                        }

                    }
                    num_verts += 5;         //Save count of Vertices after loop current row
                    num_triangles += 18;    //Save count of Triangles after loop current row
                }
                    // TEST
//CODE
                //    for (int i = 0; i < 7; i++)
                //{
                //    vertices[i + num_verts] = _vertices[i];
                //    uv[i + num_verts] = _uv[i];
                //    normals[i + num_verts] = Vector3.up;
//CODE

// CODE
                //    if (i < 6)
                //    {
                //        triangles[0 + i * 3 + num_triangles] = 0 + num_verts;
                //        triangles[1 + i * 3 + num_triangles] = 1 + i + num_verts;

                //        if (i == 5)
                //        {
                //            triangles[2 + i * 3 + num_triangles] = -4 + i + num_verts;
                //        }
                //        else
                //        {
                //            triangles[2 + i * 3 + num_triangles] = 2 + i + num_verts;
                //        }
                //    }
                //}

                #endregion

                //num_verts += 7;         //Save count of Vertices after loop current row
                //num_triangles += 18;    //Save count of Triangles after loop current row
// CODE
            }
        }

        //Create new Mesh and populated data
        __mesh =  new Mesh();
        __mesh.vertices     = vertices;
        __mesh.normals      = normals;
        __mesh.uv           = uv;
        __mesh.triangles    = triangles;

        MeshFilter mesh_filter = GetComponent<MeshFilter>();
        mesh_filter.mesh = __mesh;
        mesh_filter.name = "Round Hex Map";
        mesh_filter.mesh.name = "Hex";
    }

    Vector3[] CalculateVert(float x, float y, float z)  //x,y - center of Hex
    {
        Vector3[] _vertices = new Vector3[7];

        /*0*/   _vertices[0] = new Vector3( (0.50f + x), z, (0.50f + y) );      //
        /*1*/   _vertices[1] = new Vector3( (0.50f + x), z, (1.00f + y) );      //    / 1 \
        /*2*/   _vertices[2] = new Vector3( (1.00f + x), z, (0.75f + y) );      //  6       2
        /*3*/   _vertices[3] = new Vector3( (1.00f + x), z, (0.25f + y) );      //  |   0   |
        /*4*/   _vertices[4] = new Vector3( (0.50f + x), z, (0.00f + y) );      //  5       3
        /*5*/   _vertices[5] = new Vector3( (0.00f + x), z, (0.25f + y) );      //    \ 4 /
        /*6*/   _vertices[6] = new Vector3( (0.00f + x), z, (0.75f + y) );      //
    
        return _vertices; 
    }

    Vector2[] CalculateUV(float x, float y, float z, int _sizeX, int _sizeY)  //x,y - center of Hex
    {
        Vector2[] _uv = new Vector2[7];

        /*0*/   _uv[0] = new Vector2( (0.50f + x) / _sizeX, (0.50f + y) / _sizeY);
        /*1*/   _uv[1] = new Vector2( (0.50f + x) / _sizeX, (1.00f + y) / _sizeY);
        /*2*/   _uv[2] = new Vector2( (1.00f + x) / _sizeX, (0.75f + y) / _sizeY);
        /*3*/   _uv[3] = new Vector2( (1.00f + x) / _sizeX, (0.25f + y) / _sizeY);
        /*4*/   _uv[4] = new Vector2( (0.50f + x) / _sizeX, (0.00f + y) / _sizeY);
        /*5*/   _uv[5] = new Vector2( (0.00f + x) / _sizeX, (0.25f + y) / _sizeY);
        /*6*/   _uv[6] = new Vector2( (0.00f + x) / _sizeX, (0.75f + y) / _sizeY);

        return _uv; 
    }

}
