using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Content;
using MonoGame.Extended.Tiled.Renderers;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using PlasmaPurgatory.Generator;

namespace PlasmaPurgatory
{
    public class Level
    {

        private struct EnemyData
        {
            public Enemy enemy;
            public List<PatternPreset> patterns;
        }

        private List<EnemyData> enemies;

        private GraphicsDevice graphicsDevice;
        private SpriteBatch spriteBatch;
        private ContentManager contentManager;
        private Player player;

        private Texture2D map;
        private Vector2 mapPos;
        //private TiledMap _tiledMap;
        //private TiledMapRenderer _tiledMapRenderer;

        private Song bgm;
        private int timer;
        private int startCount;
        private int midCount;

        public Level(GraphicsDevice graphicsDevice, ContentManager contentManager)
        {
            this.graphicsDevice = graphicsDevice;
            this.contentManager = contentManager;
            enemies = new List<EnemyData>();
            
        }

        public void Initialize()
        {
            mapPos = new Vector2(0, 0);

            player = new Player(contentManager, graphicsDevice);

            CreateBarbarossa();
            CreateBarbarossa();
            CreateBarbarossa();
            
            foreach (EnemyData enemy in enemies)
                enemy.enemy.Initialize();

            CreateBarbarossaPattern(enemies[0]);
            CreateBarbarossaPattern(enemies[1]);
            CreateBarbarossaPattern(enemies[2]);

            startCount = enemies.Count;
            midCount = enemies.Count - 1;

            if (enemies.Count != startCount)
            {
                CreateDatass();
                CreateDatass();
            }

            if (midCount != enemies.Count - 1)
            {
                CreateBigGarry();
                CreateBigGarry();
                CreateBigGarry();
            }

            player.Initialize();
            timer = 0;
        }

        public void LoadContent()
        {
            MediaPlayer.Stop();
            spriteBatch = new SpriteBatch(graphicsDevice);

            map = contentManager.Load<Texture2D>("Map");
            //_tiledMap = contentManager.Load<TiledMap>("map");
            //_tiledMapRenderer = new TiledMapRenderer(graphicsDevice, _tiledMap);

            foreach (EnemyData enemy in enemies)
                enemy.enemy.LoadContent();

            foreach (EnemyData enemyData in enemies)
                for (int i = 0; i < enemyData.patterns.Count; i++)
                    for (int j = 0; j < enemyData.patterns[i].Bullets.Count; j++)
                        enemyData.patterns[i].Bullets[j].LoadContent();

            player.LoadContent();

            bgm = contentManager.Load<Song>("Vladmsorensen-Spectre [Synthwave]from Royalty Free Planet");

            MediaPlayer.Volume = .1f;
            MediaPlayer.Play(bgm);
        }

        public void Update(GameTime gameTime)
        {
            if (timer <= 0)
            {
                foreach (EnemyData enemy in enemies)
                {
                    if (enemy.enemy.Type == Enemy.EnemyType.BARBAROSSA)
                        CreateBarbarossaPattern(enemy);
                    if (enemy.enemy.Type == Enemy.EnemyType.BIGGARRY)
                        CreateBigGarryPattern(enemy);
                    if (enemy.enemy.Type == Enemy.EnemyType.DATASS)
                        CreateDatassPattern(enemy);
                    timer = 300;
                }
            }
            
            foreach (EnemyData enemy in enemies)
                enemy.enemy.Update(gameTime);
            
            foreach (EnemyData enemyData in enemies)
                for (int i = 0; i < enemyData.patterns.Count; i++)
                    for (int j = 0; j < enemyData.patterns[i].Bullets.Count; j++)
                        enemyData.patterns[i].Bullets[j].Update(gameTime);

            player.Update(gameTime);
            timer--;
        }

        private void CreateBigGarry()
        {
            EnemyData BigGar = new EnemyData();
            BigGar.enemy = new Enemy(contentManager, graphicsDevice, Enemy.EnemyType.BIGGARRY);
            BigGar.patterns = new List<PatternPreset>();
            BigGar.enemy.LoadContent();

            enemies.Add(BigGar);
        }

        private void CreateBigGarryPattern(EnemyData bigGar)
        {
            PatternPreset.PolarProperties polarProperties = new PatternPreset.PolarProperties();
            polarProperties.startMagnitude = 40f;
            polarProperties.startPhase = 0;
            polarProperties.incrementMagnitude = 2f;
            polarProperties.incrementPhase = MathsUtils.DegresToRadians(1f);
            polarProperties.multiplierMagnitude = 1;
            polarProperties.multiplierPhase = 1f;

            Bullet.BulletProperties bulletProperties = new Bullet.BulletProperties();
            bulletProperties.movementSpeed = 0.12f;
            bulletProperties.rotationSpeed = 0;
            bulletProperties.bulletProbability = 2;

            Vector2 originPat = bigGar.enemy.Position;
            originPat.X += bigGar.enemy.Texture.Width / 2;
            originPat.Y += bigGar.enemy.Texture.Height / 2;
            PatternPreset circlePreset = new PatternPreset(PatternPreset.PresetName.CIRCLE, polarProperties, bulletProperties, contentManager, graphicsDevice, originPat, 10);
            
            circlePreset.ApplyPattern();
            bigGar.patterns.Add(circlePreset);
        }

        private void CreateDatass()
        {
            EnemyData Dat = new EnemyData();
            Dat.enemy = new Enemy(contentManager, graphicsDevice, Enemy.EnemyType.DATASS);
            Dat.patterns = new List<PatternPreset>();
            Dat.enemy.LoadContent();

            CreateDatassPattern(Dat);

            enemies.Add(Dat);
        }

        private void CreateDatassPattern(EnemyData dat)
        {
            PatternPreset.PolarProperties polarProperties = new PatternPreset.PolarProperties();
            polarProperties.startMagnitude = 40f;
            polarProperties.startPhase = 0;
            polarProperties.incrementMagnitude = 2f;
            polarProperties.incrementPhase = MathsUtils.DegresToRadians(1f);
            polarProperties.multiplierMagnitude = 1;
            polarProperties.multiplierPhase = 1f;

            Bullet.BulletProperties bulletProperties = new Bullet.BulletProperties();
            bulletProperties.movementSpeed = 0.12f;
            bulletProperties.rotationSpeed = 0;
            bulletProperties.bulletProbability = 2;

            Vector2 originPat = dat.enemy.Position;
            originPat.X += dat.enemy.Texture.Width / 2;
            originPat.Y += dat.enemy.Texture.Height / 2;
            PatternPreset circlePreset = new PatternPreset(PatternPreset.PresetName.SHOTGUN, polarProperties, bulletProperties, contentManager, graphicsDevice, originPat, 3);

            circlePreset.ApplyPattern();
            dat.patterns.Add(circlePreset);
        }

        private void CreateBarbarossa()
        {
            EnemyData bar = new EnemyData();
            bar.enemy = new Enemy(contentManager, graphicsDevice, Enemy.EnemyType.BARBAROSSA);
            bar.patterns = new List<PatternPreset>();
            bar.enemy.LoadContent();

            enemies.Add(bar);
        }

        private void CreateBarbarossaPattern(EnemyData bar)
        {
            PatternPreset.PolarProperties polarProperties = new PatternPreset.PolarProperties();
            polarProperties.startMagnitude = 40f;
            polarProperties.startPhase = MathsUtils.DegresToRadians(90f);
            polarProperties.incrementMagnitude = 2f;
            polarProperties.incrementPhase = MathsUtils.DegresToRadians(1f);
            polarProperties.multiplierMagnitude = 1;
            polarProperties.multiplierPhase = 1f;

            Bullet.BulletProperties bulletProperties = new Bullet.BulletProperties();
            bulletProperties.movementSpeed = 0.12f;
            bulletProperties.rotationSpeed = 0;
            bulletProperties.bulletProbability = 2;

            Vector2 originPat = bar.enemy.Position;
            originPat.X += bar.enemy.Texture.Width / 2;
            originPat.Y += bar.enemy.Texture.Height / 2;
            PatternPreset circlePreset = new PatternPreset(PatternPreset.PresetName.CIRCLE, polarProperties, bulletProperties, contentManager, graphicsDevice, originPat, 1);
                    
            circlePreset.ApplyPattern();
            bar.patterns.Add(circlePreset);
        }

        private void CreateHades()
        {
            EnemyData had = new EnemyData();
            had.enemy = new Enemy(contentManager, graphicsDevice, Enemy.EnemyType.HADES);
            had.patterns = new List<PatternPreset>();
            had.enemy.LoadContent();

            enemies.Add(had);
        }

        public void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(map, mapPos, Color.White);
            spriteBatch.End();

            foreach(EnemyData enemyData in enemies)
                for (int i = 0; i < enemyData.patterns.Count; i++)
                    for (int j = 0; j < enemyData.patterns[i].Bullets.Count; j++)
                        enemyData.patterns[i].Bullets[j].Draw(gameTime);

            foreach (EnemyData enemy in enemies)
                enemy.enemy.Draw(gameTime);

            player.Draw(gameTime);
        }
    }
}