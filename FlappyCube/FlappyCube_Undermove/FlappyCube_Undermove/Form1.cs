using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace FlappyCube_Undermove
{
    public partial class Form1 : Form
    {
        //сила тяжести чем она выше тем быстрее игрок набирает скорость
        const int gravity = 1;
        const int minWidth = 2;
        //переменная которая хранит в себе кадр
        Bitmap bmp;
        //определяет стиль контуров
        Pen pen;
        //генерирует высоту
        Random rnd = new Random();
        //шрифт очков в правом верхнем углу
        Font f = SystemFonts.DefaultFont;
        //описание игрока
        Rectangle player;
        //скорость игрока
        int playerVelocity = 0;
        //кол во набранных очков
        int score = 0;
        bool isGlowingOn = true;
       
        // Трубы
     
        Rectangle tube1;
        Rectangle tube2;
        Rectangle tube3;
        Rectangle tube4;
        Rectangle tube5;
        Rectangle tube6;
        //расстояние между труб
        int space = 150;
        //скорость труб
        int tubesVelosity = -3;
        SoundPlayer soundplayer;
      


        public Form1()
        {
            InitializeComponent();
            //создаем кадр
            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            //Задаем стиль линий и цвет
            pen = new Pen(Brushes.Aqua);
            //размещаем игрока и задаем ему размеры
            player = new Rectangle(30, 30, 30, 30);
            //размещаем трубы
            //так чтобы верзние трубы располагалсь относительно нижних
            //на spsce пикселей выше
            tube1 = new Rectangle(500,300,80,500);
            tube2 = new Rectangle(tube1.X, tube1.Y-tube1.Height-space, 80, 500);
            tube3 = new Rectangle(700, 300, 80, 500);
            tube4 = new Rectangle(tube3.X, tube3.Y - tube3.Height - space, 80, 500);
            tube5= new Rectangle(900, 300, 80, 500);
            tube6 = new Rectangle(tube5.X, tube5.Y - tube5.Height - space, 80, 500);
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            System.IO.Stream resourseStream = assembly.GetManifestResourceStream(@"FlappyCube_Undermove.music_Fedor(1).wav");
            soundplayer = new SoundPlayer(resourseStream);
            soundplayer.PlayLooping();
        }
        //Главный цикл игры. В нем происходит отрисовка и игровая логика.
        private void timer1_Tick(object sender, EventArgs e)
        {
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.Black);
            if (pen.Width>minWidth)
            {
                pen.Width--;
            }

            
            Draw(g);

            pictureBox1.Image = bmp;
            g.Dispose();
        }
        //Метод отрисовки
        //Если мы хотим чтобы что то отобразилось на экране 
        //то мы должны добавить в него соответствующую строку
        private void Draw(Graphics g)
        {
            g.FillRectangle(Brushes.White, player);
            g.FillRectangle(Brushes.Red, tube1);
            g.FillRectangle(Brushes.Red, tube2);
            g.FillRectangle(Brushes.Blue, tube3);
            g.FillRectangle(Brushes.Blue, tube4);
            g.FillRectangle(Brushes.White, tube5);
            g.FillRectangle(Brushes.White, tube6);
            g.DrawRectangle(pen, player);
            g.DrawRectangle(pen, tube1);
            g.DrawRectangle(pen, tube2);
            g.DrawRectangle(pen, tube3);
            g.DrawRectangle(pen, tube4);
            g.DrawRectangle(pen, tube5);
            g.DrawRectangle(pen, tube6);
            g.DrawString(score.ToString()  ,f,Brushes.White,400,20);
           
        }

        private void TubesLogic()
        {
            //двигаем первую пару труб
            tube1.X += tubesVelosity;
            tube2.X = tube1.X;
            //двигаем вторую пару труб
            tube3.X += tubesVelosity;
            tube4.X = tube3.X;

            tube5.X += tubesVelosity;
            tube6.X = tube5.X;
            //возврвщваем назад епрвую пару труб
            if (tube1.Right <= 0)
            {
                tube1.X = pictureBox1.Right;
                tube1.Y = rnd.Next(200, 450);
                tube2.Y = tube1.Y - tube1.Height - space;
            }
            //возвращаем назад вторю пару труб
            if (tube3.Right <= 0)
            {
                tube3.X = pictureBox1.Right;
                tube3.Y = rnd.Next(200, 450);
                tube4.Y = tube3.Y - tube4.Height - space;
            }
            if (tube5.Right <= 0)
            {
                tube5.X = pictureBox1.Right;
                tube5.Y = rnd.Next(200, 450);
                tube6.Y = tube5.Y - tube6.Height - space;
            }

        }

        private void PlayerLogic()
        {
            //Добавляем очки игроку.
            score++;
            //Алгоритм движения игрока.
            //Скорость увеличивается в зависимостиот величины гравитации.
            //Игрок за 1 тик таймера перемещается на расстояние равное скорости.
            playerVelocity += gravity;
            player.Y += playerVelocity;
            //Если игрок столкнулся с нижней частью. то переместить его наверх и сбросить скорость иначе,
            // если игрок столкнулся с верхней частью,то погасить скорость и не датьему выйти за пределы.
            if (player.Bottom > pictureBox1.Bottom)
            {
                player.Y = 0;
                playerVelocity = 0;
                score = 0;
            }
            else if (player.Y < 0)
            {
                player.Y = 0;
                playerVelocity = 0;
            }
            //логика столкновения первой пары
            /////////////////////////////////////////////////////////////////////////////////////////
            if (player.Right >= tube1.Left && player.Bottom > tube1.Top && player.Left <= tube1.Right)
            {
                player.Y = 0;
                playerVelocity = 0;
                score = 0; 
            }
            if (player.Right >= tube2.Left && player.Top < tube2.Bottom && player.Left <= tube2.Right)
            {
                player.Y = 0;
                playerVelocity = 0;
                score = 0;
            }
            //////////////////////////////////////////////////////////////////////////////////////////
            //логика столкновения второй пары труб
            //////////////////////////////////////////////////////////////////////////////////////////
            if (player.Right >= tube3.Left && player.Bottom > tube3.Top && player.Left <= tube3.Right)
            {
                player.Y = 0;
                playerVelocity = 0;
                score = 0;
            }
            if (player.Right >= tube4.Left && player.Top < tube4.Bottom && player.Left <= tube4.Right)
            {
                player.Y = 0;
                playerVelocity = 0;
                score = 0;
            }

            /////////////////////////////////////////////////////////////////////////

            if (player.Right >= tube5.Left && player.Bottom > tube5.Top && player.Left <= tube5.Right)
            {
                player.Y = 0;
                playerVelocity = 0;
                score = 0;
            }
            if (player.Right >= tube6.Left && player.Top < tube6.Bottom && player.Left <= tube6.Right)
            {
                player.Y = 0;
                playerVelocity = 0;
                score = 0;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {//при нажатии на пробел запускаем игру и придаем игроку ускорение в верх
             if(e.KeyCode == Keys.Space)
            {
                if (isGlowingOn)
                {
                    pen.Width = 10;
                }
                DrawTimer.Start();
                playerVelocity -= 20;
                DrawTimer.Start();
                PlayerTimer.Start();
                TubesTimer.Start();    
            }
           else if (e.KeyCode == Keys.Escape)
            {
                TubesTimer.Enabled = !TubesTimer.Enabled;
                PlayerTimer.Enabled = !DrawTimer.Enabled;
                DrawTimer.Enabled = !DrawTimer.Enabled;
                
                TubesTimer.Stop();
                DrawTimer.Enabled = !DrawTimer.Enabled;
                button5.Visible = true;
                button5.Enabled = true;            }
            else if(e.KeyCode == Keys.L)
            {   DrawTimer.Stop();
              new LiderboardForm2(score).Show();
          
            }
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {//Отрисовываем первый кадр что бы экран не был пустым на старте

          
        }

        private void button1_Click(object sender, EventArgs e)
        {
Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.Black);

            Draw(g);

            pictureBox1.Image = bmp;
            g.Dispose();

            DrawTimer.Start();
            PlayerTimer.Start();
            TubesTimer.Start();
            button1.Visible = false;
            button1.Enabled = false;
            button2.Visible = false;
            button2.Enabled = false;
            button3.Visible = false;
            button3.Enabled = false;
            button4.Visible = false;
            button4.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new SettingsForm().Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DrawTimer.Stop();
            new LiderboardForm2(score).Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void TubesTimer_Tick(object sender, EventArgs e)
        {
            TubesLogic();
        }

        private void PlayerTimer_Tick(object sender, EventArgs e)
        {
            PlayerLogic();
        }

        private void SettingsUpdateTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                string[] settings;
                settings = File.ReadAllLines("Settings");
                if (settings.Length >= 3)
                {
                    PlayerTimer.Interval = Convert.ToInt32(settings[0]);
                    TubesTimer.Interval = Convert.ToInt32(settings[1]);
                    isGlowingOn = Convert.ToBoolean(settings[2]);
                }
            }
            catch (FileNotFoundException)
            {
                File.Create("settings");
                
            }
            catch(Exception)
            {

            }
           
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            button5.Visible = false;
            button5.Enabled = false;

            TubesTimer.Enabled = TubesTimer.Enabled;
            PlayerTimer.Enabled = DrawTimer.Enabled;
            DrawTimer.Enabled = DrawTimer.Enabled;

            TubesTimer.Start();
            DrawTimer.Enabled = DrawTimer.Enabled;

            
            
        }
    }
}
