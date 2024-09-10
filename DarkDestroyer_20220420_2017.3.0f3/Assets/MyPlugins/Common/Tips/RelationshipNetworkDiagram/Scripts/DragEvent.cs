using UnityEngine;
using UnityEngine.EventSystems;
namespace Common.RelationshipNetworkDiagram
{
    public class DragEvent : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        private RectTransform panelRectTransform;

        private void Awake()
        {
            panelRectTransform = transform as RectTransform;
        }
        public void OnDrag(PointerEventData eventData)
        {
            Vector3 pos;
            RectTransformUtility.ScreenPointToWorldPointInRectangle(panelRectTransform, eventData.position, eventData.enterEventCamera, out pos);
            transform.position = pos;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {

        }

        public void OnEndDrag(PointerEventData eventData)
        {

        }
    }
}