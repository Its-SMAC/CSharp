using MatchingGame.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MatchingGame
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        PictureBox firstClicked = null;
        PictureBox secondClicked = null;
        Random random = new Random();

        List<Image> images = new List<Image>()
        {
            Resources.Nim, Resources.Nim,
            Resources.Rust, Resources.Rust,
            Resources.Assembly, Resources.Assembly,
            Resources.Cobol, Resources.Cobol,
            Resources.CSharp, Resources.CSharp,
            Resources.Golang, Resources.Golang,
            Resources.Java, Resources.Java,
            Resources.Kotlin, Resources.Kotlin,
            Resources.Perl, Resources.Perl,
            Resources.Php, Resources.Php,
            Resources.Python, Resources.Python,
            Resources.Ruby, Resources.Ruby,
            Resources.Sql, Resources.Sql,
            Resources.Swift, Resources.Swift,
            Resources.Typescript, Resources.Typescript
        };

        private void MatchingGame_Load(object sender, EventArgs e)
        {
            AssignIconsToSquares();
        }

        private void playSimpleSound(string soundFile)
        {
            SoundPlayer simpleSound = new SoundPlayer(soundFile);
            simpleSound.Play();
        }

        private void AssignIconsToSquares()
        {
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                PictureBox imageBox = control as PictureBox;
                if (imageBox != null)
                {
                    int randomNumber = random.Next(images.Count);
                    imageBox.Image = images[randomNumber];
                    imageBox.ForeColor = imageBox.BackColor;
                    images.RemoveAt(randomNumber);
                }
            }
        }

        private void picture_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled == true)
                return;

            PictureBox clickedLabel = sender as PictureBox;

            if (clickedLabel != null)
            {
                string clickSoundFile = System.IO.Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "src", "sounds", "mouse-click-sound.wav");
                playSimpleSound(clickSoundFile);

                if (clickedLabel.ForeColor == Color.Black)
                    return;

                if (firstClicked == null)
                {
                    firstClicked = clickedLabel;
                    firstClicked.ForeColor = Color.Black;
                    return;
                }

                secondClicked = clickedLabel;
                secondClicked.ForeColor = Color.Black;

                CheckForWinner();

                if (firstClicked.Text == secondClicked.Text)
                {
                    firstClicked = null;
                    secondClicked = null;
                    string soundFile = System.IO.Path.Combine(
                        AppDomain.CurrentDomain.BaseDirectory,
                        "src", "sounds", "ding-sound-effect_2.wav");
                    playSimpleSound(soundFile);
                    return;
                }

                timer1.Start();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();

            firstClicked.ForeColor = firstClicked.BackColor;
            secondClicked.ForeColor = secondClicked.BackColor;
            firstClicked = null;
            secondClicked = null;
        }

        private void CheckForWinner()
        {
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                PictureBox iconLabel = control as PictureBox;

                if (iconLabel != null)
                {
                    if (iconLabel.ForeColor == iconLabel.BackColor)
                        return;
                }
            }

            string winSoundFile = System.IO.Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "src", "sounds", "mrstokes302-you-win-sfx-mrstokes302-442128.wav");
            playSimpleSound(winSoundFile);
            MessageBox.Show("You matched all the icons!", "Congratulations");
            Close();
        }

    }
}