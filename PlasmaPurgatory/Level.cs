using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using PlasmaPurgatory.Generator;

namespace PlasmaPurgatory
{
    public class Level
    {
        public struct EnemyData
        {
            public Enemy enemy;
            public List<PatternPreset> patterns;
        }

        private enum LevelStage
        {
            NORMAL,
            BOSS_STAGE1,
            BOSS_STAGE2,
        }

        private List<EnemyData> enemies;
        private Queue<EnemyData> enemiesQueue;
        
        private GraphicsDevice graphicsDevice;
        private SpriteBatch spriteBatch;
        private ContentManager contentManager;
        private Player player;

        private Texture2D map;
        private Vector2 mapPos;
        private Song bgm;
        private int timer;
        private LevelStage stage;

        private const int MAX_ENEMIES = 3;

        private Texture2D life;
        private SceneManager sceneManager;

        public Level(GraphicsDevice graphicsDevice, ContentManager contentManager, SceneManager sceneManager)
        {
            this.graphicsDevice = graphicsDevice;
            this.contentManager = contentManager;
            this.sceneManager = sceneManager;

            enemies = new List<EnemyData>();
            enemiesQueue = new Queue<EnemyData>();
        }

        public void Initialize()
        {
            mapPos = new Vector2(0, 0);
            player = new Player(contentManager, graphicsDevice);

            // Add all enemies in the level
            EnqueEnemies();

            foreach (EnemyData enemyData in enemiesQueue)
                enemyData.enemy.Initialize();
            
            player.Initialize();
            timer = 0;
        }

        public void LoadContent()
        {
            life = contentManager.Load<Texture2D>("Life3");

            MediaPlayer.Stop();
            spriteBatch = new SpriteBatch(graphicsDevice);

            map = contentManager.Load<Texture2D>("Map");

            foreach (EnemyData enemyData in enemiesQueue)
                enemyData.enemy.LoadContent();

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
            if (player.Health == 2)
            {
                life = contentManager.Load<Texture2D>("Life2");
            }
            else if (player.Health == 1)
            {
                life = contentManager.Load<Texture2D>("Life1");
            }
            else if (player.Health <= 0)
            {
                 sceneManager.ChangeScene(SceneManager.SceneType.GAMEOVER);
                 return;
            }

            Collisions.CheckCollision(player, player.RectAttack, enemies);
            RemoveDeadEnemies();
            
            if (enemies.Count < MAX_ENEMIES && stage == LevelStage.NORMAL && 
                enemiesQueue.Peek().enemy.Type != Enemy.EnemyType.HADES)
            {
                // Dequeue next enemy
                enemies.Add(enemiesQueue.Dequeue());
            } 
            else if (enemies.Count == 0 && stage == LevelStage.NORMAL)
            {
                // Dequeue hades
                enemies.Add(enemiesQueue.Dequeue());
                stage = LevelStage.BOSS_STAGE1;
            }
            
            SpawnPatterns();
            
            foreach (EnemyData enemy in enemies)
                enemy.enemy.Update(gameTime);
            
            foreach (EnemyData enemyData in enemies)
                for (int i = 0; i < enemyData.patterns.Count; i++)
                    for (int j = 0; j < enemyData.patterns[i].Bullets.Count; j++)
                        if (enemyData.patterns[i].Bullets[j] != null)
                        enemyData.patterns[i].Bullets[j].Update(gameTime);

            player.Update(gameTime);
            timer--;
        }

        private EnemyData CreateBigGarry()
        {
            EnemyData bigGar = new EnemyData();
            bigGar.enemy = new Enemy(contentManager, graphicsDevice, Enemy.EnemyType.BIGGARRY);
            bigGar.patterns = new List<PatternPreset>();
            bigGar.enemy.LoadContent();

            return bigGar;
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
            bulletProperties.movementSpeed = 0.14f;
            bulletProperties.rotationSpeed = 0;
            bulletProperties.bulletProbability = 2;

            Vector2 originPat = bigGar.enemy.Position;
            originPat.X += bigGar.enemy.Texture.Width / 2f;
            originPat.Y += bigGar.enemy.Texture.Height / 2f;
            PatternPreset circlePreset = new PatternPreset(PatternPreset.PresetName.CIRCLE, polarProperties, bulletProperties, contentManager, graphicsDevice, originPat, 8);
            
            circlePreset.ApplyPattern();
            bigGar.patterns.Add(circlePreset);
        }

        private EnemyData CreateDatass()
        {
            EnemyData dat = new EnemyData();
            dat.enemy = new Enemy(contentManager, graphicsDevice, Enemy.EnemyType.DATASS);
            dat.patterns = new List<PatternPreset>();
            dat.enemy.LoadContent();

            return dat;
        }

        private void CreateDatassPattern(EnemyData dat)
        {
            PatternPreset.PolarProperties polarProperties = new PatternPreset.PolarProperties();
            polarProperties.startMagnitude = 40f;
            polarProperties.startPhase = 0;
            polarProperties.incrementMagnitude = 2f;
            polarProperties.incrementPhase = -MathsUtils.DegresToRadians(1.5f);
            polarProperties.multiplierMagnitude = 1;
            polarProperties.multiplierPhase = 1f;

            Bullet.BulletProperties bulletProperties = new Bullet.BulletProperties();
            bulletProperties.movementSpeed = 0.12f;
            bulletProperties.rotationSpeed = -MathsUtils.DegresToRadians(0.05f);
            bulletProperties.bulletProbability = 2;

            Vector2 originPat = dat.enemy.Position;
            originPat.X += dat.enemy.Texture.Width / 2f;
            originPat.Y += dat.enemy.Texture.Height / 2f;
            PatternPreset circlePreset = new PatternPreset(PatternPreset.PresetName.SHOTGUN, polarProperties, bulletProperties, contentManager, graphicsDevice, originPat, 3);

            circlePreset.ApplyPattern();
            dat.patterns.Add(circlePreset);
        }

        private EnemyData CreateBarbarossa()
        {
            EnemyData bar = new EnemyData();
            bar.enemy = new Enemy(contentManager, graphicsDevice, Enemy.EnemyType.BARBAROSSA);
            bar.patterns = new List<PatternPreset>();
            bar.enemy.LoadContent();

            return bar;
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
            bulletProperties.movementSpeed = 0.14f;
            bulletProperties.rotationSpeed = 0;
            bulletProperties.bulletProbability = 2;

            Vector2 originPat = bar.enemy.Position;
            originPat.X += bar.enemy.Texture.Width / 2f;
            originPat.Y += bar.enemy.Texture.Height / 2f;
            PatternPreset circlePreset = new PatternPreset(PatternPreset.PresetName.CIRCLE, polarProperties, bulletProperties, contentManager, graphicsDevice, originPat, 1);
                    
            circlePreset.ApplyPattern();
            bar.patterns.Add(circlePreset);
        }

        private EnemyData CreateHades()
        {
            EnemyData had = new EnemyData();
            had.enemy = new Enemy(contentManager, graphicsDevice, Enemy.EnemyType.HADES);
            had.patterns = new List<PatternPreset>();
            had.enemy.LoadContent();

            return had;
        }

        private void PatternsHadesPhase1(EnemyData had)
        {
            Vector2 originPat = had.enemy.Position;
            originPat.X += had.enemy.Texture.Width / 2f;
            originPat.Y += had.enemy.Texture.Height / 2f;
            
            Vector2 originPatMandelbrot = had.enemy.Position;
            originPatMandelbrot.X += had.enemy.Texture.Width / 2f;
            originPatMandelbrot.Y += had.enemy.Texture.Height / 1.6f;
            
            Bullet.BulletProperties bulletProperties = new Bullet.BulletProperties();
            PatternPreset.PolarProperties polarProperties = new PatternPreset.PolarProperties();
            
            if (had.patterns.Count <= 10)
            {
                bulletProperties.movementSpeed = 0.12f;
                bulletProperties.rotationSpeed = 0.08f;
                bulletProperties.bulletProbability = 1;
                
                PatternPreset mandelbrotDualSpiralPreset = new PatternPreset(PatternPreset.PresetName.MANDELBROT_DUAL_SPIRAL,
                                                                             bulletProperties, contentManager, 
                                                                             graphicsDevice, originPatMandelbrot, 20);
                
                mandelbrotDualSpiralPreset.ApplyPattern();
                had.patterns.Add(mandelbrotDualSpiralPreset);
            }
            
            polarProperties.startMagnitude = 40f;
            polarProperties.startPhase = 0;
            polarProperties.incrementMagnitude = 6f;
            polarProperties.incrementPhase = MathsUtils.DegresToRadians(15f);
            polarProperties.multiplierMagnitude = 1;
            polarProperties.multiplierPhase = 1.8f;

            bulletProperties.movementSpeed = 0.20f;
            bulletProperties.rotationSpeed = MathsUtils.DegresToRadians(0.14f);
            bulletProperties.bulletProbability = 2;

            PatternPreset circlePreset = new PatternPreset(PatternPreset.PresetName.CIRCLE, polarProperties, 
                                                           bulletProperties, contentManager, 
                                                           graphicsDevice, originPat, 25);

            circlePreset.ApplyPattern();
            had.patterns.Add(circlePreset);
        }

        private void PatternsHadesPhase2(EnemyData had)
        {
            Vector2 originPat = had.enemy.Position;
            originPat.X += had.enemy.Texture.Width / 2f;
            originPat.Y += had.enemy.Texture.Height / 2f;
            
            Vector2 originPatMandelbrot = had.enemy.Position;
            originPatMandelbrot.X += had.enemy.Texture.Width / 2f;
            originPatMandelbrot.Y += had.enemy.Texture.Height / 1.6f;
            
            Bullet.BulletProperties bulletProperties = new Bullet.BulletProperties();
            PatternPreset.PolarProperties polarProperties = new PatternPreset.PolarProperties();
            
            if (had.patterns.Count <= 12)
            {
                bulletProperties.movementSpeed = 0.1f;
                bulletProperties.rotationSpeed = 0.08f;
                bulletProperties.bulletProbability = 2;
                
                PatternPreset mandelbrotSpiralPreset = new PatternPreset(PatternPreset.PresetName.MANDELBROT_SPIRAL,
                                                                         bulletProperties, contentManager, 
                                                                         graphicsDevice, originPatMandelbrot, 20);
                
                mandelbrotSpiralPreset.ApplyPattern();
                had.patterns.Add(mandelbrotSpiralPreset);
            }

            if (had.patterns.Count <= 14)
            {
                bulletProperties.movementSpeed = 0.15f;
                bulletProperties.rotationSpeed = 0.1f;
                bulletProperties.bulletProbability = 2;

                PatternPreset mandelbrotSunPreset = new PatternPreset(PatternPreset.PresetName.MANDELBROT_SUN,
                                                                       bulletProperties, contentManager,
                                                                       graphicsDevice, originPatMandelbrot, 40);

                mandelbrotSunPreset.ApplyPattern();
                had.patterns.Add(mandelbrotSunPreset);
            }

            polarProperties.startMagnitude = 40f;
            polarProperties.startPhase = 0;
            polarProperties.incrementMagnitude = 8f;
            polarProperties.incrementPhase = MathsUtils.DegresToRadians(16f);
            polarProperties.multiplierMagnitude = 1;
            polarProperties.multiplierPhase = -1.8f;

            bulletProperties.movementSpeed = 0.20f;
            bulletProperties.rotationSpeed = MathsUtils.DegresToRadians(0.18f);
            bulletProperties.bulletProbability = 8;

            PatternPreset spiralPreset = new PatternPreset(PatternPreset.PresetName.SPIRAL, polarProperties, 
                                                           bulletProperties, contentManager, graphicsDevice,
                                                           originPat, 35);
            spiralPreset.ApplyPattern();
            had.patterns.Add(spiralPreset);
            
            polarProperties.startMagnitude = 30f;
            polarProperties.startPhase = 0;
            polarProperties.incrementMagnitude = 6f;
            polarProperties.incrementPhase = MathsUtils.DegresToRadians(15f);
            polarProperties.multiplierMagnitude = 1;
            polarProperties.multiplierPhase = -1.8f;

            bulletProperties.movementSpeed = 0.25f;
            bulletProperties.rotationSpeed = MathsUtils.DegresToRadians(0.14f);
            bulletProperties.bulletProbability = 3;

            PatternPreset circlePreset = new PatternPreset(PatternPreset.PresetName.SHOTGUN, polarProperties, bulletProperties, contentManager, graphicsDevice, originPat, 50);

            circlePreset.ApplyPattern();
            had.patterns.Add(circlePreset);
        }

        public void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(map, mapPos, Color.White);
            spriteBatch.Draw(life, new Vector2(0,0), Color.White);
            spriteBatch.End();

            foreach (EnemyData enemy in enemies)
                enemy.enemy.Draw(gameTime);

            player.Draw(gameTime);

            foreach (EnemyData enemyData in enemies)
                for (int i = 0; i < enemyData.patterns.Count; i++)
                    for (int j = 0; j < enemyData.patterns[i].Bullets.Count; j++)
                        enemyData.patterns[i].Bullets[j].Draw(gameTime);
        }

        private void RemoveDeadEnemies()
        {
            for (int i = 0; i < enemies.Count; i++)
                if (enemies[i].enemy.IsDead) 
                    enemies.Remove(enemies[i]);
        }

        private void SpawnPatterns()
        {
            if (timer <= 0)
            {
                foreach (EnemyData enemy in enemies)
                {
                    if (enemy.enemy.Type == Enemy.EnemyType.BARBAROSSA)
                        CreateBarbarossaPattern(enemy);
                    if (enemy.enemy.Type == Enemy.EnemyType.DATASS)
                        CreateDatassPattern(enemy);
                    if (enemy.enemy.Type == Enemy.EnemyType.BIGGARRY)
                        CreateBigGarryPattern(enemy);
                    if (enemy.enemy.Type == Enemy.EnemyType.HADES)
                        if (enemy.enemy.Health > 3)
                            PatternsHadesPhase1(enemy);
                        else
                            PatternsHadesPhase2(enemy);

                    switch (stage)
                    {
                       case LevelStage.NORMAL:
                           timer = 140;
                           break;
                       
                       case LevelStage.BOSS_STAGE1:
                           timer = 130;
                           break;
                       
                       case LevelStage.BOSS_STAGE2:
                           timer = 180;
                           break;
                    }
                }
            }
        }

        private void EnqueEnemies()
        {
            /*
             * Level Stages:
             * NORMAL: 3 BARBAROSSAs next 3 DATASSs next 3 BIGGARRYs,
             * 3 BARBAROSSAs next 3 DATASSs next 3 BIGGARRYs,
             * 1 BARBAROSSAs next 2 DATASSs next 3 BIGGARRYs
             * BOSS_STAGE1: HADES (normal)
             * BOSS_STAGE2: HADES (hard)
             */

            for (int i = 0; i < MAX_ENEMIES; i++)
                enemiesQueue.Enqueue(CreateBarbarossa());
            for (int i = 0; i < MAX_ENEMIES; i++)
                enemiesQueue.Enqueue(CreateDatass());
            for (int i = 0; i < MAX_ENEMIES; i++)
                enemiesQueue.Enqueue(CreateBigGarry());
            
            for (int i = 0; i < MAX_ENEMIES; i++)
                enemiesQueue.Enqueue(CreateBarbarossa());
            for (int i = 0; i < MAX_ENEMIES; i++)
                enemiesQueue.Enqueue(CreateDatass());
            for (int i = 0; i < MAX_ENEMIES; i++)
                enemiesQueue.Enqueue(CreateBigGarry());
            
            enemiesQueue.Enqueue(CreateBarbarossa());
            for (int i = 0; i < 2; i++)
                enemiesQueue.Enqueue(CreateDatass());
            for (int i = 0; i < MAX_ENEMIES; i++)
                enemiesQueue.Enqueue(CreateBigGarry());
            
            enemiesQueue.Enqueue(CreateHades());
        }
    }
}