using System.Diagnostics;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SimpleAnim;

namespace Assignment_01;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    
    private SimpleAnimation _walkingAnimation;
    private SimpleAnimation _frogIdle;

    private SpriteFont _arial;

    private Texture2D _background;
    private Texture2D _signpost;
    private Texture2D _movingBlock;

    private Vector2 _blockPos;
    private Vector2 _blockEndPos;
    private Vector2 _blockStartPos;
    private Vector2 _playerLocation;
    private Vector2 _frogSpawn;  
    Vector2 _playerInput;
    private Rectangle _screenBox;

    private int _speed = 5;
    private int _moveSpeed = 10;
    private string _text = "Welcome to Pixel World!";

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        _graphics.PreferredBackBufferWidth = (GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width)/2;
        _graphics.PreferredBackBufferHeight = (GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height)/2;
        _graphics.ApplyChanges();
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        _playerLocation = new Vector2(20, _graphics.PreferredBackBufferHeight - 82);

        _frogSpawn = new Vector2(250, _graphics.PreferredBackBufferHeight - 82);

        _blockPos = new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight - 200);
        _blockEndPos = new Vector2(_graphics.PreferredBackBufferWidth - 200, _graphics.PreferredBackBufferHeight - 200);
        _blockStartPos = new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight - 200);

        _screenBox = new Rectangle(0, 0, _graphics.PreferredBackBufferWidth -64, _graphics.PreferredBackBufferHeight - 64);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _arial = Content.Load<SpriteFont>("Arial");

        _background = Content.Load<Texture2D>("forest_pixel");

        _signpost = Content.Load<Texture2D>("signpost32px");

        _movingBlock = Content.Load<Texture2D>("Pixel_Ground_Sprite");

        _frogIdle = new SimpleAnimation(Content.Load<Texture2D>("FrogNinjaIdle"), 64, 64, 11, 10);

        _walkingAnimation = new SimpleAnimation(Content.Load<Texture2D>("SpacemanRun"), 64, 64, 12, 10);
        

        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        
        _walkingAnimation.Update(gameTime);
        _frogIdle.Update(gameTime);

        _blockPos.X += _speed;
        
        if (_blockPos == _blockEndPos)
        {
            _speed = -_speed;
        }
        if (_blockPos == _blockStartPos)
        {
            _speed = -_speed;
        }
                KeyboardState kbCurrentState = Keyboard.GetState();
        _playerInput = Vector2.Zero;

        if (kbCurrentState.IsKeyDown(Keys.Down))
        {
            _playerInput += new Vector2(0,1);
        }
        if (kbCurrentState.IsKeyDown(Keys.Up))
        {
            _playerInput += new Vector2(0,-1);
        }
        if (kbCurrentState.IsKeyDown(Keys.Left))
        {
            _playerInput += new Vector2(-1,0);
        }
        if (kbCurrentState.IsKeyDown(Keys.Right))
        {
            _playerInput += new Vector2(1,0);
        }

        if (!_screenBox.Contains(_playerLocation))
        {
            _playerLocation.X = MathHelper.Clamp(_playerLocation.X, 0, _graphics.PreferredBackBufferWidth - 64);
            _playerLocation.Y = MathHelper.Clamp(_playerLocation.Y, 0, _graphics.PreferredBackBufferHeight - 64);
        }
        
        _playerLocation += _playerInput*_moveSpeed;

        Console.WriteLine($"{_playerLocation}");


        // TODO: Add your update logic here

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();

        _spriteBatch.Draw(_background, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.White);

        _spriteBatch.DrawString(_arial, _text, Vector2.Zero, Color.White);

        _spriteBatch.Draw(_movingBlock, _blockPos, Color.White);

        _spriteBatch.Draw(_signpost, new Rectangle(_graphics.PreferredBackBufferWidth/4, _graphics.PreferredBackBufferHeight-100,80,100),Color.White);

        _walkingAnimation.Draw(_spriteBatch, _playerLocation, SpriteEffects.None);

        _frogIdle.Draw(_spriteBatch, _frogSpawn, SpriteEffects.None);

        _spriteBatch.End();

        // TODO: Add your drawing code here

        base.Draw(gameTime);
    }
}
