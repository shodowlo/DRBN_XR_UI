using UnityEngine;
using UnityEngine.UI;

public class DeleteSpawnedObjectButton : MonoBehaviour
{
    private GameObject targetObject;

    public void SetTarget(GameObject obj)
    {
        targetObject = obj;
    }

    public void DeleteObject()
    {
        if (targetObject != null)
        {
            Destroy(targetObject);
            Destroy(transform.parent.gameObject); // delete link UI 
        }
    }
}
