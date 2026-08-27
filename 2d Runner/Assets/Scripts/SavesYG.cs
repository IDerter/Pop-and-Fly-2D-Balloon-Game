using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YG
{
    public partial class SavesYG
    {
        public string[] upgradeNames;
        public int[] upgradeLevels;

        public int coin = 0;
        public int bestScore = 0;

        public string[] completedTutorialSteps = new string[0];

        public float musicVolume = 1f;
        public float soundsVolume = 1f;
        public bool isSFXEnabled = true;


        public int boughtStars = 0;

        public string lastDailyRewardDate = ""; // Дата последнего получения
        public int dailyRewardDayIndex = 0;    // Текущий день (0-6)
        public bool isTutorialCompleted = false;

        public string currentSkin = "RedDragon";
    }
}
