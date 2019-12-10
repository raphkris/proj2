using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace Project2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = true;

            // Update label with user's filepath
            label1.Text = "Files will be written to C:\\Users\\" + Environment.UserName + "\\Desktop";

            // Load data from CSV file into an array
            President[] presidents = LoadPresidentArray();

            // Gives number of rows in presidents array
            int rowCount = presidents.Length;

            // Populate listBox1 with president names from presidents array
            for (int counter = 1; counter < rowCount; ++counter)
            {
                string presNames = presidents[counter].First + " " + presidents[counter].Last;
                listBox1.Items.Add(presNames);
            }
        }

        private President[] LoadPresidentArray()
        {
            // Path to CSV file
            string filePath = "..\\..\\..\\Presidents.csv";

            FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            StreamReader streamReader = new StreamReader(fileStream);

            // Gets number of rows in CSV file
            int rowCount = getRowCount(filePath);

            // Creates President object array
            President[] presidents = new President[rowCount];
            for (int rowCounter = 0; rowCounter < rowCount; rowCounter++)
            {
                presidents[rowCounter] = new President();
            }

            // Gets data values from CSV
            for (int rowCounter = 0; rowCounter < rowCount; ++rowCounter)
            {
                string readRow = streamReader.ReadLine();
                string[] values = readRow.Split(',');

                // Get only necessary columns
                presidents[rowCounter].Number = values[0];
                presidents[rowCounter].First = values[1];
                presidents[rowCounter].Last = values[2];
                presidents[rowCounter].Education = values[19];
                presidents[rowCounter].Occupation = values[20];
                presidents[rowCounter].CauseOfDeath = values[34];
                presidents[rowCounter].Party = values[3];
                presidents[rowCounter].Religion = values[18];
            }

            // Return the array containing data from CSV
            return presidents;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Load data from CSV file into an array
            President[] presidents = LoadPresidentArray();

            // Get index value of selected president. "+1" accounts for header from CSV
            int selectedPresident = listBox1.SelectedIndex + 1;

            // Format selected president's name into string using index value
            string selectedPresidentString = presidents[selectedPresident].First + presidents[selectedPresident].Last;

            // Format selected categories into string using index value
            string education = "Education: " + presidents[selectedPresident].Education;
            string occupation = "Occupation: " + presidents[selectedPresident].Occupation;
            string causeOfDeath = "Cause of Death: " + presidents[selectedPresident].CauseOfDeath;
            string party = "Party: " + presidents[selectedPresident].Party;
            string religion = "Religion: " + presidents[selectedPresident].Religion;

            var category = String.Join(",", checkedListBox1.CheckedItems.Cast<string>());
            string categoryValue = string.Empty;

            if (category.Contains("Education"))
            {
                categoryValue = education + "\n";
            }
            if (category.Contains("Occupation"))
            {
                categoryValue += occupation + "\n";
            }
            if (category.Contains("Death"))
            {
                categoryValue += causeOfDeath + "\n";
            }
            if (category.Contains("Party"))
            {
                categoryValue += party + "\n";
            }
            if (category.Contains("Religion"))
            {
                categoryValue += religion + "\n";
            }

            // Defensive programming
            string noPresSelection = "No president selected";
            string noCatSelection = "No category selected";
            string noSelection = "Nothing selected";

            // Output to user
            if (selectedPresident == 0 && categoryValue != string.Empty)
            {
                MessageBox.Show(noPresSelection);
            }
            if (selectedPresident != 0 && categoryValue == string.Empty)
            {
                MessageBox.Show(noCatSelection);
            }
            if (selectedPresident == 0 && categoryValue == string.Empty)
            {
                MessageBox.Show(noSelection);
            }
            if (selectedPresident != 0 && categoryValue != string.Empty)
            {
                MessageBox.Show(selectedPresidentString + "\n\n" + categoryValue);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Load data from CSV file into an array
            President[] presidents = LoadPresidentArray();

            // Get index value of selected president/accounts for header from CSV
            int selectedPresident = listBox1.SelectedIndex + 1;

            // Set headers for each category
            string numberHeader = "number";
            string firstHeader = "first";
            string lastHeader = "last";
            string educationHeader = "education";
            string occupationHeader = "occupation";
            string causeOfDeathHeader = "deathCause";
            string partyHeader = "party";
            string religionHeader = "religion";

            // Format selected categories into string using index value
            string number = presidents[selectedPresident].Number;
            string first = presidents[selectedPresident].First;
            string last = presidents[selectedPresident].Last;
            string education = presidents[selectedPresident].Education;
            string occupation = presidents[selectedPresident].Occupation;
            string causeOfDeath = presidents[selectedPresident].CauseOfDeath;
            string party = presidents[selectedPresident].Party;
            string religion = presidents[selectedPresident].Religion;

            var category = String.Join(",", checkedListBox1.CheckedItems.Cast<string>());
            string categoryValue = string.Empty;

            string delim = ",";
            string resultHeader = numberHeader + delim + firstHeader + delim + lastHeader + delim;
            string resultString = number + delim + first + delim + last + delim;

            if (category.Contains("Education"))
            {
                categoryValue = education + "\n";
                resultHeader += educationHeader + delim;
                resultString += education + delim;
            }
            if (category.Contains("Occupation"))
            {
                categoryValue += occupation + "\n";
                resultHeader += occupationHeader + delim;
                resultString += occupation + delim;
            }
            if (category.Contains("Death"))
            {
                categoryValue += causeOfDeath + "\n";
                resultHeader += causeOfDeathHeader + delim;
                resultString += causeOfDeath + delim;
            }
            if (category.Contains("Party"))
            {
                categoryValue += party + "\n";
                resultHeader += partyHeader + delim;
                resultString += party + delim;
            }
            if (category.Contains("Religion"))
            {
                categoryValue += religion + "\n";
                resultHeader += religionHeader + delim;
                resultString += religion + delim;
            }

            // Defensive programming
            string noPresSelection = "No president selected";
            string noCatSelection = "No category selected";
            string noSelection = "Nothing selected";

            // Output to user
            if (selectedPresident == 0 && categoryValue != string.Empty)
            {
                MessageBox.Show(noPresSelection);
            }
            if (selectedPresident != 0 && categoryValue == string.Empty)
            {
                MessageBox.Show(noCatSelection);
            }
            if (selectedPresident == 0 && categoryValue == string.Empty)
            {
                MessageBox.Show(noSelection);
            }
            if (selectedPresident != 0 && categoryValue != string.Empty)
            {
                MessageBox.Show(resultHeader + "\n\n" + resultString);

                string outputPath = "C:\\Users\\" + Environment.UserName + "\\Desktop\\output.csv";
                FileStream outputFile = new FileStream(outputPath, FileMode.Append, FileAccess.Write);
                StreamWriter output = new StreamWriter(outputFile);

                output.WriteLine(resultHeader + "\r\n" + resultString + "\r\n");

                output.Close();
                outputFile.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Load data from CSV file into an array
            President[] presidents = LoadPresidentArray();

            // Get the selected topic
            var checkedRadioButton = panelRadioButtons.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);
            var topic = checkedRadioButton.Text;

            // Get the checked values
            string subtopics = string.Empty;
            subtopics = String.Join(Environment.NewLine, checkedListBox2.CheckedItems.Cast<string>());

            // Defensive programming
            string noTopicSelection = "No topic selected";
            string noSubtopicSelection = "No subtopic selected";

            // Output to user
            if (topic == null)
            {
                MessageBox.Show(noTopicSelection);
            }
            if (subtopics == string.Empty)
            {
                MessageBox.Show(noSubtopicSelection);
            }
            if (topic != null && subtopics != string.Empty)
            {
                MessageBox.Show(topic + "\n\n" + subtopics);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Load data from CSV file into an array
            President[] presidents = LoadPresidentArray();

            // Get the selected topic
            var checkedRadioButton = panelRadioButtons.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);
            var topic = checkedRadioButton.Text;

            // Get the checked values
            string subtopics = string.Empty;
            subtopics = String.Join(Environment.NewLine, checkedListBox2.CheckedItems.Cast<string>());

            string delim = ",";
            string resultHeader = string.Empty;
            string resultString = string.Empty;

            if (topic.Contains("Education"))
            {
                resultHeader += "number" + delim + "first" + delim + "last" + delim + "education";
                if (subtopics.Contains("none"))
                {
                    for (int rowCounter = 0; rowCounter < presidents.Length; rowCounter++)
                    {
                        if (presidents[rowCounter].Education == "no formal" || presidents[rowCounter].Education == "No formal education")
                        {
                            resultString += presidents[rowCounter].Number + delim + presidents[rowCounter].First + delim + presidents[rowCounter].Last + delim + presidents[rowCounter].Education + "\r\n";
                        }
                    }
                }
                if (subtopics.Contains("Harvard"))
                {
                    for (int rowCounter = 0; rowCounter < presidents.Length; rowCounter++)
                    {
                        if (presidents[rowCounter].Education == "Harvard" || presidents[rowCounter].Education == "Harvard College" || presidents[rowCounter].Education == "Harvard Law")
                        {
                            resultString += presidents[rowCounter].Number + delim + presidents[rowCounter].First + delim + presidents[rowCounter].Last + delim + presidents[rowCounter].Education + "\r\n";
                        }
                    }
                }
                if (subtopics.Contains("Yale"))
                {
                    for (int rowCounter = 0; rowCounter < presidents.Length; rowCounter++)
                    {
                        if (presidents[rowCounter].Education == "Yale")
                        {
                            resultString += presidents[rowCounter].Number + delim + presidents[rowCounter].First + delim + presidents[rowCounter].Last + delim + presidents[rowCounter].Education + "\r\n";
                        }
                    }
                }
                if (subtopics.Contains("Stanford University"))
                {
                    for (int rowCounter = 0; rowCounter < presidents.Length; rowCounter++)
                    {
                        if (presidents[rowCounter].Education == "Stanford")
                        {
                            resultString += presidents[rowCounter].Number + delim + presidents[rowCounter].First + delim + presidents[rowCounter].Last + delim + presidents[rowCounter].Education + "\r\n";
                        }
                    }
                }
                if (subtopics.Contains("Oxford"))
                {
                    for (int rowCounter = 0; rowCounter < presidents.Length; rowCounter++)
                    {
                        if (presidents[rowCounter].Education == "Oxford")
                        {
                            resultString += presidents[rowCounter].Number + delim + presidents[rowCounter].First + delim + presidents[rowCounter].Last + delim + presidents[rowCounter].Education + "\r\n";
                        }
                    }
                }
            }
            if (topic.Contains("Occupation"))
            {
                resultHeader += "number" + delim + "first" + delim + "last" + delim + "occupation";
                if (subtopics.Contains("Planter"))
                {
                    for (int rowCounter = 0; rowCounter < presidents.Length; rowCounter++)
                    {
                        if (presidents[rowCounter].Occupation == "Planter")
                        {
                            resultString += presidents[rowCounter].Number + delim + presidents[rowCounter].First + delim + presidents[rowCounter].Last + delim + presidents[rowCounter].Occupation + "\r\n";
                        }
                    }
                }
                if (subtopics.Contains("Lawyer"))
                {
                    for (int rowCounter = 0; rowCounter < presidents.Length; rowCounter++)
                    {
                        if (presidents[rowCounter].Occupation == "Lawyer" || presidents[rowCounter].Occupation == "Lawyer, Soldier")
                        {
                            resultString += presidents[rowCounter].Number + delim + presidents[rowCounter].First + delim + presidents[rowCounter].Last + delim + presidents[rowCounter].Occupation + "\r\n";
                        }
                    }
                }
                if (subtopics.Contains("Soldier"))
                {
                    for (int rowCounter = 0; rowCounter < presidents.Length; rowCounter++)
                    {
                        if (presidents[rowCounter].Occupation == "Soldier")
                        {
                            resultString += presidents[rowCounter].Number + delim + presidents[rowCounter].First + delim + presidents[rowCounter].Last + delim + presidents[rowCounter].Occupation + "\r\n";
                        }
                    }
                }
                if (subtopics.Contains("Tailor"))
                {
                    for (int rowCounter = 0; rowCounter < presidents.Length; rowCounter++)
                    {
                        if (presidents[rowCounter].Occupation == "Tailor")
                        {
                            resultString += presidents[rowCounter].Number + delim + presidents[rowCounter].First + delim + presidents[rowCounter].Last + delim + presidents[rowCounter].Occupation + "\r\n";
                        }
                    }
                }
                if (subtopics.Contains("Businessman"))
                {
                    for (int rowCounter = 0; rowCounter < presidents.Length; rowCounter++)
                    {
                        if (presidents[rowCounter].Occupation == "Businessman")
                        {
                            resultString += presidents[rowCounter].Number + delim + presidents[rowCounter].First + delim + presidents[rowCounter].Last + delim + presidents[rowCounter].Occupation + "\r\n";
                        }
                    }
                }
            }
            if (topic.Contains("Death"))
            {
                resultHeader += "number" + delim + "first" + delim + "last" + delim + "deathCause";
                if (subtopics.Contains("attack"))
                {
                    for (int rowCounter = 0; rowCounter < presidents.Length; rowCounter++)
                    {
                        if (presidents[rowCounter].CauseOfDeath == "heart attack")
                        {
                            resultString += presidents[rowCounter].Number + delim + presidents[rowCounter].First + delim + presidents[rowCounter].Last + delim + presidents[rowCounter].CauseOfDeath + "\r\n";
                        }
                    }
                }
                if (subtopics.Contains("Gunshot"))
                {
                    for (int rowCounter = 0; rowCounter < presidents.Length; rowCounter++)
                    {
                        if (presidents[rowCounter].CauseOfDeath == "gunshot wound" || presidents[rowCounter].CauseOfDeath == "gunshot wound septic shock" || presidents[rowCounter].CauseOfDeath == "gunshot wound gangrene")
                        {
                            resultString += presidents[rowCounter].Number + delim + presidents[rowCounter].First + delim + presidents[rowCounter].Last + delim + presidents[rowCounter].CauseOfDeath + "\r\n";
                        }
                    }
                }
                if (subtopics.Contains("Stroke"))
                {
                    for (int rowCounter = 0; rowCounter < presidents.Length; rowCounter++)
                    {
                        if (presidents[rowCounter].CauseOfDeath == "stroke")
                        {
                            resultString += presidents[rowCounter].Number + delim + presidents[rowCounter].First + delim + presidents[rowCounter].Last + delim + presidents[rowCounter].CauseOfDeath + "\r\n";
                        }
                    }
                }
                if (subtopics.Contains("failure"))
                {
                    for (int rowCounter = 0; rowCounter < presidents.Length; rowCounter++)
                    {
                        if (presidents[rowCounter].CauseOfDeath == "heart failure")
                        {
                            resultString += presidents[rowCounter].Number + delim + presidents[rowCounter].First + delim + presidents[rowCounter].Last + delim + presidents[rowCounter].CauseOfDeath + "\r\n";
                        }
                    }
                }
                if (subtopics.Contains("disease"))
                {
                    for (int rowCounter = 0; rowCounter < presidents.Length; rowCounter++)
                    {
                        if (presidents[rowCounter].CauseOfDeath == "heart disease")
                        {
                            resultString += presidents[rowCounter].Number + delim + presidents[rowCounter].First + delim + presidents[rowCounter].Last + delim + presidents[rowCounter].CauseOfDeath + "\r\n";
                        }
                    }
                }
            }
            if (topic.Contains("Party"))
            {
                resultHeader += "number" + delim + "first" + delim + "last" + delim + "party";
                if (subtopics.Contains("Dem-Rep"))
                {
                    for (int rowCounter = 0; rowCounter < presidents.Length; rowCounter++)
                    {
                        if (presidents[rowCounter].Party == "Dem-Rep")
                        {
                            resultString += presidents[rowCounter].Number + delim + presidents[rowCounter].First + delim + presidents[rowCounter].Last + delim + presidents[rowCounter].Party + "\r\n";
                        }
                    }
                }
                if (subtopics.Contains("Democrat"))
                {
                    for (int rowCounter = 0; rowCounter < presidents.Length; rowCounter++)
                    {
                        if (presidents[rowCounter].Party == "Democrat")
                        {
                            resultString += presidents[rowCounter].Number + delim + presidents[rowCounter].First + delim + presidents[rowCounter].Last + delim + presidents[rowCounter].Party + "\r\n";
                        }
                    }
                }
                if (subtopics.Contains("Federalist"))
                {
                    for (int rowCounter = 0; rowCounter < presidents.Length; rowCounter++)
                    {
                        if (presidents[rowCounter].Party == "Federalist")
                        {
                            resultString += presidents[rowCounter].Number + delim + presidents[rowCounter].First + delim + presidents[rowCounter].Last + delim + presidents[rowCounter].Party + "\r\n";
                        }
                    }
                }
                if (subtopics.Contains("designation"))
                {
                    for (int rowCounter = 0; rowCounter < presidents.Length; rowCounter++)
                    {
                        if (presidents[rowCounter].Party == "no designation")
                        {
                            resultString += presidents[rowCounter].Number + delim + presidents[rowCounter].First + delim + presidents[rowCounter].Last + delim + presidents[rowCounter].Party + "\r\n";
                        }
                    }
                }
                if (subtopics.Contains("Republican"))
                {
                    for (int rowCounter = 0; rowCounter < presidents.Length; rowCounter++)
                    {
                        if (presidents[rowCounter].Party == "Republican")
                        {
                            resultString += presidents[rowCounter].Number + delim + presidents[rowCounter].First + delim + presidents[rowCounter].Last + delim + presidents[rowCounter].Party + "\r\n";
                        }
                    }
                }
                if (subtopics.Contains("Union"))
                {
                    for (int rowCounter = 0; rowCounter < presidents.Length; rowCounter++)
                    {
                        if (presidents[rowCounter].Party == "Union")
                        {
                            resultString += presidents[rowCounter].Number + delim + presidents[rowCounter].First + delim + presidents[rowCounter].Last + delim + presidents[rowCounter].Party + "\r\n";
                        }
                    }
                }
                if (subtopics.Contains("Whig"))
                {
                    for (int rowCounter = 0; rowCounter < presidents.Length; rowCounter++)
                    {
                        if (presidents[rowCounter].Party == "Whig")
                        {
                            resultString += presidents[rowCounter].Number + delim + presidents[rowCounter].First + delim + presidents[rowCounter].Last + delim + presidents[rowCounter].Party + "\r\n";
                        }
                    }
                }
            }
            if (topic.Contains("Religion"))
            {
                resultHeader += "number" + delim + "first" + delim + "last" + delim + "religion";
                if (subtopics.Contains("Baptist"))
                {
                    for (int rowCounter = 0; rowCounter < presidents.Length; rowCounter++)
                    {
                        if (presidents[rowCounter].Religion == "Baptist")
                        {
                            resultString += presidents[rowCounter].Number + delim + presidents[rowCounter].First + delim + presidents[rowCounter].Last + delim + presidents[rowCounter].Religion + "\r\n";
                        }
                    }
                }
                if (subtopics.Contains("Congregationalist"))
                {
                    for (int rowCounter = 0; rowCounter < presidents.Length; rowCounter++)
                    {
                        if (presidents[rowCounter].Religion == "Congregationalist")
                        {
                            resultString += presidents[rowCounter].Number + delim + presidents[rowCounter].First + delim + presidents[rowCounter].Last + delim + presidents[rowCounter].Religion + "\r\n";
                        }
                    }
                }
                if (subtopics.Contains("Disciples"))
                {
                    for (int rowCounter = 0; rowCounter < presidents.Length; rowCounter++)
                    {
                        if (presidents[rowCounter].Religion == "Disciples of Christ")
                        {
                            resultString += presidents[rowCounter].Number + delim + presidents[rowCounter].First + delim + presidents[rowCounter].Last + delim + presidents[rowCounter].Religion + "\r\n";
                        }
                    }
                }
                if (subtopics.Contains("Dutch"))
                {
                    for (int rowCounter = 0; rowCounter < presidents.Length; rowCounter++)
                    {
                        if (presidents[rowCounter].Religion == "Dutch Reformed")
                        {
                            resultString += presidents[rowCounter].Number + delim + presidents[rowCounter].First + delim + presidents[rowCounter].Last + delim + presidents[rowCounter].Religion + "\r\n";
                        }
                    }
                }
                if (subtopics.Contains("Episcopalian"))
                {
                    for (int rowCounter = 0; rowCounter < presidents.Length; rowCounter++)
                    {
                        if (presidents[rowCounter].Religion == "Episcopalian")
                        {
                            resultString += presidents[rowCounter].Number + delim + presidents[rowCounter].First + delim + presidents[rowCounter].Last + delim + presidents[rowCounter].Religion + "\r\n";
                        }
                    }
                }
                if (subtopics.Contains("Methodist"))
                {
                    for (int rowCounter = 0; rowCounter < presidents.Length; rowCounter++)
                    {
                        if (presidents[rowCounter].Religion == "Methodist")
                        {
                            resultString += presidents[rowCounter].Number + delim + presidents[rowCounter].First + delim + presidents[rowCounter].Last + delim + presidents[rowCounter].Religion + "\r\n";
                        }
                    }
                }
                if (subtopics.Contains("affiliation"))
                {
                    for (int rowCounter = 0; rowCounter < presidents.Length; rowCounter++)
                    {
                        if (presidents[rowCounter].Religion == "no formal affiliation")
                        {
                            resultString += presidents[rowCounter].Number + delim + presidents[rowCounter].First + delim + presidents[rowCounter].Last + delim + presidents[rowCounter].Religion + "\r\n";
                        }
                    }
                }
                if (subtopics.Contains("Presbyterian"))
                {
                    for (int rowCounter = 0; rowCounter < presidents.Length; rowCounter++)
                    {
                        if (presidents[rowCounter].Religion == "Presbyterian")
                        {
                            resultString += presidents[rowCounter].Number + delim + presidents[rowCounter].First + delim + presidents[rowCounter].Last + delim + presidents[rowCounter].Religion + "\r\n";
                        }
                    }
                }
                if (subtopics.Contains("Quaker"))
                {
                    for (int rowCounter = 0; rowCounter < presidents.Length; rowCounter++)
                    {
                        if (presidents[rowCounter].Religion == "Quaker")
                        {
                            resultString += presidents[rowCounter].Number + delim + presidents[rowCounter].First + delim + presidents[rowCounter].Last + delim + presidents[rowCounter].Religion + "\r\n";
                        }
                    }
                }
                if (subtopics.Contains("Unitarian"))
                {
                    for (int rowCounter = 0; rowCounter < presidents.Length; rowCounter++)
                    {
                        if (presidents[rowCounter].Religion == "Unitarian")
                        {
                            resultString += presidents[rowCounter].Number + delim + presidents[rowCounter].First + delim + presidents[rowCounter].Last + delim + presidents[rowCounter].Religion + "\r\n";
                        }
                    }
                }
                if (subtopics.Contains("United"))
                {
                    for (int rowCounter = 0; rowCounter < presidents.Length; rowCounter++)
                    {
                        if (presidents[rowCounter].Religion == "United Church of Christ")
                        {
                            resultString += presidents[rowCounter].Number + delim + presidents[rowCounter].First + delim + presidents[rowCounter].Last + delim + presidents[rowCounter].Religion + "\r\n";
                        }
                    }
                }
            }

            // Defensive programming
            string noTopicSelection = "No topic selected";
            string noSubtopicSelection = "No subtopic selected";
            string noResults = "No results found";

            // Output to user
            if (topic == null)
            {
                MessageBox.Show(noTopicSelection);
            }
            if (subtopics == string.Empty)
            {
                MessageBox.Show(noSubtopicSelection);
            }
            if (topic != null && subtopics != string.Empty && resultString == string.Empty)
            {
                MessageBox.Show(noResults);
            }
            if (topic != null && subtopics != string.Empty && resultString != string.Empty)
            {
                MessageBox.Show(resultHeader + "\n\n" + resultString);

                string outputPath = "C:\\Users\\" + Environment.UserName + "\\Desktop\\output.csv";
                FileStream outputFile = new FileStream(outputPath, FileMode.Append, FileAccess.Write);
                StreamWriter output = new StreamWriter(outputFile);

                output.WriteLine(resultHeader + "\r\n" + resultString + "\r\n");

                output.Close();
                outputFile.Close();
            }
        }

        static int getRowCount(string file)
        {
            int iterate = 0;
            string readRow;
            FileStream fileStream = new FileStream(file, FileMode.Open, FileAccess.Read);
            StreamReader streamRead = new StreamReader(fileStream);

            readRow = streamRead.ReadLine();

            while (readRow != null)
            {
                ++iterate;
                readRow = streamRead.ReadLine();
            }
            streamRead.Close();
            fileStream.Close();
            return iterate;
        }

        public class President
        {
            public string Number { get; set; }
            public string First { get; set; }
            public string Last { get; set; }

            public string Education { get; set; }
            public string Occupation { get; set; }
            public string CauseOfDeath { get; set; }
            public string Party { get; set; }
            public string Religion { get; set; }
        }

        private void radioButtonEducation_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonEducation.Checked)
            {
                checkedListBox2.Items.Clear();
                checkedListBox2.Items.AddRange(new object[]
                {
                    "none",
                    "Harvard",
                    "Yale",
                    "Stanford",
                    "Oxford"
                });
            }
        }

        private void radioButtonOccupation_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonOccupation.Checked)
            {
                checkedListBox2.Items.Clear();
                checkedListBox2.Items.AddRange(new object[]
                {
                    "Planter",
                    "Lawyer",
                    "Soldier",
                    "Tailor",
                    "Businessman"
                });
            }
        }

        private void radioButtonCauseOfDeath_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonCauseOfDeath.Checked)
            {
                checkedListBox2.Items.Clear();
                checkedListBox2.Items.AddRange(new object[]
                {
                    "Heart attack",
                    "Gunshot",
                    "Stroke",
                    "Heart failure",
                    "Heart disease"
                });
            }
        }

        private void radioButtonParty_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonParty.Checked)
            {
                checkedListBox2.Items.Clear();
                checkedListBox2.Items.AddRange(new object[]
                {
                    "Dem-Rep",
                    "Democrat",
                    "Federalist",
                    "no designation",
                    "Republican",
                    "Union",
                    "Whig"
                });
            }
        }

        private void radioButtonReligion_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonReligion.Checked)
            {
                checkedListBox2.Items.Clear();
                checkedListBox2.Items.AddRange(new object[]
                {
                    "Baptist",
                    "Congregationalist",
                    "Disciples of Christ",
                    "Dutch Reformed",
                    "Episcopalian",
                    "Methodist",
                    "no affiliation",
                    "Presbyterian",
                    "Quaker",
                    "Unitarian",
                    "United Church of Christ"
                });
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            panel2.Visible = false;
            panel3.Visible = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = true;
            panel3.Visible = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = true;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = true;
        }
    }
}