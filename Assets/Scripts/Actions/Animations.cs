using UnityEngine;

namespace Actions
{
    public class Animations
    {
        private static readonly int Run = Animator.StringToHash("Run");
        private static readonly int Walk = Animator.StringToHash("Walk");
        private static readonly int Back = Animator.StringToHash("Back");
        private static readonly int Jump = Animator.StringToHash("Jump");
        private static readonly int Action = Animator.StringToHash("Action");

        public static void Player(Animator animator)
        {

            var walk = (Input.GetKey(KeyCode.W) 
                       || Input.GetKey(KeyCode.UpArrow))
                       && !Input.GetKey(KeyCode.RightShift) 
                       && !Input.GetKey(KeyCode.Space);
            
            var run = (Input.GetKey(KeyCode.W) 
                      || Input.GetKey(KeyCode.UpArrow))
                      && Input.GetKey(KeyCode.RightShift);

            var jump = (Input.GetKey(KeyCode.W) 
                      || Input.GetKey(KeyCode.UpArrow))
                      && Input.GetKey(KeyCode.Space);
            
            var back = (Input.GetKey(KeyCode.S)
                        || Input.GetKey(KeyCode.DownArrow));
            
            animator.SetBool(Walk, walk);
            animator.SetBool(Run, run);
            animator.SetBool(Back, back);
            animator.SetBool(Jump, jump);
        }

        public static void Enemy(Animator animator, int action)
            => animator.SetInteger(Action, action);
    }
}