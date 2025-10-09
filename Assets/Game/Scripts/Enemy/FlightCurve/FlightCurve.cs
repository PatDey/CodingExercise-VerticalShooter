using UnityEngine;
using UnityEngine.Splines;

namespace CEVerticalShooter
{
    [RequireComponent(typeof(SplineContainer))]
    public class FlightCurve : MonoBehaviour
    {
        [SerializeField]
        private Vector2 minCurveOffset;
        [SerializeField]
        private Vector2 maxCurveOffset;

        private SplineContainer _splineContainer;

        public Vector2 MinCurveOffset => minCurveOffset;
        public Vector2 MaxCurveOffset => maxCurveOffset;
        public SplineContainer SplineContainer => _splineContainer;

        public void Awake()
        {
            _splineContainer = GetComponent<SplineContainer>();
        }
    }
}
