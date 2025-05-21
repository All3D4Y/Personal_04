using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

[RequireComponent(typeof(CanvasRenderer))]
public class UILineRenderer : MaskableGraphic
{
    public List<Vector2> points = new List<Vector2>();
    public float thickness = 5f;

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();
        if (points == null || points.Count < 2) return;

        for (int i = 0; i < points.Count - 1; i++)
        {
            Vector2 p1 = points[i];
            Vector2 p2 = points[i + 1];
            Vector2 direction = (p2 - p1).normalized;
            Vector2 normal = new Vector2(-direction.y, direction.x) * thickness * 0.5f;

            UIVertex[] quad = new UIVertex[4];
            quad[0] = GetVertex(p1 - normal);
            quad[1] = GetVertex(p1 + normal);
            quad[2] = GetVertex(p2 + normal);
            quad[3] = GetVertex(p2 - normal);

            int idx = vh.currentVertCount;
            for (int j = 0; j < 4; j++) vh.AddVert(quad[j]);
            vh.AddTriangle(idx, idx + 1, idx + 2);
            vh.AddTriangle(idx, idx + 2, idx + 3);
        }
    }

    UIVertex GetVertex(Vector2 point)
    {
        UIVertex vert = UIVertex.simpleVert;
        vert.color = color;
        vert.position = point;
        return vert;
    }

    public void SetPoints(List<Vector2> newPoints)
    {
        points = newPoints;
        SetVerticesDirty();
    }
}
