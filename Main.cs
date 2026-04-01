using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Geometry;

public class Main : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    CollisionNode2D cShape1;
    CollisionNode2D cShape2;

    public Main()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        base.Initialize();

        var rect1 = new RectangleShape2D(100, 100);
        var circ1 = new CircleShape2D(30);

        cShape1 = new CollisionNode2D(Vector2.Zero, rect1);
        cShape2 = new CollisionNode2D(Vector2.Zero, circ1);

        Console.WriteLine($"Intersecting: {cShape1.CheckIntersection(cShape2)}");
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();

        DrawPolygon(cShape1.Shape.Vertices);

        _spriteBatch.End();

        base.Draw(gameTime);
    }

    private void DrawPolygon(Vector2[] vertices)
    {
        for (int i = 0; i < vertices.Length; i++)
        {
            Vector2 start = vertices[i];
            Vector2 end = vertices[(i + 1) % vertices.Length];

            DrawLine(start, end, Color.Red);
        }
    }

    private void DrawLine(Vector2 start, Vector2 end, Color color)
    {
        float length = Vector2.Distance(start, end);
        float angle = (float)Math.Atan2(end.Y - start.Y, end.X - start.X);

        Texture2D lineTexture = new Texture2D(GraphicsDevice, 1, 1);
        lineTexture.SetData(new Color[] { color });

        _spriteBatch.Draw(lineTexture, start, null, color, angle, Vector2.Zero, new Vector2(length, 1), SpriteEffects.None, 0);
    }
}
