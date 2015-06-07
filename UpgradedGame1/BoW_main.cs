#region Using Statements
using WindowsGame2;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
#endregion
public class BoW_main : Microsoft.Xna.Framework.Game
{
    TitleScreen birdsTitle;
    
    int[] levelExp = new int[10] { 300, 700, 999, 1145, 1356, 1643, 2134, 2436, 2756, 30000000 };    
    KeyboardState oldState;
    Menu powerupPurchaseMenu;
    Menu playerLevelingMenu;    
    List<Texture2D> texTerrainList = new List<Texture2D>(3);
    List<Texture2D> texCloudList = new List<Texture2D>(8);
    List<AnimatedTexture> texEnemyList = new List<AnimatedTexture>(4);    
    Rectangle playerRectangle;
    Rectangle enemyRectangle;
    Rectangle bulletRectangle;
    bool gamePaused;
    bool consoleIsActive;
    TimeSpan elapsedTime;
    float getReadyTime;
    int playedX = 0;


    GamePadState previousGamePadState;
    float vibrationAmount = 0.5f;
    float vibrationTime = 0;
    float menuTime;

    Player Player1;
    EnemyManager enemyManager;
    ExplosionManager explosionManager;
    public static Random myRandomNumberGenerator = new Random();
    
    //FPS variables 
    int totalFrames;    
    double currentFPS;

    //AudioEngine gameMusicEngine;
    //WaveBank gameMusicWaveBank;
    //SoundBank gameMusicSoundBank;
    //AudioEngine gameSoundEngine;             SOUND IS DISABLED UNTIL UPGRADED
    //WaveBank gameSoundWaveBank;
    //SoundBank gameSoundSoundBank;

    Texture2D playerBulletTex;
    Texture2D overlay;
    Texture2D texProgressBar;
    Texture2D texProgressFull;
    Texture2D enemyBulletTex;
    Texture2D texHealthProgressFill;
    Texture2D border;

    List<Texture2D> titleTextures = new List<Texture2D>();
    Texture2D buttonA;
    Texture2D buttonB;
    Texture2D buttonY;
    Texture2D buttonX;
    Texture2D birdsLogo;
    
  
    

    Texture2D texPowBullets;
    Texture2D texPowShield;
    Texture2D texPowSpeed;

    AnimatedTexture explosionTex;
    AnimatedTexture enemyTex;    
    AnimatedTexture aTexShield;
    AnimatedTexture pPlane;

    ProgressBar progressBar;
    ProgressBar healthBar;
    
    SpriteBatch ForegroundBatch;     
    SpriteBatch BackgroundBatch;
    SpriteBatch MenuBatch;
    
    ScrollingBackground myBackground;    
    
    GraphicsDeviceManager graphics;
    ProjectileManager projectileManager;
    ContentManager content;
    SpriteFont Arial;
    SpriteFont ComicSans;
    Viewport viewport;  

    public BoW_main()
    {               
        // ???????????????????????????????????SOUND IS DISABLED UNTIL I UPDATE THIS
        //gameMusicEngine = new AudioEngine("cpl/audio/music/game_music.xgs");
        //gameMusicWaveBank = new WaveBank(gameMusicEngine, "CPL/audio/music/Wave Bank.xwb");
        //gameMusicSoundBank = new SoundBank(gameMusicEngine, "CPL/audio/music/Sound Bank.xsb");

        //gameSoundEngine = new AudioEngine("cpl/audio/sound/game_sound.xgs");
        //gameSoundWaveBank = new WaveBank(gameSoundEngine, "cpl/audio/sound/Wave Bank.xwb");
        //gameSoundSoundBank = new SoundBank(gameSoundEngine, "cpl/audio/sound/Sound Bank.xsb");
        
        oldState = Keyboard.GetState();
        graphics = new GraphicsDeviceManager(this);
        content = new ContentManager(Services);
        projectileManager = new ProjectileManager(50,null);
        pPlane = new AnimatedTexture(Vector2.Zero, 0, 1.0f, 0.5f);
        enemyTex = new AnimatedTexture(Vector2.Zero, 0, 1.5f, 0.5f);        
        enemyManager = new EnemyManager(10, projectileManager);
        explosionManager = new ExplosionManager(50);
        explosionTex = new AnimatedTexture(Vector2.Zero, 0, 1.0f,.4f);
        aTexShield = new AnimatedTexture(Vector2.Zero, 0, 1.0f, 1.0f);
        gamePaused = false;        

    }

    protected override void Initialize()
    {
        elapsedTime = TimeSpan.FromSeconds(.01);    

        viewport = graphics.GraphicsDevice.Viewport;
        viewport.X += 10;
        viewport.Y += 10;
        viewport.Width -= 20;
        viewport.Height -= 20;            
        
        Player1 = new Player(new Vector2(viewport.Width / 2 - 25, viewport.Height - 100), 0f, 0f, viewport, projectileManager);
        Player1.Exp = 0;
        Player1.Level = 1;
        Player1.XOffset = -27;
        Player1.Score = 0;
        Player1.LevelPoints = 0; 

        base.Initialize();
       
        //         SOUND DISABLED
        // gameMusicSoundBank.PlayCue("background_music");        
    }  
    
    protected override void LoadContent()
    {
        gamePaused = true;
        ForegroundBatch = new SpriteBatch(graphics.GraphicsDevice);
        MenuBatch = new SpriteBatch(graphics.GraphicsDevice);
        BackgroundBatch = new SpriteBatch(graphics.GraphicsDevice);
        myBackground = new ScrollingBackground(viewport, texTerrainList, texCloudList);        
        
        PopulateEnemyTextures();
        LoadXBOXButtonTextures();
        LoadTitle();
        LoadTerrain();
        LoadClouds();
        LoadProgressBars();
        
        LoadTextures();
        LoadMenus();
    }

    private void LoadTextures()
    {
        myBackground.Load(graphics.GraphicsDevice, content.Load<Texture2D>("CPL/waternewestAlpha"));
        aTexShield.Load(graphics.GraphicsDevice, content, "shieldglowStrip", 5, 5);
        border = content.Load<Texture2D>("CPL/border");
        enemyBulletTex = content.Load<Texture2D>("CPL/enemyBullet");
        playerBulletTex = content.Load<Texture2D>("CPL/bullet");
        overlay = content.Load<Texture2D>("CPL/overlay");
        explosionTex.Load(graphics.GraphicsDevice, content, "explosionNew", 12, 20);
        Arial = content.Load<SpriteFont>("CPL/Arial");
        ComicSans = content.Load<SpriteFont>("CPL/ComicBold");
        texPowBullets = content.Load<Texture2D>("CPL/pow_bullets");
        texPowShield = content.Load<Texture2D>("CPL/pow_shield");
        texPowSpeed = content.Load<Texture2D>("CPL/pow_speed");
        enemyTex.Load(graphics.GraphicsDevice, content, "enemyplane", 3, 20);
        pPlane.Load(graphics.GraphicsDevice, content, "playerPlane", 3, 27);
    }

    private void LoadMenus()
    {
        // Menu used to purchase powerups, and stuff inside it
        powerupPurchaseMenu = new Menu(new Rectangle(235, 225, 325, 155), texProgressBar, Color.Black, Player1, 'P');
        powerupPurchaseMenu.AddMenuItem(new MenuItem(MenuContentType.TextBlock, new Vector2(10, 35), null, Arial, "Speed Powerup        150 pts", Color.White, true, 150));
        powerupPurchaseMenu.AddMenuItem(new MenuItem(MenuContentType.Texture2D, new Vector2(175, 30), texPowSpeed, Arial, null, Color.White, false, 0));
        powerupPurchaseMenu.AddMenuItem(new MenuItem(MenuContentType.TextBlock, new Vector2(10, 65), null, Arial, "Weapon Powerup      300 pts", Color.White, true, 300));
        powerupPurchaseMenu.AddMenuItem(new MenuItem(MenuContentType.Texture2D, new Vector2(175, 60), texPowBullets, Arial, null, Color.White, false, 0));
        powerupPurchaseMenu.AddMenuItem(new MenuItem(MenuContentType.TextBlock, new Vector2(10, 95), null, Arial, "Shield Powerup         500 pts", Color.White, true, 500));
        powerupPurchaseMenu.AddMenuItem(new MenuItem(MenuContentType.Texture2D, new Vector2(175, 95), texPowShield, Arial, null, Color.White, false, 0));
        // Menu used to level up your player, and stuff inside it
        playerLevelingMenu = new Menu(new Rectangle(320, 260, 153, 130), texProgressBar, Color.Black, Player1, 'L');
        playerLevelingMenu.AddMenuItem(new MenuItem(MenuContentType.TextBlock, new Vector2(11, 35), null, Arial, "Speed     + 1", Color.White, true, 1));
        playerLevelingMenu.AddMenuItem(new MenuItem(MenuContentType.TextBlock, new Vector2(11, 55), null, Arial, "Weapons  + 1", Color.White, true, 1));
        playerLevelingMenu.AddMenuItem(new MenuItem(MenuContentType.TextBlock, new Vector2(12, 75), null, Arial, "Health     + 1", Color.White, true, 1));      
    }

    private void LoadProgressBars()
    {
        texProgressBar = content.Load<Texture2D>("CPL/progressbar");
        texProgressFull = content.Load<Texture2D>("CPL/progressfull");
        texHealthProgressFill = content.Load<Texture2D>("CPL/hpProgressFiller");

        healthBar = new ProgressBar(texProgressBar, texHealthProgressFill, new Vector2(8, viewport.Height - 15), 2, 2);
        progressBar = new ProgressBar(texProgressBar, texProgressFull, new Vector2(8, viewport.Height), 2, 2);
        progressBar.Progress = 0;
    }

    private void LoadClouds()
    {
        texCloudList.Add(content.Load<Texture2D>("CPL/cloud1"));
        texCloudList.Add(content.Load<Texture2D>("CPL/cloud2"));
        texCloudList.Add(content.Load<Texture2D>("CPL/cloud3"));
        texCloudList.Add(content.Load<Texture2D>("CPL/cloud4"));
        texCloudList.Add(content.Load<Texture2D>("CPL/cloud5"));
        texCloudList.Add(content.Load<Texture2D>("CPL/cloud6"));
        texCloudList.Add(content.Load<Texture2D>("CPL/cloud7"));
        texCloudList.Add(content.Load<Texture2D>("CPL/cloud8"));
    }

    private void LoadTerrain()
    {
        texTerrainList.Add(content.Load<Texture2D>("CPL/terraintype1"));
        texTerrainList.Add(content.Load<Texture2D>("CPL/terraintype2"));
        texTerrainList.Add(content.Load<Texture2D>("CPL/terraintype4"));
    }

    private void LoadTitle()
    {
        birdsLogo = content.Load<Texture2D>("cpl/title_screen_content/BirdsOfWarLogo");
        titleTextures.Add(buttonA);
        titleTextures.Add(buttonB);
        titleTextures.Add(buttonX);
        titleTextures.Add(buttonY);
        titleTextures.Add(birdsLogo);
        birdsTitle = new TitleScreen(ComicSans);
        birdsTitle.IsActive = true;        
    }

    private void LoadXBOXButtonTextures()
    {
        // load XBOX controller button textures
        buttonA = content.Load<Texture2D>("cpl/title_screen_content/xboxControllerButtonA");
        buttonB = content.Load<Texture2D>("cpl/title_screen_content/xboxControllerButtonB");
        buttonX = content.Load<Texture2D>("cpl/title_screen_content/xboxControllerButtonX");
        buttonY = content.Load<Texture2D>("cpl/title_screen_content/xboxControllerButtonY");
    }

    private void PopulateEnemyTextures()
    {
        // Populate the enemy texture list
        for (int i = 0; i < texEnemyList.Capacity; i++)
            texEnemyList.Add(new AnimatedTexture(Vector2.Zero, 0.0f, 1.5f, 0.5f));        
        
        // Add different enemy aircraft types to the list of enemy textures
        texEnemyList[0].Load(graphics.GraphicsDevice, content, "enemyPlane", 3, 20);
        texEnemyList[1].Load(graphics.GraphicsDevice, content, "enemyPlane1", 3, 20);
        texEnemyList[2].Load(graphics.GraphicsDevice, content, "enemyPlane2", 3, 20);
        texEnemyList[3].Load(graphics.GraphicsDevice, content, "enemyPlane3", 3, 20);        
    }

    protected override void UnloadContent() { content.Unload(); }    
    
    protected override void Update(GameTime gameTime)
    {
        //gameMusicEngine.Update();

        if (!birdsTitle.IsActive)
            getReadyTime += gameTime.ElapsedGameTime.Milliseconds;
        
        if (birdsTitle.IsActive)
        {     
             //      SOUND DISABLED
            //if (!gameMusicSoundBank.IsInUse)
                //gameMusicSoundBank.PlayCue("background_music");

            birdsTitle.Update(gameTime);
            CheckKeyboardInput(gameTime);
            CheckGamepadInput(gameTime);
            return;
        }       
        
        progressBar.Progress = (float)Player1.Exp / (float)levelExp[Player1.Level];
        healthBar.Progress = (float)((float)Player1.CurHealth / (float)Player1.MaxHealth);        

        if (!gamePaused)
        {           
            int randomNum = myRandomNumberGenerator.Next(0, 150);
            
            if (randomNum == 1)
            {
                for (int i = 0; i < enemyManager.EnemyList.Count; i++)
                {
                    if (!enemyManager.EnemyList[i].IsActive)
                    {
                        randomNum = myRandomNumberGenerator.Next(10, viewport.Width - 50);
                        enemyManager.StartEnemy(new Vector2((float)randomNum, (float)-50), compassDir.South, 5);
                        break;
                    }
                }
            }

           // check for a level up
            if (Player1.Exp >= levelExp[Player1.Level - 1])
            {
                Player1.Exp = 0;
                Player1.Level++;
                Player1.LevelPoints += 1;
                Player1.CurHealth = Player1.MaxHealth;
            }

            // scrolling background timer and updates  
   
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;           
            myBackground.Update(elapsed * 100);  
                    
            // increment frame counter.               
            totalFrames++;

            //fps calculations               
            currentFPS = 1 / gameTime.ElapsedGameTime.TotalSeconds;
            
            // update stuff
            Player1.Update(gameTime);
            projectileManager.Update(gameTime, viewport);
            explosionManager.Update(gameTime, viewport);
            pPlane.UpdateFrame(elapsed);
            aTexShield.UpdateFrame(elapsed);
            explosionTex.UpdateFrame(elapsed);
            enemyManager.Update(gameTime, viewport, Player1.CurLocation);
            
            // check for any collisions
            CheckCollisions();            
        }
        
        CheckKeyboardInput(gameTime);
        CheckGamepadInput(gameTime);
        oldState = Keyboard.GetState();        
        base.Update(gameTime);      
    }

    protected override void Draw(GameTime gameTime)
    {
        string output = currentFPS.ToString();
        Vector2 FontOrigin = Arial.MeasureString(output) / 2;

        graphics.GraphicsDevice.Clear(Color.Black);        
       
        // draw background
        BackgroundBatch.Begin();
        
        // draw game title if active
        if (birdsTitle.IsActive == true)
        {
            birdsTitle.Draw(gameTime, BackgroundBatch, ComicSans, titleTextures);
            BackgroundBatch.End();
            return;
        }
        myBackground.Draw(BackgroundBatch);
        BackgroundBatch.End();            
        
        // draw foreground batch items      
        ForegroundBatch.Begin();
        
        // SOUND DISABLED
        // Play gameplay countdown
        if (getReadyTime < 3000)
        {
            
            if (getReadyTime > 0 && playedX == 0)
            {
                playedX = 1;
                //gameSoundSoundBank.PlayCue("eep");
                
            }
            if (getReadyTime > 1000 && playedX == 1)
            {
                playedX = 2;
                //gameSoundSoundBank.PlayCue("eep");
                
            }
            if (getReadyTime > 2000 && playedX == 2)
            {
                playedX = 3;
                //gameSoundSoundBank.PlayCue("eep");
                gamePaused = false;
            }          

            ForegroundBatch.DrawString(ComicSans, "G E T  R E A D Y !", new Vector2(viewport.Width / 2 - 80, viewport.Height / 2), Color.WhiteSmoke);
        }        

        if (consoleIsActive)
        {         
            ForegroundBatch.DrawString(Arial , output + " FPS", new Vector2(viewport.X + 75, viewport.Y + 10), Color.Red, 0.0f, FontOrigin, .75f, SpriteEffects.None, 0.5f);
            ForegroundBatch.DrawString(Arial, "Total Enemies: " + enemyManager.Count, new Vector2(viewport.X + 75, viewport.Y + 25), Color.Red, 0.0f, FontOrigin, .75f, SpriteEffects.None, 0.5f);
            ForegroundBatch.DrawString(Arial, "Total Bullets: " + projectileManager.Count, new Vector2(viewport.X + 75, viewport.Y + 40), Color.Red, 0.0f, FontOrigin, .75f, SpriteEffects.None, 0.5f);
            ForegroundBatch.DrawString(Arial, "Total EXP: [" + "LVL" + Player1.Level + "] " + Player1.Exp + "/" + levelExp[Player1.Level - 1], new Vector2(viewport.X + 75, viewport.Y + 55), Color.Red, 0.0f, FontOrigin, .75f, SpriteEffects.None, 0.5f);
            ForegroundBatch.DrawString(Arial, "Skill points left: " + Player1.LevelPoints, new Vector2(viewport.X + 75, viewport.Y + 70), Color.Red, 0.0f, FontOrigin, .75f, SpriteEffects.None, 0.5f);
          
        }

        // draw progress bar
        progressBar.Draw(gameTime, ForegroundBatch);
        healthBar.Draw(gameTime, ForegroundBatch);

        ForegroundBatch.DrawString(ComicSans, "SCORE: " + Player1.Score, new Vector2(viewport.X + 2, viewport.Height - 65), Color.WhiteSmoke, 0.0f, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0.5f);

        ForegroundBatch.DrawString(ComicSans, "LEVEL: " + Player1.Level, new Vector2(viewport.X + 2, viewport.Height - 40), Color.WhiteSmoke);
        // draw bullets that are active
        projectileManager.Draw(ForegroundBatch, playerBulletTex, enemyBulletTex, gameTime);
        // draw enemies that are active
        enemyManager.Draw(ForegroundBatch, texEnemyList, gameTime);
        // draw player if not dead
        if (Player1.IsActive) 
        {
            pPlane.DrawFrame(ForegroundBatch, Player1.CurLocation);
        }
        // draw player's shield if it is active
        if (Player1.ShieldActive) 
            aTexShield.DrawFrame(ForegroundBatch, new Vector2(Player1.CurLocation.X - 65, Player1.CurLocation.Y - 65));
        // draw explosions that are active
        explosionManager.Draw(ForegroundBatch, explosionTex, gameTime);
        
        // draw speed icon if player has it
        if (Player1.HasSpeedPowerup)
            ForegroundBatch.Draw(texPowSpeed, new Vector2(150, viewport.Height - 9), Color.White);
        // draw weapon icon if player has it
        if (Player1.HasWeaponPowerup)   
            ForegroundBatch.Draw(texPowBullets, new Vector2(180, viewport.Height - 15), Color.White);
        // draw shield icon if player has it
        if (Player1.HasShieldPowerup)  
            ForegroundBatch.Draw(texPowShield, new Vector2(205, viewport.Height - 15), Color.White);
        // I used a pink overlay to visually represent collision test's bounding boxes during debug
        //ForegroundBatch.Draw(overlay, new Rectangle(viewport.X + 10, viewport.Y + 10, viewport.Width - 20, viewport.Height - 20), Color.White);
        //ForegroundBatch.Draw(overlay, bulletRectangle, Color.White);
        
        // DRAW A BORDER FOOL!
        ForegroundBatch.Draw(border,new Vector2(0,0), Color.Black);
        
        ForegroundBatch.End();

        // begin menu spritebatch, and draw any active menus
        MenuBatch.Begin();
        if (powerupPurchaseMenu.IsActive)
        {
            gamePaused = true;
            powerupPurchaseMenu.Draw(MenuBatch, gameTime);
        }
        if (playerLevelingMenu.IsActive)
        {
            playerLevelingMenu.Draw(MenuBatch, gameTime);
            gamePaused = true;
        }
        MenuBatch.End();

        if (!Player1.IsActive)
            gamePaused = true;

        base.Draw(gameTime);
    }

    protected void CheckCollisions()
    {       
        List<Enemy> enemies = enemyManager.EnemyList;
        List <Bullet> bullets = projectileManager.ProjectileList;

        for (int k = 0; k < bullets.Count; k++)
            if (bullets[k].IsActive && bullets[k].Shooter == 'E')
            {
                bulletRectangle = new Rectangle((int)bullets[k].CurPosition.X, (int)bullets[k].CurPosition.Y, enemyBulletTex.Width, enemyBulletTex.Height);
                playerRectangle = new Rectangle((int)Player1.CurLocation.X, (int)Player1.CurLocation.Y, pPlane.MyTexture.Width / 3, pPlane.MyTexture.Height);
                
                if (bulletRectangle.Intersects(playerRectangle))
                {
                    bullets[k].IsActive = false;
                    if (!Player1.ShieldActive)
                    {
                        explosionManager.StartExplosion(new Vector2(Player1.CurLocation.X, (Player1.CurLocation.Y)));
                        //gameSoundSoundBank.PlayCue("explosion");
                        GamePad.SetVibration(PlayerIndex.One, .8f, .8f);
                        vibrationTime = 0;
                        bullets[k].IsActive = false;
                        Player1.CurHealth -= 1;
                        if (Player1.CurHealth == 0)
                        {                           
                            Player1.IsActive = false;                            
                        }                          
                    }
                }
            }

        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i].IsActive)
            {
                enemyRectangle = new Rectangle((int)enemies[i].CurPosition.X, (int)enemies[i].CurPosition.Y, (enemyTex.MyTexture.Width /3 ), enemyTex.MyTexture.Height);
                
                for (int j = 0; j < bullets.Count; j++)
                {
                    if (bullets[j].IsActive && bullets[j].Shooter == 'P')
                    {                       
                        bulletRectangle = new Rectangle((int)bullets[j].CurPosition.X, (int)bullets[j].CurPosition.Y, playerBulletTex.Width, playerBulletTex.Height);
                        // Check collision 
                        if (bulletRectangle.Intersects(enemyRectangle))
                        {
                            explosionManager.StartExplosion(new Vector2(enemies[i].CurPosition.X - ((enemyTex.MyTexture.Width / 3) /2), enemies[i].CurPosition.Y - enemyTex.MyTexture.Height / 2));
                            //gameSoundSoundBank.PlayCue("explosion");
                            Player1.Score += 50;
                            Player1.Exp += 50;
                            enemies[i].IsActive = false;
                            bullets[j].IsActive = false;
                        }
                    }
                }
            }
        }
        enemyManager.EnemyList = enemies;
        projectileManager.ProjectileList = bullets;
    }   

    void CheckGamepadInput(GameTime gameTime)
    {
        vibrationTime += (float)gameTime.ElapsedGameTime.Milliseconds;
        menuTime += (float)gameTime.ElapsedGameTime.Milliseconds;
        if (vibrationTime > 500)
        {
            GamePad.SetVibration(PlayerIndex.One, 0, 0);
            vibrationTime = 0.0f;
        }
        
         GamePadState currentState = GamePad.GetState(PlayerIndex.One);
         if (currentState.ThumbSticks.Left.X < -.2)
            {
                if (!gamePaused && !birdsTitle.IsActive && Player1.CurLocation.X > 0)
                {
                     
                        Player1.Move(compassDir.West);
                }
            }
            if (currentState.ThumbSticks.Left.X > .2)
            {
                if (!gamePaused && !birdsTitle.IsActive && (Player1.CurLocation.X < viewport.Width - 45))
                {
                    Player1.Move(compassDir.East);
                }
            }
            if (currentState.ThumbSticks.Left.Y < -.2)
            {
                if (powerupPurchaseMenu.IsActive)
                {

                    if (menuTime > 100)
                    {
                        powerupPurchaseMenu.KeyPress(Keys.Down);
                        menuTime = 0;
                    }
                   
                }
                if (playerLevelingMenu.IsActive)
                {
                    if (menuTime > 100)
                    {
                        playerLevelingMenu.KeyPress(Keys.Down);
                        menuTime = 0;
                    }
                    
                }
                else if (!powerupPurchaseMenu.IsActive && !playerLevelingMenu.IsActive && !gamePaused)
                    if (Player1.CurLocation.Y < viewport.Height - 45)
                        Player1.Move(compassDir.South);
            }
            if (currentState.ThumbSticks.Left.Y > .2)
            {
                if (powerupPurchaseMenu.IsActive)
                {

                    if (menuTime > 100)
                    {
                        powerupPurchaseMenu.KeyPress(Keys.Up);
                        menuTime = 0;
                    }
                    
                }
                if (playerLevelingMenu.IsActive)
                {

                    if (menuTime > 100)
                    {
                        playerLevelingMenu.KeyPress(Keys.Up);
                        menuTime = 0;
                    }
                    
                }
                else if (!powerupPurchaseMenu.IsActive && !playerLevelingMenu.IsActive && !gamePaused)
                    if (Player1.CurLocation.Y > 300)
                        Player1.Move(compassDir.North);     
            }        
       
        // Check the current state of Player One.
        // Process input only if connected 
        if ((currentState.IsConnected))            
        {            
            if (currentState.Buttons.A == ButtonState.Pressed &&
                previousGamePadState.Buttons.A == ButtonState.Released)
            {
                // Button A has just been pressed.
                if (birdsTitle.IsActive)
                {
                    //gameSoundSoundBank.PlayCue("eep");
                    birdsTitle.IsActive = false;
                    return;
                }
                if (powerupPurchaseMenu.IsActive)
                {                    
                        powerupPurchaseMenu.KeyPress(Keys.Space);                    
                }
                if (playerLevelingMenu.IsActive)
                {                   
                        playerLevelingMenu.KeyPress(Keys.Space);                    
                }
                else
                {
                    if (!gamePaused && !birdsTitle.IsActive && (elapsedTime > TimeSpan.FromSeconds(.3)))
                    {
                        GamePad.SetVibration(PlayerIndex.One, vibrationAmount, vibrationAmount);
                   
                        Player1.Fire();
                        // gameSoundSoundBank.PlayCue("gunshot");
                        elapsedTime = TimeSpan.Zero;
                        vibrationTime = 0;                          
                    }
                }                  
            }
            if (currentState.Buttons.LeftShoulder == ButtonState.Pressed &&
                previousGamePadState.Buttons.LeftShoulder == ButtonState.Released)
            {
                if (!powerupPurchaseMenu.IsActive && !birdsTitle.IsActive)
                {
                    // gameSoundSoundBank.PlayCue("eep");
                    powerupPurchaseMenu.IsActive = true;
                    gamePaused = true;
                }
                else if(powerupPurchaseMenu.IsActive && !birdsTitle.IsActive)
                {
                    // gameSoundSoundBank.PlayCue("eep");
                    gamePaused = false;
                    powerupPurchaseMenu.IsActive = false;
                }

            }
            if (currentState.Buttons.RightShoulder == ButtonState.Pressed &&
                previousGamePadState.Buttons.RightShoulder == ButtonState.Released)
            {
                if (!playerLevelingMenu.IsActive && !birdsTitle.IsActive)
                {
                    // gameSoundSoundBank.PlayCue("eep");
                    playerLevelingMenu.IsActive = true;
                    gamePaused = true;
                }
                else if (playerLevelingMenu.IsActive && !birdsTitle.IsActive)
                {
                    // gameSoundSoundBank.PlayCue("eep");
                    gamePaused = false;
                    playerLevelingMenu.IsActive = false;
                }

            }
            if (currentState.Triggers.Left > .05f &&
               previousGamePadState.Triggers.Left < .05f)
            {
                if (Player1.HasShieldPowerup)
                {
                    if (Player1.ShieldActive == false)
                        Player1.ShieldActive = true;
                    else
                        Player1.ShieldActive = false;
                }                

            }
            // When finished with differences, update PreviousGamePadState.
            previousGamePadState = currentState;
        }
    }

    protected void CheckKeyboardInput(GameTime gameTime)
    {
        elapsedTime += gameTime.ElapsedGameTime;
        //Get the current state of the keyboard
        KeyboardState aKeyboard = Keyboard.GetState();
        //Get the current keys being pressed
        Keys[] aCurrentKeys = aKeyboard.GetPressedKeys();
        
        //Cycle through all of the keys being pressed and move the sprite accordingly
        for (int aCurrentKey = 0; aCurrentKey < aCurrentKeys.Length; aCurrentKey++)
        {
            //Change the sprite's direction to the left and move it left
            if (aCurrentKeys[aCurrentKey] == Keys.Left)
            {
                if (powerupPurchaseMenu.IsActive || playerLevelingMenu.IsActive)
                {
                    if (!oldState.IsKeyDown(Keys.Left) && Keyboard.GetState().IsKeyDown(Keys.Left))
                    {
                        powerupPurchaseMenu.KeyPress(Keys.Left);
                    }
                }
                else
                    if (Player1.CurLocation.X > 0)                       
                        Player1.Move(compassDir.West);
            }
            //Change the sprite's direction to the right and move it right
            if (aCurrentKeys[aCurrentKey] == Keys.Right)
            {
                if (powerupPurchaseMenu.IsActive || playerLevelingMenu.IsActive)
                {
                    if (!oldState.IsKeyDown(Keys.Right) && Keyboard.GetState().IsKeyDown(Keys.Right))
                    {
                        powerupPurchaseMenu.KeyPress(Keys.Right);
                    }
                }
                else
                    if (Player1.CurLocation.X < viewport.Width - 45)                     
                        Player1.Move(compassDir.East);
            }
            //Change the sprite's direction to up and move it up
            if (aCurrentKeys[aCurrentKey] == Keys.Up)
            {
                if (powerupPurchaseMenu.IsActive)
                {
                    if (!oldState.IsKeyDown(Keys.Up) && Keyboard.GetState().IsKeyDown(Keys.Up))
                    {
                        powerupPurchaseMenu.KeyPress(Keys.Up);
                    }
                }
                if (playerLevelingMenu.IsActive)
                {
                    if (!oldState.IsKeyDown(Keys.Up) && Keyboard.GetState().IsKeyDown(Keys.Up))
                    {
                        playerLevelingMenu.KeyPress(Keys.Up);
                    }
                }
                else if (!powerupPurchaseMenu.IsActive && !playerLevelingMenu.IsActive)
                    if (Player1.CurLocation.Y > 300)                        
                        Player1.Move(compassDir.North);                              
            }
            //Change the sprite's direction to down and move it down
            if (aCurrentKeys[aCurrentKey] == Keys.Down)
            {

                if (powerupPurchaseMenu.IsActive)
                {
                    if (!oldState.IsKeyDown(Keys.Down) && Keyboard.GetState().IsKeyDown(Keys.Down))
                    {
                        powerupPurchaseMenu.KeyPress(Keys.Down);
                    }
                }
                if (playerLevelingMenu.IsActive)
                {
                    if (!oldState.IsKeyDown(Keys.Down) && Keyboard.GetState().IsKeyDown(Keys.Down))
                    {
                        playerLevelingMenu.KeyPress(Keys.Down);
                    }
                }
                else if (!powerupPurchaseMenu.IsActive && !playerLevelingMenu.IsActive)
                    if (Player1.CurLocation.Y < viewport.Height - 45)              
                        Player1.Move(compassDir.South);
            }
            //Exit the game if the Escape key has been pressed
            if (aCurrentKeys[aCurrentKey] == Keys.Escape)
            {
                this.Exit();
            }
            if (aCurrentKeys[aCurrentKey] == Keys.Space)
            {
                if (powerupPurchaseMenu.IsActive)
                {
                    if (!oldState.IsKeyDown(Keys.Space) && Keyboard.GetState().IsKeyDown(Keys.Space))
                    {
                        powerupPurchaseMenu.KeyPress(Keys.Space);
                    }
                }
                if (playerLevelingMenu.IsActive)
                {
                    if (!oldState.IsKeyDown(Keys.Space) && Keyboard.GetState().IsKeyDown(Keys.Space))
                    {
                        playerLevelingMenu.KeyPress(Keys.Space);
                    }
                }
                else
                {                   
                    if (!gamePaused)
                    {
                        if (!oldState.IsKeyDown(Keys.Space) && Keyboard.GetState().IsKeyDown(Keys.Space) && elapsedTime > TimeSpan.FromSeconds(.3)) 
                        {
                            Player1.Fire();
                            //gameSoundSoundBank.PlayCue("gunshot");
                            elapsedTime = TimeSpan.Zero; 
                        }
                    }                    
                }
            }
                    
            if (!oldState.IsKeyDown(Keys.LeftShift) && Keyboard.GetState().IsKeyDown(Keys.LeftShift))
            {
                if (Player1.HasShieldPowerup)
                {
                    if (Player1.ShieldActive == false)
                        Player1.ShieldActive = true;
                    else
                        Player1.ShieldActive = false;
                }
            }

            if (!oldState.IsKeyDown(Keys.N) && Keyboard.GetState().IsKeyDown(Keys.N))
            {
                if (birdsTitle.IsActive)
                {
                    //gameSoundSoundBank.PlayCue("eep");
                    birdsTitle.IsActive = false;
                }
            }
            if (!oldState.IsKeyDown(Keys.Q) && Keyboard.GetState().IsKeyDown(Keys.Q))
            {
                if (birdsTitle.IsActive)
                {
                    // gameSoundSoundBank.PlayCue("eep");
                    Exit();
                }
            }
            
            if (!oldState.IsKeyDown(Keys.M) && Keyboard.GetState().IsKeyDown(Keys.M))
            {             
                if (!powerupPurchaseMenu.IsActive)
                {
                    //gameSoundSoundBank.PlayCue("eep");
                    powerupPurchaseMenu.IsActive = true;
                    gamePaused = true;                 
                }
                else 
                {
                    //gameSoundSoundBank.PlayCue("eep");
                    gamePaused = false;
                    powerupPurchaseMenu.IsActive = false;                    
                }
            }
            if (!oldState.IsKeyDown(Keys.C) && Keyboard.GetState().IsKeyDown(Keys.C))
            {
                if (!playerLevelingMenu.IsActive)
                {
                    //gameSoundSoundBank.PlayCue("eep");
                    playerLevelingMenu.IsActive = true;
                    gamePaused = true;
                }
                else
                {
                    //gameSoundSoundBank.PlayCue("eep");
                    gamePaused = false;
                    playerLevelingMenu.IsActive = false;
                }
            }
            if (!oldState.IsKeyDown(Keys.OemTilde) && Keyboard.GetState().IsKeyDown(Keys.OemTilde))
            {
                if (!consoleIsActive)
                {
                    consoleIsActive = true;
                }
                else
                {
                    consoleIsActive = false;
                }
            }
        }        
    }   
}



