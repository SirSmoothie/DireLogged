using UnityEngine;
using DireLoggedSystem;

namespace DireLoggedSystem
{
    public class ActivateDialogue : MonoBehaviour
    {
        public DialogueSystem dialogueSystem;

        void Update()
        {
            if (Input.GetKeyDown("space"))
            {
                dialogueSystem.Interacted();
            }
        }
    }
}
