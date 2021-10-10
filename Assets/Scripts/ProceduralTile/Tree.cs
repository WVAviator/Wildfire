using UnityEngine;

namespace Wildfire
{
    public class Tree : MonoBehaviour
    {
        [SerializeField] GameObject healthyTree;
        [SerializeField] GameObject deadTree;

        public void BurnDown()
        {
            healthyTree.SetActive(false);
            deadTree.SetActive(true);
        }

        public bool IsDead()
        {
            return deadTree.activeSelf;
        }

    }
}
