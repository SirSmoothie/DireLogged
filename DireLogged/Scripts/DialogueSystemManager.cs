using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DireLoggedSystem;

namespace DireLoggedSystem
{
    [System.Serializable]
    public class Buttons
    {
        public TextMeshProUGUI buttonText;
        public Button button;
    }

    public class DialogueSystemManager : MonoBehaviour
    {
        public bool drawDialogueTree = false;

        public CanvasGroup dialogueCanvas;
        public TextMeshProUGUI textBox;
        public GameObject QuestionsCanvas;
        private DialogueSystem LoadedDialogueSystem;
        private int LoadedDialogueNode;
        public Buttons[] buttons;

        private bool dialogueActive = false;
        private float currentDialogueDuration;
        private float currentTime;

        private void OnEnable()
        {
            dialogueCanvas.gameObject.SetActive(true);
            dialogueCanvas.alpha = 0;
        }

        public void StartText(DialogueSystem dialogueSystem, int dialogueNodeIndex)
        {
            LoadedDialogueNode = dialogueNodeIndex;
            LoadedDialogueSystem = dialogueSystem;
            textBox.text = LoadedDialogueSystem.ReturnText();
            StartDialogue();
            currentDialogueDuration = LoadedDialogueSystem.dialogueNode[LoadedDialogueNode].dialogueDuration;
            currentTime = 0f;
            if (LoadedDialogueSystem.dialogueNode[LoadedDialogueNode].IsQuestion)
            {
                dialogueActive = false;
                QuestionsCanvas.SetActive(true);
                SetOptions();
            }
            else
            {
                QuestionsCanvas.SetActive(false);
                dialogueActive = true;
            }
        }

        public void TurnOffDialogue()
        {
            dialogueCanvas.alpha = 0;
        }

        public void StartDialogue()
        {
            dialogueActive = true;
            dialogueCanvas.alpha = 1;
        }

        void SetOptions()
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].button.gameObject.SetActive(false);
            }

            for (int i = 0; i < LoadedDialogueSystem.dialogueNode[LoadedDialogueNode].questions.Length; i++)
            {
                buttons[i].button.gameObject.SetActive(true);
                buttons[i].buttonText.text = LoadedDialogueSystem.dialogueNode[LoadedDialogueNode].questions[i].Text;
            }
        }

        public void ButtonClicked(int numberClicked)
        {
            LoadedDialogueSystem.dialogueNode[LoadedDialogueNode].questions[numberClicked].DialogueToContinue
                .Interacted();
        }

        private void Update()
        {
            if (!dialogueActive) return;
            currentTime += Time.fixedDeltaTime;
            if (currentTime > currentDialogueDuration)
            {
                if (LoadedDialogueSystem.dialogueNode[LoadedDialogueNode].endOfDialogue)
                {
                    dialogueActive = false;
                    TurnOffDialogue();
                    LoadedDialogueNode = 0;
                    return;
                }

                LoadedDialogueSystem.NextDialogue();
            }
        }
    }
}
