using System.Collections.Generic;
using System.Linq;
using ExtremelySimpleLogger;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MLEM.Data;
using MLEM.Data.Content;
using MLEM.Textures;
using TinyLife;
using TinyLife.Actions;
using TinyLife.Emotions;
using TinyLife.Mods;
using TinyLife.Objects;
using TinyLife.Utilities;
using TinyLife.World;

namespace FroggySetMod;

public class FroggySetMod : Mod {

    public static Logger Logger { get; private set; }

    public override string Name => "Froggy Set";
    public override string Description => "Not just Froggy Chair but Lily-Pad Table too (:";
    public override TextureRegion Icon => UI[0, 0];

    private UniformTextureAtlas LUT;
    public TextureRegion BaseFroggy => LUT[0, 0];
    public TextureRegion PinkFroggy => LUT[1, 0];
    public TextureRegion YellowFroggy => LUT[0, 1];

    public ColorScheme BaseFroggyColors => ColorScheme.Create(BaseFroggy);
    public ColorScheme PinkFroggyColors => ColorScheme.Create(PinkFroggy);
    public ColorScheme YellowFroggyColors => ColorScheme.Create(YellowFroggy);

    private UniformTextureAtlas UI;

    public override void AddGameContent(GameImpl game, ModInfo info) {
        FurnitureType.Register(new FurnitureType.TypeSettings(
            "FroggySetMod.LilyPadTable", 
            new Point(2, 2), 
            ObjectCategory.Table, 
            150, 
            BaseFroggyColors,
            PinkFroggyColors,
            YellowFroggyColors
        ) {
            ConstructedType = typeof(LilyPadTable),
            Icon = Icon,
            ObjectSpots = ObjectSpot.TableSpots(new Point(2, 2)).ToArray()
        });
        FurnitureType.Register(new FurnitureType.TypeSettings(
            "FroggySetMod.FroggyChair", 
            new Point(1, 1), 
            ObjectCategory.Chair, 
            75, 
            BaseFroggyColors,
            PinkFroggyColors,
            YellowFroggyColors
        ) {
            ConstructedType = typeof(FroggyChair),
            Icon = Icon,
            ObjectSpots = {}
        });
    }

    public override void Initialize(Logger logger, RawContentManager content, RuntimeTexturePacker texturePacker, ModInfo info) {
        Logger = logger;

        texturePacker.Add(content.Load<Texture2D>("UiTextures"), r => UI = new UniformTextureAtlas(r, 16, 16));
        texturePacker.Add(content.Load<Texture2D>("FroggySetLUT"), r => LUT = new UniformTextureAtlas(r, 4, 4));
    }

    public override IEnumerable<string> GetCustomFurnitureTextures(ModInfo info) {
        yield return "FroggySetFurniture";
    }

}