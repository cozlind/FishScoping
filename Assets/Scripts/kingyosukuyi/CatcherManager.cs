using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GoldfishScoping {
    public class CatcherManager : ControllerManager {


        Rigidbody rb;
        Vector3 rot;
        public float moveSpeed=2;
        public Vector3 v;
        public BoxCollider collider;
        public bool isScoping = false;
        public bool isUsePoi = false;
        public PoiController poi;
        
        private void Start () {
            rb = GetComponent<Rigidbody> ();
            resetPos = transform.position;
            Invoke("Calibration",0.9f);
        }
        void Calibration () {
            benchmark.Set (pitch, 0, roll);
        }
        void FixedUpdate () {
            UpdateData ();
            rot.Set (-eulerAngles.x, 0, eulerAngles.z);
            transform.eulerAngles = rot;

            roll = Mathf.Abs (roll - benchmark.z) < 4 ? 0 : roll - benchmark.z;
            pitch = Mathf.Abs (pitch - benchmark.x) < 4 ? 0 : pitch - benchmark.x;
            //Debug.Log (roll+":"+ pitch);


            //move
            v = new Vector3(-roll * moveSpeed * Time.fixedDeltaTime,0, -pitch * moveSpeed * Time.fixedDeltaTime);
            if (fr1 > 1 || fl5 > 1) v = Vector3.zero;
            if (fr2 > 1 || fl4 > 1) {
                v = Vector3.up * moveSpeed * 10 * Time.fixedDeltaTime;
                isUsePoi = true;
                if (poi.currFish!=null)  isScoping = true;
            } else {
                isUsePoi = false;
                isScoping = false;
                transform.position = new Vector3 (transform.position.x, -1.5f, transform.position.z);
            }
            if (collider.bounds.Contains (transform.position + v))
                transform.position += v; 
        }
    }
}
