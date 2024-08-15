using Guessing_Game.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Guessing_Game
{
    public partial class Fact_Frenzy : Form
    {

        
        System.Media.SoundPlayer player = new System.Media.SoundPlayer();
        private List<Question> questions;
        private int currentQuestionIndex;
        private Button[] answerButtons;


        int correctAnswerCount = 0;
        int wrongAnswerCount = 0;
        int numquestions = 15;
        int lifecount = 5;
        int setselect = 0;

        private Timer questionTimer;
        private const int questionTimeLimit = 10; 
        private int timeLeft;
        public int s;

        public Fact_Frenzy()
        {
            InitializeComponent();

            player.SoundLocation = "bgm1.wav";
            this.Load += new EventHandler(Form1_Load);

            pictureBox6.Visible = false;
            pictureBox7.Visible = false;
           progressBar1.Visible = false;
            button7.Visible = false;
            pictureBox4.Visible = false;
            pictureBox5.Visible = false;
            pictureBox3.Visible = false;

            label6.Visible = false;

            pictureBox2.Parent = pictureBox1;
            pictureBox2.Location = new Point(180, 140);
            label4.Visible = false;
            label5.Visible = false;
            label3.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
            button4.Visible = false;
            button5.Visible = false;

            answerButtons = new[] { button2, button3, button4, button5 };

            questionTimer = new Timer();
            questionTimer.Interval = 1000;
            questionTimer.Tick += QuestionTimer_Tick;
            progressBar1.Value = 0 ;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            player.Play();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            player.SoundLocation = "bgm2.wav";
            player.Play();
            timer1.Start();


            pictureBox6.Visible= true;
            pictureBox7.Visible= true;

            progressBar1.Visible = true;
            pictureBox3.Visible = true;
            pictureBox5.Visible = true;
            pictureBox4.Visible = true;

            this.BackgroundImage = null;
            pictureBox1.Image = Resources.quizbg2;
            pictureBox2.Image = null;

            label4.Visible = true;
            label5.Visible = true;
            label2.Visible = false;
            button6.Visible = false;
            button1.Visible = false;
            label1.Visible = false;

            label3.Visible = true;
            button2.Visible = true;
            button3.Visible = true;
            button4.Visible = true;
            button5.Visible = true;

            this.BackgroundImage = Resources.quizbg2;
            this.BackgroundImageLayout = ImageLayout.Stretch;

            LoadQuestions();
            currentQuestionIndex = 0;
            DisplayNextQuestion();
        }

        private void LoadQuestions()
        {
            questions = new List<Question>
            {
                new Question("What does HTTP stand for?", new[] { "Hyper Text Transfer Protocol", "High Text Transfer Protocol", "Hyperlink Text Transfer Protocol", "Hyperlinking Text Transfer Protocol" }, 0),
                new Question("Which of the following is a type of malware?", new[] { "Firewall", "Antivirus", "Trojan", "Cookie" }, 2),
                new Question("What is the primary function of a firewall?", new[] { "To encrypt data", "To block unauthorized access", "To speed up internet connection", "To monitor employee activities" }, 1),
                new Question("In networking, what does LAN stand for?", new[] { "Local Area Network", "Large Area Network", "Long Area Network", "Logical Area Network" }, 0),
                new Question("Which programming language is primarily used for web development?", new[] { "Python", "C++", "JavaScript", "COBOL" }, 2),
                new Question("What is the purpose of an IP address?", new[] { "To store user data", "To identify a device on a network", "To encrypt communications", "To speed up data transfer" }, 1),
                new Question("What is cloud computing?", new[] { "Delivering computing services over the internet", "Using software to create network clouds", "Installing software on a local computer", "Building physical data centers" }, 0),
                new Question("Which protocol is used to send email?", new[] { "HTTP", "FTP", "SMTP", "TCP" }, 2),
                new Question("What does SQL stand for?", new[] { "Simple Query Language", "Structured Query Language", "Sequential Query Language", "Standard Query Language" }, 1),
                new Question("Which of the following is not an operating system?", new[] { "Windows", "Linux", "Java", "macOS" }, 2),
                new Question("Which of the following storage devices typically has the largest capacity?", new[] { "CD-ROM", "Hard Disk Drive (HDD)", "USB Flash Drive", "Blu-ray Disc" }, 1),
                new Question("What is the purpose of a VPN?", new[] { "To increase bandwidth", "To create a secure network connection over the internet", "To improve computer performance", "To monitor network traffic" }, 1),
                new Question("What does RAM stand for?", new[] { "Random Access Memory", "Read Access Memory", "Rapid Access Memory", "Real-time Access Memory" }, 0),
                new Question("Which type of database management system is most commonly used?", new[] { "Hierarchical", "Network", "Relational", "Object-oriented" }, 2),
                new Question("What is an example of an open-source operating system?", new[] { "Windows", "macOS", "Linux", "iOS" }, 2)
            };
        }

        private void DisplayNextQuestion()
        {
            if (currentQuestionIndex < questions.Count)
            {
                var question = questions[currentQuestionIndex];
                label3.Text = question.Text;
                for (int i = 0; i < answerButtons.Length; i++)
                {
                    answerButtons[i].Text = question.Options[i];
                    answerButtons[i].BackColor = DefaultBackColor;
                    answerButtons[i].Enabled = true;
                    setselect = 0;

                }
                timeLeft = questionTimeLimit;
                questionTimer.Start();
                progressBar1.Value = 0;
                while(progressBar1.Value == 0)
                {
                    progressBar1.Value += 1;
                    s++;

                    if(progressBar1.Value == 100)
                    {
                        CheckAnswer(1);
                    }

                }
            }
            else
            {
                label3.Text = "Congratulations! You Passed the quiz!";
                button2.Visible = false;
                button3.Visible = false;
                button4.Visible = false;
                button5.Visible = false;
                progressBar1.Visible = false;
                questionTimer.Stop();
            }
        }

        private void CheckAnswer(int selectedAnswerIndex)
        {
            var question = questions[currentQuestionIndex];
            int correctAnswerIndex = question.CorrectAnswerIndex;

            for (int i = 0; i < answerButtons.Length; i++)
            {
                if (i == question.CorrectAnswerIndex)
                {
                    answerButtons[i].BackColor = Color.Green;
                    correctAnswerCount++;
                    numquestions--;

                    label4.Text = "Remaining Questions: " + numquestions.ToString() + "/15 ";

                    setselect = 0;
                }
                else
                {

                    setselect = 0;

                    answerButtons[i].BackColor = Color.Red;
                    if (selectedAnswerIndex == i)
                    {
                        pictureBox6.Visible = false;

                        wrongAnswerCount++;
                        if (wrongAnswerCount == 2)
                        {
                            pictureBox5.Visible = false;
                        }
                        if (wrongAnswerCount == 3)
                        {
                            pictureBox4.Visible = false;
                        }
                        if (wrongAnswerCount == 4)
                        {
                            pictureBox3.Visible = false;
                        }
                        if (wrongAnswerCount == 5)
                        {
                            pictureBox7.Visible = false;
                        }
                       
                       
                        lifecount--;
                        label5.Text = "Life: " + lifecount.ToString();

                        if (wrongAnswerCount >= 5)
                        {

                            player.SoundLocation = "gameover.wav";
                            player.Play();

                            label6.Visible = true;
                            label3.Visible = false;
                            label4.Visible = false;
                            label5.Visible          = false;

                            progressBar1.Visible = false;
                            button7.Visible = true;
                        }
                    }
                }
                answerButtons[i].Enabled = false;
            }

            if (wrongAnswerCount >= 5)
            {
                button2.Visible = false;
                button3.Visible = false;
                button4.Visible = false;
                button5.Visible = false;
                return;
            }

            questionTimer.Stop();

            Timer nextQuestionTimer = new Timer();
            nextQuestionTimer.Interval = 2000;
            nextQuestionTimer.Tick += (s, e) =>
            {
                nextQuestionTimer.Stop();
                currentQuestionIndex++;
                DisplayNextQuestion();
            };
            nextQuestionTimer.Start();
        }

        private void QuestionTimer_Tick(object sender, EventArgs e)
        {
            if (timeLeft > 0)
            {
                timeLeft--;
            }
            else
            {
                questionTimer.Stop();
                CheckAnswer(1); 
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CheckAnswer(0);
            progressBar1.Value = 0;
            setselect = 0;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            CheckAnswer(1);
            setselect = 1;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            CheckAnswer(2);
            setselect = 1;

        }

        private void button5_Click(object sender, EventArgs e)
        {
            CheckAnswer(3);
            setselect = 1;

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label4_Click_1(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
                
            if(setselect == 0)
            {
                progressBar1.Value += 1;
                s++;
                if (progressBar1.Value == 100)
                {

                    button5.Select();


                }

                if(progressBar1.Value == 100)
                {
                    progressBar1.Value = 0;
                }

            }
            else
            {
                progressBar1.Value = 0;
                setselect = 0;
            }
        


           }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }
        private void button7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }

    public class Question
    {
        public string Text { get; set; }
        public string[] Options { get; set; }
        public int CorrectAnswerIndex { get; set; }

        public Question(string text, string[] options, int correctAnswerIndex)
        {
            Text = text;
            Options = options;
            CorrectAnswerIndex = correctAnswerIndex;
        }
    }
}
