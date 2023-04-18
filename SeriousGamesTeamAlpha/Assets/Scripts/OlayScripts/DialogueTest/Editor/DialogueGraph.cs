using System;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace OlayScripts.DialogueTest
{
    public class DialogueGraph : EditorWindow
    {
        private DialogueGraphView _graphView;
        private string _fileName;
        [MenuItem("Graph/Dialogue Graph")]
        public static void OpenDialogueGraphWindow()
        {
            var window = GetWindow<DialogueGraph>();
            window.titleContent = new GUIContent("Dialogue Graph");
            
        }

        private void OnEnable()
        {
           ConstructGraphView();
           GenerateToolBar();
        }

        private void GenerateToolBar()
        {
            var toolbar = new Toolbar();

            var fileNameTextField = new TextField("File Name:");
            fileNameTextField.SetValueWithoutNotify(_fileName);
            fileNameTextField.MarkDirtyRepaint();
            fileNameTextField.RegisterValueChangedCallback(evt => _fileName = evt.newValue);
            toolbar.Add(fileNameTextField);
            
            toolbar.Add(new Button(() => RequestDataOperation(true)){text = "Save Data"});
            toolbar.Add(new Button(() => RequestDataOperation(false)){text = "Load Data"});
            
            var nodeCreateButton = new Button(() =>
            {
                _graphView.CreateNode("Dialogue Node");
            });

            nodeCreateButton.text = "Create Dialogue Node";
            toolbar.Add(nodeCreateButton);
            rootVisualElement.Add(toolbar);
        }

        private void RequestDataOperation(bool save)
        {
            if (string.IsNullOrEmpty(_fileName))
            {
                EditorUtility.DisplayDialog("Invalid File Name!", "Please Enter A Valid File Name.", "OK");
                return;
            }

            var saveUtility = GraphSaveUtility.GetInstance(_graphView);
            if (save)
            {
                saveUtility.SaveGraph(_fileName);
            }

            else
            {
                saveUtility.LoadGraph(_fileName);
            }
        }
        
        

        private void OnDisable()
        {
            rootVisualElement.Remove(_graphView);
        }

        private void ConstructGraphView()
        {
            _graphView = new DialogueGraphView
            {
                name = "Dialogue Graph"
            };
            
            _graphView.StretchToParentSize();
            rootVisualElement.Add(_graphView);
        }

        
    }
    
    
}