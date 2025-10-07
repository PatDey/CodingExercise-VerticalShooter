using CEVerticalShooter.Core.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CEVerticalShooter
{
    [CreateAssetMenu(fileName = "SceneSettings", menuName = "ScriptableObjects/SceneSettings", order = 1)]
    public class SceneSettings : ScriptableObject
    {
        [SerializeField] 
        private List<ScenePair> scenes = new List<ScenePair> ();

        public AssetReference GetSceneAsset(SceneID scene)
        {
            ScenePair scenePair = scenes.FirstOrDefault(x => x.scene == scene);

            if(scenePair == null)
            {
                Debug.LogError($"Did not find scene {scene} in sceneSettings");
            }

            return scenePair.sceneReference;
        }
    }

    [Serializable]
    public class ScenePair
    {
        public SceneID scene;
        public AssetReference sceneReference;
    }
}
