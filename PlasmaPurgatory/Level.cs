using System.Collections.Generic;
using System.Diagnostics;
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

            foreach (EnemyData enemy in enemies)
                enemy.enemy.Initialize();

            foreach (EnemyData enemyData in enemies)
                for (int i = 0; i < enemyData.patterns.Count; i++)
                    enemyData.patterns[i].ApplyPattern();


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
            Debug.WriteLine(timer);
            foreach (EnemyData enemy in enemies)
                enemy.enemy.Update(gameTime);

            foreach (EnemyData enemy in enemies)
            {
                if (timer <= 0)
                {
                    Debug.WriteLine(enemy.enemy.Type);
                    if (enemy.enemy.Type == Enemy.EnemyType.BARBAROSSA)
                        CreateBarbarossaPattern(enemy);
                    if (enemy.enemy.Type == Enemy.EnemyType.BIGGARRY)
                        CreateBigGarryPattern(enemy);
                    if (enemy.enemy.Type == Enemy.EnemyType.DATASS)
                        CreateDatassPattern(enemy);
                    timer = 60 * 2;
                }
            }

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

        private void CreateBigGarryPattern(EnemyData BigGar)
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

            Vector2 originPat = BigGar.enemy.Position;
            originPat.X += BigGar.enemy.Texture.Width / 2;
            originPat.Y += BigGar.enemy.Texture.Height / 2;
            PatternPreset circlePreset = new PatternPreset(PatternPreset.PresetName.CIRCLE, polarProperties, bulletProperties, contentManager, graphicsDevice, originPat, 10);

            BigGar.patterns.Add(circlePreset);
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

        private void CreateDatassPattern(EnemyData Dat)
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

            Vector2 originPat = Dat.enemy.Position;
            originPat.X += Dat.enemy.Texture.Width / 2;
            originPat.Y += Dat.enemy.Texture.Height / 2;
            PatternPreset circlePreset = new PatternPreset(PatternPreset.PresetName.SHOTGUN, polarProperties, bulletProperties, contentManager, graphicsDevice, originPat, 3);

            Dat.patterns.Add(circlePreset);
        }

        private void CreateBarbarossa()
        {
            EnemyData Bar = new EnemyData();
            Bar.enemy = new Enemy(contentManager, graphicsDevice, Enemy.EnemyType.BARBAROSSA);
            Bar.patterns = new List<PatternPreset>();
            Bar.enemy.LoadContent();

            enemies.Add(Bar);
        }

        private void CreateBarbarossaPattern(EnemyData Bar)
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

            Vector2 originPat = Bar.enemy.Position;
            originPat.X += Bar.enemy.Texture.Width / 2;
            originPat.Y += Bar.enemy.Texture.Height / 2;
            PatternPreset circlePreset = new PatternPreset(PatternPreset.PresetName.CIRCLE, polarProperties, bulletProperties, contentManager, graphicsDevice, originPat, 1);

            Bar.patterns.Add(circlePreset);
        }

        private void CreateHades()
        {
            EnemyData Had = new EnemyData();
            Had.enemy = new Enemy(contentManager, graphicsDevice, Enemy.EnemyType.HADES);
            Had.patterns = new List<PatternPreset>();
            Had.enemy.LoadContent();

            enemies.Add(Had);
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