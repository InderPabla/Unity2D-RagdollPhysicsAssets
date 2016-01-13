using UnityEngine;
using System.Collections;

public class PolygonShader : MonoBehaviour {	
	bool down = false;
	Vector3 startPoint,endPoint;
	void Start () {
		int pointCount = 0;
		
		PolygonCollider2D pc2 = gameObject.GetComponent<PolygonCollider2D>();
		pointCount = pc2.GetTotalPointCount();
		
		MeshFilter mf = GetComponent<MeshFilter>();
		Mesh mesh = new Mesh();
		
		
		Vector2[] points = pc2.points;
		Vector3[] vertices = new Vector3[pointCount];
		
		for(int j=0; j<pointCount; j++){
			Vector2 actual = points[j];
			vertices[j] = new Vector3(actual.x, actual.y, 0);
		}
		
		Triangulator tr = new Triangulator(points);
		int [] triangles = tr.Triangulate();
		
		
		mesh.vertices = vertices;
		mesh.triangles = triangles;
		
		mf.mesh = mesh;
	}


	// Update is called once per frame
	void Update () 
	{
		if(Input.GetMouseButtonDown(0))
		{
			down = true;
			startPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		}

		if(Input.GetMouseButtonUp(0))
		{
			down = false;
			endPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

			Vector2 startPoint2D = new Vector2(startPoint.x, startPoint.y);
			Vector2 endPoint2D = new Vector2(endPoint.x, endPoint.y);


			Debug.Log("Start Point: "+startPoint2D);
			Debug.Log("End Point: "+endPoint2D);

			RaycastHit2D hit = Physics2D.Raycast(startPoint2D, endPoint2D-startPoint2D);
			if(hit.collider!=null)
			{
				Vector2 point1, point2;
				bool isPointIn1 = hit.collider.OverlapPoint(startPoint2D);
				bool isPointIn2 = hit.collider.OverlapPoint(endPoint2D);

				if(!isPointIn1 && !isPointIn2 && hit.collider.name.Equals("Slice"))
				{
					point1 = hit.point;
					hit = Physics2D.Raycast(endPoint2D, startPoint2D-endPoint2D);
					point2 = hit.point;


					Debug.Log("Point 1 : "+point1);
					Debug.Log("Point 1 : "+point2);

				}
			}



		}
	}
}
