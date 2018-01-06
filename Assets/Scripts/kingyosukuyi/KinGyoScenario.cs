using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GoldfishScoping {
    public class KinGyoScenario : MonoBehaviour {

        public CatcherManager catcher;
        public Image energyUI;
        public Text fishNumUI;
        public Image poiUI;
        public Text timeUI;
        private bool isSuccess = false;
        public int score = 0;
        public int time = 120;
        private float startTime = 0;
        public GameObject[] pois;
        public int poiNum = 2;

        private void Awake () {
            startTime = Time.time;
        }

        private void FixedUpdate () {

            time = Mathf.RoundToInt (Time.time - startTime);
            timeUI.text = time.ToString ();

            if (catcher.isScoping) {
                energyUI.fillAmount += Time.fixedDeltaTime;
                if (energyUI.fillAmount >= 1 ) {
                    catcher.isScoping = false;
                    score += catcher.poi.currFish.score;
                    fishNumUI.text = score.ToString();
                    
                    Destroy (catcher.poi.currFish.gameObject);
                }
            } else {
                energyUI.fillAmount = 0;
            }

            if (catcher.isUsePoi) {
                poiUI.fillAmount += Time.fixedDeltaTime / 14;
                if (poiUI.fillAmount >= 1) {
                    pois[poiNum--].SetActive (false);
                    if(poiNum>=0) poiUI.fillAmount = 0;
                }
            } 
        }
    }
}
