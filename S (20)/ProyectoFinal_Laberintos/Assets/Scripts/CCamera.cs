using UnityEngine;

public class CCamera : MonoBehaviour
{
    public float minSize;
    public float maxSize;
    [Range(5,50)]
    public float zoomSpeed;
    [Range(0.1f,5f)]
    public float moveSpeed;

    private float scroll;
    private Vector3 startPos;
    private Vector3 endPos;

	void Update ()
    {
        scroll = Input.GetAxisRaw("MouseScrollWheel");
        if(scroll != 0)
        {
            Camera.main.orthographicSize += -(scroll * zoomSpeed);
            moveSpeed -= scroll;
        }
        if (Camera.main.orthographicSize >= maxSize)
            Camera.main.orthographicSize = maxSize;
        if (Camera.main.orthographicSize <= minSize)
            Camera.main.orthographicSize = minSize;
        if(Input.GetMouseButtonDown(2))
        {
            startPos = Input.mousePosition;
        }
        if(Input.GetMouseButton(2))
        {
            endPos = Input.mousePosition;
            Vector3 diff = endPos - startPos;
            transform.Translate(-diff * Time.deltaTime * moveSpeed);
            startPos = Input.mousePosition;
        }
	}
}
