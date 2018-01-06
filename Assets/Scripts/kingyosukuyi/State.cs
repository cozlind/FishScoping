using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GoldfishScoping {
    public class State<T> {

        private static State<T> instance;
        protected State(){
            if (instance != null) return;
            instance = this;
        }
        public static State<T> Instance {
            get { if (instance == null) new State<T> ();
                return instance; }
        }

        public virtual void Enter (T t) { }
        public virtual void Execute (T t) { Debug.Log ("x"); }
        public virtual void Exit(T t) { }
        public virtual bool OnMessage (T t) { return false; }
    }
}