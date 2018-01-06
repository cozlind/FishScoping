using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GoldfishScoping {
    public class StateMachine<T> {
        public T owner;
        public State<T> currentState;
        public State<T> previousState;
        public State<T> globalState;
        public StateMachine (T o) {
            owner = o;
        }
        public void Update () {
            if (globalState != null) {
                globalState.Execute (owner);
            }
            if (currentState != null) {
                currentState.Execute (owner);
            }
        }

        public bool HandleMessage () {
            if (globalState != null && globalState.OnMessage (owner)) {
                return true;
            }
            if (currentState != null && currentState.OnMessage (owner)) {
                return true;
            }
            return false;
        }

        public void ChangeState(State<T> newState) {
            previousState = currentState;
            currentState.Exit (owner);
            currentState = newState;
            currentState.Enter (owner);
        }

        public void RevertToPreviousState () {
            ChangeState (previousState);
        }

        public bool isInState(State<T> state) {
            if (currentState == state) return true;
            return false;
        }
    }
}
