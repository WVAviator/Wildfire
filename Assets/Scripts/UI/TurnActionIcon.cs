using UnityEngine;
using UnityEngine.UI;

namespace Wildfire
{
    public class TurnActionIcon : MonoBehaviour
    {
        [Tooltip("The sprite to show when this icon's related selection state is active.")]
        [SerializeField] Image selectedIcon;
        [Tooltip("The selection state with which to associate this icon (will show selected when this state is active).")]
        [SerializeField] StateType stateType;

        void Start()
        {
            SelectionStateManager.OnEnterSelectionState += ShowSelected;
        }

        void ShowSelected(SelectionState ss)
        {
            selectedIcon.enabled = (ss.GetType() == SelectionStateManager.StateDictionary[stateType]);
        }
    }
}