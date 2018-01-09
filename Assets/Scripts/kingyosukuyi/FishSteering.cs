using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GoldfishScoping {
    public class FishSteering : MonoBehaviour {

        public Rigidbody rb;
        public Vector3 wanderTarget;
        private Ray wallAvoidanceRay;
        private RaycastHit wallHit;
        public Vector3 acceleration;
        public Vector3 velocity;
        public float mass = 1;
        private bool isHitWall=false;

        private void Awake () {
            rb = GetComponent<Rigidbody> ();
            neighbors = new List<Transform> ();

            wanderTarget = Params.Instance.wanderRadius * Random.insideUnitCircle;
            wanderTarget=new Vector3 (wanderTarget.x, 0, wanderTarget.y);
        }

        public void Execute () {
            float smoothRate;
            Vector3 force = Calculate ();
            Vector3 newAcceleration = force / mass;

            smoothRate = Mathf.Clamp (9 * Time.fixedDeltaTime, 0.15f, 0.4f) / 2.0f;
            BlendIntoAccumulator (smoothRate, newAcceleration, ref acceleration);

            velocity += acceleration * Time.fixedDeltaTime;
            velocity = Vector3.ClampMagnitude (velocity, Params.Instance.maxSpeed);
            if (Params.Instance.wall.bounds.Contains (rb.position + velocity)) {
                rb.velocity = velocity;

                if (rb.position.y > 1.1f) {
                    rb.velocity += -9.8f * Vector3.up;
                }else if (rb.position.y < 0.9f) {
                        rb.velocity += 9.8f * Vector3.up;
                    }

            }
            transform.forward = velocity;
        }
        private void BlendIntoAccumulator (float smoothRate, Vector3 newValue, ref Vector3 smoothedAccumulator) {
            smoothedAccumulator = Vector3.Lerp (smoothedAccumulator, newValue, Mathf.Clamp01 (smoothRate));
        }
        private bool accumulateForce (ref Vector3 runningTotal, Vector3 force) {
            float soFar = runningTotal.magnitude;

            float remaining = Params.Instance.maxForce - soFar;
            if (remaining <= 0) {
                return false;
            }

            if (force.magnitude < remaining) {
                runningTotal += force;
            } else {
                runningTotal += Vector3.Normalize (force) * remaining;
            }
            return true;
        }

        public Vector3 Calculate() {
            Vector3 force = Vector3.zero;
            Vector3 steeringForce = Vector3.zero;

            if (Params.Instance.wallAvoidanceEnabled) {
                force = WallAvoidance () * Params.Instance.wallAvoidanceWeight;
                if (!accumulateForce (ref steeringForce, force)) {
                    return steeringForce;
                }
            }

            if (Params.Instance.wanderEnabled) {
                force = Wander () * Params.Instance.wanderWeight ;
                if(!accumulateForce(ref steeringForce, force)) {
                    return steeringForce;
                }
            }
            
            if (Params.Instance.separationEnabled) {
                force = Separation () * Params.Instance.separationWeight;
                if (!accumulateForce (ref steeringForce, force)) {
                    return steeringForce;
                }
            }

            if (Params.Instance.alignmentEnabled) {
                force = Alignment () * Params.Instance.alignmentWeight;
                if (!accumulateForce (ref steeringForce, force)) {
                    return steeringForce;
                }
            }

            if (Params.Instance.cohesionEnabled) {
                force = Cohesion () * Params.Instance.cohesionWeight;
                if (!accumulateForce (ref steeringForce, force)) {
                    return steeringForce;
                }
            }

            return steeringForce;
        }

        public Vector3 Wander () {
            float jitterThisTimeSlice = Params.Instance.wanderJitter * Time.fixedDeltaTime;
            Vector3 toAdd = Random.insideUnitCircle * jitterThisTimeSlice;
            wanderTarget += new Vector3 (toAdd.x,0,toAdd.y);
            wanderTarget.Normalize ();
            wanderTarget*= Params.Instance.wanderRadius;
            //wanderTarget.Scale (new Vector3(1/transform.localScale.x,1/transform.localScale.y,1/transform.localScale.z));
            Vector3 localTarget = wanderTarget + Vector3.forward * Params.Instance.wanderDistance;
            Vector3 worldTarget = transform.TransformPoint (localTarget);
            Vector3 move = worldTarget - transform.position;
            move.Set (move.x, 0, move.z);
            return move;
        }

        public Vector3 WallAvoidance () {
            Vector3 force = Vector3.zero;

            wallAvoidanceRay = new Ray (transform.position, transform.forward);
            if (isHitWall = Physics.Raycast (wallAvoidanceRay, out wallHit, Params.Instance.wallSeekDist, 1 << LayerMask.NameToLayer ("Wall"))) {
                Vector3 seekPoint = wallAvoidanceRay.GetPoint (Params.Instance.wallSeekDist);
                float intersectDist = (wallHit.point - seekPoint).magnitude;
                Vector3 normal = new Vector3 (wallHit.normal.x, 0, wallHit.normal.z);
                force = normal * intersectDist;
            } 

            return force;
        }
        //flocking
        public List<Transform> neighbors;
        private void OnTriggerEnter (Collider other) {
            if(other.isTrigger==true)
                neighbors.Add (other.transform);
        }
        private void OnTriggerExit (Collider other) {
            if(other.isTrigger==true)
                neighbors.Remove (other.transform);
        }
        public Vector3 Separation () {
            Vector3 steeringAcce = Vector3.zero;
            for(int i = 0; i < neighbors.Count; i++) {
                if (neighbors[i] == null) {
                    neighbors.RemoveAt (i);
                    if (i >= neighbors.Count) break;
                }
                steeringAcce += transform.position - neighbors[i].position;
            }
            steeringAcce.Set (steeringAcce.normalized.x, 0, steeringAcce.normalized.z);
            return steeringAcce;
        }
        public Vector3 Alignment () {
            Vector3 heads = Vector3.zero;
            for (int i = 0; i < neighbors.Count; i++) {
                heads += neighbors[i].forward;
            }
            Vector3 move = (heads / neighbors.Count - transform.forward).normalized;
            move.Set (move.x, 0, move.z);
            return move;
        }
        public Vector3 Cohesion () {
            Vector3 centers = Vector3.zero;
            for (int i = 0; i < neighbors.Count; i++) {
                centers += neighbors[i].position;
            }
            return Seek (centers / neighbors.Count);
        }
        public Vector3 Seek(Vector3 target) {
            Vector3 desired = (target - transform.position).normalized * Params.Instance.maxSpeed;
            Vector3 move= desired - rb.velocity;
            move.Set (move.x, 0, move.z);
            return move;
        }

        private void OnDrawGizmos () {
            //Environment
            Gizmos.color = Color.white;
            Gizmos.DrawWireCube (Params.Instance.wall.transform.position, Params.Instance.wall.bounds.size);
            //Wander
            if (Params.Instance.wanderEnabled) {
                Gizmos.color = new Color (0.5f, 0.9f, 1, 0.4f);
                Gizmos.DrawSphere (transform.TransformPoint (Vector3.forward * Params.Instance.wanderDistance), Params.Instance.wanderRadius);
                Gizmos.color = new Color (0.1f, 0.1f, 1f, 0.9f);
                Gizmos.DrawSphere (transform.TransformPoint (wanderTarget + Vector3.forward * Params.Instance.wanderDistance), 0.01f);
                Gizmos.color = new Color (0.1f, 0.1f, 1f, 0.9f);
                Gizmos.DrawLine (transform.TransformPoint (Vector3.forward * Params.Instance.wanderDistance), transform.TransformPoint (wanderTarget + Vector3.forward * Params.Instance.wanderDistance));
            }
            //Wall Avoidance
            if (Params.Instance.wallAvoidanceEnabled) {
                Gizmos.color = new Color (1f, 0.1f, 0.1f, 1f);
                Gizmos.DrawLine (wallAvoidanceRay.origin, wallAvoidanceRay.GetPoint (Params.Instance.wallSeekDist));
                Gizmos.color = new Color (1f, 0.1f, 1f, 1f);
                if(isHitWall)Gizmos.DrawLine (wallHit.point, wallHit.point+wallHit.normal* ((wallHit.point - wallAvoidanceRay.GetPoint (Params.Instance.wallSeekDist)).magnitude));
            }
        }
    }
}
