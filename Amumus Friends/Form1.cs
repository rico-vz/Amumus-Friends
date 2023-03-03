using MaterialSkin;
using MaterialSkin.Controls;
using System.IO;
using Newtonsoft.Json.Linq;
using PoniLCU;
using static PoniLCU.LeagueClient;
using Newtonsoft.Json;
using System.Globalization;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace Amumus_Friends
{
    public partial class amumus_friends : MaterialForm
    {
        public static LeagueClient leagueClient = new LeagueClient(credentials.cmd);

        public void OnClientConnected()
        {
            this.Enabled = true;
            statusLabel.Text = "LCU Status: Connected.";
        }

        public void OnClientDisconnected()
        {
            this.Enabled = false;
            statusLabel.Text = "LCU Status: Disconnected.";
        }

        public amumus_friends()
        {
            InitializeComponent();

            leagueClient.OnConnected += OnClientConnected;
            leagueClient.OnDisconnected += OnClientDisconnected;

            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Green400, Primary.Green500, Primary.Green600, Accent.Green100, TextShade.WHITE);

            if (leagueClient.IsConnected)
            {
                this.Enabled = true;
                statusLabel.Text = "LCU Status: Connected.";
            }
            else
            {
                this.Enabled = false;
                statusLabel.Text = "LCU Status: Disconnected.";
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            string exportsFolder = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Exports");
            if (Directory.Exists(exportsFolder))
            {
                string[] files = Directory.GetFiles(exportsFolder);
                if (files.Length > 0)
                {
                    foreach (string file in files)
                    {
                        string formattedDate = File.GetLastWriteTime(file).ToString("yyyy-MM-dd HH:mm:ss");
                        KeyValuePair<string, string> fileData = new KeyValuePair<string, string>(formattedDate, file);
                        exportsListBox.Items.Add(fileData);
                    }
                }
                else
                {
                    exportsListBox.Enabled = false;
                }
            }
            else
            {
                exportsListBox.Enabled = false;
            }
        }


        private async void deleteAllBtn_Click(object sender, EventArgs e)
        {
            try
            {
                var result = MessageBox.Show("Are you sure you want to delete all your friends?\nThis can NOT be undone.", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.No)
                {
                    return;
                }

                var friendListStr = await leagueClient.Request(requestMethod.GET, "lol-game-client-chat/v1/buddies");

                List<string> friendList = JsonConvert.DeserializeObject<List<string>>(friendListStr);
                foreach (string friend in friendList)
                {
                    var friendDataStr = await leagueClient.Request(requestMethod.GET, "/lol-summoner/v1/summoners?name=" + friend);
                    JObject friendDataObj = JsonConvert.DeserializeObject<JObject>(friendDataStr);
                    var friendPuuid = friendDataObj["puuid"].ToString();

                    try
                    {
                        await leagueClient.Request(requestMethod.DELETE, "/lol-chat/v1/friends/" + friendPuuid);
                    }
                    catch (Exception ex)
                    {
                    }

                    // Don't wanna spam the API too much so we'll wait a bit (This delay is sufficient according to my tests)
                    await Task.Delay(100);
                }


                MessageBox.Show("Deleted all friends.\nClient could need a few minutes to fully update your friendslist.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
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


        private async void importBtn_Click(object sender, EventArgs e)
        {
            if (exportsListBox.SelectedIndex >= 0)
            {
                KeyValuePair<string, string> selectedFileData = (KeyValuePair<string, string>)exportsListBox.SelectedItem;
                string filePath = selectedFileData.Value;

                string fileContents = File.ReadAllText(filePath);

                List<Dictionary<string, string>> dataList = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(fileContents);

                // Loop over each dictionary and extract the 'puuid' value
                foreach (Dictionary<string, string> data in dataList)
                {
                    string puuid = data["puuid"];
                    string name = data["InternalName"];

                    try
                    {
                        var friendDataStr = await leagueClient.Request(requestMethod.GET,
                            "/lol-summoner/v1/summoners-by-puuid-cached/" + puuid);
                        JObject friendDataObj = JsonConvert.DeserializeObject<JObject>(friendDataStr);

                        if (name != friendDataObj["internalName"].ToString())
                        {
                            var result = MessageBox.Show("The name of the friend you are trying to import has changed from " + name + " to " + friendDataObj["internalName"].ToString() + ".\nDo you want to continue adding them?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            if (result == DialogResult.Yes)
                            {
                                var body2 = JsonConvert.SerializeObject(new { direction = "out", name = friendDataObj["internalName"].ToString() });
                                await leagueClient.Request(requestMethod.POST, "/lol-chat/v1/friend-requests", body2);


                            }
                            else
                            {
                                continue;
                            }
                        }

                        var body = JsonConvert.SerializeObject(new { direction = "out", name = name });
                        await leagueClient.Request(requestMethod.POST, "/lol-chat/v1/friend-requests", body);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                    // Don't wanna spam the API too much so we'll wait a bit (This delay is sufficient according to my tests)
                    await Task.Delay(100);
                }
            }
            else
            {
                MessageBox.Show("You did not select a file to import.");
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}