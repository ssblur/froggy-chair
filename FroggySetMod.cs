using System.Collections.Generic;
using ExtremelySimpleLogger;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MLEM.Data;
using MLEM.Data.Content;
using MLEM.Textures;
using TinyLife;
using TinyLife.Mods;
using TinyLife.Objects;
using TinyLife.Utilities;
using MLEM.Misc;

namespace FroggySetMod {

    public class FroggySetMod : Mod {

        public static Logger Logger { get; private set; }
        public static ModInfo Info;

        public override string Name => "Froggy Set";
        public override string Description => "Not just Froggy Chair but Lily-Pad Table too (:";
        public override string TestedVersionRange => "[0.40.0,0.42.4]";

        public override TextureRegion Icon => UI[0, 0];

        private UniformTextureAtlas LUT;
        public TextureRegion FroggyBack => LUT[0, 0];
        public TextureRegion FroggyPad => LUT[0, 1];

        public ColorScheme FroggyBackColors => ColorScheme.Load(FroggyBack);
        public ColorScheme FroggyPadColors => ColorScheme.Load(FroggyPad);

        private UniformTextureAtlas UI;

        public override void AddGameContent(GameImpl game, ModInfo info) {
            FurnitureType.Register(new FurnitureType.TypeSettings(
                $"{info.Id}.LilyPadTable", 
                new Point(1, 1), 
                ObjectCategory.Table, 
                150, 
                ColorScheme.White,
                FroggyBackColors,
                FroggyPadColors
            ) {
                ConstructedType = typeof(LilyPadTable),
                Icon = Icon,
                ObjectSpots = ObjectSpot.TableSpots(new Point(1, 1))
            });

            FurnitureType.Register(new FurnitureType.TypeSettings(
                $"{info.Id}.FroggyChair", 
                new Point(1, 1), 
                ObjectCategory.Chair, 
                75, 
                FroggyBackColors,
                FroggyPadColors,
                ColorScheme.White,
                FroggyBackColors
            ) {
                ConstructedType = typeof(FroggyChair),
                Icon = Icon,
                ObjectSpots = {},
                ActionSpots = new[] {new ActionSpot(Vector2.Zero, -2 / 16F, Direction2Helper.Adjacent) {
                    DrawLayer = f => 2
                    }
                },
                DefaultRotation = Direction2.Down
            });
        }

        public override void Initialize(Logger logger, RawContentManager content, RuntimeTexturePacker texturePacker, ModInfo info) {
            Logger = logger;
            Info = info;

            texturePacker.Add(
                content.Load<Texture2D>("UiTextures"), 
                r => UI = new UniformTextureAtlas(r, 1, 1)
            );
            texturePacker.Add(
                content.Load<Texture2D>("FroggySetLUT"), 
                r => LUT = new UniformTextureAtlas(r, 1, 16)
            );
        }

        public override IEnumerable<string> GetCustomFurnitureTextures(ModInfo info) {
            yield return "FroggySetFurniture";
        }

    }
}