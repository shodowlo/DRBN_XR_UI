using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    //Le transform du point de départ de l'ouverture du menu
    public Transform startTransform;
    //Le transform de la position finale du menu
    public Transform endTransform;
    //La durée de l'animation d'ouverture/fermeture du menu
    public float animationDuration = 0.2f;

    [System.Serializable]
    public class Tabs
    {
        // Le panneau de l'onglet
        public GameObject tabPanel;
        // Bouton de l'onglet
        public GameObject tabButton;
        public Image tabImage;
        // L'image qui indique visuellement si l'onglet est actif ou non
        public GameObject tabSelector;
        // Title de l'onglet
        public GameObject tabTitle;
        //Optionnel : les gameobject à désactiver quand on change d'onglet/quitter le menu
        public GameObject[] objectsToHide;
        //Optionnel : les scripts à désactiver quand on change d'onglet/quitter le menu
        public GameObject[] objectsToShow;
        public MonoBehaviour[] scriptsToDeactivate;
        //Optionnel : les scripts à activer quand on ouvre l'onglet
        public MonoBehaviour[] scriptsToActivate;
    }
    public Tabs[] tabs; // Array of tabs

    private bool isAnimating = false;
    private bool isMenuOpen = false;

    public int CurrentTabIndex { get; private set; } = 0;

    void Start()
    {
        Init();
    }

    public void Init()
    {
        // Prendre le premier tab l'afficher et cacher les autres
        for (int i = 0; i < tabs.Length; i++)
        {

            int index = i; // Nécessaire pour la fermeture de la boucle
            var button = tabs[i].tabButton.GetComponent<UnityEngine.UI.Button>();
            button.onClick.AddListener(() => ChangeTab(index));

            // Initialiser l'alpha des images à 60
            SetImageAlpha(tabs[i].tabImage, 60f);

            // Ajouter events pour hover
            UnityEngine.EventSystems.EventTrigger trigger = tabs[i].tabButton.GetComponent<UnityEngine.EventSystems.EventTrigger>();
            if (trigger == null)
                trigger = tabs[i].tabButton.AddComponent<UnityEngine.EventSystems.EventTrigger>();

            // Entrée souris
            var entryEnter = new UnityEngine.EventSystems.EventTrigger.Entry
            {
                eventID = UnityEngine.EventSystems.EventTriggerType.PointerEnter
            };
            entryEnter.callback.AddListener((data) =>
            {
                if (index != CurrentTabIndex)
                    SetImageAlpha(tabs[index].tabImage, 255f);
            });
            trigger.triggers.Add(entryEnter);

            // Sortie souris
            var entryExit = new UnityEngine.EventSystems.EventTrigger.Entry
            {
                eventID = UnityEngine.EventSystems.EventTriggerType.PointerExit
            };
            entryExit.callback.AddListener((data) =>
            {
                if (index != CurrentTabIndex)
                    SetImageAlpha(tabs[index].tabImage, 60f);
            });
            trigger.triggers.Add(entryExit);
        }
        ChangeTab(0);
    }

    public void ChangeTab(int index, bool activateScripts = true)
    {
        if (index < 0 || index >= tabs.Length) return;

        // Désactiver tous les onglets, sélecteurs, titres, objets à afficher, objets à cacher, et scripts
        for (int i = 0; i < tabs.Length; i++)
        {
            tabs[i].tabPanel.SetActive(false);
            tabs[i].tabSelector.SetActive(false);
            tabs[i].tabTitle.SetActive(false);

            SetImageAlpha(tabs[i].tabImage, 60f);

            if (tabs[i].objectsToHide != null)
            {
                foreach (GameObject obj in tabs[i].objectsToHide)
                {
                    if (obj != null)
                        obj.SetActive(false);
                }
            }

            if (tabs[i].objectsToShow != null)
            {
                foreach (GameObject obj in tabs[i].objectsToShow)
                {
                    if (obj != null)
                        obj.SetActive(false);
                }
            }

            if (tabs[i].scriptsToDeactivate != null)
            {
                foreach (var script in tabs[i].scriptsToDeactivate)
                {
                    if (script != null)
                        script.enabled = false;
                }
            }
        }

        // Activer le nouvel onglet, ses éléments associés et ses scripts
        tabs[index].tabPanel.SetActive(true);
        tabs[index].tabSelector.SetActive(true);
        tabs[index].tabTitle.SetActive(true);

        SetImageAlpha(tabs[index].tabImage, 255f);

        if (tabs[index].objectsToShow != null)
        {
            foreach (GameObject obj in tabs[index].objectsToShow)
            {
                if (obj != null)
                    obj.SetActive(true);
            }
        }

        if (activateScripts == true && tabs[index].scriptsToActivate != null)
        {
            foreach (var script in tabs[index].scriptsToActivate)
            {
                if (script != null)
                    script.enabled = true;
            }
        }
        CurrentTabIndex = index;
    }


    public void ShowMenu()
    {
        if (isAnimating || isMenuOpen || startTransform == null || endTransform == null || gameObject == null)
            return;

        gameObject.SetActive(true);
        gameObject.transform.position = startTransform.position;
        gameObject.transform.rotation = startTransform.rotation;
        gameObject.transform.localScale = Vector3.one * 0.1f;

        // Activer les scripts du premier onglet si il y a un premier onglet
        if (tabs.Length > 0 && tabs[0].scriptsToActivate != null)
        {
            foreach (var script in tabs[0].scriptsToActivate)
            {
                if (script != null)
                {
                    script.enabled = true;
                }
            }
        }

        //show les objects a show
        if (tabs.Length > 0 && tabs[0].objectsToShow != null)
        {
            foreach (GameObject obj in tabs[0].objectsToShow)
            {
                if (obj != null)
                    obj.SetActive(true);
            }
        }
        StartCoroutine(AnimateMenu(endTransform, true));
    }

    public void CloseMenu()
    {
        if (isAnimating || !isMenuOpen || startTransform == null || gameObject == null)
            return;

        StartCoroutine(AnimateMenu(startTransform, false));
    }

    public void ToggleMenu()
    {
        if (isAnimating || gameObject == null)
            return;

        if (isMenuOpen)
        {
            CloseMenu();
        }
        else
        {
            ShowMenu();
        }
    }

    private System.Collections.IEnumerator AnimateMenu(Transform target, bool opening)
    {
        isAnimating = true;
        float elapsed = 0f;

        Vector3 startPos = gameObject.transform.position;
        Quaternion startRot = gameObject.transform.rotation;
        Vector3 startScale = gameObject.transform.localScale;

        Vector3 targetPos = target.position;
        Quaternion targetRot = target.rotation;
        Vector3 targetScale = opening ? target.localScale : Vector3.one * 0.1f;

        while (elapsed < animationDuration)
        {
            float t = elapsed / animationDuration;

            gameObject.transform.position = Vector3.Lerp(startPos, targetPos, t);
            gameObject.transform.rotation = Quaternion.Slerp(startRot, targetRot, t);
            gameObject.transform.localScale = Vector3.Lerp(startScale, targetScale, t);

            elapsed += Time.deltaTime;
            yield return null;
        }

        gameObject.transform.position = targetPos;
        gameObject.transform.rotation = targetRot;
        gameObject.transform.localScale = targetScale;

        isAnimating = false;
        isMenuOpen = opening;

        if (!opening)
        {
            gameObject.SetActive(false);
            ChangeTab(0, activateScripts: false);

            // Désactiver tous les objectsToShow (au cas où)
            for (int i = 0; i < tabs.Length; i++)
            {
                if (tabs[i].objectsToShow != null)
                {
                    foreach (GameObject obj in tabs[i].objectsToShow)
                    {
                        if (obj != null)
                            obj.SetActive(false);
                    }
                }
            }
        }
    }

    void Togglescripts(MonoBehaviour[] scripts, bool enable)
    {
        if (scripts != null)
        {
            foreach (var script in scripts)
            {
                if (script != null)
                {
                    script.enabled = enable;
                }
            }
        }
    }

    private void SetImageAlpha(Image image, float alpha)
{
    if (image != null)
    {
        Color color = image.color;
        color.a = alpha / 255f;
        image.color = color;
    }
}

}
