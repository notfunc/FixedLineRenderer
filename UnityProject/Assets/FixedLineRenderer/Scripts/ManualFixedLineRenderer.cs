using UnityEngine;
using System.Collections.Generic;

namespace FixedLineRenderer {
	public class ManualFixedLineRenderer : FixedLineRendererBase
	{
		[SerializeField] Line[] _lines = new Line[]{};
		
		protected override IEnumerable<Line> lines
		{
			get
			{
				return _lines;
			}
			
			set
			{
				_lines = value as Line[];
			}
		}

		public void SetLines(Line[] lines)
		{
			this.lines = lines;
		}
	}
}