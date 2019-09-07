using UnityEngine;

namespace SimpleAchievements.Main
{
    /// <summary>
    /// If you need create achievement with progress
    /// </summary>
    [CreateAssetMenu(fileName = "SimpleAchievements", menuName = "Simple Achievements/Progress Achievement")]
    public sealed class ProgressAchievement : Achievement
    {
        public readonly byte MAX_PROGRESS_VALUE = 100;

        [SerializeField]
        [Header("This is achievement progress")]
        [Range(0,100)]
        private byte progress; // If this value equal is 100 -> achievement unlocked

        public byte AddProgress(byte progress) => this.progress += progress;
        public byte GetProgress() => progress;

        public override void Reset()
        {
            base.Reset();
            progress = 0;

        }

        /// <summary>
        /// check progress count for this achievement
        /// </summary>
        /// <returns></returns>
        public bool Check() => isUnlocked = (progress == MAX_PROGRESS_VALUE) ? true : false;

    }
}