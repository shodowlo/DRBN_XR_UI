using UnityEngine;
using System.Collections;

public class PerlinNoise3D : MonoBehaviour {
	public float power = 3.0f;
	public float scale = 1.0f;
	public float density = 1.0f;
	//sampling for 2D planes
	//private Vector2 v2SampleStart = new Vector2(0f, 0f);
	private Vector3 v3SampleStart = new Vector3(0f, 0f);

	void Start () {
		MakeSomeNoise ();
	}

	void Update () {
		//if (Input.GetKeyDown (KeyCode.Space)) {
		//code for 2D planes	
		//v2SampleStart = new Vector2(Random.Range (0.0f, 100.0f), Random.Range (0.0f, 100.0f));
			v3SampleStart = new Vector3(Random.Range(0.0f, 100.0f), Random.Range(0.0f, 100.0f), Random.Range(0.0f, 100.0f));
			MakeSomeNoise();
		//}
	}

	public static float Perlin3D(float x, float y, float z, float density, float scale)
	{
		float XY = Mathf.PerlinNoise(x, y);
		float YZ = Mathf.PerlinNoise(y, z);
		float ZX = Mathf.PerlinNoise(z, x);

		float YX = Mathf.PerlinNoise(y, z);
		float ZY = Mathf.PerlinNoise(z, y);
		float XZ = Mathf.PerlinNoise(x, z);

		float val = (XY + YZ + ZX + YX + ZY + XZ) / 6f;
		return val * scale;
	}

	void MakeSomeNoise() {
		MeshFilter mf = GetComponent<MeshFilter>();
		//subtituting nromals from vertex coordinates might help in perlin noise effect applied to non planar meshes
		Vector3[] vertices = mf.mesh.vertices;
		Vector3[] normals = mf.mesh.normals;
		for (int i = 0; i < vertices.Length; i++) {
			float xnorm = v3SampleStart.x + normals[i].x * scale;
			float ynorm = v3SampleStart.y + normals[i].y * scale;
			float znorm = v3SampleStart.z + normals[i].z * scale;
			//vertices[i] = vertices[i].x*
			

			//code for planar meshes
			//float xCoord = v2SampleStart.x + vertices[i].x  * scale;
			//float yCoord = v2SampleStart.y + vertices[i].z  * scale;
			//float zCoord = v2SampleStart.z + vertices[i].y * scale;
			//instead of calculating for vertices.y, calculate for norm... 
			//vertices[i].y = (Mathf.PerlinNoise (xCoord, yCoord) - 0.5f) * power;
		}
		mf.mesh.vertices = vertices;
		mf.mesh.RecalculateBounds();
		mf.mesh.RecalculateNormals();
	}
} 
