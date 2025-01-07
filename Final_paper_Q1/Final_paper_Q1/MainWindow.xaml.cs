using Final_paper_Q1;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Microsoft.VisualBasic;


namespace Final_paper_Q1
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<Question> Questions { get; set; } = new ObservableCollection<Question>();

        public MainWindow()
        {
            InitializeComponent();
            QuestionsDataGrid.ItemsSource = Questions;

            TopicFilterComboBox.ItemsSource = new[] { "All", "Math", "Science", "History" };
            DifficultyFilterComboBox.ItemsSource = new[] { "All", "Easy", "Medium", "Hard" };
            TopicFilterComboBox.SelectedIndex = 0;
            DifficultyFilterComboBox.SelectedIndex = 0;

            LoadQuestions();
        }

        private void LoadQuestions()
        {
            LoadingProgressBar.Visibility = Visibility.Visible;

            try
            {
                // Simulate data loading
                Questions.Add(new Question("What is 2+2?", "4;3;2;1", "1", "4", 1, "30 sec") { Topic = "Math", Difficulty = "Easy" });
                Questions.Add(new Question("what is 4*5", "5;8;30;20", "4", "20", 2, "1 min") { Topic = "Maths", Difficulty = "Medium" });
                Questions.Add(new Question("What is the capital of France?", "Paris;London;Berlin;Rome", "1", "Paris", 1, "30 sec") { Topic = "Science", Difficulty = "Medium" });
                Questions.Add(new Question("Who painted the Mona Lisa?", "Van Gogh;Da Vinci;Picasso;Monet", "2", "Da Vinci", 3, "1 min") { Topic = "History", Difficulty = "Hard" });
                Questions.Add(new Question("Who discovered gravity?", "Newton;Einstein;Galileo;Tesla", "1", "Newton", 2, "1 min") { Topic = "Science", Difficulty = "Medium" });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading questions: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                LoadingProgressBar.Visibility = Visibility.Collapsed;
            }
        }

        private void FilterChanged(object sender, EventArgs e)
        {
            var topicFilter = TopicFilterComboBox.SelectedItem.ToString();
            var difficultyFilter = DifficultyFilterComboBox.SelectedItem.ToString();

            var filtered = Questions.Where(q =>
                (topicFilter == "All" || q.Topic == topicFilter) &&
                (difficultyFilter == "All" || q.Difficulty == difficultyFilter)).ToList();

            QuestionsDataGrid.ItemsSource = new ObservableCollection<Question>(filtered);
        }



        private void AddQuestion_Click(object sender, RoutedEventArgs e)
        {
            var questionText = Prompt("Enter question text:");
            if (string.IsNullOrWhiteSpace(questionText)) return;

            var options = Prompt("Enter options (separated by semicolons):");
            if (string.IsNullOrWhiteSpace(options)) return;

            var correctOption = Prompt("Enter correct option number:");
            if (string.IsNullOrWhiteSpace(correctOption)) return;

            var correctAnswer = Prompt("Enter correct answer:");
            if (string.IsNullOrWhiteSpace(correctAnswer)) return;

            var marks = Prompt("Enter marks:");
            if (!int.TryParse(marks, out var parsedMarks)) return;

            var timeLimit = Prompt("Enter time limit:");
            if (string.IsNullOrWhiteSpace(timeLimit)) return;

            var topic = Prompt("Enter topic:");
            var difficulty = Prompt("Enter difficulty:");

            Questions.Add(new Question(questionText, options, correctOption, correctAnswer, parsedMarks, timeLimit)
            {
                Topic = topic,
                Difficulty = difficulty
            });
        }

        private void EditQuestion_Click(object sender, RoutedEventArgs e)
        {
            if (QuestionsDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Please select a question to edit.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var selectedQuestion = (Question)QuestionsDataGrid.SelectedItem;

            var questionText = Prompt($"Edit question text:", selectedQuestion.QuestionText);
            var options = Prompt("Edit options (separated by semicolons):", selectedQuestion.Options);
            var correctOption = Prompt("Edit correct option number:", selectedQuestion.CorrectOption);
            var correctAnswer = Prompt("Edit correct answer:", selectedQuestion.CorrectAnswer);
            var marks = Prompt("Edit marks:", selectedQuestion.Marks.ToString());
            var timeLimit = Prompt("Edit time limit:", selectedQuestion.TimeLimit);
            var topic = Prompt("Edit topic:", selectedQuestion.Topic);
            var difficulty = Prompt("Edit difficulty:", selectedQuestion.Difficulty);

            selectedQuestion.QuestionText = questionText;
            selectedQuestion.Options = options;
            selectedQuestion.CorrectOption = correctOption;
            selectedQuestion.CorrectAnswer = correctAnswer;
            selectedQuestion.Marks = int.TryParse(marks, out var parsedMarks) ? parsedMarks : selectedQuestion.Marks;
            selectedQuestion.TimeLimit = timeLimit;
            selectedQuestion.Topic = topic;
            selectedQuestion.Difficulty = difficulty;

            QuestionsDataGrid.Items.Refresh();
        }

        private void DeleteQuestion_Click(object sender, RoutedEventArgs e)
        {
            if (QuestionsDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Please select a question to delete.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var selectedQuestion = (Question)QuestionsDataGrid.SelectedItem;
            Questions.Remove(selectedQuestion);
        }
        private string Prompt(string message, string defaultValue = "")
        {
            // Use a simple dialog window for user input
            string input = Microsoft.VisualBasic.Interaction.InputBox(message, "Input Required", defaultValue);
            return input;
        }



        private void FilterChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }
    }

    public class Question
    {
        public string QuestionText { get; set; }
        public string Options { get; set; }
        public string CorrectOption { get; set; }
        public string CorrectAnswer { get; set; }
        public int Marks { get; set; }
        public string TimeLimit { get; set; }
        public string Topic { get; set; } 
        public string Difficulty { get; set; }  

        public Question(string questionText, string options, string correctOption, string correctAnswer, int marks, string timeLimit)
        {
            QuestionText = questionText;
            Options = options;
            CorrectOption = correctOption;
            CorrectAnswer = correctAnswer;
            Marks = marks;
            TimeLimit = timeLimit;
        }
    }
}


 

