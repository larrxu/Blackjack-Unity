using UnityEngine;
using System.Collections;

namespace Animation
{
    public class Lerp : MonoBehaviour
    {
        /// <summary>
        /// The time taken to move from the start to finish positions
        /// </summary>
        public float TimeTakenDuringLerp = 1f;

        //Whether we are currently interpolating or not
        private bool _isLerping;

        //The start and finish positions for the interpolation
        private Vector3 _startPosition;
        private Vector3 _endPosition;

        //The Time.time value when we started the interpolation
        private float _timeStartedLerping;

        /// <summary>
        /// Called to begin the linear interpolation
        /// </summary>
        public void StartLerping(Vector3 endPosition)
        {
            if (_isLerping)
            {
                return;
            }
            _isLerping = true;
            _timeStartedLerping = Time.time;
            
            _startPosition = transform.position;
            _endPosition = endPosition;
        }
        
        //We do the actual interpolation in FixedUpdate(), since we're dealing with a rigidbody
        void FixedUpdate()
        {
            if (_isLerping)
            {
                //We want percentage = 0.0 when Time.time = _timeStartedLerping
                //and percentage = 1.0 when Time.time = _timeStartedLerping + timeTakenDuringLerp
                //In other words, we want to know what percentage of "timeTakenDuringLerp" the value
                //"Time.time - _timeStartedLerping" is.
                float timeSinceStarted = Time.time - _timeStartedLerping;
                float percentageComplete = timeSinceStarted / TimeTakenDuringLerp;
                
                //Perform the actual lerping.  Notice that the first two parameters will always be the same
                //throughout a single lerp-processs (ie. they won't change until we hit the space-bar again
                //to start another lerp)
                transform.position = Vector3.Lerp(_startPosition, _endPosition, percentageComplete);
                
                //When we've completed the lerp, we set _isLerping to false
                if (percentageComplete >= 1.0f)
                {
                    _isLerping = false;
                }
            }
        }
    }
}