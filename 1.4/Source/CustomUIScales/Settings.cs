using System;
using RimWorld;
using UnityEngine;
using Verse;

namespace CustomUIScales
{
	public class Settings : ModSettings
	{
		//Use Mod.settings.setting to refer to this setting.
		public float UIScale;
		private string _uiScaleBuffer;
		public bool ApplyCustomUIScale;

		public void DoWindowContents(Rect wrect)
		{
			var options = new Listing_Standard();
			options.Begin(wrect);

			options.TextFieldNumericLabeled("UI Scale", ref UIScale, ref _uiScaleBuffer, -0.999f, float.MaxValue);
			options.Gap();
			options.CheckboxLabeled("Apply custom UI Scale", ref ApplyCustomUIScale);
			options.End();
		}

		public override void ExposeData()
		{
			Scribe_Values.Look(ref UIScale, "customUIScale");
			Scribe_Values.Look(ref ApplyCustomUIScale, "applyCustomUIScale");


			if (Scribe.mode == LoadSaveMode.LoadingVars || Scribe.mode == LoadSaveMode.ResolvingCrossRefs ||
			    !ApplyCustomUIScale || UIScale == 0 ||
			    !(Math.Abs(Prefs.UIScale - (double)UIScale) > 0.01)) return;
			
			if (Math.Abs(UIScale - 1.0) > 0.01 &&
			    !ResolutionUtility.UIScaleSafeWithResolution(UIScale, Screen.width, Screen.height))
			{
				Messages.Message("MessageScreenResTooSmallForUIScale".Translate(), MessageTypeDefOf.RejectInput, false);
			}
			else
			{
				ResolutionUtility.SafeSetUIScale(UIScale);
				Prefs.Save();
			}
		}
	}
}
