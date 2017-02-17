# TogglWorkSyncService

This small application is ment to create or update a time entry for a specific project and workspace within Toggl. 

## Installation

Get the source, open the solution (f.ex. in Visual Studio) and adjust the App.config.
- "LogFileDestination" is the subdirectory/file within your local app data folder
- The "AuthorizationKey" has to have the format `Basic TOKEN` where TOKEN is the hashed api token/login 
- Set the "DomainName" and "DomainProjectName" to the desired Toggl specific `workspace` and `project`

Compile the application and you are ready to go.

## Usage

Run the executable within a windows service, cron-job, event triggered or however you like.

## Contributing

Contributions are always welcome.

1. Fork it!
2. Create your feature branch: `git checkout -b my-new-feature`
3. Commit your changes: `git commit -am 'Add some feature'`
4. Push to the branch: `git push origin my-new-feature`
5. Submit a pull request

## History

2017.02.16 - v1.6

* Basic Functionality

2017.02.16 - Created repository.

## Credits

[Toggl](https://www.toggl.com/)

[Toggl Api v8](https://github.com/toggl/toggl_api_docs)

[RestSharp](http://restsharp.org/)

[Json.NET](http://www.newtonsoft.com/json)


## License

MIT License Copyright (c) 2017

To the extent possible under law, [Raul Schnelzer](http://https://github.com/RaulSebastian) has waived all copyright and related or neighboring rights to this work.
