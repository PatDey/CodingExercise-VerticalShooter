using UnityEngine;

namespace CEVerticalShooter
{
    public class ScrollBackground : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField]
        private Transform[] scrollImageTransforms;

        [Header("Settings")]
        [SerializeField]
        private Vector3 startPosition;
        [SerializeField]
        private Vector3 endPosition;
        [SerializeField]
        private float scrollSpeed;

        private void Update()
        {
            foreach (var imageTransform in scrollImageTransforms) 
            {
                imageTransform.position -= new Vector3(0f, scrollSpeed * Time.deltaTime, 0f);

                if(imageTransform.position.y <= endPosition.y)
                    imageTransform.position = startPosition;
            }
        }
    }
}
