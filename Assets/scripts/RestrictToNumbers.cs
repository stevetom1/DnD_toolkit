using TMPro;
using UnityEngine;

public class RestrictToNumbers : MonoBehaviour
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
        inputField.text = RemoveNonNumericCharacters(input);
    }

    private string RemoveNonNumericCharacters(string input)
    {
        System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("[^0-9]");
        return regex.Replace(input, "");
    }
}
