using BitBenderGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] TouchInputController touchInputController;

    Camera Camera;
    Rect CameraRect;
    Transform Transform;
    Plane RefPlane;
    
    private void Awake()
    {
        Camera = this.GetComponent<Camera>();
        float Width = Screen.width;
        float Height = Screen.height;
        CameraRect = new Rect(Camera.rect.x * Width, Camera.rect.y * Height, (Camera.rect.width - Camera.rect.x) * Width, (Camera.rect.height - Camera.rect.y) * Height);
        Transform = transform;
        RefPlane = new Plane(Vector3.back, 0);

        touchInputController.OnDragStart += TouchInputController_OnDragStart;
        touchInputController.OnDragUpdate += TouchInputController_OnDragUpdate;
        touchInputController.OnDragStop += TouchInputController_OnDragStop;
        touchInputController.OnPinchStart += TouchInputController_OnPinchStart;
        touchInputController.OnPinchUpdate += TouchInputController_OnPinchUpdate;
        touchInputController.OnPinchStop += TouchInputController_OnPinchStop;
    }

    public bool IsPoint { get; set; }

    #region Drag

    Vector3 CameraDragStartPosition;

    bool IsDrag;

    private void TouchInputController_OnDragStart(Vector3 pos, bool isLongTap)
    {
        if (CameraRect.Contains(pos) == false) return;
        if (IsPoint == false) return; 

        IsDrag = true;
        CameraDragStartPosition = Transform.position;
    }

    private void TouchInputController_OnDragUpdate(Vector3 dragPosStart, Vector3 dragPosCurrent, Vector3 correctionOffset)
    {
        if (IsDrag == false) return;

        Vector3 dragVector = GetDragVector(dragPosStart, dragPosCurrent + correctionOffset);
        Transform.position = CameraDragStartPosition - dragVector;
    }

    private void TouchInputController_OnDragStop(Vector3 dragStopPos, Vector3 dragFinalMomentum)
    {
        IsDrag = false;
    }

    private Vector3 GetDragVector(Vector3 dragPosStart, Vector3 dragPosCurrent)
    {
        Vector3 intersectionDragStart = GetIntersectionPoint(Camera.ScreenPointToRay(dragPosStart));
        Vector3 intersectionDragCurrent = GetIntersectionPoint(Camera.ScreenPointToRay(dragPosCurrent));
        return (intersectionDragCurrent - intersectionDragStart);
    }

    public Vector3 GetIntersectionPoint(Ray ray)
    {
        float distance = 0;
        bool success = RefPlane.Raycast(ray, out distance);
        return (ray.origin + ray.direction * distance);
    }

    #endregion

    #region Pinch

    [SerializeField] float ZoomScala = 10;
    [SerializeField] int MinZoom = 3;
    [SerializeField] int MaxZoom = 100;

    float CameraZoomStartSize;

    bool IsPinch;

    private void TouchInputController_OnPinchStart(Vector3 pinchCenter, float pinchDistance)
    {
        if (CameraRect.Contains(pinchCenter) == false) return;
        if (IsPoint == false) return;

        IsPinch = true;
        CameraZoomStartSize = Camera.orthographicSize;
    }

    private void TouchInputController_OnPinchUpdate(Vector3 pinchCenter, float pinchDistance, float pinchStartDistance)
    {
        if (IsPinch == false) return;

        float deltaDistance = pinchDistance - pinchStartDistance;
        float Zoom = Mathf.Clamp(CameraZoomStartSize - deltaDistance * ZoomScala, MinZoom, MaxZoom);
        Camera.orthographicSize = Zoom;
    }

    private void TouchInputController_OnPinchStop()
    {
        IsPinch = false;
    }

#if UNITY_EDITOR || UNITY_STANDALONE
    private void Update()
    {
        float Wheel = Input.GetAxis("Mouse ScrollWheel");
        if (Wheel != 0)
        {
            Vector2 pinchCenter = Input.mousePosition;
            TouchInputController_OnPinchStart(pinchCenter, 0);
            TouchInputController_OnPinchUpdate(pinchCenter, Wheel, 0);
            TouchInputController_OnPinchStop();
        }
    }
#endif

#endregion

}
