using UnityEngine;

namespace SimpleAchievements.Main
{
    [CreateAssetMenu(fileName ="SimpleAchievements", menuName = "Simple Achievements/Achievement")]
    public class Achievement : ScriptableObject
    {
        [Header("This is achievement ID")]
        [SerializeField]
        private int id;
        [SerializeField]
        [Header("This is achievement header")]
        private string header;
        [SerializeField]
        [Header("This is achievement icon lock and unlock")]
        private Sprite icon_lock, icon_unlock;
        [SerializeField]
        [Header("This is achievement state")]
        protected bool isUnlocked = false;

        public bool SetState(bool state) => isUnlocked = state;
        public bool GetState() => isUnlocked;
        public Sprite GetUnlockedIcon() => icon_unlock;
        public Sprite GetLockedIcon() => icon_lock;
        public string GetHeader() => header;

        /// <summary>
        /// Reset all changes for this achievements (state, progress, etc.)
        /// </summary>
        public virtual void Reset()
        {
            isUnlocked = false;
        }

        public int GetID() => id;
    }
}