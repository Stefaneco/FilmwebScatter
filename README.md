# Filmweb Scatter

Export your movie data from filmweb to csv file.

## How to use

There are two way of using the software
<details closed>
  <summary>
    <h3> Download exe file (Recommended) </h3>
  </summary>

  #### 1. Download
  
  You can download zip package with an exe file that will run the bot on your windows machine.
  
![ExeInstruction1](https://github.com/Stefaneco/FilmwebScatter/assets/67236866/521f7e3e-9d1d-4591-9ed6-158fb35d06c4)
![ExeInstruction2](https://github.com/Stefaneco/FilmwebScatter/assets/67236866/f0918276-7aa3-4021-bb37-0f0c8e9c5aa3)

 #### 2. Configure (Optional)
 
  Open FilmwebScatter.dll.config file using any text editor, e.g. Notepad. You can specify following parameters:
| Parameter           | Description                                                                                                                                                                            |
|---------------------|----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| Username            | Your filmweb username. If you dont provide any value you will be asked for it when you run the program.<br>Valid values: Username<br>Default value: None                               |
| Password            | Your filmweb password. If you dont provide any value you will be asked for it when you run the program.<br>Valid values: Password<br>Default value: None                               |
| StartAtFilmNumber   | Number of the movie the bot will start from. The latest rated movie is number 1.<br>Valid values: Numbers 1 and above<br>Default value: 1                                              |
| EndAtFilmNumber     | Number of the movie the bot will end at. The latest rated movie is number 1.<br>Valid values: Numbers 1 and above<br>Default value: None                                               |
| AttachToExistingCsv | Attach the data to an existing "filmData.csv" and "actorsData.csv" files.<br>Remember to rename your files to match those names.<br>Valid values: True / False<br>Default value: False |

#### 3. Run

 Use FilmwebScatter.exe to run the program. You might get a windows popup asking if you trust the program.
  
</details>
<details closed>
  <summary>
    <h3> Clone and build </h3>
  </summary>

If you want to build the app on your machine and make some changes to it feel free to do so. Pull requests are welcome!
  
  </details>
