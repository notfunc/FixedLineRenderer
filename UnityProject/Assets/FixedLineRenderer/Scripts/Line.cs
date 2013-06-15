using UnityEngine;

namespace FixedLineRenderer {
	[System.Serializable]
	public class Line
	{
		const float pointThreshould = 0.0000005f;
		public Vector3 from;
		public Vector3 to;
		public Color fromColor;
		public Color toColor;

		public Color combinedColor
		{
			get
			{
				return Color.Lerp(fromColor, toColor, 0.5f);
			}
		}

		public Line(Vector3 from, Vector3 to, Color fromColor, Color toColor)
		{
			this.from = from;
			this.to = to;
			this.fromColor = fromColor;
			this.toColor = toColor;
		}
	}
}