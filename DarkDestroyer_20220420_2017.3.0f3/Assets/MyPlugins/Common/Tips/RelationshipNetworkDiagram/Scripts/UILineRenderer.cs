using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


namespace Common.RelationshipNetworkDiagram
{

    [RequireComponent(typeof(CanvasRenderer))]
    public class UILineRenderer : Graphic
    {

        public Vector2 startPoint;
        public Vector2 endPoint;
        public float thickness = 2f;

        public Line line;

        protected override void OnPopulateMesh(VertexHelper vh)
        {
            vh.Clear();

            var rect = rectTransform.rect;
            var dir = (endPoint - startPoint).normalized;
            var normal = new Vector2(-dir.y, dir.x) * thickness / 2f;

            var corner1 = startPoint - normal;
            var corner2 = startPoint + normal;
            var corner3 = endPoint + normal;
            var corner4 = endPoint - normal;

            vh.AddVert(corner1, color, new Vector2(0, 0));
            vh.AddVert(corner2, color, new Vector2(0, 1));
            vh.AddVert(corner3, color, new Vector2(1, 1));
            vh.AddVert(corner4, color, new Vector2(1, 0));

            vh.AddTriangle(0, 1, 2);
            vh.AddTriangle(2, 3, 0);
        }

        // Call this method to update line position
        public void SetPositions(Vector2 start, Vector2 end)
        {
            startPoint = start;
            endPoint = end;
            SetVerticesDirty(); // Causes the graphic to redraw
        }
    }
}
