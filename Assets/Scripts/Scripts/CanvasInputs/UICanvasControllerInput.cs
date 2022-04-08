using UnityEngine;

namespace StarterAssets
{
    public class UICanvasControllerInput : MonoBehaviour
    {

        [Header("Output")]
        private PlayerMovement starterAssetsInputs;

        private void Start()
        {
            starterAssetsInputs = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();

        }
        private void Update()
        {
           if (starterAssetsInputs == null)
                starterAssetsInputs = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();

        }

        public void VirtualMoveInput(Vector2 virtualMoveDirection)
        {
            starterAssetsInputs.MoveInput(virtualMoveDirection);
        }

       
        public void VirtualJumpInput(bool virtualJumpState)
        {
            starterAssetsInputs.JumpInput(virtualJumpState);
        }

        
        
    }

}
