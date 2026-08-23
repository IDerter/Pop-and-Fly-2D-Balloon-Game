using UnityEngine;

namespace AmNuamRunner
{
	public enum Languages
    {
        ENG,
        RUS,
        ESP
    }
    public class Language : MonoBehaviour
    {
        [SerializeField] private Languages _language;
        public Languages GetLanguage => _language;
    }
}