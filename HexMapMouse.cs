using UnityEngine;
using System.Collections;

public class HexMapMouse : MonoBehaviour {
    public Vector2 currentSelectedHex;
    public int hexSize;
    // Use this for initialization
    void Start () {

        hexSize = 2;

    }
	
	// Update is called once per frame
	void Update () {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity) && Input.GetKeyDown(KeyCode.Mouse0)) //
        {
            
            MeshCollider meshCollider = hit.collider as MeshCollider;
            if (meshCollider == null || meshCollider.sharedMesh == null)
                return;

            Mesh mesh = meshCollider.sharedMesh;
            Vector3[] vertices = mesh.vertices;
            int[] triangles = mesh.triangles;
            int hitTriangle = (int)(hit.triangleIndex/6);

            //Debug.Log("hitTriangle" + hitTriangle);

            Vector3 p0 = vertices[hitTriangle*6 + 0+ hitTriangle];
            Vector3 p1 = vertices[hitTriangle*6 + 1 + hitTriangle];
            Vector3 p2 = vertices[hitTriangle*6 + 2 + hitTriangle];
            Vector3 p3 = vertices[hitTriangle*6 + 3 + hitTriangle];
            Vector3 p4 = vertices[hitTriangle*6 + 4 + hitTriangle];
            Vector3 p5 = vertices[hitTriangle*6 + 5 + hitTriangle];
            Vector3 p6 = vertices[hitTriangle*6 + 6 + hitTriangle];
            //Debug.Log("Point 0 - " + (hitTriangle * 6 + 0));
            //Debug.Log("Point 1 - " + (hitTriangle*6 + 1) + ", Point 2 - " + (hitTriangle*6 + 2) + ", Point 3 - " + (hitTriangle*6 + 3));
            //Debug.Log("Point 4 - " + (hitTriangle * 6 + 4) + ", Point 5 - " + (hitTriangle * 6 + 5) + ", Point 6 - " + (hitTriangle * 6 + 6));
            Transform hitTransform = hit.collider.transform;

            bool oddSize = ((hexSize + 2) % 2==0) ? true : false;
            
            if (oddSize)
            {
                //Debug.Log("oddSize(true)=" + oddSize);
                if ((Mathf.FloorToInt(p0.z / 0.75f) + 2) % 2 == 0)
                {
                    //Debug.Log("ODD ROW!!!");
                    currentSelectedHex = new Vector2(Mathf.FloorToInt(p0.z / 0.75f), Mathf.Floor(p0.x));
                }
                else
                {
                    //Debug.Log("NOT ODD ROW!!!");
                    currentSelectedHex = new Vector2(Mathf.FloorToInt(p0.z / 0.75f), Mathf.Floor(p0.x) - 1);
                }
                    

            }
            else
            {
                //Debug.Log("oddSize(false)=" + oddSize);
                if ((Mathf.FloorToInt(p0.z / 0.75f) + 2) % 2 == 0)
                {
                    //Debug.Log("ODD ROW!!!");
                    currentSelectedHex = new Vector2(Mathf.FloorToInt(p0.z / 0.75f), Mathf.Floor(p0.x) - 1);
                }
                else
                {
                    //Debug.Log("NOT ODD ROW!!!");
                    currentSelectedHex = new Vector2(Mathf.FloorToInt(p0.z / 0.75f), Mathf.Floor(p0.x));
                }
                
            }
            
            Debug.Log("HEX[" + currentSelectedHex.x + "," + currentSelectedHex.y + "]");

            Debug.DrawLine(p1, p2);
            Debug.DrawLine(p2, p3);
            Debug.DrawLine(p3, p4);
            Debug.DrawLine(p4, p5);
            Debug.DrawLine(p5, p6);
            Debug.DrawLine(p1, p6);
        }
    }

    Vector2 _PixelToHex(Vector3 _mousePosition, int _size)
    {
        int _col, _row;
        int _col1, _row1;
        int _col2, _row2;

        float _mouseX, _mouseY;
        Vector2 _hexNum = new Vector2(0,0);
        
        _mouseX = _mousePosition.x;
        _mouseY = _mousePosition.z;

        Debug.Log("current position (x,y): " + _mouseX + "," + _mouseY);

        
        //_row = (Mathf.FloorToInt(_mouseY / 0.75f) > _size) ? _size : Mathf.FloorToInt(_mouseY / 0.75f);

        //if ((_row + 2) % 2 == 0) Debug.Log("(_row + 2) % 2 == 0");
        //if ((_row + 2) % 2 != 0) Debug.Log("(_row + 2) % 2 != 0");

        //if (_size % 2 == 0) Debug.Log("_size % 2 == 0");
        //if (_size % 2 != 0) Debug.Log("_size % 2 != 0");
        /////<summary>
        /////     ______
        /////    /******\
        /////   /**/  \**\
        /////  |**|    |**|
        /////          /**|
        /////         /**/
        /////        /**/
        /////       |**|
        /////        __
        /////       |**|
        ///// 
        ///// </summary>
        //_col = ((_row + 2) % 2 == 0 && _size%2==0) ? Mathf.CeilToInt(_mouseX) : Mathf.FloorToInt(_mouseX);
        //_col = ((_row + 2) % 2 == 0 && _size % 2 != 0) ? Mathf.FloorToInt(_mouseX) : Mathf.RoundToInt(_mouseX);

        //_col = (_col > _size) ? _size : _col;
        //_col = (_col < 0) ? 0 : _col;

        //_col1 = (_col-1 < 0) ? 0 : _col-1;
        //_col2 = (_col+1 >= _size) ? _col : _col+1;

        //_row1 = (_row-1 < 0) ? 0 : _row-1;
        //_row2 = (_row+1 >= _size) ? _row : _row+1;

        //Debug.Log("Hex[" + _row + "," + _col + "]: _row1=" + _row1 + ", _row2=" + _row2 + "; _col1=" + _col1 + "_col2=" + _col2);
        
        //for (int y= _row1; y <= _row2; y++)
        //{
        //    for (int x=_col1; x<= _col2; x++)
        //    {
        //       // Debug.Log("Searh into HEX[" + y + "," + x + "]...");
        //    }
        //}
        //_hexNum = new Vector2(_row, _col);
        return _hexNum;
    }
}
