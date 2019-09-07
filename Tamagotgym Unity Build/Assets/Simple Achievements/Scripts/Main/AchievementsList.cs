using System;
using UnityEngine;
using UnityEngine.UI;
using SimpleAchievements.Other;

namespace SimpleAchievements.Main
{
    [AddComponentMenu("SimpleAchievements/Achievements List")]
    /// <summary>
    /// Use this class if you need show all achievements in game
    /// </summary>
    public sealed class AchievementsList : MonoBehaviour, IObserver
    {
        private readonly string LOCK_STATE = "locked";
        private readonly string UNLOCK_STATE = "unlocked";

        [SerializeField]
        [Header("Select prefab UI element for list with 2 Text and 1 Image components")]
        private GameObject prefabAchievementElement;
        [SerializeField]
        [Header("Name UI Text achievement header")]
        private string prefabAchievementHeader = "header";
        [SerializeField]
        [Header("Name UI Text achievement state")]
        private string prefabAchievementState = "state";
        [SerializeField]
        [Header("Name UI Text achievement icon")]
        private string prefabAchievementIcon = "icon";
        [SerializeField]
        [Header("Select grid where will be created achievements for list")]
        private RectTransform achievementsGrid;

        [Header("This color for state text in prefab")]
        [SerializeField]
        private string lockColor = "#E83838";
        [SerializeField]
        private string unlockColor = "#1CC046";

        private Color colorState;
        private GameObject[] achievementInstances;

        private void Start()
        {
            AchievementsControl.Instance.AddObserver(this);

            InitList(prefabAchievementElement);
        }

        public void OnUpdate(GameObject prefabAchievement, int idAchievement)
        {
            Achievement achievementElement
             = Array.Find(AchievementsControl.Instance.GetAchievements(), target => target.GetID() == idAchievement);

            GameObject achievement = Array.Find(achievementInstances,
            target => target.GetComponent<AchievementElement>().GetAchievementID() == idAchievement);

            Text header = achievement.transform.Find(prefabAchievementHeader).GetComponent<Text>();
            Text state = achievement.transform.Find(prefabAchievementState).GetComponent<Text>();
            Image icon = achievement.transform.Find(prefabAchievementIcon).GetComponent<Image>();

            OutputInfo(achievementElement, header, state, icon);
        }

        /// <summary>
        /// Initialize achievements list
        /// </summary>
        private void InitList(GameObject prefabAchievement)
        {
            achievementInstances = new GameObject[AchievementsControl.Instance.GetAchievements().Length];

            for (int count = 0; count < AchievementsControl.Instance.GetAchievements().Length; count++)
            {
                GameObject instance = Instantiate(prefabAchievement, achievementsGrid);

                instance.GetComponent<AchievementElement>().SetAchievementID(count);
                Text header = instance.transform.Find(prefabAchievementHeader).GetComponent<Text>();
                Text state = instance.transform.Find(prefabAchievementState).GetComponent<Text>();
                Image icon = instance.transform.Find(prefabAchievementIcon).GetComponent<Image>();
                Achievement achievement = AchievementsControl.Instance.GetAchievements()[count];

                OutputInfo(achievement, header, state, icon);
                achievementInstances[count] = instance;
            }
        }


        /// <summary>
        /// Output all information achievement
        /// </summary>
        /// <param name="achievement"></param>
        private void OutputInfo(Achievement achievement, Text header, Text state, Image icon)
        {
            header.text = achievement.GetHeader();
            if (achievement.GetState())
            {
                icon.sprite = achievement.GetUnlockedIcon();
                ColorUtility.TryParseHtmlString(unlockColor, out colorState);
                state.color = colorState;

                if (achievement is ProgressAchievement)
                {
                    ProgressAchievement progressAchievement = (ProgressAchievement)achievement;
                    state.text = UNLOCK_STATE + " " + progressAchievement.GetProgress().ToString() + "/" + progressAchievement.MAX_PROGRESS_VALUE.ToString();
                }
                else state.text = UNLOCK_STATE;
            }
            else
            {
                icon.sprite = achievement.GetLockedIcon();
                ColorUtility.TryParseHtmlString(lockColor, out colorState);
                state.color = colorState;

                if (achievement is ProgressAchievement)
                {
                    ProgressAchievement progressAchievement = (ProgressAchievement)achievement;
                    state.text = LOCK_STATE + " " + progressAchievement.GetProgress().ToString() + "/" + progressAchievement.MAX_PROGRESS_VALUE.ToString();
                }
                else state.text = LOCK_STATE;

            }
        }

        public void OnProgressUpdate(GameObject prefabAchievements, int idAchievements)
        => OnUpdate(prefabAchievements, idAchievements);
        
    }
}