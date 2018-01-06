using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GoldfishScoping {
    public class VertexSin : MonoBehaviour {

        float ymax = 0.006796f;
        float ymin = -0.0065f;
        float zmax = 0.2339117f;
        float zmin = -0.2411392f;
        float xmax = 0.6409438f;
        float xmin = -0.8110144f;


        public float frequency = 16.5f;
        public float amplification = 0.09f;
        public float widthFactor = 1.4f;
        public float heightFactor = 0.003f;

        public bool isRotate = true;
        public float rotateAngle = 4;
        public float rotateFrequency = 5;

        private void Update () {
            Material mat = GetComponent<Renderer> ().material;
            mat.SetFloat ("_Frequency", frequency);
            mat.SetFloat ("_Amplification", amplification);
            mat.SetFloat ("_WidthFactor", widthFactor);
            mat.SetFloat ("_HeightFactor", heightFactor);
            mat.SetFloat ("_RotateFrequency", rotateFrequency);
            mat.SetFloat ("_RotateAngle", rotateAngle);
            mat.SetInt ("_IsRotate", System.Convert.ToInt32(isRotate));
            Mesh mesh = GetComponent<MeshFilter> ().mesh;
            mesh.RecalculateNormals ();
        }
    }
}
