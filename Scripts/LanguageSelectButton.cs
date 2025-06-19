using UnityEngine;

public class LanguageSelectButton : MonoBehaviour
{
    [SerializeField] string language;
    public void OnClick()
    {
        PlayerPrefs.SetString("language", language);
        foreach (var localization in FindObjectsOfType<Localization>())
        {
            localization.UpdateLanguage();
        }
    }
}
