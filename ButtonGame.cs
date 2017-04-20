using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ButtonGame
{
    public partial class Form1 : Form
    {
        public const string BELL_FILE = "Bell.wav";
        public const string BUTTON_IMAGE_FILE = "buttonImage.jpg";
        List<Button> buttonList; // This will hold a list of all your "?" button controls
        int hidingPlace; // Button number where your sewing buttons are hidden
        int currentScore; // Current score for (the current round of play
        int totalScore; // This will hold the total score
        int gameCount; // Number of games that have been played
        System.Media.SoundPlayer bell; // Media player to play the sound of a bell
        Bitmap picture; // Picture of your sewing buttons.
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            buttonList = new List<Button>();
// Go through the form’s controls and find all the "?" button controls
// Placing them in your list.
foreach (Control item in this.Controls)
{
if ((string)item.Text == "?")
{
buttonList.Add((Button)item);
}
}
// Get file path that this executable loaded from
string appPath = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
// Create file path to sound and image files
string bellFilePath = System.IO.Path.Combine(appPath, BELL_FILE);
string pictureFilePath = System.IO.Path.Combine(appPath, BUTTON_IMAGE_FILE);
// Load bell sound file and sewing buttons picture file
bell = new System.Media.SoundPlayer(bellFilePath);
picture = new Bitmap(pictureFilePath);
// initialize counts
currentScore = 0;
totalScore = 0;
gameCount = 0;
hidingPlace = 0;
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            // Clear out button image from last game
            if (buttonList[hidingPlace] != null)
            {
                buttonList[hidingPlace].Image = null;
                buttonList[hidingPlace].Refresh();
            }
            // Flash the "?" buttons randomly with Hot Pink
            // At end of loop, hidingPlace will always have
            // index number of last button flashed.
            for (int i = 1; i <= 24; i++) // flash 24 times
            {
                Random dice = new Random(); // Random number generator
                Color previous;
                hidingPlace = dice.Next(1, 600) % 6; // Number between 0 and 5 inclusive
                previous = buttonList[hidingPlace].BackColor; // Save original color
                buttonList[hidingPlace].BackColor = Color.HotPink; // paint it Hot Pink
                buttonList[hidingPlace].Refresh();
                System.Threading.Thread.Sleep(20); // 20ms is fastest a human can respond
                buttonList[hidingPlace].BackColor = previous; // restore previous color
                buttonList[hidingPlace].Refresh();
            }
            gameCount = gameCount + 1; // increment gameCount
            currentScore = 6; // reset guess score 

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int p;
            Button b;
            // Get the button that was clicked and see if it is the hiding place
            b = (Button)sender;
            p = buttonList.FindIndex(b.Equals);
            if (p == hidingPlace)
            {
                totalScore = totalScore + currentScore;
                b.Image = picture;
                bell.Play();
            }
            else
            {
                if (currentScore > 0) currentScore = currentScore - 1;
            }
            // Format and display score information
            lblCurrent.Text = String.Format("Points Remaining: {0:N0}", currentScore);
            lblTotal.Text = String.Format("Total Score: {0:N0} out of { 1:N0} games", totalScore, gameCount);
            lblCurrent.Refresh();
            lblTotal.Refresh();
        }
    }
}
