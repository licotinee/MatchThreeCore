



using UnityEngine;

namespace DVAH2ten.Core
{
    // This class is responsible for loading the next scene in a transition.
    public class SceneTransition : MonoBehaviour
    {
        public string scene = "<Insert scene name>";
        public float duration = 1.0f;
        public Color color = Color.black;

        /// <summary>
        /// Performs the transition to the next scene.
        /// </summary>
        public void PerformTransition()
        {
            Transition.LoadLevel(scene, duration, color);
        }
    }
}
