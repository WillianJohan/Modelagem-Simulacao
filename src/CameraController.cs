using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera camera;
    public float maxOrtographicSize = 25f;
    public float minOrtographicSize = 4f;
    public float zoomValue = .5f;
    private void Start()
    {
        if (!camera) camera = GetComponent<Camera>();
    }

    private void Update()
    {
        float mouseScroll = Input.mouseScrollDelta.y;
        float newSize = camera.orthographicSize + (zoomValue * mouseScroll);
        if (mouseScroll != 0) ajustarZoom(newSize);

        if (Input.GetMouseButtonDown(0)) clickInCell();
        if (Input.GetMouseButton(1)) clickInCell();
    }

    private void clickInCell()
    {
        RaycastHit hit;
        if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out hit, 10.0f))
            if (hit.transform.GetComponent<Cell>()) hit.transform.GetComponent<Cell>().setNextStatus();
    }

    public void ajustarZoom(float size)
    {
        camera.orthographicSize = Mathf.Clamp(size, minOrtographicSize, maxOrtographicSize);
    }
}
