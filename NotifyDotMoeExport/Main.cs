using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NotifyDotMoeExport.Exports;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace NotifyDotMoeExport
{
    public partial class Main : Form
    {
        const string NotifyAPI = "https://notify.moe/api/";
        const string AnilistAPI = "https://graphql.anilist.co/";
        const string AnilistAPIQuery = @"mutation ($mediaId: Int, $status: MediaListStatus, $score: Int, $progress: Int, $repeat: Int, $private: Boolean, $notes: String, $startedAt: FuzzyDateInput, $completedAt: FuzzyDateInput) {
                                      SaveMediaListEntry(mediaId: $mediaId, status: $status, scoreRaw: $score, progress: $progress, repeat: $repeat, private: $private, notes: $notes, startedAt: $startedAt, completedAt: $completedAt) {
                                        id
                                        status
                                        score
                                        progress
                                        repeat
                                        private
                                        notes
                                        startedAt {
                                          year
                                          month
                                          day
                                        }
                                        completedAt {
                                          year
                                          month
                                          day
                                        }
                                      }
                                    }";
        public Main()
        {
            InitializeComponent();
            SetProgressBarVisibility(false);
        }

        private void buttonSelectFile_Click(object sender, EventArgs e)
        {
            /* Get the content of the Tag propriety of the button as it define which path to define.
             * Index 0 is the file type, index 1 is the name of the textbox where the Text property  will be updated
             * Ex : "json|textBoxJsonPath" will ask for json files, and set the Text of textBoxJsonPath
             */
            string[] buttonArgs = (sender as Button).Tag.ToString().Split('|');
            openFileDialogMenu.Filter = $"Notify File (*.{buttonArgs[0]})|*.{buttonArgs[0]}";
            openFileDialogMenu.FileName = string.Empty;
            DialogResult result = openFileDialogMenu.ShowDialog();
            if (result == DialogResult.OK)
            {
                TextBox currentTextBox = (TextBox)this.Controls.Find(buttonArgs[1], true)[0];
                currentTextBox.Text = openFileDialogMenu.FileName;
            }
        }

        private async void buttonSaveFile_Click(object sender, EventArgs e)
        {
            // Get the content of the Tag propriety of the button as it define which path to define.
            string pathType = (string)(sender as Button).Tag;
            saveFileDialogMenu.Filter = $"Notify Save (*.{pathType})|*.{pathType}";
            saveFileDialogMenu.FileName = string.Empty;
            DialogResult result = saveFileDialogMenu.ShowDialog();
            if (result == DialogResult.OK)
            {
                switch (pathType) 
                {
                    case "json":
                        MessageBoxWithTextInput textInputMessageBox = new MessageBoxWithTextInput("Please enter your notify.moe username.","Username needed");
                        DialogResult confirmTextInputMessageBoxResult = textInputMessageBox.ShowDialog();
                        if (confirmTextInputMessageBoxResult == DialogResult.OK) 
                        {
                            bool downloaded = await DownloadNotifyAnimeListFormUsername(saveFileDialogMenu.FileName, textInputMessageBox.GetUserInput);
                            if (downloaded) 
                            {
                                textBoxJsonPath.Text = saveFileDialogMenu.FileName;
                            }
                            else 
                            {
                                MessageBox.Show($"Failed to find username '{textInputMessageBox.GetUserInput}'.\nAnime list wasn't downloaded.","Failed Download", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        // Froms shown with ShowDialog need to be disposed manually
                        textInputMessageBox.Dispose();
                        break;
                    case "dat":
                        DialogResult confirmMessageBoxResult = MessageBox.Show("Downloading the 'ActivityConsumeAnime' DB file of notify.moe causes some stress on the server.\nIf you already have a DB file who is more recent than your notify json export, please re-use this one insead.\n\n Download the database file ?", "Notice", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (confirmMessageBoxResult == DialogResult.No)
                            return;

                        await DownloadNotifyConsumeAnimeActivityDB(openFileDialogMenu.FileName);
                        textBoxConsAnimeDatPath.Text = saveFileDialogMenu.FileName;
                        break;
                    case "xml":
                        textBoxMalOutputXML.Text = saveFileDialogMenu.FileName;
                        break;
                }
            }
        }

        private void buttonCacheOutputPath_Click(object sender, EventArgs e)
        {
            saveFileDialogMenu.Filter = $"Notify Save (*.json-cache)|*.json-cache";
            saveFileDialogMenu.FileName = string.Empty;
            DialogResult result = saveFileDialogMenu.ShowDialog();
            if (result == DialogResult.OK)
            {
                textBoxCacheOutputPath.Text = saveFileDialogMenu.FileName;
            }
        }

        private async void buttonConvert_Click(object sender, EventArgs e)
        {
            bool isCacheFileUsed = textBoxCacheInputPath.Text.Length >= 1 ? true : false;
            // Verify obligatory inputs
            if (textBoxJsonPath.Text.Length <= 0)
            {
                MessageBox.Show("Path to the json anime list export file invalid!", "Invalid Path", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (textBoxConsAnimeDatPath.Text.Length <= 0)
            {
                MessageBox.Show("Path to ConsumeAnime dat file invalid!", "Invalid Path", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (textBoxMalOutputXML.Text.Length <= 0 && radioButtonModeMALFormat.Checked)
            {
                MessageBox.Show("There is no output path for your MAL export!", "Invalid Path", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (textBoxCacheOutputPath.Text.Length <= 0)
            {
                MessageBox.Show("There is no output path for your cache!", "Invalid Path", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Disable buttons & show progress bar
            SetProgressBarVisibility(true);

            // Load json anime list export
            NotifyTemplate notifyList = await LoadNotifyTemplate(textBoxJsonPath.Text);
            // Load Anime Consume Activity
            List<NotifyActivityConsumeAnime> notifyUserActivity = await LoadNotifyActivityConsumeAnime(textBoxConsAnimeDatPath.Text, notifyList.userID, CreateProgress("Phrasing consumed anime file..."));
            // Loop trought the notifyList and consume activity to get start and end dates
            notifyList = await AddNotifyDates(notifyList, notifyUserActivity, CreateProgress("Searching dates..."));
            // Verify for cache file
            if (isCacheFileUsed)
            {
                // Load cached anime list export
                NotifyTemplate cachedNotifyList = await LoadNotifyTemplate(textBoxCacheInputPath.Text);
                // Merge the new anime list export to the previously cached version, only add/overwrite new & modified anime entry to the list
                notifyList = await MergeNotifyTemplateItemEntry(cachedNotifyList, notifyList);
            }
            // Fetch notify mappings
            notifyList = await AddNotifyMappings(notifyList, CreateProgress("Fetching notify mappings..."));

            // Mode
            if (radioButtonModeMALFormat.Checked)
            {
                // Convert notify list to a MAL list
                MALTemplate MALxmlFile = await NotifyTemplateToMalTemplate(notifyList, CreateProgress("Converting list to MAL format..."));
                // Write the MALTemplate to a xml file
                SaveMALTemplateToXml(textBoxMalOutputXML.Text, MALxmlFile);
            }
            else if (radioButtonModeAnilistAPI.Checked) // Use Anilist API mode
            {
                MessageBoxWithTextInput textInputMessageBox = new MessageBoxWithTextInput("Please enter your anilist token.", "Anilist APP token needed");
                DialogResult confirmTextInputMessageBoxResult = textInputMessageBox.ShowDialog();
                if (confirmTextInputMessageBoxResult == DialogResult.OK)
                {
                    IDictionary<string, IEnumerable<string>> failedImports = await PushNotifyTemplateToAnilistAPI(notifyList, textInputMessageBox.GetUserInput, checkBoxIgnoreAnilistRules.Checked, CreateProgress("Pushing animes to Anilist API..."));
                    if (failedImports.Count != 0) 
                    {
                        MessageBox.Show("The exporter was unable to copy certain entries. A file with the list of relevant entries will be created, please select its location.","Anilist API Mode - Failed Imports",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                        saveFileDialogMenu.Filter = $"Notify Save (*.txt)|*.txt";
                        saveFileDialogMenu.FileName = string.Empty;
                        DialogResult result = saveFileDialogMenu.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            SaveAnilistFailedImportsToTxt(saveFileDialogMenu.FileName, failedImports);
                        }
                    }
                }
                textInputMessageBox.Dispose();
            }
            // Save the cache file
            SaveNotifyTemplateToJson(textBoxCacheOutputPath.Text, notifyList);
            SetProgressBarVisibility(false);
        }

        /// <summary>
        /// Phrase JSON file as a notify export
        /// </summary>
        /// <param name="filePath">Path of the json file</param>
        /// <returns>NotifyTemplate list</returns>
        private async Task<NotifyTemplate> LoadNotifyTemplate(string filePath)
        {
            string json = File.ReadAllText(filePath);
            NotifyTemplate newTemplate = await Task.Run(() => JsonConvert.DeserializeObject<NotifyTemplate>(json));
            return newTemplate;
        }

        /// <summary>
        /// Loop trought a NotifyTemplate listToMerge (inside items), if no matching (by animeID) items are found in the mainList or if the cached one differ from the listToMerge, the item will be added/overwritten in the mainList
        /// </summary>
        /// <param name="mainList">The main list, items from the 'listToMerge' will added into this list</param>
        /// <param name="listToMerge">Items that are not already in the mainList will be added to it</param>
        /// <returns>The mainList with the second list merged into it</returns>
        private async Task<NotifyTemplate> MergeNotifyTemplateItemEntry(NotifyTemplate mainList, NotifyTemplate listToMerge)
        {
            await Task.Run(() => 
            {
                foreach (Items newItemEntry in listToMerge.items)
                {
                    // Verify if a entry in the listToMerge already exist in the mainList. If it does, verify if anything differ from the mainList.
                    if (mainList.items.Any(value => value.animeID == newItemEntry.animeID))
                    {
                        int currentMainListItemIndex = mainList.items.Select((f, i) => new { Fields = f, Index = i })
                                                                                .Where(x => x.Fields.animeID == newItemEntry.animeID)
                                                                                .Select(x => x.Index)
                                                                                .First();
                        bool isOverwrite = false;
                        isOverwrite = mainList.items[currentMainListItemIndex].isPrivate != newItemEntry.isPrivate ? true : isOverwrite;
                        isOverwrite = mainList.items[currentMainListItemIndex].notes != newItemEntry.notes ? true : isOverwrite;
                        isOverwrite = mainList.items[currentMainListItemIndex].rating.overall != newItemEntry.rating.overall ? true : isOverwrite;
                        isOverwrite = mainList.items[currentMainListItemIndex].rating.visuals != newItemEntry.rating.visuals ? true : isOverwrite;
                        isOverwrite = mainList.items[currentMainListItemIndex].rating.soundtrack != newItemEntry.rating.soundtrack ? true : isOverwrite;
                        isOverwrite = mainList.items[currentMainListItemIndex].rating.story != newItemEntry.rating.story ? true : isOverwrite;
                        isOverwrite = mainList.items[currentMainListItemIndex].rewatchCount != newItemEntry.rewatchCount ? true : isOverwrite;
                        isOverwrite = mainList.items[currentMainListItemIndex].status != newItemEntry.status ? true : isOverwrite;
                        isOverwrite = mainList.items[currentMainListItemIndex].watchedEpisodes != newItemEntry.watchedEpisodes ? true : isOverwrite;
                        isOverwrite = mainList.items[currentMainListItemIndex].watchedDate != newItemEntry.watchedDate ? true : isOverwrite;
                        isOverwrite = mainList.items[currentMainListItemIndex].endedDate != newItemEntry.endedDate ? true : isOverwrite;
                        if (isOverwrite) 
                        {
                            mainList.items[currentMainListItemIndex].isPrivate = newItemEntry.isPrivate;
                            mainList.items[currentMainListItemIndex].notes = newItemEntry.notes;
                            mainList.items[currentMainListItemIndex].rating.overall = newItemEntry.rating.overall;
                            mainList.items[currentMainListItemIndex].rating.visuals = newItemEntry.rating.visuals;
                            mainList.items[currentMainListItemIndex].rating.soundtrack = newItemEntry.rating.soundtrack;
                            mainList.items[currentMainListItemIndex].rating.story = newItemEntry.rating.story;
                            mainList.items[currentMainListItemIndex].rewatchCount = newItemEntry.rewatchCount;
                            mainList.items[currentMainListItemIndex].status = newItemEntry.status;
                            mainList.items[currentMainListItemIndex].watchedEpisodes = newItemEntry.watchedEpisodes;
                            mainList.items[currentMainListItemIndex].watchedDate = newItemEntry.watchedDate;
                            mainList.items[currentMainListItemIndex].endedDate = newItemEntry.endedDate;
                            mainList.items[currentMainListItemIndex].isNewEntry = true; // Set the item as new since it was updated
                        }
                    }
                    else // Entry does not exist in mainList, add it.
                    {
                        mainList.items = mainList.items.Append(newItemEntry).ToArray();
                    }
                }
            });
            return mainList;
        }

        /// <summary>
        /// Loop trought a NotifyTemplate object (inside item) and populate it's mappings & title by fetching notify.moe api
        /// *If a item already have the mappings and title populated, it will be skipped.
        /// </summary>
        /// <param name="currentList">A NotifyTemplate class</param>
        /// <param name="progress">Optional, report progress</param>
        /// <returns>A copy of the NotifyTemplate class with mappings populated</returns>
        private async Task<NotifyTemplate> AddNotifyMappings(NotifyTemplate currentList, IProgress<int> progress = null)
        {
            await Task.Run(async () =>
            {
                HttpClient webClient = new HttpClient { BaseAddress = new Uri($"{NotifyAPI}anime/") };
                for (int i = 0; i <= currentList.items.Length - 1; i++)
                {
                    // Only make API request for mappings and titles if one of them is null, else keep the previous value
                    if (currentList.items[i].mappings == null || string.IsNullOrEmpty(currentList.items[i].title)) 
                    {
                        string webRequestResponse = await webClient.GetAsync(currentList.items[i].animeID).Result.Content.ReadAsStringAsync();
                        // Cconvert the webRequestResponse to a json object
                        JObject webRequestJsonObject = (JObject)JsonConvert.DeserializeObject(webRequestResponse);

                        // Convert the "mappings" key inside the json object to a array[]
                        JArray webMappingsJsonArray = (JArray)webRequestJsonObject["mappings"];
                        // Convert the array[] to a Mappings[] & set item mappings
                        currentList.items[i].mappings = webMappingsJsonArray.ToObject<Mappings[]>();

                        // Set the anime title, fallback to others key if one is empty/null
                        if (!string.IsNullOrEmpty(webRequestJsonObject["title"].Value<string>("canonical")))
                        {
                            currentList.items[i].title = webRequestJsonObject["title"].Value<string>("canonical");
                        }
                        else if (!string.IsNullOrEmpty(webRequestJsonObject["title"].Value<string>("romaji")))
                        {
                            currentList.items[i].title = webRequestJsonObject["title"].Value<string>("romaji");
                        }
                        else
                        {
                            currentList.items[i].title = webRequestJsonObject["title"].Value<string>("english");
                        }
                        await Task.Delay(100); // 100ms delay between api requests
                    }

                    // Progress bar update
                    if (progress != null)
                    {
                        progress.Report(i * 100 / (currentList.items.Length - 1));
                    }
                }
                webClient.Dispose();
            });
            return currentList;
        }

        /// <summary>
        /// Loop trought a NotifyTemplate object (inside items) and search inside all of the userActivity to populate watching dates of items
        /// *If a item already have both watching dates, it will be skipped.
        /// </summary>
        /// <param name="currentList">A NotifyTemplate class</param>
        /// <param name="userActivity">A NotifyActivityConsumeAnime list</param>
        /// <param name="progress">Optional, report progress</param>
        /// <returns>A copy of the NotifyTemplate class with start and end dates populated. Dates that where not found are all set to null</returns>
        private async Task<NotifyTemplate> AddNotifyDates(NotifyTemplate currentList, List<NotifyActivityConsumeAnime> userActivity, IProgress<int> progress = null)
        {
            await Task.Run(() =>
            {
                for (int i = 0; i <= currentList.items.Length - 1; i++)
                {
                    // Only search & overwrite dates if one of the date in the current entry is null (missing)
                    if (currentList.items[i].watchedDate == null || currentList.items[i].endedDate == null) 
                    {
                        // Assume the anime only have dates if it's not a planned status
                        if (currentList.items[i].status != AnimeEnums.NotifyStatus.planned)
                        {
                            /* NOTE :
                             * We use Where insead of SingleOrDefault since NotifyActivityConsumeAnime sometimes have duplicates entry that match all conditions.
                             * OrderBy re-order all the Activity matching from the oldest date to the most recent.
                             */
                            currentList.items[i].watchedDate = userActivity.Where(activity => activity.animeID == currentList.items[i].animeID && activity.fromEpisode == 1 && activity.toEpisode >= 1)
                                                                            .OrderBy(activity => activity.created)
                                                                            .DefaultIfEmpty(null)
                                                                            .First()?.created;
                            // Assume the user have a end date only if the anime status is set to completed
                            if (currentList.items[i].status == AnimeEnums.NotifyStatus.completed)
                            {
                                currentList.items[i].endedDate = userActivity.Where(activity => activity.animeID == currentList.items[i].animeID && activity.toEpisode == currentList.items[i].watchedEpisodes)
                                                                                .OrderBy(activity => activity.created)
                                                                                .DefaultIfEmpty(null)
                                                                                .First()?.created;
                            }
                            else if (currentList.items[i].status == AnimeEnums.NotifyStatus.dropped) // Get the date of the latest episode watched for dropped
                            {
                                currentList.items[i].endedDate = userActivity.Where(activity => activity.animeID == currentList.items[i].animeID && activity.toEpisode <= currentList.items[i].watchedEpisodes)
                                                                                .OrderBy(activity => activity.created)
                                                                                .DefaultIfEmpty(null)
                                                                                .First()?.created;
                            }
                        }
                    }

                    // Progress bar update
                    if (progress != null)
                    {
                        progress.Report(i * 100 / (currentList.items.Length - 1));
                    }
                }
            });
            return currentList;
        }

        /// <summary>
        /// Loop trought notify consume anime DAT file and make a list.
        /// </summary>
        /// <param name="filePath">Path of the DAT file</param>
        /// <param name="userID">Only activity made by this userID will be kept</param>
        /// <param name="progress">Optional, report progress</param>
        /// <returns>A NotifyActivityConsumeAnime list of all the user activities</returns>
        private async Task<List<NotifyActivityConsumeAnime>> LoadNotifyActivityConsumeAnime(string filePath, string userID, IProgress<int> progress = null)
        {
            // Create a list of unknow lenght
            List<NotifyActivityConsumeAnime> newList = new List<NotifyActivityConsumeAnime>();
            await Task.Run(() =>
            {
                // Only get lines that start with ' {" ' as they are json
                IEnumerable<string> fileJsonLines = File.ReadLines(filePath).Where(line => line.StartsWith("{\""));
                int maxProgress = fileJsonLines.Count();
                int currentProgress = 1;
                foreach (string jsonLine in fileJsonLines) 
                {
                    NotifyActivityConsumeAnime jsonActivity = JsonConvert.DeserializeObject<NotifyActivityConsumeAnime>(jsonLine);
                    // Add the Activity to the new list only if the activity was made by the same userID
                    if (jsonActivity.userID == userID)
                    {
                        newList.Add(jsonActivity);
                    }

                    // Progress bar update
                    if (progress != null)
                    {
                        progress.Report(currentProgress * 100 / maxProgress);
                        currentProgress++;
                    }
                }
            });
            return newList;
        }

        /// <summary>
        /// Take a NotifyTemplate object and convert it into a MALTemplate object
        /// </summary>
        /// <param name="currentList">A NotifyTemplate class</param>
        /// <param name="progress">Optional, report progress</param>
        /// <returns></returns>
        private async Task<MALTemplate> NotifyTemplateToMalTemplate(NotifyTemplate currentList, IProgress<int> progress = null)
        {
            // Create a MALTemplate object
            MALTemplate newList = new MALTemplate
            {
                //Create MALAnimeItem list for animes
                animes = new List<MALAnimeItem>()
            };
            await Task.Run(() =>
            {
                XmlDocument xmlDoc = new XmlDocument();
                for (int i = 0; i <= currentList.items.Length - 1; i++)
                {
                    int malID;
                    // Search for a MAL ID, only continue if found
                    if (int.TryParse(currentList.items[i].mappings.FirstOrDefault(value => value.service.Contains("myanimelist/anime"))?.serviceId, out malID)) 
                    {
                        MALAnimeItem newItem = new MALAnimeItem
                        {
                            ID = malID,
                            title = xmlDoc.CreateCDataSection(currentList.items[i].title),
                            rating = currentList.items[i].rating.overall,
                            notes = xmlDoc.CreateCDataSection(currentList.items[i].notes),
                            watchedEpisodes = currentList.items[i].watchedEpisodes,
                            timeRewatched = currentList.items[i].rewatchCount,
                        };
                        // Convert status
                        switch (currentList.items[i].status)
                        {
                            case AnimeEnums.NotifyStatus.watching:
                                newItem.status = "Watching";
                                break;
                            case AnimeEnums.NotifyStatus.completed:
                                newItem.status = "Completed";
                                break;
                            case AnimeEnums.NotifyStatus.planned:
                                newItem.status = "Plan to Watch";
                                break;
                            case AnimeEnums.NotifyStatus.hold:
                                newItem.status = "On-Hold";
                                break;
                            case AnimeEnums.NotifyStatus.dropped:
                                newItem.status = "Dropped";
                                break;
                        }

                        // Apply start & end dates if they are not null
                        if (currentList.items[i].watchedDate != null) 
                        {
                            // Convert DateTime to the string format XML use for dates
                            newItem.startDate = currentList.items[i].watchedDate?.ToString("yyyy'-'MM'-'dd");
                        }
                        if (currentList.items[i].endedDate != null)
                        {
                            newItem.endDate = currentList.items[i].endedDate?.ToString("yyyy'-'MM'-'dd");
                        }

                        // Add the newItem into the MALAnimeItem list
                        newList.animes.Add(newItem);
                    }

                    // Progress bar update
                    if (progress != null)
                    {
                        progress.Report(i * 100 / (currentList.items.Length - 1));
                    }
                }
            });
            return newList;
        }

        /// <summary>
        /// Loop trought the items of a NotifyTemplate and push them to a anilist list using the given TOKEN for api requests.
        /// </summary>
        /// <param name="currentList">A NotifyTemplate class</param>
        /// <param name="anilistToken">A APP token for anilist</param>
        /// <param name="skipRules">If true, every entry will get pushed except if they do not have 'isNewEntry' on true</param>
        /// <param name="progress">Optional, report progress</param>
        /// <returns>A Dictionary containing the ID of animes with a Enumerable list containings error messages returned by the api. All items that failed to be push.</returns>
        private async Task<IDictionary<string, IEnumerable<string>>> PushNotifyTemplateToAnilistAPI(NotifyTemplate currentList, string anilistToken, bool skipRules = false, IProgress<int> progress = null) 
        {
            IDictionary<string, IEnumerable<string>> failedImports = new Dictionary<string, IEnumerable<string>>();

            HttpClient webClient = new HttpClient { BaseAddress = new Uri(AnilistAPI) };
            webClient.DefaultRequestHeaders.Add("Accept", "application/json");
            webClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", anilistToken);

            await Task.Run(async () =>
            {
                for (int i = 0; i <= currentList.items.Length - 1; i++)
                {
                    // Only continue if anime is new/updated from the original cache file (without a cache file, it is always true)
                    if (currentList.items[i].isNewEntry)
                    {
                        // Verify if the anime is private, has a decimal rating or if verification rules need to be ignored.
                        if (currentList.items[i].isPrivate || (currentList.items[i].rating.overall % 1) != 0 || skipRules)
                        {
                            string currentItemAnilistID = GetMappingIDFromNotifyItem(currentList.items[i], "anilist/anime");
                            // Ensure that a mapping for anilist was found
                            if (!string.IsNullOrEmpty(currentItemAnilistID))
                            {
                                // Convert notify status to a anilist status
                                string currentItemStatus = "";
                                switch (currentList.items[i].status)
                                {
                                    case AnimeEnums.NotifyStatus.hold:
                                        currentItemStatus = "PAUSED";
                                        break;
                                    case AnimeEnums.NotifyStatus.planned:
                                        currentItemStatus = "PLANNING";
                                        break;
                                    case AnimeEnums.NotifyStatus.watching:
                                        currentItemStatus = "CURRENT";
                                        break;
                                    case AnimeEnums.NotifyStatus.completed:
                                        currentItemStatus = "COMPLETED";
                                        break;
                                    case AnimeEnums.NotifyStatus.dropped:
                                        currentItemStatus = "DROPPED";
                                        break;
                                }

                                // Data send to graphl
                                JObject jsonDataObject = new JObject
                                {
                                    ["query"] = AnilistAPIQuery,
                                    ["variables"] = new JObject
                                    {
                                        ["mediaId"] = int.Parse(currentItemAnilistID),
                                        ["status"] = currentItemStatus,
                                        ["score"] = (int)((Math.Round(currentList.items[i].rating.overall,2) / 10.0) * 100), // Take decimal 0/10 notify score and convert it to a 0/100 int
                                        ["progress"] = currentList.items[i].watchedEpisodes,
                                        ["repeat"] = currentList.items[i].rewatchCount,
                                        ["private"] = currentList.items[i].isPrivate,
                                        ["notes"] = currentList.items[i].notes,
                                        ["startedAt"] = new JObject
                                        {
                                            ["year"] = currentList.items[i].watchedDate.GetValueOrDefault().Year,
                                            ["month"] = currentList.items[i].watchedDate.GetValueOrDefault().Month,
                                            ["day"] = currentList.items[i].watchedDate.GetValueOrDefault().Day
                                        },
                                        ["completedAt"] = new JObject
                                        {
                                            ["year"] = currentList.items[i].endedDate.GetValueOrDefault().Year,
                                            ["month"] = currentList.items[i].endedDate.GetValueOrDefault().Month,
                                            ["day"] = currentList.items[i].endedDate.GetValueOrDefault().Day
                                        }
                                    }
                                };

                                int retryLeft = 3;
                                do
                                {
                                    var postRequestResult = await webClient.PostAsync("", new StringContent(jsonDataObject.ToString(), Encoding.UTF8, "application/json"));
                                    retryLeft--;
                                    if (postRequestResult.StatusCode != HttpStatusCode.OK)
                                    {
                                        // If last retry for the current item didn't return a 200 OK
                                        if (retryLeft == 0)
                                        {
                                            List<string> errors = new List<string>() { "APP: Exporter ran out of retry and has abandoned this item. Please update manually." };
                                            failedImports.Add(currentList.items[i].animeID, errors);
                                        }

                                        // Rate limit
                                        if (postRequestResult.StatusCode == (HttpStatusCode)429)
                                        {
                                            if (postRequestResult.Headers.TryGetValues("Retry-After", out IEnumerable<string> retryAfterEnumerable))
                                            {
                                                int timeToWait = int.Parse(retryAfterEnumerable.DefaultIfEmpty("0").FirstOrDefault());
                                                if (timeToWait > 0)
                                                {
                                                    // Convert seconds to MS
                                                    int timeToWaitMS = timeToWait * 1000;
                                                    await Task.Delay(timeToWaitMS);
                                                }
                                            }
                                        }
                                        else // Handle error if is isn't a rate limit
                                        {
                                            retryLeft = 0;
                                            string postRequestResultContent = await postRequestResult.Content.ReadAsStringAsync();

                                            JObject postRequestJsonObject = (JObject)JsonConvert.DeserializeObject(postRequestResultContent);
                                            JArray postRequestJsonObjectErrors = (JArray)postRequestJsonObject["errors"];

                                            IEnumerable<string> errors = new List<string>();
                                            foreach (var currentError in postRequestJsonObjectErrors)
                                            {
                                                errors = errors.Append(currentError["message"].ToString());
                                            }

                                            failedImports.Add(currentList.items[i].animeID, errors);
                                        }
                                    }
                                    else // If Post request didn't fail (OK response)
                                    {
                                        retryLeft = 0;
                                    }
                                    // Additonal 600ms delay before next request
                                    await Task.Delay(600);
                                } while (retryLeft > 0);
                            } 
                            else // Failed to find anilist mapping ID
                            {
                                List<string> errors = new List<string>() { "APP: Exporter failed to find anilist mapping ID. Please update manually." };
                                failedImports.Add(currentList.items[i].animeID, errors);
                            }
                        }
                    }

                    // Progress bar update
                    if (progress != null)
                    {
                        progress.Report(i * 100 / (currentList.items.Length - 1));
                    }
                }
            });
            webClient.Dispose();
             return failedImports;
        }

        /// <summary>
        /// Get the ID for a specific mapping service.
        /// </summary>
        /// <param name="notifyItem">One item of a notify list (items)</param>
        /// <param name="MappingName">The name of the mapping service</param>
        /// <returns>If found, a string with the ID, else, NULL</returns>
        private string GetMappingIDFromNotifyItem(Items notifyItem, string MappingName) 
        {
            return notifyItem.mappings.FirstOrDefault(item => item.service == MappingName)?.serviceId ?? null;
        }

        /// <summary>
        /// Download the ConsumeAnimeActivity dat file of notify.moe
        /// *Call the SetProgressBarVisibility method with (true,true) during operation
        /// </summary>
        /// <param name="filePath">Path where the dat file will be saved</param>
        /// <returns></returns>
        private async Task DownloadNotifyConsumeAnimeActivityDB(string filePath)
        {
            SetProgressBarVisibility(true, true);
            WebClient webClient = new WebClient();
            await webClient.DownloadFileTaskAsync($"{NotifyAPI}types/ActivityConsumeAnime/download", saveFileDialogMenu.FileName);
            webClient.Dispose();
            SetProgressBarVisibility(false, false);
        }

        /// <summary>
        /// Make a call to notify API to get the userID and download the user animelist from the api
        /// *Call the SetProgressBarVisibility method with (true,true) during operation
        /// </summary>
        /// <param name="filePath">Path where the json file will be saved</param>
        /// <param name="username">Name of the user</param>
        /// <returns>True if the user exist, false if the user didn't exist</returns>
        private async Task<bool> DownloadNotifyAnimeListFormUsername(string filePath, string username)
        {
            SetProgressBarVisibility(true, true);
            // Get the userID from notify API
            HttpClient httpClient = new HttpClient { BaseAddress = new Uri($"{NotifyAPI}") };
            string webRequestNickNameResponse = await httpClient.GetAsync($"nicktouser/{username}").Result.Content.ReadAsStringAsync();

            // If the user does not exist
            if (webRequestNickNameResponse.StartsWith("Not found:") || webRequestNickNameResponse == string.Empty) 
            {
                SetProgressBarVisibility(false, false);
                return false;
            }

            // Cconvert the webRequestNickNameResponse to a json object
            JObject webRequestJsonObject = (JObject)JsonConvert.DeserializeObject(webRequestNickNameResponse);
            string userID = webRequestJsonObject.Value<string>("userId");
            httpClient.Dispose();

            if (!string.IsNullOrEmpty(userID))
            {
                WebClient webClient = new WebClient();
                await webClient.DownloadFileTaskAsync($"{NotifyAPI}animelist/{userID}", filePath);
                webClient.Dispose();
                SetProgressBarVisibility(false, false);
                return true;
            }
            SetProgressBarVisibility(false, false);
            return false;
        }

        /// <summary>
        /// Convert a MALTemplate object into a xml file
        /// </summary>
        /// <param name="filePath">Path where the template will be saved</param>
        /// <param name="templateToSave">MALTemplate object to save</param>
        private void SaveMALTemplateToXml(string filePath, MALTemplate templateToSave)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(MALTemplate));

            using (StreamWriter file = new StreamWriter(filePath))
            {
                xmlSerializer.Serialize(file, templateToSave);
            }
        }

        /// <summary>
        /// Convert a NotifyTemplate Object into a json file
        /// WARNING : Before export, every Items get their 'isNewEntry' set to false
        /// </summary>
        /// <param name="filePath">Path where the json file will be saved</param>
        /// <param name="templateToSave">NotifyTemplate object to save</param>
        private void SaveNotifyTemplateToJson(string filePath, NotifyTemplate templateToSave)
        {
            for (int i = 0; i <= templateToSave.items.Length - 1; i++) 
            {
                templateToSave.items[i].isNewEntry = false;
            }
            using (StreamWriter file = File.CreateText(filePath))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, templateToSave);
            }
        }

        /// <summary>
        /// Convert a Anilist FailedImports IDictionary to a text file
        /// </summary>
        /// <param name="filePath">Path where the text file will be saved</param>
        /// <param name="failedImports">The anilist IDictionary of failedImports</param>
        private void SaveAnilistFailedImportsToTxt(string filePath, IDictionary<string, IEnumerable<string>> failedImports)
        {
            // Write the string array to a new file named "WriteLines.txt".
            using (StreamWriter file = new StreamWriter(filePath))
            {
                foreach (KeyValuePair<string, IEnumerable<string>> animeID in failedImports) 
                {
                    file.WriteLine($"NotifyID : {animeID.Key}");
                    file.WriteLine("Errors :");
                    foreach (string currentError in animeID.Value) 
                    {
                        file.WriteLine($"\t{currentError}");
                    }
                    file.Write("\n=-=-=-=\n");
                }
            }
        }

        /// <summary>
        /// Change the visibility of 'progressBarConvert' and others controls during load operations.
        /// </summary>
        /// <param name="isVisible">Is the progress bar visible?</param>
        private void SetProgressBarVisibility(bool isVisible, bool isWaitTimeUnknown = false) 
        {
            // isVisible
            progressBarConvert.Visible = isVisible;
            buttonConvert.Visible = !isVisible;
            buttonSelectJsonPath.Enabled = !isVisible;
            buttonSelectConsAnimeDatPath.Enabled = !isVisible;
            buttonDownloadConsAnimeDat.Enabled = !isVisible;
            buttonDownloadJsonExport.Enabled = !isVisible;
            buttonMalOutputXML.Enabled = !isVisible;
            buttonCacheOutputPath.Enabled = !isVisible;
            buttonSelectCacheInputPath.Enabled = !isVisible;
            radioButtonModeAnilistAPI.Enabled = !isVisible;
            radioButtonModeMALFormat.Enabled = !isVisible;
            checkBoxIgnoreAnilistRules.Enabled = radioButtonModeAnilistAPI.Checked ? !isVisible : false; // Only re-enable the control if Anilist API radiobutton is checked
            this.UseWaitCursor = isVisible;

            // isWaitTimeUnknown
            progressBarConvert.Style = isWaitTimeUnknown ? ProgressBarStyle.Marquee : ProgressBarStyle.Blocks;
        }

        /// <summary>
        /// Create a IProgress to update progressBarConvert with the % of a value
        /// </summary>
        /// <param name="onGoing">Text shown on in to form text</param>
        /// <returns>IProgress</returns>
        private IProgress<int> CreateProgress(string onGoing) 
        {
            return new Progress<int>(value =>
            {
                progressBarConvert.Value = value;
                this.Text = $"{onGoing} | {value}%";
            });
        }

        private void MainForm_OnClosing(object sender, FormClosingEventArgs e)
        {
            // Assume that if progressBarConvert is visible, the application is doing work
            if (progressBarConvert.Visible) 
            {
                DialogResult confirmResult = MessageBox.Show("Do you want to exit the program now?\nCurrent work will be lost.","Exit Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirmResult == DialogResult.No)
                    e.Cancel = true;
            }
        }

        private void radioButtonModeAnilistAPI_CheckedChanged(object sender, EventArgs e)
        {
            // Only enable if the Anilist API mode is selected
            checkBoxIgnoreAnilistRules.Enabled = radioButtonModeAnilistAPI.Checked;
            panelMalOutputPath.Enabled = !radioButtonModeAnilistAPI.Checked;
            // Reset the checkbox state
            checkBoxIgnoreAnilistRules.Checked = false;
        }
    }
}
