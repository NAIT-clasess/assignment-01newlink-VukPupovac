using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SimpleAnim;

namespace Assignment_01;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private SpriteFont _arial;
    private SimpleAnimation _walkingAnimation;
    Vector2 _playerInput;
    private Texture2D _background;
    private Texture2D _signpost;
    private Texture2D _movingBlock;
    private Vector2 _blockPos;
    private Vector2 _blockEndPos;
    private Vector2 _blockStartPos;
    private Vector2 _spawnPoint;

    private int _speed = 5;
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


        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _arial = Content.Load<SpriteFont>("Arial");
        _background = Content.Load<Texture2D>("forest_pixel");
        _signpost = Content.Load<Texture2D>("signpost32px");
        _movingBlock = Content.Load<Texture2D>("Pixel_Ground_Sprite");
        _blockPos = new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight - 200);
        _blockEndPos = new Vector2(_graphics.PreferredBackBufferWidth - 200, _graphics.PreferredBackBufferHeight - 200);
        _blockStartPos = new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight - 200);
        _spawnPoint = new Vector2(20, _graphics.PreferredBackBufferHeight/2);
        _walkingAnimation = new SimpleAnimation(Content.Load<Texture2D>("Run (32x32)"), 384/12, 32, 12, 10);
        

        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        
        _walkingAnimation.Update(gameTime);

        _blockPos.X += _speed;
        
        if (_blockPos == _blockEndPos)
        {
            _speed = -_speed;
        }
        if (_blockPos == _blockStartPos)
        {
            _speed = -_speed;
        }

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
        _walkingAnimation.Draw(_spriteBatch, _spawnPoint, SpriteEffects.None);
        _spriteBatch.End();

        // TODO: Add your drawing code here

        base.Draw(gameTime);
    }
}
