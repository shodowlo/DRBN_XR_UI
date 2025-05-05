using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

// La classe pour stocker une entrée
[System.Serializable]
public class MenuEntry
{
    public GameObject entryObject;        // L'objet d'interface instancié
    public GameObject spawnedObject;       // L'objet 3D associé
    public GameObject dashedLineObject;    // L'objet qui contient le DashedLineMol
    public float entryHeight;               // Largeur de l'entrée (en pixels)

    public MenuEntry(GameObject entryObject, GameObject spawnedObject, GameObject dashedLineObject, float entryHeight)
    {
        this.entryObject = entryObject;
        this.spawnedObject = spawnedObject;
        this.dashedLineObject = dashedLineObject;
        this.entryHeight = entryHeight;
    }
}

public class DeleteMenu : MonoBehaviour
{
    public GameObject entryPrefab;  // Le prefab de l'entrée
    public GameObject content;      // Le conteneur du scroll (si nécessaire pour ajuster la taille du contenu)
    public Button deleteAllButton; // Le bouton pour supprimer toutes les entrées

    private List<MenuEntry> entries = new List<MenuEntry>(); // La liste d'entrées

    private void Start()
    {
        if (deleteAllButton != null)
        {
            deleteAllButton.onClick.AddListener(DeleteAllEntries);
        }
        else
        {
            Debug.LogError("Le bouton 'Delete All' n'a pas été assigné !");
        }
    }

    public Button AddEntry(string labelText, GameObject spawnedObject)
    {
        if (entryPrefab == null || content == null)
        {
            Debug.LogError("EntryPrefab ou Content non assigné !");
            return null;
        }

        // Instanciation sous le GameObject auquel ce script est attaché
        GameObject newEntry = Instantiate(entryPrefab, transform);

        // Modifier le texte de l'entrée
        TextMeshProUGUI label = newEntry.GetComponentInChildren<TextMeshProUGUI>();
        if (label != null)
        {
            label.text = labelText;
        }

        // Gestion des boutons
        Button[] buttons = newEntry.GetComponentsInChildren<Button>();

        Button deleteButton = null;

        if (buttons.Length >= 2)
        {
            deleteButton = buttons[0];
            deleteButton.onClick.AddListener(() => RemoveEntry(newEntry));

            Button highlightButton = buttons[1];
            highlightButton.onClick.AddListener(() => ToggleSelected(newEntry));
        }

        // Chercher le GameObject contenant DashedLineMol
        DashedLine dashedLineScript = newEntry.GetComponentInChildren<DashedLine>(true);
        GameObject startDashedLineObject = null;

        if (dashedLineScript != null)
        {
            startDashedLineObject = spawnedObject?.gameObject;

            if (startDashedLineObject != null)
            {
                // Trouver le dernier enfant de la chaîne
                Transform lastChild = startDashedLineObject.transform;
                while (lastChild.childCount > 0)
                {
                    lastChild = lastChild.GetChild(0);
                }

                // Affecter les valeurs
                dashedLineScript.startPoint = lastChild;
                dashedLineScript.targetObject = content;

                // Désactiver le GameObject contenant DashedLineMol
                dashedLineScript.gameObject.SetActive(false);
            }
        }
        else
        {
            Debug.LogWarning("Aucun DashedLineMol trouvé dans le prefab.");
        }

        // Obtenir la largeur de l'élément
        float entryHeight = 0f;
        RectTransform entryRect = newEntry.GetComponent<RectTransform>();
        if (entryRect != null)
        {
            entryHeight = entryRect.rect.height;
            AdjustContentHeight(entryHeight+5); // Augmenter la taille du content
        }
        else
        {
            Debug.LogWarning("Le prefab n'a pas de RectTransform !");
        }

        // Ajouter à la liste
        entries.Add(new MenuEntry(newEntry, spawnedObject, dashedLineScript?.gameObject, entryHeight));
        
        EventTrigger eventTrigger = spawnedObject.GetComponent<EventTrigger>();
        if (eventTrigger == null)
        {
            eventTrigger = spawnedObject.AddComponent<EventTrigger>();  // Ajouter un EventTrigger si pas déjà présent
        }

        // Créer un événement pour le clic sur l'objet 3D
        EventTrigger.Entry entryClick = new EventTrigger.Entry();
        entryClick.eventID = EventTriggerType.PointerClick;
        entryClick.callback.AddListener((eventData) => RemoveEntry(newEntry));  // Appel de RemoveEntry pour supprimer l'entrée

        eventTrigger.triggers.Add(entryClick);  // Ajouter l'événement au EventTrigger de spawnedObject

        //retourne le bouton pour supprimer
        return deleteButton;
    }

    public void RemoveEntry(GameObject entryObject)
    {
        MenuEntry toRemove = entries.Find(e => e.entryObject == entryObject);
        if (toRemove != null)
        {
            entries.Remove(toRemove);

            if (toRemove.spawnedObject != null)
            {
                Destroy(toRemove.spawnedObject);
            }

            if (toRemove.dashedLineObject != null)
            {
                Destroy(toRemove.dashedLineObject);
            }

            Destroy(toRemove.entryObject);

            // Réduire la taille du content
            AdjustContentHeight(-toRemove.entryHeight-5);
        }
    }

    private void ToggleSelected(GameObject entryObject)
    {
        // Désélectionner toutes les autres entrées
        foreach (var entry in entries)
        {
            if (entry.entryObject != entryObject)
            {
                // Chercher l'enfant "Selected"
                Transform selectedChild = entry.entryObject.transform.Find("Selected");
                if (selectedChild != null)
                {
                    selectedChild.gameObject.SetActive(false);

                    // Mettre à jour aussi DashedLineMol
                    if (entry.dashedLineObject != null)
                    {
                        entry.dashedLineObject.SetActive(false);
                    }
                }
            }
        }

        // Sélectionner l'entrée cliquée
        Transform selected = entryObject.transform.Find("Selected");
        if (selected != null)
        {
            bool activeNow = !selected.gameObject.activeSelf;
            selected.gameObject.SetActive(activeNow);

            // Mettre à jour aussi DashedLineMol
            MenuEntry entry = entries.Find(e => e.entryObject == entryObject);
            if (entry != null && entry.dashedLineObject != null)
            {
                entry.dashedLineObject.SetActive(activeNow);
            }
        }
    }

    private void AdjustContentHeight(float deltaHeight)
    {
        if (content != null)
        {
            RectTransform contentRect = content.GetComponent<RectTransform>();
            if (contentRect != null)
            {
                Vector2 size = contentRect.sizeDelta;
                size.y += deltaHeight;

                contentRect.sizeDelta = size;
            }
        }
    }

    private void setContentHeight(float height)
    {
        if (content != null)
        {
            RectTransform contentRect = content.GetComponent<RectTransform>();
            if (contentRect != null)
            {
                Vector2 size = contentRect.sizeDelta;
                size.y = height;

                contentRect.sizeDelta = size;
            }
        }
    }

    // Méthode pour supprimer toutes les entrées
    public void DeleteAllEntries()
    {
        // Supprimer chaque entrée dans la liste
        foreach (var entry in entries)
        {
            if (entry.spawnedObject != null)
            {
                Destroy(entry.spawnedObject);
            }

            if (entry.dashedLineObject != null)
            {
                Destroy(entry.dashedLineObject);
            }

            Destroy(entry.entryObject);
        }

        // Réinitialiser la liste et ajuster la taille du contenu
        entries.Clear();
        setContentHeight(30);
    }

    public Button GetDeleteButtonForObject(GameObject spawnedObject)
    {
        foreach (var entry in entries)
        {
            if (entry.spawnedObject == spawnedObject)
            {
                return entry.entryObject.GetComponent<Button>();
            }
        }
        return null;
    }
}
