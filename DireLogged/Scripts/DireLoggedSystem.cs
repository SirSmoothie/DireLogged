using System;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;
using DireLoggedSystem;

namespace DireLoggedSystem
{

    public class DireLoggedSystem : EditorWindow
    {
        [MenuItem("Tools/DireLoggedSystem")]
        static void Init()
        {
            var window = GetWindow<DireLoggedSystem>();
            window.position = new Rect(0, 0, 800, 400);
            window.Show();
        }

        private void OnGUI()
        {
            if (GUILayout.Button("Spawn DireLogged System Preset", GUILayout.Height(200), GUILayout.Height(100)))
            {
                Object DireLoggedSystem = AssetDatabase.LoadAssetAtPath("Assets/DireLogged/DireLoggedSystem.prefab", typeof(Object));
                Instantiate(DireLoggedSystem);
            }
            GUILayout.Label("Note: this does not come with any dialogue. You WILL need to add in the scripts yourself.");
            GUILayout.Label("Also ensure each instance of the dialogueSystem has the dialogueSystemManager script set in inspector.");
            GUILayout.Space(20);
            if(GUILayout.Button("Spawn DireLogged System Exmaple Dialogue", GUILayout.Height(200), GUILayout.Height(100)))
            {
                Object DireLoggedSystemExample = AssetDatabase.LoadAssetAtPath("Assets/DireLogged/DireLoggedSystemExample.prefab", typeof(Object));
                Instantiate(DireLoggedSystemExample);
            }
            GUILayout.Label("Press 'space' to activate the dialogue.");
        }
    }
}
