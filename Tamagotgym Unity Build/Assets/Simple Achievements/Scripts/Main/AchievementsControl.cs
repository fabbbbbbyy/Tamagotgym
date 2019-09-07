using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using SimpleAchievements.Other;

namespace SimpleAchievements.Main
{
    public interface  IObserver
    {
        /// <summary>
        /// Calls when achievement unlock
        /// </summary>
        /// <param name="prefabAchievement"></param>
        void OnUpdate(GameObject prefabAchievement, int idAchievement);
        void OnProgressUpdate(GameObject prefabAchievements, int idAchievements);
    }

    public interface IObservable
    {
        void AddObserver(IObserver observer);
        void RemoveObserver(IObserver observer);
        void Notify(GameObject prefabAchievement, int idAchievement, bool callForProgress);
    }

    [AddComponentMenu("SimpleAchievements/Manager")]
    public sealed class AchievementsControl : MonoBehaviour, IObservable
    {
        public static AchievementsControl Instance = null;

        private List<IObserver> observers = new List<IObserver>();
        private Achievement[] achievements;

        public Achievement[] GetAchievements() => achievements;

        [SerializeField]
        [Header("Select prefab with ui text and image objects")]
        private GameObject prefabAchievement;
        [SerializeField]
        [Header("Path to Folder (in 'Resources' folder) with Achievements (ScriptableObject)")]
        private string resourcesFolderPathWithAchievements = "Achievements";
        [SerializeField]
        [Header("If you need reset all progress achievements set 'true' value")]
        private bool resetAllProgressAchievementsInStart = true;

        [Header("Use this variables if you want consistently show your achievements. ")]
        [Header("-------------------------------")]
        [SerializeField]
        private Text achievement_header;
        [SerializeField]
        private Image achievement_icon;
        [Header("-------------------------------")]

        [SerializeField]
        [Tooltip("If you need show 2 or more achievements at the same time.")]
        private bool useMultipleAchievements;
        [SerializeField]
        [Header("If you need show 2 or more achievements at the same time set all settings below")]
        [Header("Select vertical grid where will be created prefab achievements")]
        private RectTransform achievementsGrid;
        [SerializeField]
        [Tooltip("Write 'header' (UI Text) name your prefab")]
        [Header("Name UI Text achievement header")]
        private string prefabNameHeader;
        [SerializeField]
        [Header("Name UI Image achievement icon")]
        [Tooltip("Write 'icon' (UI Image) name your prefab")]
        private string prefabNameIcon;
        [SerializeField]
        [Header("How much time is necessary to show the unlocked achievement")]
        private float timeOfDestroying = 5;

        private void Awake()
        {
            if (Instance == null) Instance = this; 
            
            else if (Instance == this) Destroy(gameObject); 
 
            DontDestroyOnLoad(gameObject);

            InitAchievementsList();
        }

        /// <summary>
        /// Initialize all game achievements
        /// </summary>
        private void InitAchievementsList()
        {
            achievements = Resources.LoadAll<Achievement>(resourcesFolderPathWithAchievements);


            if (resetAllProgressAchievementsInStart)  ResetAllProgress();

            if (useMultipleAchievements) prefabAchievement.GetComponent<AchievementElement>().SetAnimationState(true);
            else prefabAchievement.GetComponent<AchievementElement>().SetAnimationState(false);
        }

        /// <summary>
        /// Reset all progress achievements
        /// </summary>
        private void ResetAllProgress()
        {
            for (int countAchievements = 0; countAchievements < achievements.Length; countAchievements++)
            {
                Achievement achievement = (Achievement) achievements[countAchievements];
                achievement.Reset();
            }
        }

        /// <summary>
        /// Output achievement information in UI Text and Image
        /// </summary>
        private void Output(Text header, Image icon, Achievement achievement)
        {           
            header.text = achievement.GetHeader();
            icon.sprite = achievement.GetUnlockedIcon();
        }

        /// <summary>
        /// Unlock selected achievement
        /// </summary>
        /// <param name="achievement"></param>
        private void UnlockAchievement(Achievement achievement, Text outputHeader, Image outputIcon)
        {
            if (!achievement.GetState())
            {
                if(achievement is ProgressAchievement)
                {
                    ProgressAchievement progressAchievement = (ProgressAchievement)achievement;
                    progressAchievement.AddProgress(100);
                }

                achievement.SetState(true);
                Notify(prefabAchievement, achievement.GetID(), false);
                Output(outputHeader, outputIcon, achievement);
            }
        }

        private Achievement FindAchievementByHeader(string header) =>
        Array.Find(achievements, target => target.GetHeader() == header);

        private Achievement FindAchievementByID(int id) =>
        Array.Find(achievements, target => target.GetID() == id);

        /// <summary>
        /// Create selected achievement
        /// </summary>
        /// <param name="achievement"></param>
        /// <param name="headerName"></param>
        /// <param name="iconName"></param>
        public void GetAchievementInPrefab(Achievement achievement)
        {
            if (!achievement.GetState())
            {
                GameObject instance = Instantiate(prefabAchievement, achievementsGrid);
                Text header = instance.transform.Find(prefabNameHeader).GetComponent<Text>();
                Image icon = instance.transform.Find(prefabNameIcon).GetComponent<Image>();

                instance.GetComponent<AchievementElement>().SetAnimationState(true);
                instance.GetComponent<AchievementElement>().SetAchievementID(achievement.GetID());

                UnlockAchievement(achievement, header, icon);

                Destroy(instance, timeOfDestroying);
            }
        }

        /// <summary>
        /// Create achievement by header
        /// </summary>
        /// <param name="headerAchievement"></param>
        /// <param name="headerName"></param>
        /// <param name="iconName"></param>
        public void GetAchievementInPrefabByHeader(string headerAchievement)
        => GetAchievementInPrefab(FindAchievementByHeader(headerAchievement));

        /// <summary>
        /// Create achievement by ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="headerName"></param>
        /// <param name="iconName"></param>
        public void GetAchievementInPrefabByID(int id)
        => GetAchievementInPrefab(FindAchievementByID(id));

        /// <summary>
        /// Get selected achievement
        /// </summary>
        /// <param name="achievement"></param>
        public void GetAchievement(Achievement achievement) 
        => UnlockAchievement(achievement, achievement_header, achievement_icon);

        /// <summary>
        /// Get achievement by his header
        /// </summary>
        /// <param name="headerAchievement"></param>
        public void GetAchievement(string headerAchievement)
        => UnlockAchievement(FindAchievementByHeader(headerAchievement), achievement_header, achievement_icon);

        /// <summary>
        /// Get achievement by his ID
        /// </summary>
        /// <param name="idAchievement"></param>
        public void GetAchievement(int idAchievement) 
        => UnlockAchievement(FindAchievementByID(idAchievement), achievement_header, achievement_icon);


        /// <summary>
        /// Add progress to selected achievement
        /// </summary>
        /// <param name="achievement"></param>
        /// <param name="count"></param>
        public void AddProgressAchievement(ProgressAchievement achievement, byte count)
        {
            if (!achievement.GetState())
            {
                achievement.AddProgress(count);
                achievement.Check();
                Notify(prefabAchievement, achievement.GetID(), true);

                if (achievement.Check())
                {
                    Notify(prefabAchievement, achievement.GetID(), false);
                    Output(achievement_header, achievement_icon, achievement);
                }
            }
        }

        /// <summary>
        /// Add some progress to achievement by header
        /// </summary>
        /// <param name="headerAchievement"></param>
        /// <param name="count"></param>
        public void AddProgressAchievementByHeader(string headerAchievement, byte count)
        => AddProgressAchievement(FindAchievementByHeader(headerAchievement) as ProgressAchievement, count);

        /// <summary>
        /// Add some progress to achievement by ID
        /// </summary>
        /// <param name="idAchievement"></param>
        /// <param name="count"></param>
        public void AddProgressAchievementByID(int idAchievement, byte count)
        => AddProgressAchievement(FindAchievementByID(idAchievement) as ProgressAchievement, count);

        public void AddObserver(IObserver observer) => observers.Add(observer);

        public void RemoveObserver(IObserver observer) => observers.Remove(observer);

        /// <summary>
        /// Notify all observers
        /// </summary>
        public void Notify(GameObject prefabAchievement, int idAchievement, bool callForProgress)
        {
           foreach(IObserver observer in observers)
            {
                if (callForProgress) observer.OnProgressUpdate(prefabAchievement, idAchievement);
                else observer.OnUpdate(prefabAchievement, idAchievement);
            }
        }
    }
}