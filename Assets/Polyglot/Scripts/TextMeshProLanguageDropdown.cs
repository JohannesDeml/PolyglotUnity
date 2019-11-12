#if TMP_PRESENT
using TMPro;
using UnityEngine;

namespace Polyglot
{
    #if UNITY_5_2 || UNITY_5_3 || UNITY_5_4_OR_NEWER
    [RequireComponent(typeof(TMP_Dropdown))]
    #endif
    [AddComponentMenu("UI/TMP Language Dropdown", 36)]
    public class TextMeshProLanguageDropdown : MonoBehaviour, ILocalize
    {
        #if UNITY_5_2 || UNITY_5_3 || UNITY_5_4_OR_NEWER
        [Tooltip("The dropdown to populate with all the available languages")]

        [SerializeField]
        private TMP_Dropdown dropdown;

        #if UNITY_5
        [UsedImplicitly]
        #endif
        public void Reset()
        {
            dropdown = GetComponent<TMP_Dropdown>();
        }

        #if UNITY_5
        [UsedImplicitly]
        #endif
        public void Start()
        {
            CreateDropdown();

            Localization.Instance.AddOnLocalizeEvent(this);
        }

        private void CreateDropdown()
        {
            var flags = dropdown.hideFlags;
            dropdown.hideFlags = HideFlags.DontSaveInEditor;

            dropdown.options.Clear();

            var languageNames = Localization.Instance.EnglishLanguageNames;

            for (int index = 0; index < languageNames.Count; index++)
            {
                var languageName = languageNames[index];
                dropdown.options.Add(new TMP_Dropdown.OptionData(languageName));
            }

            dropdown.value = -1;
            dropdown.value = Localization.Instance.SelectedLanguageIndex;

            dropdown.hideFlags = flags;
        }

        #endif
        public void OnLocalize()
        {
            #if UNITY_5_2 || UNITY_5_3 || UNITY_5_4_OR_NEWER
            dropdown.onValueChanged.RemoveListener(Localization.Instance.SelectLanguage);
            dropdown.value = Localization.Instance.SelectedLanguageIndex;
            dropdown.onValueChanged.AddListener(Localization.Instance.SelectLanguage);
            #endif
        }
    }
}
#endif