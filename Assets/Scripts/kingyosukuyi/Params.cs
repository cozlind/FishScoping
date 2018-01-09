using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GoldfishScoping {
    public class Params : MonoBehaviour {

        private static Params instance;
        public static Params Instance {
            get {
                return instance;
            }
        }
        private void Awake () {
            instance = this;
        }

        [Header ("State")]
        public float idleTime = 10;
        public float swimTime = 20;
        public float maxForce = 10;
        public float maxSpeed = 20;
        [Header ("Debug")]
        public bool wanderEnabled = true;
        public bool wallAvoidanceEnabled = true;
        public bool separationEnabled = true;
        public bool alignmentEnabled = true;
        public bool cohesionEnabled = true;
        [Header ("Steering Parameters")]
        public Collider wall;
        [Range (0f, 5f)]
        public float wallAvoidanceWeight = 1;
        [Range (0f, 5f)]
        public float wanderWeight = 1;
        [Range (0f, 5f)]
        public float separationWeight = 1;
        [Range (0f, 5f)]
        public float alignmentWeight = 1;
        [Range (0f, 5f)]
        public float cohesionWeight = 1;
        [Header ("Wander")]
        public float wanderJitter = 20;
        public float wanderRadius = 10;
        public float wanderDistance = 15;
        public float globalUp = 0.2f;
        public float accelUp = 0.05f;
        [Header ("Avoidance")]
        public float wallSeekDist = 25;
        public float size = 50;
    }
}
