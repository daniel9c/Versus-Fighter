using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using PorradaEngine.Anim;
using PorradaEngine;
using PorradaEngine.Xml;

namespace Versus_Fighter
{
    /// <summary>
    /// Classe principal Versus_Fighter
    /// </summary>
    public class VersusFighter : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Player player1;
        Player player2;

        public VersusFighter()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// </summary>
        protected override void Initialize()
        {
            
            //Setando o nome da pasta das texturas
            ResourceManager.Instance.PastaTextura = ResourceManager.pastaSprites;

            //Setando o nome das texturas a serem carregadas
            ResourceManager.Instance.AddTextureToLoad(ResourceManager.spritePersonagemRyu);
            //nome da textura de colisao, esta textura sera criada internamente
            ResourceManager.Instance.AddTextureToLoad(ResourceManager.rectangleColision); 


            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);            
            
            //carregando as texturas
            ResourceManager.Instance.LoadTextures(Content, GraphicsDevice);

            //carregando a extrutura xml do personagem para um objeto com a mesma estrutura
            PlayerXml xml = Content.Load<PlayerXml>(@"Personagens/Ryu");

            //criando player
            player1 = new Player(xml);
            player1.PosX = 50;
            player1.PosY = 100;

            //config. teclas de acao
            ControllerConfiguration controller1 = new ControllerConfiguration();
            controller1.HardPunch = Keys.Q;
            controller1.Left = Keys.J;
            controller1.Right = Keys.L;
            player1.Controller = controller1;

            player2 = new Player(xml);
            player2.PosX = 250;
            player2.PosY = 100;
            player2.FaceLeft = false;

            ControllerConfiguration controller2 = new ControllerConfiguration();
            controller2.HardPunch = Keys.D9;
            controller2.Left = Keys.Left;
            controller2.Right = Keys.Right;
            player2.Controller = controller2;

        }


        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            player1.Update();
            player2.Update();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            player1.Draw(spriteBatch);
            player2.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
