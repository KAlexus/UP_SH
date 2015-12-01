using UnityEngine;
using System.Collections;

public class SimpleHex : MonoBehaviour {

	// Use this for initialization
	public static Mesh SimpleMesh()
    {
        
        Mesh __mesh = new Mesh();   // Создаем Mesh который мы будем передавать далее
        Vector3[] __vertices;       // Создаем переменную для хранения вершин
        int[] __triangles;          // Создаем переменную для хранения треугольников
        Vector2[] __uv;             // Создаем переменную для хранения UV
        Vector3[] __normals;

        #region verts
       
        //positions vertices of the hexagon to make a normal hexagon
        const float __floorLevel = 0;
        
        __vertices = new Vector3[]
                {
                    /*0*/new Vector3( 0.00f, __floorLevel,  0.00f),
                    /*1*/new Vector3( 0.00f, __floorLevel,  0.50f),
                    /*2*/new Vector3( 0.50f, __floorLevel,  0.25f),
                    /*3*/new Vector3( 0.50f, __floorLevel, -0.25f),
                    /*4*/new Vector3( 0.00f, __floorLevel, -0.50f),
                    /*5*/new Vector3(-0.50f, __floorLevel, -0.25f),
                    /*6*/new Vector3(-0.50f, __floorLevel,  0.25f)
                };
        #endregion
        
        #region Normals

        //positions vertices of the hexagon to make a normal hexagon      

        __normals = new Vector3[]
                {
                    /*0*/Vector3.up,
                    /*1*/Vector3.up,
                    /*2*/Vector3.up,
                    /*3*/Vector3.up,
                    /*4*/Vector3.up,
                    /*5*/Vector3.up,
                    /*6*/Vector3.up
                };
        #endregion
        #region triangles
        //triangles connecting the verts

        __triangles = new int[]
                {
                    0,1,2,
                    0,2,3,
                    0,3,4,
                    0,4,5,
                    0,5,6,
                    0,6,1
                };
        #endregion

        #region uv
        //uv mappping
        __uv = new Vector2[]
                {
                    /*0*/new Vector2( 0.00f,  0.00f),
                    /*1*/new Vector2( 0.00f,  0.50f),
                    /*2*/new Vector2( 0.50f,  0.25f),
                    /*3*/new Vector2( 0.50f, -0.25f),
                    /*4*/new Vector2( 0.00f, -0.50f),
                    /*5*/new Vector2(-0.50f, -0.25f),
                    /*6*/new Vector2(-0.50f,  0.25f)
                };
        #endregion

        //#region normals
        //for (int i=0; i<7; i++)
        //{
        //    __normals[i] = new Vector3.up();
        //}        
        //#endregion

        #region finalize               
        //assign verts
        __mesh.vertices = __vertices;
        //assign triangles
        __mesh.triangles = __triangles;
        //assign uv
        __mesh.uv = __uv;
        //asiggn normals
        __mesh.normals = __normals;

        __mesh.RecalculateNormals();
        //set temp gameObject's mesh to the flat hexagon mesh
        #endregion

        return __mesh;
    }

    public static Mesh HexMap(int x, int y, int z)
    {
        
        int numHex = x * y;
        int numVerts = 7 * numHex;
        int numTriangles = 3 * 6 * numHex;
        float _x = x;
        float _y = y -( x - 1.75f * y);

        Mesh __mesh = new Mesh();                   // Создаем Mesh который мы будем передавать далее

        Vector3[] vertices = new Vector3[numVerts]; // Создаем переменную для хранения вершин
        Vector3[] normals = new Vector3[numVerts];  // Создаем переменную для хранения нормалей
        Vector2[] uv = new Vector2[numVerts];       // Создаем переменную для хранения UV

        //Vector2[] uv2 = new Vector2[4];             // Создаем переменную для хранения UV

        int[] triangles = new int[numTriangles];    // Создаем переменную для хранения треугольников

        bool notOdd;
        int num_Hex = 0;
        int num_verts = 0;
        int num_triangles = 0;

        for (int ly = 0; ly < y; ly++)
        {
            for (int lx = 0; lx < x; lx++)
            {
                num_Hex++; //Count of сurrent HEX

                #region Vertices and UV
                //determine if we are in an odd row; if so we need to offset the hexagons
                float _dx = 0;  //Можно удалить, т.к. планировал избавиться от смещения UV на правом краю карты

                //_dx = (lx == 0) ? 0.5f : 0; //Смещение для первого гекса в каждом нечетном ряду

                notOdd = ((ly % 2) == 0);
                if (notOdd == true)
                {
                    //generate the hexagons in the normal positioning for this row
                    /*0*/
                    vertices[0 + num_verts] = new Vector3(0.0f + lx, z, 0.00f + 0.75f * ly);
                    uv[0 + num_verts] = new Vector2((0.0f + lx + _dx) / _x, (0.00f + 0.75f * ly) / _y);

                    /*1*/
                    vertices[1 + num_verts] = new Vector3(0.0f + lx, z, 0.50f + 0.75f * ly);
                    uv[1 + num_verts] = new Vector2((0.0f + lx + _dx) / _x, (0.50f + 0.75f * ly) / _y);

                    /*2*/
                    vertices[2 + num_verts] = new Vector3(0.5f + lx, z, 0.25f + 0.75f * ly);
                    uv[2 + num_verts] = new Vector2((0.5f + lx + _dx) / _x, (0.25f + 0.75f * ly) / _y);

                    /*3*/
                    vertices[3 + num_verts] = new Vector3(0.5f + lx, z, -0.25f + 0.75f * ly);
                    uv[3 + num_verts] = new Vector2((0.5f + lx + _dx) / _x, (-0.25f + 0.75f * ly) / _y);

                    /*4*/
                    vertices[4 + num_verts] = new Vector3(0.0f + lx, z, -0.50f + 0.75f * ly);
                    uv[4 + num_verts] = new Vector2((0.0f + lx + _dx) / _x, (-0.50f + 0.75f * ly) / _y);

                    /*5*/
                    vertices[5 + num_verts] = new Vector3(-0.5f + lx, z, -0.25f + 0.75f * ly);
                    uv[5 + num_verts] = new Vector2((-0.5f + lx + _dx) / _x, (-0.25f + 0.75f * ly) / _y);

                    /*6*/
                    vertices[6 + num_verts] = new Vector3(-0.5f + lx, z, 0.25f + 0.75f * ly);
                    uv[6 + num_verts] = new Vector2((-0.5f + lx + _dx) / _x, (0.25f + 0.75f * ly) / _y);
                }
                else
                {
                    //generate the hexagons in the offset positioning for this row
                    /*0*/
                    vertices[0 + num_verts] = new Vector3(0.5f + lx, z, 0.75f + 0.75f * (ly - 1));
                    uv[0 + num_verts] = new Vector2((0.5f + lx - _dx) / _x, (0.75f + 0.75f * (ly - 1)) / _y);

                    /*1*/
                    vertices[1 + num_verts] = new Vector3(0.5f + lx, z, 1.25f + 0.75f * (ly - 1));
                    uv[1 + num_verts] = new Vector2((0.5f + lx - _dx) / _x, (1.25f + 0.75f * (ly - 1)) / _y);

                    /*2*/
                    vertices[2 + num_verts] = new Vector3(1.0f + lx, z, 1.00f + 0.75f * (ly - 1));
                    uv[2 + num_verts] = new Vector2((1.0f + lx - _dx) / _x, (1.00f + 0.75f * (ly - 1)) / _y);

                    /*3*/
                    vertices[3 + num_verts] = new Vector3(1.0f + lx, z, 0.50f + 0.75f * (ly - 1));
                    uv[3 + num_verts] = new Vector2((1.0f + lx - _dx) / _x, (0.50f + 0.75f * (ly - 1)) / _y);

                    /*4*/
                    vertices[4 + num_verts] = new Vector3(0.5f + lx, z, 0.25f + 0.75f * (ly - 1));
                    uv[4 + num_verts] = new Vector2((0.5f + lx - _dx) / _x, (0.25f + 0.75f * (ly - 1)) / _y);

                    /*5*/
                    vertices[5 + num_verts] = new Vector3(0.0f + lx, z, 0.50f + 0.75f * (ly - 1));
                    uv[5 + num_verts] = new Vector2((0.0f + lx - _dx) / _x, (0.50f + 0.75f * (ly - 1)) / _y);

                    /*6*/
                    vertices[6 + num_verts] = new Vector3(0.0f + lx, z, 1.00f + 0.75f * (ly - 1));
                    uv[6 + num_verts] = new Vector2((0.0f + lx - _dx) / _x, (1.00f + 0.75f * (ly - 1)) / _y);
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
        //uv2[0] = new Vector2(0, 0);
        //uv2[1] = new Vector2(0, 0.75f * y);
        //uv2[2] = new Vector2(x, 0);
        //uv2[3] = new Vector2(x, y);

        __mesh.uv = uv;
        __mesh.triangles = triangles;

        return __mesh;
    }
}
