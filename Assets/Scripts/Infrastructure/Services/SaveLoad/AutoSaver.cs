using System;
using System.Collections;
using UnityEngine;

namespace Internal.Scripts.Infrastructure.Services.SaveLoad
{
    public class AutoSaver : MonoBehaviour
    {
        private ISaveLoadService _saveLoadService;
        private Coroutine _autoSavingRoutine;
        private float _saveInterval;
        
        public void StartSaving(ISaveLoadService saveLoadService, float interval)
        {
            _saveLoadService = saveLoadService;
            
            if (_autoSavingRoutine != null)
                StopCoroutine(_autoSavingRoutine);
            
            _autoSavingRoutine = StartCoroutine(MakeSavings());
        }

        private IEnumerator MakeSavings()
        {
            while (gameObject != null)
            {
                yield return new WaitForSeconds(_saveInterval);
                _saveLoadService.SaveProgress();
            }
        }

        private void OnApplicationQuit()
        {
            _saveLoadService.SaveProgress();
        }
    }
}