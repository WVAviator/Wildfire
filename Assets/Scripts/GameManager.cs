using System;
using UnityEngine;

namespace Wildfire
{
    public class GameManager : MonoBehaviour
    {
        private void Awake()
        {
            SelectionStateManager.SetState(new DefaultSelectionState());
            Fire.OnAllFiresExtinguished += LevelComplete;
        }
    
        public static event Action OnAdvanceNextTurn = delegate { };
        public static void AdvanceNextTurn() => OnAdvanceNextTurn?.Invoke();

        void LevelComplete()
        {
            Debug.Log("You win!!!");
        }
        
    }
}

