using UnityEngine;

public class Tutorial : MonoBehaviour
{
    // Class contenant les liste de gameobjects à toggle à chaque step
    [System.Serializable]
    public class Step
    {
        public GameObject[] objectsToToggle;
    }

    public string preferenceKey = "startTutorial";
    public GameObject tutorialUI;

    public Step[] steps; // Array of steps

    private int step = 0; // Step counter

    //Previous step button
    public GameObject previousStepButton;


    void Start()
    {
        bool shouldInit = PlayerPrefs.GetInt(preferenceKey, 1) == 1;

        if (shouldInit)
        {
            Init();
        }
    }


    public void Init()
    {
        // Prendre le premier step l'afficher et cacher les autres
        step = 0;
        deactiveAndActiveCurrentGameObjects(step);
        tutorialUI.SetActive(true);

        //desactiver le bouton previous step
        previousStepButton.SetActive(false);
    }

    public void NextStep()
    {
        step++;
        // si on est a la fin des steps, on desactive le tuto
        if (step >= steps.Length)
        {
            tutorialUI.SetActive(false);
            return;
        }
        //on desactive le step precedent et on active le step suivant
        deactiveAndActiveCurrentGameObjects(step);
        // Si on est pas au premier step, on active le bouton previous step
        if (step > 0)
        {
            previousStepButton.SetActive(true);
        }
    }

    public void PreviousStep()
    {
        step--;
        // si on est a la fin des steps, on desactive le bouton previous step
        if (step == 0)
        {
            previousStepButton.SetActive(false);
        }
        //on desactive le step precedent et on active le step suivant
        deactiveAndActiveCurrentGameObjects(step);
    }
    private void setActiveGameObjects(bool active, GameObject[] gameObjects)
    {
        foreach (GameObject obj in gameObjects)
        {
            obj.SetActive(active);
        }
    }

    private void deactiveAndActiveCurrentGameObjects(int step)
    {
        for (int i = 0; i < steps.Length; i++)
        {
            if (i == step)
            {
                setActiveGameObjects(true, steps[i].objectsToToggle);
            }
            else
            {
                setActiveGameObjects(false, steps[i].objectsToToggle);
            }
        }
    }
}
