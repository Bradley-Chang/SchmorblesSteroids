using System;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley;



namespace Steroids
{
    /// <summary>The mod entry point.</summary>
    public class ModEntry : Mod {
        public static IModHelper Helper;


        /*********
        ** Public methods
        *********/
        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <param name="helper">Provides simplified APIs for writing mods.</param>
        public override void Entry(IModHelper helper)
        {
            Helper = helper;
            helper.Events.Input.ButtonPressed += this.OnButtonPressed;
            helper.Events.GameLoop.DayStarted += this.OnSaveLoaded;
            helper.Content.AssetEditors.Add(new AssetEditor());

        }


        /*********
        ** Private methods
        *********/
        /// <summary>Raised after the player presses a button on the keyboard, controller, or mouse.</summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event data.</param>
        private void OnButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            // ignore if player hasn't loaded a save yet
            if (!Context.IsWorldReady)
                return;

            // print button presses to the console window
            this.Monitor.Log($"{Game1.player.Name} pressed {e.Button}.", LogLevel.Debug);
        }

        private void OnSaveLoaded(object sender, EventArgs e)
        {
            if (!Game1.player.craftingRecipes.ContainsKey("Schmorble's Steroids"))
            {
                this.Monitor.Log("SAVE LOADED", LogLevel.Debug);
                Game1.player.craftingRecipes.Add("Schmorble's Steroids", 0);
            }
        }
    }
}