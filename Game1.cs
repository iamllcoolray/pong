using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace pong;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private Point gameBounds = new Point(1280, 720);
    
    private KeyboardState keyboardState;
    
    private Rectangle paddleLeft, paddleRight, ball;
    private float paddleSpeed = 1.0f;
    
    private Vector2 ballVelocity, ballPosition;
    private float ballSpeed = 2.0f;
    
    private Texture2D texture;
    
    private Random random = new Random();

    private byte hitCounter = 0;
    
    private int scoreLeft, scoreRight;
    private const int MAX_TOTAL_SCORE = 5;
    
    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        Window.Title = "Pong";
        _graphics.PreferredBackBufferWidth = gameBounds.X;
        _graphics.PreferredBackBufferHeight = gameBounds.Y;
        _graphics.ApplyChanges();
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

        // TODO: use this.Content to load your game content here
        StartGame();
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
        PaddleMovement(gameTime);
        AIPaddleMovement(gameTime);
        BallMovement(gameTime);
        
        IsGameOver();
        
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        // TODO: Add your drawing code here
        _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp);

        int total = gameBounds.Y / 20;
        for (int i = 0; i < total; i++)
        {
            DrawRectangle(_spriteBatch, new Rectangle(gameBounds.X / 2 - 4, 5 + (i * 20), 8 , 8), Color.White);
        }
        
        DrawRectangle(_spriteBatch, paddleLeft, Color.White);
        DrawRectangle(_spriteBatch, paddleRight, Color.White);

        ball.X = (int)ballPosition.X;
        ball.Y = (int)ballPosition.Y;
        DrawRectangle(_spriteBatch, ball, Color.White);

        for (int i = 0; i < scoreLeft; i++)
        {
            DrawRectangle(_spriteBatch, new Rectangle((gameBounds.X / 2 - 25) - i * 12, 10, 10, 10), Color.White);
        }
        
        for (int i = 0; i < scoreRight; i++)
        {
            DrawRectangle(_spriteBatch, new Rectangle((gameBounds.X / 2 + 15) + i * 12, 10, 10, 10), Color.White);
        }
        
        _spriteBatch.End();

        base.Draw(gameTime);
    }
    
    private void BallMovement(GameTime gameTime)
    {
        const float MAX_VELOCITY = 1.5f;

        if (ballVelocity.X > MAX_VELOCITY)
        {
            ballVelocity.X = MAX_VELOCITY;
        }
        else if (ballVelocity.X < -MAX_VELOCITY)
        {
            ballVelocity.X = -MAX_VELOCITY;
        }
        
        if (ballVelocity.Y > MAX_VELOCITY)
        {
            ballVelocity.Y = MAX_VELOCITY;
        }
        else if (ballVelocity.Y < -MAX_VELOCITY)
        {
            ballVelocity.Y = -MAX_VELOCITY;
        }
        
        
        ballPosition.X += ballVelocity.X * ballSpeed * gameTime.ElapsedGameTime.Milliseconds;
        ballPosition.Y += ballVelocity.Y * ballSpeed * gameTime.ElapsedGameTime.Milliseconds;

        hitCounter++;
        if (hitCounter > 10)
        {
            if (paddleLeft.Intersects(ball))
            {
                ballVelocity.X *= -1;
                ballVelocity.Y *= 1.1f;
                hitCounter = 0;
                ballPosition.X = paddleLeft.X + paddleLeft.Width + 10;
            }

            if (paddleRight.Intersects(ball))
            {
                ballVelocity.X *= -1;
                ballVelocity.Y *= 1.1f;
                hitCounter = 0;
                ballPosition.X = paddleRight.X - 10;
            }
        }
        
        if (ballPosition.X < 0)
        {
            ballPosition.X = ballSpeed + 1;
            ballVelocity.X *= -1;
            scoreRight++;
        }
        else if (ballPosition.X > gameBounds.X)
        {
            ballPosition.X = ballSpeed + gameBounds.X - 1;
            ballVelocity.X *= -1;
            scoreLeft++;
        }

        if (ballPosition.Y < 0 + 10)
        {
            ballPosition.Y = ballSpeed + 10 + 1;
            ballVelocity.Y *= -(1 + random.Next(-100, 101) * 0.005f);
        }
        else if (ballPosition.Y > gameBounds.Y - 10)
        {
            ballPosition.Y = ballSpeed + gameBounds.Y - 1 * gameTime.ElapsedGameTime.Milliseconds;
            ballVelocity.Y *= -(1 + random.Next(-100, 101) * 0.005f);
        }
    }
    private void PaddleMovement(GameTime gameTime)
    {
        keyboardState = Keyboard.GetState();
        
        if (keyboardState.IsKeyDown(Keys.W))
        {
            paddleLeft.Y -= (int)paddleSpeed * gameTime.ElapsedGameTime.Milliseconds;
        }
        if (keyboardState.IsKeyDown(Keys.S))
        {
            paddleLeft.Y += (int)paddleSpeed * gameTime.ElapsedGameTime.Milliseconds;
        }
        PaddleBoundsLimiter(ref paddleLeft);
    }

    private void AIPaddleMovement(GameTime gameTime)
    {
        int paddleCenter = paddleRight.Y + paddleRight.Height / 2;
        if (paddleCenter < ballPosition.Y - 20)
        {
            paddleRight.Y += (int)((ballPosition.Y - paddleCenter) * paddleSpeed * 5f  * (float)gameTime.ElapsedGameTime.TotalSeconds);
        }
        else if (paddleCenter > ballPosition.Y + 20)
        {
            paddleRight.Y -= (int)((paddleCenter - ballPosition.Y) * paddleSpeed * 5f * (float)gameTime.ElapsedGameTime.TotalSeconds);
        }
        PaddleBoundsLimiter(ref paddleRight);
    }
    
    private void PaddleBoundsLimiter(ref Rectangle paddle)
    {
        if (paddle.Y < 10)
        {
            paddle.Y = 10;
        }
        else if (paddle.Y + paddle.Height > gameBounds.Y - 10)
        {
            paddle.Y = gameBounds.Y - paddle.Height - 10;
        }
    }
    
    private void DrawRectangle(SpriteBatch spriteBatch, Rectangle rectangle, Color color)
    {
        Vector2 position = new Vector2(rectangle.X, rectangle.Y);
        Vector2 size = new Vector2(rectangle.Width, rectangle.Height);
        spriteBatch.Draw(texture, position, null, color * 1.0f, 0.0f, Vector2.Zero, size, SpriteEffects.None, 0.0f);
    }
    
    private void StartGame()
    {
        if (texture == null)
        {   //create texture to draw with if it does not exist
            texture = new Texture2D(_graphics.GraphicsDevice, 1, 1);
            texture.SetData<Color>(new Color[] { Color.White });
        }
        
        int paddleWidth = 20;
        int paddleHeight = 100;
        paddleLeft = new Rectangle(10, (gameBounds.Y / 2) - (paddleHeight / 2), paddleWidth, paddleHeight);
        paddleRight = new Rectangle(gameBounds.X - 30, (gameBounds.Y / 2) - (paddleHeight / 2), paddleWidth, paddleHeight);
        
        int ballSize = 20;
        ballPosition = new Vector2((gameBounds.X / 2) - (ballSize / 2), (gameBounds.Y / 2) - (ballSize / 2));
        ballVelocity = new Vector2(1, 0.1f);
        ball = new Rectangle((int)ballPosition.X, (int)ballPosition.Y, ballSize, ballSize);
        
        scoreLeft = 0;
        scoreRight = 0;
    }

    private void IsGameOver()
    {
        if (scoreLeft >= MAX_TOTAL_SCORE)
        {
            StartGame();
        }
        else if (scoreRight >= MAX_TOTAL_SCORE)
        {
            StartGame();
        }
    }
}