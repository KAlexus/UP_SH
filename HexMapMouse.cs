using UnityEngine;
using System.Collections;

public class HexMapMouse : MonoBehaviour
{
    public Vector2 currentSelectedHex;
    public int hexSize;
    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            currentSelectedHex = PixelToHex(2);
            Debug.Log("HEX[" + currentSelectedHex.x + "," + currentSelectedHex.y + "]");
        }    
    }

    public Vector2 PixelToHex(int _size)
    {

        Vector2 hexNum = new Vector2(0, 0);

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        #region Calculate
        if (Physics.Raycast(ray, out hit, Mathf.Infinity) && Input.GetKeyDown(KeyCode.Mouse0)) //
        {

            MeshCollider meshCollider = hit.collider as MeshCollider;
            if (meshCollider == null || meshCollider.sharedMesh == null)
                return hexNum;

            Mesh mesh = meshCollider.sharedMesh;
            Vector3[] vertices = mesh.vertices;
            int[] triangles = mesh.triangles;
            int hitTriangle = (int)(hit.triangleIndex / 6);



            Vector3 p0 = vertices[hitTriangle * 6 + 0 + hitTriangle];
            Vector3 p1 = vertices[hitTriangle * 6 + 1 + hitTriangle];
            Vector3 p2 = vertices[hitTriangle * 6 + 2 + hitTriangle];
            Vector3 p3 = vertices[hitTriangle * 6 + 3 + hitTriangle];
            Vector3 p4 = vertices[hitTriangle * 6 + 4 + hitTriangle];
            Vector3 p5 = vertices[hitTriangle * 6 + 5 + hitTriangle];
            Vector3 p6 = vertices[hitTriangle * 6 + 6 + hitTriangle];

            //Transform hitTransform = hit.collider.transform;

            bool oddSize = ((_size + 2) % 2 == 0) ? true : false;

            if (oddSize)
            {
                //Debug.Log("oddSize(true)=" + oddSize);
                if ((Mathf.FloorToInt(p0.z / 0.75f) + 2) % 2 == 0)
                {
                    //Debug.Log("ODD ROW!!!");
                    hexNum = new Vector2(Mathf.FloorToInt(p0.z / 0.75f), Mathf.Floor(p0.x));
                }
                else
                {
                    //Debug.Log("NOT ODD ROW!!!");
                    hexNum = new Vector2(Mathf.FloorToInt(p0.z / 0.75f), Mathf.Floor(p0.x) - 1);
                }


            }
            else
            {
                //Debug.Log("oddSize(false)=" + oddSize);
                if ((Mathf.FloorToInt(p0.z / 0.75f) + 2) % 2 == 0)
                {
                    //Debug.Log("ODD ROW!!!");
                    hexNum = new Vector2(Mathf.FloorToInt(p0.z / 0.75f), Mathf.Floor(p0.x) - 1);
                }
                else
                {
                    //Debug.Log("NOT ODD ROW!!!");
                    hexNum = new Vector2(Mathf.FloorToInt(p0.z / 0.75f), Mathf.Floor(p0.x));
                }

            }           

            Debug.DrawLine(p1, p2, Color.cyan);
            Debug.DrawLine(p2, p3, Color.cyan);
            Debug.DrawLine(p3, p4, Color.cyan);
            Debug.DrawLine(p4, p5, Color.cyan);
            Debug.DrawLine(p5, p6, Color.cyan);
            Debug.DrawLine(p1, p6, Color.cyan);            
        }
        #endregion

        return hexNum;
    }
}
