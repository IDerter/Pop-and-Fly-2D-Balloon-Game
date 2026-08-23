using UnityEngine;
using UnityEngine.Localization;

namespace AmNuamRunner
{
	[CreateAssetMenu]
    public sealed class UpgradeAsset : ScriptableObject
    {
        public Sprite sprite;
        public LocalizedString localizedName;
        public LocalizedString localizedDescription;
        public int[] costByLevel = { 3, 5, 10 };
        public int step = 5;

        public int MaxLevel => costByLevel.Length;
        public bool IsInApp;
    }
}

