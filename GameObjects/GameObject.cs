using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Catapult
{
    class GameObject
    {
        protected Texture2D _texture;

        public Vector2 Position;
        public float Rotation;
        public int Scale;
        public int width, height;
        public Vector2 Velocity;

        public string Name;

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, _texture.Width * Scale, _texture.Height * Scale);
            }
        }

        public GameObject(Texture2D texture)
        {
            _texture = texture;
            Position = Vector2.Zero;
            Scale = 1;
            Rotation = 0f;
            width = _texture.Width;
            height = _texture.Height;
        }

        public GameObject()
        {
            Position = Vector2.Zero;
            Scale = 1;
            Rotation = 0f;
        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }

        public virtual void Reset()
        {

        }

        #region Collision
        protected bool IsTouchingLeft(GameObject sprite)
        {
            return this.Rectangle.Right + this.Velocity.X > sprite.Rectangle.Left &&
                    this.Rectangle.Left < sprite.Rectangle.Left &&
                    this.Rectangle.Bottom > sprite.Rectangle.Top &&
                    this.Rectangle.Top < sprite.Rectangle.Bottom;
        }
        protected bool IsTouchingRight(GameObject sprite)
        {
            return this.Rectangle.Left + this.Velocity.X < sprite.Rectangle.Right &&
                    this.Rectangle.Right > sprite.Rectangle.Right &&
                    this.Rectangle.Bottom > sprite.Rectangle.Top &&
                    this.Rectangle.Top < sprite.Rectangle.Bottom;
        }
        protected bool IsTouchingTop(GameObject sprite)
        {
            return this.Rectangle.Bottom + this.Velocity.Y > sprite.Rectangle.Top &&
                    this.Rectangle.Top < sprite.Rectangle.Top &&
                    this.Rectangle.Right > sprite.Rectangle.Left &&
                    this.Rectangle.Left < sprite.Rectangle.Right;
        }
        protected bool IsTouchingBottom(GameObject sprite)
        {
            return this.Rectangle.Top + this.Velocity.Y < sprite.Rectangle.Bottom &&
                    this.Rectangle.Bottom > sprite.Rectangle.Bottom &&
                    this.Rectangle.Right > sprite.Rectangle.Left &&
                    this.Rectangle.Left < sprite.Rectangle.Right;
        }
        #endregion
    }
}
