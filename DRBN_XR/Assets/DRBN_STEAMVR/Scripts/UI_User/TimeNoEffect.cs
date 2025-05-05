using UnityEngine;

public class TimeNoEffect : MonoBehaviour
{
    void Update()
    {
        if (Time.timeScale == 0 || Time.timeScale == 2)
        {
            UpdateComponents(this.gameObject);
        }
    }

    void UpdateComponents(GameObject obj)
    {
        UpdateComponent(obj);

        foreach (Transform child in obj.transform)
        {
            UpdateComponents(child.gameObject);
        }
    }

    void UpdateComponent(GameObject obj)
    {
        var exampleComponent = obj.GetComponent<ExampleComponent>();
        if (exampleComponent != null)
        {
            exampleComponent.ManualUpdate(Time.unscaledDeltaTime);
        }
    }
}

// Exemple de composant qui utilise ManualUpdate
public class ExampleComponent : MonoBehaviour
{
    public void ManualUpdate(float deltaTime)
    {
        // Logique de mise à jour ici
        // Par exemple, déplacer l'objet
        transform.Translate(Vector3.right * deltaTime);
    }
}