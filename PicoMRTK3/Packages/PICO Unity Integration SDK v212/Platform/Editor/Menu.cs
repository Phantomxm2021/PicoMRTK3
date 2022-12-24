#if PICO_INSTALL

using UnityEditor;

namespace Pico.Platform.Editor
{
    public class Menu
    {
        [MenuItem("PXR_SDK/PC Debug Settings")]
        public static void EditPcConfig()
        {
            var obj = PcConfigEditor.load();
            obj.name = "PC Debug Configuration";
            Selection.activeObject = obj;
        }
    }
}
#endif