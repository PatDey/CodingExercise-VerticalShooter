using UnityEngine;
using VContainer;

namespace CEVerticalShooter.Game
{
    public class PlaneController : MonoBehaviour
    {
        ICharacterHandler _characterHandler;

        [Inject]
        private void Construct(ICharacterHandler characterHandler)
        {
            _characterHandler = characterHandler;
        }

        private void Update()
        {
            transform.position += (Vector3)_characterHandler.Move();
        }
    }
}
