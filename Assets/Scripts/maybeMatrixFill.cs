using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class maybeMatrixFill : MonoBehaviour
{
	public Mesh wallMesh;
	public Material color;
	public Vector3 curRot;
	public float rotateCoef;
	public int size;
	public int radius;
	public int n;
	public int m;
	
	void generateLab (int [,] map) {
		int i = 1;
		int j = 1;
		map[i,j] = 0;
		List<int> listForCheckI = new List <int>();
		List<int> listForCheckJ = new List <int>();
		listForCheckI.Add(i);
		listForCheckJ.Add(j + 2);
		map[i, j + 2] = 0;
		listForCheckI.Add(i + 2);
		listForCheckJ.Add(j);
		map[i + 2, j] = 0;
		while (listForCheckI.Count > 0) {
			i = listForCheckI[0];
			j = listForCheckJ[0];
			listForCheckI.RemoveAt(0);
			listForCheckJ.RemoveAt(0);
			List<int> dir = new List <int>();
			dir.Add(1);
			dir.Add(2);
			dir.Add(3);
			dir.Add(4);
			int ch = 4;
			while (dir.Count > 0) {
				int dInd = Random.Range(0,dir.Count);
				Debug.Log("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!" + dir[dInd].ToString());
				if (dir[dInd] == 1) {
					if (j - 2 >= 0 && map[i,j - 2] == 0 && map[i, j - 1] > 0) {
						map[i, j - 1] = 0;
						dir.Clear();
						ch = 0;
					}
				}
				
				else if (dir[dInd] == 2) {
					if (j + 2 < m && map[i,j + 2] == 0 && map[i, j + 1] > 0) {
						map[i, j + 1] = 0;
						dir.Clear();
						ch = 0;
					}
				}
				
				else if (dir[dInd] == 3) {
					if (i - 2 >= 0 && map[i - 2, j] == 0 && map[i - 1, j] > 0) {
						map[i - 1, j] = 0;
						dir.Clear();
						ch = 0;
					}
				}
				
				else if (dir[dInd] == 4) {
					if (i + 2 <n && map[i + 2, j] == 0 && map[i + 1, j] > 0) {
						map[i + 1, j] = 0;
						dir.Clear();
						ch = 0;
					}
				}
				if(ch > 0) {
					dir.RemoveAt(dInd);
				}
				else {
					break;
				}	
			}
			if (j - 2 >= 0 && map[i, j - 2] > 0) {
				listForCheckI.Add(i);
				listForCheckJ.Add(j - 2);
				map[i, j - 2] = 0;
			}
			if (j + 2 < m && map[i, j + 2] > 0) {
				listForCheckI.Add(i);
				listForCheckJ.Add(j + 2);
				map[i, j + 2] = 0;
			}
			if (i - 2 >= 0 && map[i - 2, j] > 0) {
				listForCheckI.Add(i - 2);
				listForCheckJ.Add(j);
				map[i - 2, j] = 0;
			}
			if (i + 2 < n && map[i + 2, j] > 0) {
				listForCheckI.Add(i + 2);
				listForCheckJ.Add(j);
				map[i + 2, j] = 0;
			}
		}
		for (int ii = 0; i < n; i++) {
			for (int jj = 0; j < m; j++) {
				Debug.Log(map[ii,jj]);
			}
		}
		
		
	}
	
	void Start()
    {
		int [,] mapForLab = new int[n,m];
		for (int i = 0; i < n; i++) {
			for (int j = 0; j < m; j++) {
				mapForLab[i,j] = 1;
			}
		}
		//generateLab(mapForLab);
		float dist = 0.25f;
		//GameObject.CreatePrimitive(PrimitiveType.Cube);
		for (int i = 0; i < n; i++) {
			for (int j = 0; j < m; j++) {
				int ind = i * j;
				//Debug.Log(ind);
				var wall = new GameObject();
				wall.name = "wall" + i.ToString() + j.ToString();
				wall.AddComponent<MeshFilter>();
				wall.AddComponent<MeshRenderer>();
				wall.AddComponent<Renderer>();
				wall.AddComponent<MeshCollider>();
				
				//wall.AddComponent<Grabbable>();
				
				if (mapForLab[i,j] == 0) {
					wall.SetActive(false);
				}
				wall.GetComponent<MeshFilter>().mesh = wallMesh;
				wall.GetComponent<MeshCollider>().sharedMesh = wallMesh;
				wall.GetComponent<MeshCollider>().convex = true;
				wall.GetComponent<Renderer>().material = color;
				wall.transform.localScale *= size;
				wall.transform.SetParent(transform);
				
				Debug.Log("!!!!!!!!!!!!!!!!!!!!" + i.ToString() + "    " + j.ToString());
				if (rotateCoef*i <= 90) {
					float angle = rotateCoef*i * Mathf.Deg2Rad;
					Debug.Log(rotateCoef*i);
					Debug.Log(angle);
					Debug.Log(Mathf.Sin(angle));
					wall.transform.localPosition = new Vector3(dist * i, radius * Mathf.Sin(angle),-(dist * 2) * j);
				}
				else {
					float angle = (180 - rotateCoef*i) * Mathf.Deg2Rad;
					Debug.Log(180 - rotateCoef*i);
					Debug.Log(angle);
					Debug.Log(radius * Mathf.Cos(angle));
					wall.transform.localPosition = new Vector3(dist * (180 - i), radius *(1 + Mathf.Cos(angle)),-(dist * 2) * j);
				}
				wall.transform.eulerAngles = new Vector3(curRot.x % 360f, curRot.y % 360f, curRot.z % 360f);
				//wall.transform.eulerAngles = new Vector3(curRot.x % 360f, (rotateCoef * i) % 360f, curRot.z % 360f);
				//Debug.Log((rotateCoef * i) % 360f);
			}
		}
    }

    void Update()
    {
        
    }
}
