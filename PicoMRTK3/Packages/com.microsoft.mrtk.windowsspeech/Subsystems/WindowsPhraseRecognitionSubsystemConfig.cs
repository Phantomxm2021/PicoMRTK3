// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using UnityEngine;

#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN || UNITY_WSA
using UnityEngine.Windows.Speech;
#endif // UNITY_STANDALONE_WIN || UNITY_WSA || UNITY_EDITOR_WIN

namespace Microsoft.MixedReality.Toolkit.Speech.Windows
{
    /// <summary>
    /// The configuration object for WindowsPhraseRecognitionSubsystem.
    /// </summary>
    [CreateAssetMenu(
        fileName = "WindowsPhraseRecognitionSubsystemConfig.asset",
        menuName = "MRTK/Subsystems/Windows Phrase Recognition Subsystem Config")]
    public class WindowsPhraseRecognitionSubsystemConfig : BaseSubsystemConfig
    {
        [SerializeField, Tooltip("The confidence threshold for the recognizer to return its result.")]
        private WindowsSpeechConfidenceLevel confidenceLevel = WindowsSpeechConfidenceLevel.Medium;

#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN || UNITY_WSA
        /// <summary>
        /// The confidence threshold for the recognizer to return its result.
        /// </summary>
        public ConfidenceLevel ConfidenceLevel
        {
            get => confidenceLevel.ToUnityConfidenceLevel();
            set => confidenceLevel = value.ToWindowsSpeechConfidenceLevel();
        }
#endif // UNITY_STANDALONE_WIN || UNITY_WSA || UNITY_EDITOR_WIN
    }
}
