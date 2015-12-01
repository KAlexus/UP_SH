using UnityEngine;
using System.Collections;

public class MapGenerator : MonoBehaviour {
    public int _sizeX;
    public int _sizeY;
    void Start()
    {
        __MapGenerator(_sizeX, _sizeY);        
    }

    void __MapGenerator(int sizeX, int sizeY)
    {
        Mesh __hex;

        //__hex = SimpleHex.SimpleMesh();
        __hex = SimpleHex.HexMap(sizeX, sizeY, 0);

        MeshFilter  mesh_filter = GetComponent<MeshFilter>();
                    mesh_filter.mesh = __hex;
                    mesh_filter.name = "Hex Map";
                    mesh_filter.mesh.name = "Hex";   
    }
}
