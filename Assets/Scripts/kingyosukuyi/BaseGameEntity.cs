using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GoldfishScoping {
    public abstract class BaseGameEntity {
        public abstract void Update ();
        public abstract bool HandleMessage ();
    }
}