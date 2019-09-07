using UnityEngine;

namespace SimpleAchievements.Other
{
    [AddComponentMenu("SimpleAchievements/Achievement Element")]
    /// <summary>
    /// Use this class for achievement prefab to auto destroy in scene
    /// </summary>
    public sealed class AchievementElement : MonoBehaviour
    {
        [SerializeField]
        [Header("Trigger in achievement animator ('Fade' name by default. Set empty value if you don't use Animator)")]
        private string animatorTriggerFadeAnimationName = "Fade";
        [SerializeField]
        [Header("If you instantiating prefab achievement set 'true' value")]
        private bool playAnimationInStart = false;

        private Animator animator;
        private int achievementID;

        public int GetAchievementID() => achievementID;
        public int SetAchievementID(int id) => achievementID = id;

        public bool SetAnimationState(bool state) => playAnimationInStart = state;

        private void Start()
        {
            if(playAnimationInStart) Fade();
        }

        public void Fade()
        {
            if (animatorTriggerFadeAnimationName != string.Empty)
            {
                animator = GetComponent<Animator>();

                animator.SetTrigger(animatorTriggerFadeAnimationName);
            }
        }
    }
}