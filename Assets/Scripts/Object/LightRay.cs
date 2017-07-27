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
		}
		viewMesh.Clear ();
		viewMesh.vertices = vertices;
		viewMesh.triangles = triangles;
		viewMesh.RecalculateNormals ();
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
}
