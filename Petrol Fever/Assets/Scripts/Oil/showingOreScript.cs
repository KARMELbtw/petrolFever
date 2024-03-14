using System;
using UnityEngine;

namespace Oil
{
    public class showingOreScript : MonoBehaviour
    {
        private bool oreIsShown = false;
        public void showOre() {
            if (oreIsShown) {
                return;
            }
            Debug.Log("Odkrywanie ropy "+gameObject.name);
            if (this.CompareTag("LeftWall")) {
                this.transform.position += new Vector3(-0.5f, 0, 0);
            }
            else {
                this.transform.position += new Vector3(0, 0, -0.5f);
            }

            ChunkGeneration.oilVeins.Remove(this.gameObject);
            oreIsShown = true;
        }
    }
}