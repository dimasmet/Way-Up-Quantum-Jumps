using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStickMain : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] protected GameObject _handle;

    [SerializeField] protected float MoveRadius;

    private float distancePercent;

    public Vector2 Direction
    {
        get
        {
            return (_handle.transform.position - transform.position).normalized;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 inputPosition = Camera.main.ScreenToWorldPoint(eventData.position);

        Vector3 offset = inputPosition - gameObject.transform.position;

        offset = new Vector3(offset.x, offset.y, 0);

        _handle.transform.position = transform.position + Vector3.ClampMagnitude(offset, MoveRadius);
        distancePercent = Vector3.ClampMagnitude(offset, MoveRadius).magnitude / 1f;

        Action(distancePercent);
    }

    public virtual void Action(float percentSpeed)
    { }

    public void OnEndDrag(PointerEventData eventData)
    {
        _handle.transform.localPosition = Vector3.zero;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        DownToJoystick();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        UpToJoystick();
    }

    public virtual void DownToJoystick()
    { }

    public virtual void UpToJoystick()
    { }
}
