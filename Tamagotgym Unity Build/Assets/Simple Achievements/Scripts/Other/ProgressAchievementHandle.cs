using UnityEngine;
using SimpleAchievements.Main;

namespace SimpleAchievements.Other
{
    [AddComponentMenu("SimpleAchievements/Progress Achievement")]
    /// <summary>
    /// Use this class if you need create a progress achievement
    /// </summary>
    public sealed class ProgressAchievementHandle : MonoBehaviour
    {
        [SerializeField]
        [Range(1, 100)]
        private byte countProgress = 10;

        public void AddProgress(ProgressAchievement achievement)
        => AchievementsControl.Instance.AddProgressAchievement(achievement, countProgress);

        public void AddProgress(string header)
        => AchievementsControl.Instance.AddProgressAchievementByHeader(header, countProgress);

        public void AddProgress(int id)
        => AchievementsControl.Instance.AddProgressAchievementByID(id, countProgress);
    }
}
