using UnityEngine;
using System.Collections.Generic;

namespace FixedLineRenderer {
	public class MeshFixedLineRenderer : FixedLineRendererBase
	{
		[SerializeField] protected Mesh referenceMesh;
		[SerializeField] bool useGuard;
		
		protected override IEnumerable<Line> lines
		{
			get
			{
				if(referenceMesh != null)
				{
					var vertices = referenceMesh.vertices;
					var colors = referenceMesh.colors;
					var triangles = referenceMesh.triangles;

					for(int i = 0; i < triangles.Length; i+=3)
					{
						var t1 = triangles[i];
						var t2 = triangles[i+1];
						var t3 = triangles[i+2];
						var v1 = vertices[t1];
						var v2 = vertices[t2];
						var v3 = vertices[t3];
						var c1 = colors.Length == 0 ? Color.white : colors[t1];
						var c2 = colors.Length == 0 ? Color.white : colors[t2];
						var c3 = colors.Length == 0 ? Color.white : colors[t3];
						yield return new Line(v1, v2, c1, c2);
						yield return new Line(v2, v3, c2, c3);
						yield return new Line(v3, v1, c3, c1);
					}
				}
			}
			
			set
			{
			}
		}

		protected override void CreateGuard()
		{
			if(!useGuard) return;
			var go = new GameObject("Guard");
			go.transform.parent = transform;
			go.transform.localPosition = Vector3.zero;
			go.transform.localScale = Vector3.one;
			go.transform.localRotation = Quaternion.identity;
			var renderer = go.AddComponent<MeshRenderer>();
			renderer.material = Resources.Load("Materials/FixedLineRendererGurd") as Material;
			var filter = go.AddComponent<MeshFilter>();
			filter.mesh = referenceMesh;
		}

		public void SetLines(Line[] lines)
		{
			this.lines = lines;
		}
	}
}