using UnityEngine;
using Verse;

namespace CustomUIScales
{
	[StaticConstructorOnStartup]
	public class CustomUIScalesMod : Mod
	{
		public static Settings Settings;

		public CustomUIScalesMod(ModContentPack content) : base(content)
		{
			// initialize settings
			Settings = GetSettings<Settings>();
			Log.Message($"Current UI Scale: {Prefs.UIScale}");
		}

		public override void DoSettingsWindowContents(Rect inRect)
		{
			base.DoSettingsWindowContents(inRect);
			Settings.DoWindowContents(inRect);
		}

		public override string SettingsCategory()
		{
			return "CustomUIScales";
		}
	}
}
