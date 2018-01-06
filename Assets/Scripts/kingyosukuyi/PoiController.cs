using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GoldfishScoping {
    public class PoiController : MonoBehaviour {

        public Fish currFish;
        private void Awake () {
            currFish = null;
        }
        private void OnTriggerStay (Collider other) {
            if (!other.isTrigger) {
                currFish = other.GetComponentInParent<Fish> ();
            }
        }
        private void OnTriggerExit (Collider other) {
            if (!other.isTrigger)
                currFish = null;
        }
    }
}
