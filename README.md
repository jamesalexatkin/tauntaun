<h1 align="center">
  <br>
  <img src="tauntaun-icon-250px.png" alt="Tauntaun" width="200">
  <br>
  Tauntaun
  <br>
</h1>

<h4 align="center">A map manager for Star Wars: Battlefront II (2005) custom maps.</h4>

_(Please note this project is still a work in progress.)_

# 🎨 Features
* Manage currently installed maps.
* Download and install new maps.
* Pulls together download methods for existing maps into a single application.
* Supports downloads from [ModDB](https://www.moddb.com/games/star-wars-battlefront-ii).
* Supports extraction of ZIP, RAR and 7ZIP archives.
* Supports external installers in EXE format.

# 📥 Installation
Tauntaun can be installed by grabbing the latest release from the [Releases](https://github.com/jamesalexatkin/tauntaun/releases) page. 

In the `Settings` section of the app you must specify your game's `addon` folder. This is located under `GameData` and, if you don't already have one, you must create it.

For Steam users it is typically found at:

`C:\Program Files\Steam\steamapps\common\Star Wars Battlefront II\GameData\addon`

While a regular Windows install usually places it at:

`C:\Program Files\Lucas Arts\Star Wars Battlefront II\GameData\addon`

# 💻 Technology

Tauntaun is developed in C# using Visual Studio 2019. It requires [.NET Framework 4.7.2](https://dotnet.microsoft.com/download/dotnet-framework/net472) to run.

# 💬 Contributing
If you wish to contribute, please do so by opening a pull request with a fitting description of your changes.

## 🗺️ Adding a map
Maps are stored in `known_maps.json` in the following format:

```json
{
  "Name": "Rhen Var: Temple",
  "Folder": "RVT",
  "Author": "Commander-Sev",
  "Type": "Map",
  "DownloadUrl": "https://www.moddb.com/mods/filefronts-hidden-gems/downloads/rhen-var-temple",
  "ImageUrl": "https://media.moddb.com/cache/images/downloads/1/119/118726/thumb_620x2000/20170217101914_1.jpg",
  "InstallationInstructions": "EXTRACT, MOVE RVT TO addon"
}
```

* `Name` specifies the map's name.

* `Folder` is the name of the folder containing the map stored in the game's `addon` directory.

* `Author` is the creator of the map.

* `Type` specifies if this is a standalone map or a map pack. It takes values of `Map` or `Map pack`.

* `DownloadUrl` the download page for the map. At present, only ModDB is supported although this may change in future.

* `ImageUrl` the direct URL of an image representing the map. This can be taken from ModDB or another image hosting site, and found by right-clicking the image in-browser and selecting `Copy Link Location`.

* `InstallationInstructions` specify how the map is installed following its download. Instructions are separated by commas. There are three keywords:

  * `EXTRACT` performs a decompression of the file. ZIP, RAR and 7ZIP archives are supported.

  * `MOVE <folder> TO addon` copies the specified folder within the download to the user's `addon` folder.

  * `RUN .exe` runs the installer provided for the map.

When adding a new map, please add it in alphabetical order by name in `known_maps.json`.

### `InstallationInstructions` Examples

In the above example for the map _Rhen Var: Temple_, the downloaded file is a `.rar` archive containing the folder `RVT` which must be copied to `addon`. Two instructions are given. The first extracts the file, the second copies the correct folder to `addon`.

As another example, the _Khimera: Battlegrounds_ map pack download is a `.exe` file which installs the maps. The installation instructions for this map are given simply as: 

```json
"InstallationInstructions": "RUN .exe"
```


# 📜 License
This software is licensed under the [GNU General Public License v3](https://www.gnu.org/licenses/gpl-3.0.html).