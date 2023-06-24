namespace SharedSettingsAcrossMultipleProjects.Extensions;

public static class SharedSettingsExtension
{
    private static string SharedSettingFolder = "SharedConfig";
    private static string SharedSettingProject = "Shared.Configuration.Project";

    public static WebApplicationBuilder SetSharedAppSettingJson(this WebApplicationBuilder builder, IWebHostEnvironment environment)
    {
        builder.SetSharedAppSettingJsonForDevelopment(environment);
        builder.SetSharedAppSettingJsonForPublish(environment);
        builder.SetCurrentProjectAppSettingJson(environment);

        return builder;
    }

    private static WebApplicationBuilder SetSharedAppSettingJsonForDevelopment(this WebApplicationBuilder builder, IWebHostEnvironment environment)
    {
        // find the shared folder in the parent folder
        string sharedSettingJsonFile = GetSharedJsonFile(environment.EnvironmentName);

        // development usage to include shared settings
        string sharedFolder = Path.Combine(environment.ContentRootPath, "..", SharedSettingProject, SharedSettingFolder);
        string finalPath = Path.Combine(sharedFolder, sharedSettingJsonFile);

        // For development version to use shared settings
        builder.Configuration.AddJsonFile(
            path: finalPath,
            optional: true,
            reloadOnChange: true
        );

        return builder;
    }

    private static WebApplicationBuilder SetSharedAppSettingJsonForPublish(this WebApplicationBuilder builder, IWebHostEnvironment environment)
    {
        // find the shared folder in the parent folder
        string sharedSettingJsonFile = GetSharedJsonFile(environment.EnvironmentName);

        // For build version to use shared settings
        builder.Configuration.AddJsonFile(
            path: $"{SharedSettingFolder}/{sharedSettingJsonFile}",
            optional: true,
            reloadOnChange: true
        );

        return builder;
    }

    private static WebApplicationBuilder SetCurrentProjectAppSettingJson(this WebApplicationBuilder builder, IWebHostEnvironment environment)
    {

        // Override shared settings + Add extra settings
        builder.Configuration.AddJsonFile(
            path: $"appsettings.{environment.EnvironmentName}.json",
            optional: false,
            reloadOnChange: true
        );


        return builder;
    }

    private static string GetSharedJsonFile(string environmentName)
    {
        return $"appsettings.{environmentName}.Shared.json";
    }
}
