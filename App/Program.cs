
using EasyImGui;
using EasyImGui.Core;
using Hexa.NET.ImGui;
using Hexa.NET.ImGui.Widgets;
using SharpDX.Direct3D9;
using System;
using System.Drawing;
using System.Numerics;
using System.Windows.Forms;

namespace TestApp
{
    internal static class Program
    {
        private static bool _menuVisible   = true;
        private static bool _insertWasDown = false;
        private static DateTime _lastToggle = DateTime.MinValue;

        [STAThread]
        private static void Main()
        {
            if (!Diagnostic.RunDiagnostic())
            {
                Console.WriteLine("Diagnostics failed. Resolve missing libraries and restart.");
                Console.ReadKey();
                return;
            }

            using (var window = CreateWindow())
            {
                ConfigureWindow(window);
                RegisterCallbacks(window);

                try
                {
                    Application.Run(window);
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message, "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Environment.Exit(1);
                }
            }
        }

        private static Overlay CreateWindow() => new Overlay
        {
            EnableDrag       = false,
            ResizableBorders = true,
            ShowInTaskbar    = false,
            AutoInitialize   = true,
        };

        private static void ConfigureWindow(Overlay window)
        {
            window.ImguiManager.Architecture = Runtimes.Architecture.Auto;

            window.PresentParams = new PresentParameters
            {
                Windowed               = true,
                SwapEffect             = SwapEffect.Discard,
                BackBufferFormat       = Format.X8R8G8B8,
                PresentationInterval   = PresentInterval.Immediate,
                EnableAutoDepthStencil = false,
                AutoDepthStencilFormat = Format.Unknown,
                MultiSampleType        = MultisampleType.None,
                MultiSampleQuality     = 0,
            };

            window.Location = new Point(0, 0);
            window.Size = new Size(
                Screen.PrimaryScreen.WorkingArea.Width,
                Screen.PrimaryScreen.WorkingArea.Height
            );
        }

        private static void RegisterCallbacks(Overlay window)
        {
            WidgetDemo widgetDemo = null;
            InputImguiEmu input   = null;

            window.ImguiManager.ConfigContex += () =>
            {
                ApplyTheme(window);

                window.ImguiManager.IO.ConfigDebugIsDebuggerPresent    = false;
                window.ImguiManager.IO.ConfigErrorRecoveryEnableAssert = false;

                widgetDemo = new WidgetDemo();
                input      = new InputImguiEmu(window.ImguiManager.IO);

                return true;
            };

            window.OnImGuiReady += (sender, ready) =>
            {
                if (!ready) return;

                window.ImguiManager.Render += () =>
                {
                    HandleMenuToggle(input);

                    if (_menuVisible)
                    {
                        WidgetManager.Draw();
                        DrawMainWindow(widgetDemo);
                    }

                    window.Interactive(_menuVisible);
                    return true;
                };
            };
        }

        private static void HandleMenuToggle(InputImguiEmu input)
        {
            bool isDown = input?.IsKeyDown(Keys.Insert) ?? false;

            if (isDown && !_insertWasDown && (DateTime.Now - _lastToggle).TotalMilliseconds > 100)
            {
                _menuVisible = !_menuVisible;
                _lastToggle  = DateTime.Now;
            }

            _insertWasDown = isDown;
        }

        private static void DrawMainWindow(WidgetDemo widgetDemo)
        {
            if (!ImGui.Begin("ImGui", ref _menuVisible))
            {
                ImGui.End();
                return;
            }

            widgetDemo?.DrawContent();
            ImGui.End();
        }

        private static void ApplyTheme(Overlay window)
        {
            var style = ImGui.GetStyle();
            var c     = style.Colors;

            c[(int)ImGuiCol.Text]                 = new Vector4(0.95f, 0.95f, 0.95f, 1.00f);
            c[(int)ImGuiCol.TextDisabled]          = new Vector4(0.40f, 0.40f, 0.40f, 1.00f);
            c[(int)ImGuiCol.WindowBg]              = new Vector4(0.00f, 0.00f, 0.00f, 1.00f);
            c[(int)ImGuiCol.ChildBg]               = new Vector4(0.02f, 0.02f, 0.02f, 1.00f);
            c[(int)ImGuiCol.PopupBg]               = new Vector4(0.00f, 0.00f, 0.00f, 0.98f);
            c[(int)ImGuiCol.Border]                = new Vector4(0.18f, 0.18f, 0.18f, 1.00f);
            c[(int)ImGuiCol.BorderShadow]          = new Vector4(0.00f, 0.00f, 0.00f, 0.00f);
            c[(int)ImGuiCol.FrameBg]               = new Vector4(0.06f, 0.06f, 0.06f, 1.00f);
            c[(int)ImGuiCol.FrameBgHovered]        = new Vector4(0.12f, 0.12f, 0.12f, 1.00f);
            c[(int)ImGuiCol.FrameBgActive]         = new Vector4(0.18f, 0.18f, 0.18f, 1.00f);
            c[(int)ImGuiCol.TitleBg]               = new Vector4(0.00f, 0.00f, 0.00f, 1.00f);
            c[(int)ImGuiCol.TitleBgActive]         = new Vector4(0.04f, 0.04f, 0.04f, 1.00f);
            c[(int)ImGuiCol.TitleBgCollapsed]      = new Vector4(0.00f, 0.00f, 0.00f, 0.80f);
            c[(int)ImGuiCol.MenuBarBg]             = new Vector4(0.02f, 0.02f, 0.02f, 1.00f);
            c[(int)ImGuiCol.ScrollbarBg]           = new Vector4(0.00f, 0.00f, 0.00f, 1.00f);
            c[(int)ImGuiCol.ScrollbarGrab]         = new Vector4(0.22f, 0.22f, 0.22f, 1.00f);
            c[(int)ImGuiCol.ScrollbarGrabHovered]  = new Vector4(0.32f, 0.32f, 0.32f, 1.00f);
            c[(int)ImGuiCol.ScrollbarGrabActive]   = new Vector4(0.45f, 0.45f, 0.45f, 1.00f);
            c[(int)ImGuiCol.CheckMark]             = new Vector4(0.90f, 0.90f, 0.90f, 1.00f);
            c[(int)ImGuiCol.SliderGrab]            = new Vector4(0.28f, 0.28f, 0.28f, 1.00f);
            c[(int)ImGuiCol.SliderGrabActive]      = new Vector4(0.50f, 0.50f, 0.50f, 1.00f);
            c[(int)ImGuiCol.Button]                = new Vector4(0.08f, 0.08f, 0.08f, 1.00f);
            c[(int)ImGuiCol.ButtonHovered]         = new Vector4(0.16f, 0.16f, 0.16f, 1.00f);
            c[(int)ImGuiCol.ButtonActive]          = new Vector4(0.26f, 0.26f, 0.26f, 1.00f);
            c[(int)ImGuiCol.Header]                = new Vector4(0.08f, 0.08f, 0.08f, 1.00f);
            c[(int)ImGuiCol.HeaderHovered]         = new Vector4(0.16f, 0.16f, 0.16f, 1.00f);
            c[(int)ImGuiCol.HeaderActive]          = new Vector4(0.24f, 0.24f, 0.24f, 1.00f);
            c[(int)ImGuiCol.Separator]             = new Vector4(0.16f, 0.16f, 0.16f, 1.00f);
            c[(int)ImGuiCol.SeparatorHovered]      = new Vector4(0.30f, 0.30f, 0.30f, 1.00f);
            c[(int)ImGuiCol.SeparatorActive]       = new Vector4(0.45f, 0.45f, 0.45f, 1.00f);
            c[(int)ImGuiCol.ResizeGrip]            = new Vector4(0.14f, 0.14f, 0.14f, 1.00f);
            c[(int)ImGuiCol.ResizeGripHovered]     = new Vector4(0.28f, 0.28f, 0.28f, 1.00f);
            c[(int)ImGuiCol.ResizeGripActive]      = new Vector4(0.45f, 0.45f, 0.45f, 1.00f);
            c[(int)ImGuiCol.Tab]                   = new Vector4(0.04f, 0.04f, 0.04f, 1.00f);
            c[(int)ImGuiCol.TabHovered]            = new Vector4(0.16f, 0.16f, 0.16f, 1.00f);
            c[(int)ImGuiCol.TabSelected]           = new Vector4(0.10f, 0.10f, 0.10f, 1.00f);
            c[(int)ImGuiCol.TabDimmed]             = new Vector4(0.02f, 0.02f, 0.02f, 1.00f);
            c[(int)ImGuiCol.TabDimmedSelected]     = new Vector4(0.08f, 0.08f, 0.08f, 1.00f);
            c[(int)ImGuiCol.DockingPreview]        = new Vector4(0.50f, 0.50f, 0.50f, 0.70f);
            c[(int)ImGuiCol.DockingEmptyBg]        = new Vector4(0.02f, 0.02f, 0.02f, 1.00f);
            c[(int)ImGuiCol.PlotLines]             = new Vector4(0.70f, 0.70f, 0.70f, 1.00f);
            c[(int)ImGuiCol.PlotLinesHovered]      = new Vector4(0.90f, 0.90f, 0.90f, 1.00f);
            c[(int)ImGuiCol.PlotHistogram]         = new Vector4(0.60f, 0.60f, 0.60f, 1.00f);
            c[(int)ImGuiCol.PlotHistogramHovered]  = new Vector4(0.80f, 0.80f, 0.80f, 1.00f);
            c[(int)ImGuiCol.TableHeaderBg]         = new Vector4(0.04f, 0.04f, 0.04f, 1.00f);
            c[(int)ImGuiCol.TableBorderStrong]     = new Vector4(0.20f, 0.20f, 0.20f, 1.00f);
            c[(int)ImGuiCol.TableBorderLight]      = new Vector4(0.10f, 0.10f, 0.10f, 1.00f);
            c[(int)ImGuiCol.TableRowBg]            = new Vector4(0.00f, 0.00f, 0.00f, 0.00f);
            c[(int)ImGuiCol.TableRowBgAlt]         = new Vector4(1.00f, 1.00f, 1.00f, 0.03f);
            c[(int)ImGuiCol.TextSelectedBg]        = new Vector4(0.30f, 0.30f, 0.30f, 0.60f);
            c[(int)ImGuiCol.DragDropTarget]        = new Vector4(0.80f, 0.80f, 0.80f, 0.90f);
            c[(int)ImGuiCol.NavCursor]             = new Vector4(0.80f, 0.80f, 0.80f, 1.00f);
            c[(int)ImGuiCol.NavWindowingHighlight] = new Vector4(0.80f, 0.80f, 0.80f, 0.70f);
            c[(int)ImGuiCol.NavWindowingDimBg]     = new Vector4(0.00f, 0.00f, 0.00f, 0.40f);
            c[(int)ImGuiCol.ModalWindowDimBg]      = new Vector4(0.00f, 0.00f, 0.00f, 0.50f);

            style.WindowPadding     = new Vector2(10f, 10f);
            style.FramePadding      = new Vector2(6f, 4f);
            style.CellPadding       = new Vector2(6f, 5f);
            style.ItemSpacing       = new Vector2(8f, 6f);
            style.ItemInnerSpacing  = new Vector2(6f, 6f);
            style.TouchExtraPadding = new Vector2(0f, 0f);
            style.IndentSpacing     = 22;
            style.ScrollbarSize     = 12;
            style.GrabMinSize       = 10;
            style.WindowBorderSize  = 1;
            style.ChildBorderSize   = 1;
            style.PopupBorderSize   = 1;
            style.TabBorderSize     = 0;
            style.WindowRounding    = 8;
            style.ChildRounding     = 6;
            style.FrameRounding     = 5;
            style.PopupRounding     = 6;
            style.ScrollbarRounding = 6;
            style.GrabRounding      = 5;
            style.LogSliderDeadzone = 4;
            style.TabRounding       = 6;

            if ((window.ImguiManager.IO.ConfigFlags & ImGuiConfigFlags.ViewportsEnable) != 0)
            {
                style.WindowRounding = 0f;
                c[(int)ImGuiCol.WindowBg].W = 1f;
            }
        }
    }
}
