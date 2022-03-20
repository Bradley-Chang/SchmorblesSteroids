using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using StardewValley;
using System.Collections.Generic;
using System.IO;


namespace Steroids
{
    public class AssetEditor : IAssetEditor
    {
        //public static IModHelper helper;

        public bool CanEdit<T>(IAssetInfo asset)
        {
            bool canEdit =
                   asset.AssetNameEquals("Maps/springobjects")
                || asset.AssetNameEquals("Data/ObjectInformation")
                || asset.AssetNameEquals("Data/CraftingRecipes");
            return canEdit;
        }

        public void Edit<T>(IAssetData asset)
        {
            
            if (asset.AssetNameEquals("Maps/springobjects"))
            {
                Texture2D steroids = ModEntry.Helper.Content.Load<Texture2D>(Path.Combine("Assets", "SchmorblesSteroids.png"), ContentSource.ModFolder);
                Texture2D old = asset.AsImage().Data;
                asset.ReplaceWith(new Texture2D(Game1.graphics.GraphicsDevice, old.Width, System.Math.Max(old.Height, 1200 / 24 * 16)));
                asset.AsImage().PatchImage(old);
                asset.AsImage().PatchImage(steroids, targetArea: this.GetRectangle(SchmorblesSteroids.INDEX));
            }
            else if (asset.AssetNameEquals("Data/ObjectInformation"))
            {
                asset.AsDictionary<int, string>().Data.Add(SchmorblesSteroids.INDEX, $"Schmorble's Steroids/{SchmorblesSteroids.PRICE}/{SchmorblesSteroids.EDIBILITY}/{SchmorblesSteroids.TYPE} {SchmorblesSteroids.CATEGORY}/Schmorble's Steroids/May cause side effects./drink/-10 -10 -10 0 0 -10 0 -10 -10 10 -100 -100/300");
            }
            else if (asset.AssetNameEquals("Data/CraftingRecipes"))
            {
                asset.AsDictionary<string, string>().Data.Add("Schmorble's Steroids", $"253 1 346 1 349 1 351 1/Home/{SchmorblesSteroids.INDEX} 1/false/none");

            }

        }

        public Rectangle GetRectangle(int id)
        {
            int x = (id % 24) * 16;
            int y = (id / 24) * 16;
            return new Rectangle(x, y, 16, 16);
        }
    }
}

public class SchmorblesSteroids
{
    public const int INDEX = 1143;
    public const int PRICE = 2000;
    public const int EDIBILITY = 1;
    public const string TYPE = "Basic";
    public const int CATEGORY = Object.CookingCategory;
    public const int CRAFTING_LEVEL = 9;
}