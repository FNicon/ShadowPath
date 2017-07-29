using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LightRay : MonoBehaviour {
	//private EdgeCollider2D shadowCollider;
	private List<Vector2> newVertices;

	public float lightRadius;
	[Range(0,360)]
	public float lightAngle;
	public LayerMask obstacleMask;

	public float MeshResolution;
	public int edgeResolveIterations;
	public float edgeDistanceThreshold;

	public float maskCutAwayDistance = 0.1f;

	public MeshFilter viewMeshFilter;
	Mesh viewMesh;

	private List<EdgeCollider2D> shadowSurfaces = new List<EdgeCollider2D>();
	public float shadowSurfaceMinLength;
	public int hitEdgesCount;

	public float pingX;
	public float pingY;
	public float pingX1;
	public float pingY1;
	public float pingX2;
	public float pingY2;

	// Use this for initialization
	void Start () {
		newVertices = new List<Vector2> ();

		viewMesh = new Mesh ();
		viewMesh.name = "View Mesh";
		viewMeshFilter.mesh = viewMesh;
		//shadowCollider = GetComponent<EdgeCollider2D> ();
		//EdgeCollider2D ec = gameObject.AddComponent<EdgeCollider2D> () as EdgeCollider2D;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		DrawFieldOfLight ();
		//buildPath ();
	}
	public Vector3 DirectionFromAngle(float angleInDegrees, bool isGlobalAngle) {
		if (!isGlobalAngle) {
			angleInDegrees = angleInDegrees + transform.eulerAngles.y;
		}
		return (new Vector3(Mathf.Sin (angleInDegrees*Mathf.Deg2Rad),Mathf.Cos (angleInDegrees*Mathf.Deg2Rad),0));
	}
	void DrawFieldOfLight() {
		int stepCount = Mathf.RoundToInt (lightAngle * MeshResolution);
		float stepAngleSize = lightAngle / stepCount;
		List<Vector3> viewPoints = new List<Vector3>();
		viewCastInfo oldViewCast = new viewCastInfo ();
		List<viewCastInfo> hitEdges = new List<viewCastInfo> ();
		List<Vector3> hitEdgesNow = new List<Vector3> ();
		for (int i = 0; i<= stepCount; i++) {
			float angle = transform.eulerAngles.y - lightAngle/2 + stepAngleSize*i;
			//Debug.DrawLine (transform.position,transform.position + DirectionFromAngle (angle,true)*lightRadius,Color.red);
			viewCastInfo newViewCast = viewCast (angle);
			if (i > 0) {
				bool edgeDistanceThresholdExceeded = Mathf.Abs (oldViewCast.distance - newViewCast.distance) > edgeDistanceThreshold;
				if (oldViewCast.hit != newViewCast.hit || (oldViewCast.hit && newViewCast.hit && edgeDistanceThresholdExceeded)) {
					EdgeInfo edge = FindEdge (oldViewCast, newViewCast);
					if (edge.pointA != Vector3.zero) {
						viewPoints.Add (edge.pointA);
					}
					if (edge.pointB != Vector3.zero) {
						viewPoints.Add (edge.pointB);
					}
				}
			}
			viewPoints.Add (newViewCast.point);
			oldViewCast = newViewCast;
			if (newViewCast.hit) {
				hitEdgesNow.Add(newViewCast.point);
			}
		}
		int vertexCount = viewPoints.Count + 1;
		Vector3[] vertices = new Vector3[vertexCount];
		int[] triangles = new int[(vertexCount-2)*3];

		vertices [0] = Vector3.zero;
		for (int i = 0; i < vertexCount - 1; i++) {
			vertices[i+1] = transform.InverseTransformPoint(viewPoints [i]) + Vector3.forward * maskCutAwayDistance;
			if (i < vertexCount - 2) {
				triangles[i*3]=0;
				triangles[i*3+1] = i + 1;
				triangles[i*3+2] = i + 2;
			}
			pingX = vertices [i + 1].x;
			pingY = vertices [i + 1].y;
		}
		viewMesh.Clear ();
		viewMesh.vertices = vertices;
		viewMesh.triangles = triangles;
		viewMesh.RecalculateNormals ();

		constructShadowSurface (farEdge(hitEdgesNow));
	}
	EdgeInfo FindEdge(viewCastInfo minViewCast, viewCastInfo maxViewCast) {
		float minAngle = minViewCast.angle;
		float maxAngle = maxViewCast.angle;
		Vector3 minPoint = Vector3.zero;
		Vector3 maxPoint = Vector3.zero;
		for (int i = 0; i < edgeResolveIterations; i++) {
			float angle = (minAngle + maxAngle) / 2;
			viewCastInfo newViewCast = viewCast (angle);
			bool edgeDstThresholdExceeded = Mathf.Abs (minViewCast.distance - newViewCast.distance) > edgeDistanceThreshold;
			if (newViewCast.hit == minViewCast.hit && !edgeDstThresholdExceeded) {
				minAngle = angle;
				minPoint = newViewCast.point;
			} else {
				maxAngle = angle;
				maxPoint = newViewCast.point;
			}
		}	
		return new EdgeInfo (minPoint, maxPoint);
	}
	viewCastInfo viewCast(float globalAngle) {
		Vector3 direction = DirectionFromAngle (globalAngle, true);
		RaycastHit2D hit;
		hit = Physics2D.Raycast (transform.position,direction,lightRadius,obstacleMask);
		if (hit) {
			return (new viewCastInfo (true, hit.point, hit.distance, globalAngle));
		} else {
			return (new viewCastInfo(false,transform.position + direction * lightRadius ,lightRadius,globalAngle));
		}
	}
	public struct viewCastInfo {
		public bool hit;
		public Vector3 point;
		public float distance;
		public float angle;

		public viewCastInfo(bool _hit, Vector3 _point, float _distance, float _angle) {
			hit = _hit;
			point = _point;
			distance = _distance;
			angle = _angle;
		}
	}
	public struct EdgeInfo {
		public Vector3 pointA;
		public Vector3 pointB;
		
		public EdgeInfo(Vector3 _pointA, Vector3 _pointB) {
			pointA = _pointA;
			pointB = _pointB;
		}
	}
	/*void buildPath() {
		shadowCollider.Reset ();
		newVertices.Add (new Vector2 (-0.5f, -0.5f));
		newVertices.Add (new Vector2 (1f, 1f));
		shadowCollider.points = newVertices.ToArray();
	}*/

	float countDistance(Vector3 coordinate){
		float output,deltaX,deltaY;
		deltaX = coordinate.x - transform.position.x;
		deltaY = coordinate.y - transform.position.y;
		output = Mathf.Sqrt ((deltaX * deltaX) + (deltaY * deltaY));
		return output;
	}

	List<Vector3> farEdge(List<Vector3> inputList){
		List<Vector3> outputList = new List<Vector3> ();
		for (int i = 0; i < inputList.Count; i++) {
			bool surfaceMinLengthExceeded,closerThanBefore,closerThanAfter;

			if (i < inputList.Count - 1) {
				surfaceMinLengthExceeded = Mathf.Abs (countDistance (inputList [i]) - countDistance (inputList [i + 1])) > shadowSurfaceMinLength;
			} else {
				surfaceMinLengthExceeded = Mathf.Abs (countDistance (inputList [i]) - countDistance (inputList [0])) > shadowSurfaceMinLength;
			}

			/*
			if (i == 0) {
				closerThanBefore = countDistance (inputList [i]) - countDistance (inputList [inputList.Count - 1]) > -shadowSurfaceMinLength;
			} else {
				closerThanBefore = countDistance (inputList [i]) - countDistance (inputList [i - 1]) > -shadowSurfaceMinLength;
			}

			if (i == inputList.Count) {
				closerThanAfter = countDistance (inputList [i]) - countDistance (inputList [0]) > -shadowSurfaceMinLength;
			} else {
				closerThanAfter = countDistance (inputList [i]) - countDistance (inputList [i+1]) > -shadowSurfaceMinLength;
			}

			if (closerThanBefore) {
				pingX1 = 1;
			}
			if (closerThanAfter) {
				pingY1 = 1;
			}
			surfaceMinLengthExceeded = closerThanAfter || closerThanBefore;
			*/
			if (surfaceMinLengthExceeded) {
				/*
				outputList.Add(transform.InverseTransformPoint(inputList[i]));
				if (i == inputList.Count) {
					outputList.Add (transform.InverseTransformPoint (inputList [0]));
				} else {
					outputList.Add(transform.InverseTransformPoint(inputList[i+1]));
				}
				*/
				if (countDistance (inputList [i]) < countDistance (inputList [(i + 1) % inputList.Count])) {
					outputList.Add(transform.InverseTransformPoint(inputList[i]));
				} else {
					outputList.Add (transform.InverseTransformPoint (inputList [(i + 1) % inputList.Count]));
				}
			}
		}
		//pingX1 = outputList [0].x;
		//pingY1 = outputList [0].y;
		return outputList;
	}

	float realDistance (Vector3 coordinate){
		float output,deltaX,deltaY;
		deltaX = coordinate.x * transform.lossyScale.x;
		deltaY = coordinate.y * transform.lossyScale.y;
		output = Mathf.Sqrt ((deltaX * deltaX) + (deltaY * deltaY));
		return output;
	}

	void constructShadowSurface(List<Vector3> hitEdges){
		hitEdgesCount = hitEdges.Count;

		int existingSurfaces = shadowSurfaces.Count;
		int newSurfaces = 0;


		for (int i = 0; i < hitEdges.Count; i++) {
			if (newSurfaces + 1 > existingSurfaces) {
				shadowSurfaces.Add (new EdgeCollider2D ());
			}
			shadowSurfaces [newSurfaces] = gameObject.AddComponent<EdgeCollider2D> ();
			Vector2[] surfaceEdges = new Vector2[2];
			surfaceEdges [0] = new Vector2 (hitEdges [i].x, hitEdges [i].y);
			float edge2x, edge2y;
			/*
			edge2x = transform.position.x + lightRadius / hitEdges [i].distance * (hitEdges [i].point.x - transform.position.x);
			edge2y = transform.position.y + lightRadius / hitEdges [i].distance * (hitEdges [i].point.y - transform.position.y);
			*/
			edge2x = (lightRadius / realDistance(hitEdges[i])) * (hitEdges [i].x);
			edge2y = (lightRadius / realDistance(hitEdges[i])) * (hitEdges [i].y);
			surfaceEdges [1] = new Vector2 (edge2x, edge2y);
			shadowSurfaces [newSurfaces].points = surfaceEdges;
			newSurfaces++;
		}

		int deadcount = 0;

		EdgeCollider2D[] surfaces = GetComponents<EdgeCollider2D> ();
		foreach (EdgeCollider2D surface in surfaces) {
			if (deadcount < newSurfaces) {
				surface.points = shadowSurfaces [deadcount].points;
				deadcount++;
				pingX2 = surface.points [0].x;
				pingY2 = surface.points [0].y;
			} else {
				Destroy (surface);
			}
		}

		/*
		while (newSurfaces < shadowSurfaces.Count) {
			EdgeCollider2D destroyCandidate = shadowSurfaces [newSurfaces];
			shadowSurfaces.RemoveAt (newSurfaces);
			Destroy (destroyCandidate);
			gameObject.
		}
		*/



		/*
		int existingSurfaces = shadowSurfaces.Count;
		int newSurfaces = 0;
		for (int i = 0; i < hitEdges.Count; i++) {
			bool surfaceMinLengthExceeded;
			if (i < hitEdges.Count - 1) {
				surfaceMinLengthExceeded = Mathf.Abs (hitEdges [i].distance - hitEdges [i + 1].distance) > shadowSurfaceMinLength;
			} else {
				surfaceMinLengthExceeded = Mathf.Abs (hitEdges [i].distance - hitEdges [1].distance) > shadowSurfaceMinLength;
			}
			if (surfaceMinLengthExceeded) {
				
				newSurfaces++;
				if (newSurfaces > existingSurfaces) {
					//shadowSurfaces.Add (new EdgeCollider2D ());
				}
				shadowSurfaces [newSurfaces-1] = gameObject.AddComponent<EdgeCollider2D> ();
				Vector2[] surfaceEdges = new Vector2[2];
				surfaceEdges [0] = new Vector2 (hitEdges [i].point.x, hitEdges [i].point.y);
				float edge2x, edge2y;
				edge2x = transform.position.x + lightRadius / hitEdges [i].distance * (hitEdges [i].point.x - transform.position.x);
				edge2y = transform.position.y + lightRadius / hitEdges [i].distance * (hitEdges [i].point.y - transform.position.y);
				surfaceEdges [1] = new Vector2 (edge2x, edge2y);
				shadowSurfaces [newSurfaces-1].points = surfaceEdges;

			}
		}
		if (newSurfaces < existingSurfaces) {
			shadowSurfaces.RemoveRange (newSurfaces, existingSurfaces - newSurfaces);
		}
		*/
	}
}
