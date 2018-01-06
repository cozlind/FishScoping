using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GoldfishScoping {
    [RequireComponent (typeof (FishSteering))]
    public class Fish : MonoBehaviour {
        [Range(2,15)]
        public int score;
        public StateMachine<Fish> stateMachine;
        [HideInInspector]
        public FishSteering fishSteering;
        private void Awake () {
            stateMachine = new StateMachine<Fish> (this);
            fishSteering = GetComponent<FishSteering> ();
            stateMachine.currentState = FishIdle.Instance;
            stateMachine.currentState.Enter (this);
            stateMachine.globalState = FishGlobalState.Instance;
            stateMachine.globalState.Enter (this);
        }
        private void FixedUpdate () {
            stateMachine.Update ();
        }
    }

    public class FishGlobalState : State<Fish> {
        private static FishGlobalState instance;
        protected FishGlobalState () {
            if (instance != null) return;
            instance = this;
        }
        public static new FishGlobalState Instance {
            get {
                if (instance == null) new FishGlobalState ();
                return instance;
            }
        }

        public override void Enter (Fish fish) {

        }
        public override void Execute (Fish fish) {
        }
        public override void Exit (Fish fish) {

        }
        public override bool OnMessage (Fish fish) {
            return false;
        }
    }
    public class FishIdle : State<Fish> {
        private static FishIdle instance;
        private float continueTime;
        protected FishIdle () {
            if (instance != null) return;
            instance = this;
        }
        public static new FishIdle Instance {
            get {
                if (instance == null) new FishIdle ();
                return instance;
            }
        }
        public override void Execute (Fish fish) {
                fish.stateMachine.ChangeState (FishSwim.Instance);
        }
    }


    public class FishSwim : State<Fish> {
        private static FishSwim instance;
        private float continueTime;
        protected FishSwim () {
            if (instance != null) return;
            instance = this;
        }
        public static new FishSwim Instance {
            get {
                if (instance == null) new FishSwim ();
                return instance;
            }
        }
        public override void Enter (Fish fish) {
            continueTime = Time.time + Random.Range (0.0f, Params.Instance.swimTime);
            Params.Instance.wanderEnabled = true;
            Params.Instance.wallAvoidanceEnabled = true;
        }
        public override void Execute (Fish fish) {
            fish.fishSteering.Execute ();
            if (Time.time > continueTime)
                fish.stateMachine.ChangeState (FishIdle.Instance);
        }
    }

    //public class FishFastSwim : State<Fish> {
    //    private static FishFastSwim instance;
    //    protected FishFastSwim () {
    //        if (instance != null) return;
    //        instance = this;
    //    }
    //    public static new FishFastSwim Instance {
    //        get {
    //            if (instance == null) new FishFastSwim ();
    //            return instance;
    //        }
    //    }
    //    public override void Enter (Fish fish) {

    //    }
    //    public override void Execute (Fish fish) {
    //        Debug.Log ("FishFastSwim");
    //    }
    //    public override void Exit (Fish fish) {

    //    }
    //    public override bool OnMessage (Fish fish) {
    //        return false;
    //    }
    //}
}