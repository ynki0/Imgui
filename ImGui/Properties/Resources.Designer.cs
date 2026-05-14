
namespace EasyImGui.Properties {
    using System;
    

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }

        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("EasyImGui.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        internal static byte[] win_arm64_cimgui {
            get {
                object obj = ResourceManager.GetObject("win_arm64_cimgui", resourceCulture);
                return ((byte[])(obj));
            }
        }

        internal static byte[] win_arm64_ImGuiImpl {
            get {
                object obj = ResourceManager.GetObject("win_arm64_ImGuiImpl", resourceCulture);
                return ((byte[])(obj));
            }
        }
        
        internal static byte[] win_x64_cimgui {
            get {
                object obj = ResourceManager.GetObject("win_x64_cimgui", resourceCulture);
                return ((byte[])(obj));
            }
        }
        
        internal static byte[] win_x64_ImGuiImpl {
            get {
                object obj = ResourceManager.GetObject("win_x64_ImGuiImpl", resourceCulture);
                return ((byte[])(obj));
            }
        }
        
        internal static byte[] win_x86_cimgui {
            get {
                object obj = ResourceManager.GetObject("win_x86_cimgui", resourceCulture);
                return ((byte[])(obj));
            }
        }
        
        internal static byte[] win_x86_ImGuiImpl {
            get {
                object obj = ResourceManager.GetObject("win_x86_ImGuiImpl", resourceCulture);
                return ((byte[])(obj));
            }
        }
    }
}
