using MaterialSkin;
using MaterialSkin.Controls;
using System.IO;
using Newtonsoft.Json.Linq;
using PoniLCU;
using static PoniLCU.LeagueClient;
using Newtonsoft.Json;
using System.Globalization;

namespace Amumus_Friends
{
    public partial class amumus_friends : MaterialForm
    {
        public static LeagueClient leagueClient = new LeagueClient(credentials.cmd);

        public amumus_friends()
        {
            InitializeComponent();

            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Green400, Primary.Green500, Primary.Green600, Accent.Green100, TextShade.WHITE);

        }


        private void Form1_Load(object sender, EventArgs e)
        {
            string exportsFolder = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Exports");
            if (Directory.Exists(exportsFolder))
            {
                string[] files = Directory.GetFiles(exportsFolder);
                if (files.Length > 0)
                {
                    // Populate the list box with the last update dates of the files
                    foreach (string file in files)
                    {
                        string formattedDate = File.GetLastWriteTime(file).ToString("yyyy-MM-dd HH:mm:ss");
                        exportsListBox.Items.Add(formattedDate);
                    }
                }
                else
                {
                    // If there are no files, disable the list box
                    exportsListBox.Enabled = false;
                }
            }
            else
            {
                // If the folder doesn't exist, disable the list box
                exportsListBox.Enabled = false;
            }
        }


        private void deleteAllBtn_Click(object sender, EventArgs e)
        {

        }

        private async void exportBtn_Click(object sender, EventArgs e)
        {
            try
            {
                var friendListStr = await leagueClient.Request(requestMethod.GET, "lol-game-client-chat/v1/buddies");

                List<string> friendList = JsonConvert.DeserializeObject<List<string>>(friendListStr);
                List<object> friendDataList = new List<object>();
                foreach (string friend in friendList)
                {
                    var ok = friend;
                    var friendDataStr = await leagueClient.Request(requestMethod.GET, "/lol-summoner/v1/summoners?name=" + friend);
                    JObject friendDataObj = JsonConvert.DeserializeObject<JObject>(friendDataStr);
                    var friendInternalName = friendDataObj["internalName"].ToString();
                    var friendPuuid = friendDataObj["puuid"].ToString();

                    // Add the friend data to the list
                    friendDataList.Add(new { InternalName = friendInternalName, puuid = friendPuuid });

                    // Don't wanna spam the API too much so we'll wait a bit (This delay is sufficient according to my tests)
                    await Task.Delay(100);
                }

                // Create the "Exports" folder if it doesn't exist
                string exportsFolder = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Exports");
                if (!Directory.Exists(exportsFolder))
                {
                    Directory.CreateDirectory(exportsFolder);
                }

                string fileName = "exported_friends_" + DateTime.Now.ToString("yyyyMMddHHmmsstt") + ".json";
                string filePath = Path.Combine(exportsFolder, fileName);
                using (StreamWriter file = new StreamWriter(filePath))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, friendDataList);
                }
                MessageBox.Show("Exported friends to " + filePath, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        private void importBtn_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}