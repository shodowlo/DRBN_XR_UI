using TMPro;
using UnityEngine;

public class KeyboardManager : MonoBehaviour
{
    public TMP_InputField inputField; // L'input à remplir

    // Appelé par chaque touche
    public void AddLetter(string letter)
    {
        inputField.text += letter.ToLower();
    }

    public void Backspace()
    {
        if (inputField.text.Length > 0)
            inputField.text = inputField.text.Substring(0, inputField.text.Length - 1);
    }

    public void HideKeyboard()
    {
        gameObject.SetActive(false);
    }
}
