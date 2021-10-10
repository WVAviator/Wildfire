using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wildfire
{
    public class SelectionStateManager
    {

        static SelectionState activeState;
        static SelectionState previousState;

        public static event Action<SelectionState> OnEnterSelectionState = delegate { };
        public static event Action OnSelectionStateChanged = delegate { };
        public static event Action<Unit> OnNewUnitSelection = delegate { };
        public static event Action<SelectionState> OnExitSelectionState = delegate { };

        public static void SetState(SelectionState s)
        {
            OnSelectionStateChanged?.Invoke();

            OnExitSelectionState?.Invoke(activeState);

            previousState = activeState;
            activeState = s;

            ProcessUnitSelectionChange();

            OnEnterSelectionState?.Invoke(activeState);

            Debug.Log("Transition to state: " + activeState);
        }

        private static void ProcessUnitSelectionChange()
        {
            Unit newUnit = activeState.GetSelectedUnit();
            Unit oldUnit = previousState?.GetSelectedUnit();

            if (newUnit != null && newUnit != oldUnit) OnNewUnitSelection?.Invoke(newUnit);
        }

        public static SelectionState GetState()
        {
            if (activeState != null) return activeState;
            activeState = new DefaultSelectionState();
            previousState = new DefaultSelectionState();
            return activeState;
        }

        public static SelectionState GetPreviousState()
        {
            return previousState;
        }

    

        public static readonly Dictionary<StateType, Type> StateDictionary = new Dictionary<StateType, Type>()
        {
            {StateType.Default, typeof(DefaultSelectionState)},
            {StateType.HexTile, typeof(HexTileSelectionState) },
            {StateType.Move, typeof(MoveSelectionState) },
            {StateType.Unit, typeof(UnitSelectionState) },
            {StateType.Water, typeof(WaterSuppressantSelectionState)},
            {StateType.Foam, typeof(FoamSuppressantSelectionState)}
        };



    }

    public enum StateType
    {
        Default, HexTile, Move, Unit, Water, Foam
    }
}