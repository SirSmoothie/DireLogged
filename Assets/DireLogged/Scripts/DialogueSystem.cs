using System;
using UnityEngine;
using DireLoggedSystem;

namespace DireLoggedSystem
{
    [System.Serializable]
    public class DialogueNode
    {
        public string Text;
        public bool IsQuestion = false;
        public bool endOfDialogue = false;
        public float dialogueDuration = 10f;
        public QuestionNode[] questions;

    }

    [System.Serializable]
    public class QuestionNode
    {
        public string Text;
        public DialogueSystem DialogueToContinue;
    }

    public class DialogueSystem : MonoBehaviour
    {
        //public bool IsDialogueUnlocked = false;
        public DialogueSystemManager manager;
        public DialogueNode[] dialogueNode;
        private int currentDialogueIndex = 0;
        public int PlayerRereadIndex = 0;
        private bool PlayerHasRead = false;

        public void Interacted()
        {
            StartDialogue();
        }

        public void StartDialogue()
        {
            if (PlayerHasRead)
            {
                manager.StartText(gameObject.GetComponent<DialogueSystem>(), PlayerRereadIndex);
            }

            manager.StartText(gameObject.GetComponent<DialogueSystem>(), currentDialogueIndex);

        }

        public string ReturnText()
        {
            return dialogueNode[currentDialogueIndex].Text;
        }

        public void NextDialogue()
        {
            currentDialogueIndex++;
            if (currentDialogueIndex >= dialogueNode.Length)
            {
                PlayerHasRead = true;
            }

            manager.StartText(gameObject.GetComponent<DialogueSystem>(), currentDialogueIndex);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.white;
            Gizmos.DrawCube(transform.position, new Vector3(1, 1, 1));
            if (!manager.drawDialogueTree) return;
            for (int i = 0; i < dialogueNode.Length; i++)
            {
                for (int q = 0; q < dialogueNode[i].questions.Length; q++)
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawLine(transform.position,
                        dialogueNode[i].questions[q].DialogueToContinue.transform.position);

                }
            }
        }
    }
}


