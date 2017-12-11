﻿using MemoryReader.Handler;
using Sync.Tools;
using System;
using System.Windows.Media;

namespace RealTimePPDisplayer
{
    #region Converter

    internal static class ColorConverter
    {
        public static Color StringToColor(string color_str)

        {
            var color = new Color();
            color.A = Convert.ToByte(color_str.Substring(0, 2), 16);
            color.R = Convert.ToByte(color_str.Substring(2, 2), 16);
            color.G = Convert.ToByte(color_str.Substring(4, 2), 16);
            color.B = Convert.ToByte(color_str.Substring(6, 2), 16);
            return color;
        }

        public static string ColorToString(Color c)
        {
            return $"{c.A:X2}{c.R:X2}{c.G:X2}{c.B:X2}";
        }
    }

    #endregion Converter

    internal class SettingIni : IConfigurable
    {
        public ConfigurationElement UseText { get; set; }
        public ConfigurationElement TextOutputPath { get; set; }
        public ConfigurationElement DisplayHitObject { set; get; }
        public ConfigurationElement PPFontSize { set; get; }
        public ConfigurationElement PPFontColor { set; get; }
        public ConfigurationElement HitObjectFontSize { set; get; }
        public ConfigurationElement HitObjectFontColor { set; get; }
        public ConfigurationElement BackgroundColor { set; get; }
        public ConfigurationElement WindowHeight { set; get; }
        public ConfigurationElement WindoWidth { set; get; }
        public ConfigurationElement SmoothTime { get; set; }
        public ConfigurationElement FPS { get; set; }

        public void onConfigurationLoad()
        {
            try
            {
                Setting.UseText = bool.Parse(UseText);
                Setting.TextOutputPath = TextOutputPath;
                Setting.DisplayHitObject = bool.Parse(DisplayHitObject);
                Setting.PPFontColor = ColorConverter.StringToColor(PPFontColor);
                Setting.PPFontSize = int.Parse(PPFontSize);
                Setting.HitObjectFontSize = int.Parse(HitObjectFontSize);
                Setting.HitObjectFontColor = ColorConverter.StringToColor(HitObjectFontColor);
                Setting.BackgroundColor = ColorConverter.StringToColor(BackgroundColor);
                Setting.WindowHeight = int.Parse(WindowHeight);
                Setting.WindowWidth = int.Parse(WindoWidth);
                Setting.SmoothTime = int.Parse(SmoothTime);
                Setting.FPS = int.Parse(FPS);
            }
            catch (Exception e)
            {
                onConfigurationSave();
            }
        }

        public void onConfigurationSave()
        {
            UseText = Setting.UseText.ToString();
            TextOutputPath = Setting.TextOutputPath;
            DisplayHitObject = Setting.DisplayHitObject.ToString();
            PPFontColor = ColorConverter.ColorToString(Setting.PPFontColor);
            PPFontSize = Setting.PPFontSize.ToString();
            HitObjectFontSize = Setting.HitObjectFontSize.ToString();
            HitObjectFontColor = ColorConverter.ColorToString(Setting.HitObjectFontColor);
            BackgroundColor = ColorConverter.ColorToString(Setting.BackgroundColor);
            WindowHeight = Setting.WindowHeight.ToString();
            WindoWidth = Setting.WindowWidth.ToString();
            SmoothTime = Setting.SmoothTime.ToString();
            FPS = Setting.SmoothTime.ToString();
        }
    }

    internal static class Setting
    {
        public static bool UseText = false;
        public static string TextOutputPath = @"..\rtpp.txt";
        public static bool DisplayHitObject = true;
        public static int PPFontSize = 48;
        public static Color PPFontColor = Colors.White;
        public static int HitObjectFontSize = 24;
        public static Color HitObjectFontColor = Colors.White;
        public static Color BackgroundColor = ColorConverter.StringToColor("FF00FF00");
        public static int WindowWidth = 280;
        public static int WindowHeight = 150;
        public static int SmoothTime = 200;
        public static int FPS = 60;

        private static SettingIni setting_output = new SettingIni();
        private static PluginConfiuration plugin_config = null;

        public static RealTimePPDisplayerPlugin PluginInstance
        {
            set
            {
                plugin_config = new PluginConfiuration(value, setting_output);
            }
        }

        static Setting()
        {
            ExitHandler.OnConsloeExit += () => plugin_config?.ForceSave();
        }
    }
}