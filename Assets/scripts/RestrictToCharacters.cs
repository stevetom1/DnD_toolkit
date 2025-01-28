using TMPro;
using UnityEngine;

public class RestrictToCharacters : MonoBehaviour
{
    public TMP_InputField inputField;

    private void Start()
    {
        if (inputField == null)
        {
            inputField = GetComponent<TMP_InputField>();
        }

        inputField.onValueChanged.AddListener(ValidateInput);
    }

    private void ValidateInput(string input)
    {
        inputField.text = RemoveNonAlphabeticCharacters(input);
    }

    private string RemoveNonAlphabeticCharacters(string input)
    {
        System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("[^a-zA-Z]");
        return regex.Replace(input, "");
    }
}
