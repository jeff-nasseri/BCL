# BCL [![version](https://img.shields.io/badge/version-1.0-green.svg)](https://semver.org)

BCL is a testing and configuration tool for websites. This software helps you properly configure different sites, including login pages and other elements. Currently supports HTTP and HTTPS protocols only.

## Architecture

BCL is organized into panels, each with an Action Layer and Command Layer. The Action Layer contains functions that execute commands in the Command Layer. Each panel has its own repository in the storage folder.

### Panel Structure
```
├───Combo  
│   ├───ActionLayer  
│   └───CommandLayer  
├───Proxy  
│   ├───ActionLayer  
│   └───CommandLayer  
├───Recorder  
│   ├───ActionLayer  
│   └───CommandLayer  
├───Request  
│   ├───Actions Layer  
│   └───Commands Layer  
├───Response  
│   ├───Actions Layer  
│   └───Command Layer  
├───Utilities  
│   ├───Actions Layer  
│   └───Commands Layer  
└───Variables  
    ├───ActionLayer  
    └───CommandLayer  
```

## Getting Started

### Example: Displaying a Web Page

1. Navigate to the request panel:
```
> req_manager
```

2. Generate a new request with a key and URL:
```
BCL.Request._Manager > generate 1 http://google.com
```

3. Switch to the response panel:
```
> res_manager
```

4. Get the response using your request key:
```
BCL.Response._Manager > get 1
```

5. Activate your response (required before further operations):
```
BCL.Response._Manager > active 1
```

6. Go to the response view panel:
```
BCL.Response._Manager > res_view
```

7. View the full front-end code in console:
```
BCL.Response._View > page
```

## Recording Commands

The Record panel captures all commands entered across the application, even in play mode. This allows you to save your workflow as a configuration file.

### Example: Viewing Recorded Commands

1. Navigate to the record view:
```
> rec_view
```

2. Display recorded commands:
```
BCL.Records._View > list
```

Output will look like:
```
[INFO] : 1      BCL.Request._Manager    Void _GenerateNewRequest(System.String, System.String)  [1,http://google.com,]  
[INFO] : 2      BCL.Response._Manager   Void _CreateNewResponse(System.String)  [1,]  
[INFO] : 3      BCL.Response._Manager   Void _ActivateResponse(System.String)   [1,]  
[INFO] : 4      BCL.Response._View      Void _showHtmlPage(System.String)       []  
```

## Help Commands

- Use `--help` for more information about any panel
- Use `-h` for help with a specific command

## System Requirements

- .NET Core 3.1
