using UnityEngine;
using UnityEngine.EventSystems;

public class GazeInteractor : MonoBehaviour
{
    [Header("Gaze Settings")]
    public float gazeTime = 1.5f; // Time required to trigger interaction
    private float timer = 0f;

    [Header("References")]
    public Camera vrCamera;

    private GameObject currentFocusedObject;

    void Update()
    {
        Ray ray = new Ray(vrCamera.transform.position, vrCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 20f))
        {
            GameObject obj = hit.collider.gameObject;

            if (obj != currentFocusedObject)
            {
                currentFocusedObject = obj;
                timer = 0f;
            }

            timer += Time.deltaTime;

            if (timer >= gazeTime)
            {
                ExecuteEvents.Execute<IPointerClickHandler>(
                    currentFocusedObject,
                    new PointerEventData(EventSystem.current),
                    ExecuteEvents.pointerClickHandler);

                timer = 0f;
            }
        }
        else
        {
            currentFocusedObject = null;
            timer = 0f;
        }
    }
}
