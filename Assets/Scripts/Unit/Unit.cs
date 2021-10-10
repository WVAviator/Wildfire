using System;
using System.Collections.Generic;
using UnityEngine;
using Wildfire.TurnActions;

namespace Wildfire
{
    public class Unit : MonoBehaviour
    {
        [Header("Move Settings")]
        [Tooltip("The base number of moves this unit should start with each turn.")]
        [SerializeField] int startTurnActions = 2;
        int remainingTurnActions = 2;

        [Header("Visual Information")] [Tooltip("The image to be used in the icon above the unit and in the UI panel.")]
        public Sprite UnitImage;
        [Tooltip("The base name (this will be appended with a phonetic.")]
        public string UnitName;

        [HideInInspector] public HexTile CurrentHex;
        [HideInInspector] public List<TurnAction> AvailableTurnActions;


        static readonly List<Unit> AllUnits = new List<Unit>();

        void OnEnable() => AllUnits.Add(this);
        void OnDisable() => AllUnits.Remove(this);

        void Awake()
        {
            UnitName = UnitNameGenerator.GenerateUnitName(UnitName);
            GameManager.OnAdvanceNextTurn += NextTurn;
        }

        public static void SelectNextUnit()
        {
            Unit activeUnit = SelectionStateManager.GetState().GetSelectedUnit();
            if (activeUnit == null)
            {
                SelectionStateManager.SetState(new UnitSelectionState(AllUnits[0]));
                return;
            }
            int nextUnitIndex = AllUnits.IndexOf(activeUnit) + 1;
            if (nextUnitIndex >= AllUnits.Count) nextUnitIndex = 0;
            SelectionStateManager.SetState(new UnitSelectionState(AllUnits[nextUnitIndex]));
        }
        public static void SelectPreviousUnit()
        {
            Unit activeUnit = SelectionStateManager.GetState().GetSelectedUnit();

            if (activeUnit == null)
            {
                SelectionStateManager.SetState(new UnitSelectionState(AllUnits[0]));
                return;
            }
            int nextUnitIndex = AllUnits.IndexOf(activeUnit) - 1;
            if (nextUnitIndex < 0) nextUnitIndex = AllUnits.Count - 1;
            SelectionStateManager.SetState(new UnitSelectionState(AllUnits[nextUnitIndex]));
        }

        void NextTurn()
        {
            ResetTurnActions();
        }

        protected virtual void Start()
        {
            SetHexParent();
            AvailableTurnActions = new List<TurnAction>(GetComponents<TurnAction>());
        }

        private void SetHexParent()
        {
            CurrentHex = HexTileMap.FindHexTile(transform.position);
            transform.parent = CurrentHex.transform;
        }

        internal int GetRemainingTurnActions()
        {
            return remainingTurnActions;
        }
        public void ResetTurnActions()
        {
            remainingTurnActions = startTurnActions;
        }

        public bool HasAvailableTurnActions()
        {
            return remainingTurnActions > 0;
        }

        public static event Action<Unit> OnTurnActionCompleted = delegate { };
        /// <summary>
        /// Use this to decrement to moves remaining in a turn. It's for any kind of move.
        /// </summary>
        public void ExecuteTurnAction()
        {
            remainingTurnActions--;
            OnTurnActionCompleted?.Invoke(this);
        }
    }
}