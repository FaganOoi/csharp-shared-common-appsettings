# C# Shared App Settings Across Multiple Projects

## Steps
- Create library project to keep the common settings that will be used across projects
    - In this repo, the library project named as `Shared.Configuration.Project` is created. Inside, we create a folder `SharedConfig` and put related settings json file such as `appsettings.Development.Shared.json`.
    - As a reminder, we must set `Copy to Output Directory` property of json file to `Copy Always` or `Copy if newer`.
- Go to project wish to use common App Settings and made modification such as below:
    - Add project reference to `Shared.Configuration.Project` created at first step
    - In our `Program.cs`, we need to manual add json files. (Can refer to `Extensions\SharedSettingsExtension.cs`)
        - We will load 3 different paths for json files
            - Load shared App Settings for development usage (Use local directory path to load it)
            - Load shared App Settings for publish version (Load shared settings from root)
            - Load current project App Settings

## Publis testing
- Build docker image
    ```
     docker build -t sharedAppSettingWebApi -f .\SharedSettingsAcrossMultipleProjects\Dockerfile .
    ```

- Deploy docker image
    ```
    docker run --name local-web-api --env ASPNETCORE_ENVIRONMENT=Development -dp 5000:80 sharedAppSettingWebApi
    ```