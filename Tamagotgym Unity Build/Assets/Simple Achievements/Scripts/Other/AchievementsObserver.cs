using UnityEngine;
using SimpleAchievements.Main;

namespace SimpleAchievements.Other
{

    /// <summary>
    /// Demo observer class
    /// </summary>
    public sealed class AchievementsObserver : MonoBehaviour, IObserver
    {
        [SerializeField]
        private AudioSource achievementUnlockSound;

        private delegate void UnlockEffect();

        private GameObject achievement;
        private event UnlockEffect UnlockAchievementEvent;

        private void Start()
        {
            AchievementsControl.Instance.AddObserver(this);

            InitAllUnlockEvents();

        }

        private void InitAllUnlockEvents()
        {
            UnlockAchievementEvent += SoundPlay;
            UnlockAchievementEvent += AnimationPlay;
        }

        private void SoundPlay() => achievementUnlockSound.Play();
        private void AnimationPlay() => achievement.GetComponent<AchievementElement>().Fade();
        

        public void OnUpdate(GameObject prefabAchievement, int idAchievement)
        {
            achievement = prefabAchievement;

            UnlockAchievementEvent.Invoke();
        }

        public void OnProgressUpdate(GameObject prefabAchievements, int idAchievements)
        {
            
        }
    }

}