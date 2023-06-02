using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class maybeMatrixFill : MonoBehaviour
{
	public Mesh wallMesh;
	public Material color;
	public Vector3 curRot;
	public int size;
	public int n;
	public int m;
	
	void Start()
    {
		int [,] mapForLab = new int[n,m];
		//GameObject.CreatePrimitive(PrimitiveType.Cube);
		//for ()
		var wall1 = new GameObject();
		wall1.name = "wall";
		wall1.AddComponent<MeshFilter>();
		wall1.AddComponent<MeshRenderer>();
		wall1.AddComponent<Renderer>();
		//wallMesh = wall1.GetComponent<MeshFilter>().mesh;
		wall1.GetComponent<MeshFilter>().mesh = wallMesh;
		wall1.GetComponent<Renderer>().material = color;
		wall1.transform.localScale *= size;
		wall1.transform.SetParent(transform);
		wall1.transform.localPosition = new Vector3(0,1,0);
		wall1.transform.eulerAngles = new Vector3(curRot.x % 360f, curRot.y % 360f, curRot.z % 360f) ;
		//var newWall = new GameObject("wall");
        //newWall.transform.parent = GameObject;
    }

    void Update()
    {
        
    }
}
