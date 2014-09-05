using System.Collections.Generic;
public class EdgeComp : IComparer<Edge>
{
	public int Compare (Edge e1, Edge e2)
	{
		return e1.Weight - e2.Weight;
	}
}