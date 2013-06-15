using UnityEngine;
using System.Collections.Generic;

namespace FixedLineRenderer {
	public abstract class FixedLineRendererBase : MonoBehaviour
	{
		protected abstract IEnumerable<Line> lines { get; set; }
		protected virtual void CreateGuard()
		{
		}
		
		Mesh mesh;

		void Awake() {
			var renderer = gameObject.AddComponent<MeshRenderer>();
			renderer.material = Resources.Load("Materials/FixedLineRenderer") as Material;
			var filter = gameObject.AddComponent<MeshFilter>();
			mesh = filter.mesh;
			Burn();
		}

		public void Burn()
		{
			if(lines == null) return;

			var vertices = new List<Vector3>();
			var uv1 = new List<Vector2>();
			var uv2 = new List<Vector2>();
			var normals = new List<Vector3>();
			var triangles = new List<int>();
			var colors = new List<Color>();
			int[] trianglesTemplate = new []{0,1,3, 3,1,4, 1,2,4, 4,2,5};

			foreach(var line in lines)
			{
				vertices.Add(line.from);
				vertices.Add(line.from);
				vertices.Add(line.from);
				vertices.Add(line.to);
				vertices.Add(line.to);
				vertices.Add(line.to);

				for(int l = 0; l < 6; l++) normals.Add((line.to - line.from).normalized * 100);
				for(int l = 0; l < 3; l++) colors.Add(line.fromColor);
				for(int l = 0; l < 3; l++) colors.Add(line.toColor);

				uv1.Add(new Vector2(0,0));
				uv1.Add(new Vector2(0.5f,0));
				uv1.Add(new Vector2(1,0));
				uv1.Add(new Vector2(0,1));
				uv1.Add(new Vector2(0.5f,1));
				uv1.Add(new Vector2(1,1));

				uv2.Add(new Vector2(1,1));
				uv2.Add(new Vector2(0,0));
				uv2.Add(new Vector2(-1,-1));
				uv2.Add(new Vector2(1,1));
				uv2.Add(new Vector2(0,0));
				uv2.Add(new Vector2(-1,-1));

				foreach(var l in trianglesTemplate)
					triangles.Add(vertices.Count - 6 + l);
			}

			mesh.vertices = vertices.ToArray();
			mesh.uv = uv1.ToArray();
			mesh.uv2 = uv2.ToArray();
			mesh.normals = normals.ToArray();
			mesh.triangles = triangles.ToArray();
			mesh.colors = colors.ToArray();
			mesh.RecalculateBounds();

			CreateGuard();
			lines = null;
		}

		public void OnDrawGizmosSelected()
		{
			if(lines == null) return;
			Gizmos.matrix = transform.localToWorldMatrix;
			foreach(var l in lines)
			{
				Gizmos.color = l.combinedColor;
				Gizmos.DrawLine(l.from, l.to);
			}
		}
	}
}