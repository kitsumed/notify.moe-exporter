# notify.moe-exporter
This project is a c# winform application allow users to export their [notify.moe](https://notify.moe) list to a mal formatted XML file for anilist import or directly trought anilist API.
> [!WARNING]
> The mal formatted XML file is intended to be used as a import file for anilist and has not been tested on the MAL website. Some fields MAL use like `update_on_import` are also missing.
## Download
You can download the latest release [**here**](https://github.com/kitsumed/notify.moe-exporter/releases/latest/download/Release.zip).
## Usage
Notify.moe-exporter requires a som files to work, these steps will guide you through the process :
> [!NOTE]
> Although the application can retrieve your anime list for you, anime defined as private will not be included in the export, so it is recommend that you download the files from your settings.
1. Download your notify anime list. This can be done in the app itself or by going on [notify.moe](https://notify.moe) under `Settings>Accounts>Export>Export as JSON`
3. In the application, click on the download button to download the ConsumeAnime Database. This database need to be redownloaded each time you get a newer version of your anime list export.
4. The `Cache Path` is a optional setting and can be left empty, refer to **Export Modes** for more informations.
5. For the `MAL Output Path` and `Cache Output Path`, select the location where you want the files to be saved. If you plan to use this application again in the future, keep the cache file it generate.

## Export Modes
### 1. MAL Format
The MAL Format mode will convert your anime list into a format that the anilist MAL importer will accept.
This is the quickest way to convert your anime list, but it does have a few downsides :
1. All your overall scores will be rounded to integers (8,4 become 8)
2. All animes set as `private` on notify won't be private anymore when importing on anilist

**Workaround** : Once you've finished converting your list to MAL format, go to your anilist account and import the file. Once the import is complete, return to the application and set the `cache path` field to the cache file that was generated at the end of the conversion. Change the **Export mode** to `Anilist API` and press **Convert**. After a while, the application will ask you for an **Anilist APP token**, see the section below "**Anilist API**" to find out how to obtain one.
### 2. Anilist API
