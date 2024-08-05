using System.IO;
using System.Text;
using Infrastructure.Services.LevelService;
using Unity.Plastic.Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

namespace Editor.CustomEditors
{
    [CustomEditor(typeof(LevelServiceConfig))]
    public class LevelServiceConfigEditor : UnityEditor.Editor
    {
        private const string JSON_FILE_PATH = "Assets/Resources/Levels.json";
        private const string CONVERT_TO_JSON = "Convert to JSON";
        private const string LOAD_FROM_JSON = "Load from JSON";
        
        public override void OnInspectorGUI() 
        {
            base.OnInspectorGUI();

            if (GUILayout.Button(CONVERT_TO_JSON))
            {
                var levelServiceConfig = GetLevelServiceConfig();
                if (levelServiceConfig != null)
                {
                    var json = JsonConvert.SerializeObject(levelServiceConfig.AllLevels, Formatting.Indented);
                    using (FileStream fs = new FileStream(JSON_FILE_PATH, FileMode.Create))
                    {
                        using (StreamWriter writer = new StreamWriter(fs))
                        {
                            writer.Write(json);
                        }
                    }
                    AssetDatabase.Refresh ();
                }
                else
                {
                    Debug.LogError($"{nameof(LevelServiceConfig)} is null!");
                }
            }
            
            if (GUILayout.Button(LOAD_FROM_JSON))
            {
                var levelServiceConfig = GetLevelServiceConfig();
                var file = Resources.Load<TextAsset>("Levels");

                if (file == null)
                {
                    Debug.LogError($"Can not read from path: {JSON_FILE_PATH}");
                    return;
                }
                levelServiceConfig.AllLevels = JsonConvert.DeserializeObject<LevelConfig[]>(file.text);
            }
        }

        private LevelServiceConfig GetLevelServiceConfig()
        {
            SerializedProperty property = serializedObject.FindProperty("_allLevels");
            var targetObject = property.serializedObject.targetObject;
            var levelServiceConfig = targetObject as LevelServiceConfig;
            return levelServiceConfig;
        }
    }
}
