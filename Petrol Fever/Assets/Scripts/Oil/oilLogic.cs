using System;
using System.Collections;
using UnityEngine;
using Random = System.Random;

namespace Oil
{
    public class oilLogic : MonoBehaviour
    {
        private int capacity;
        public int numberOfOilBlocks;
        private int oilNow;
        private bool isBeingDrilled = false;
        public void startDrilling()
        {
            if (!isBeingDrilled) {
                return;
            }
            isBeingDrilled = true;
            StartCoroutine(drilling());
        }
        
        IEnumerator drilling()
        {
            if (GameManager.checkOilMaxWithNow()) { 
                GameManager.addOil(1);
            }
            oilNow--;
            if (oilNow <= 0) {
                StopCoroutine(drilling());
            }
            yield return new WaitForSeconds(4);
        }

        void Start() {
            Random rnd = new Random();
            capacity = numberOfOilBlocks * rnd.Next(50, 100);
            oilNow = capacity;
        }
    }
}