# BCL [![version](https://img.shields.io/badge/version-1.0-green.svg)](https://semver.org)


Use this software to test and configure different sites. This software will help you to properly configure different sites and for example the site login page.
This software only supports HTTP and HTTPS protocols




# Panels
├───Combo  
│   ├───ActionLayer  
│   └───CommnadLayer  
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




Each panel has two layers of action and commands, which are functions in the action layer that execute commands in the commands layer.
The commands layer for each panel contains views and configs, but there are some differences  
Notice that each panel has a repository in the storage folder  
  
# Sample
Comedy example for displaying a web page  
  
//go to request panel  
> req_manager



###### generate new request by request key and url  
###### more information of each panel by --help,for each command use -h   
BCL.Request._Manager > generate 1 http://google.com  



###### go to response panel  
> res_manager
  
  
  
###### get response of your request bu rquest key  
BCL.Response._Manager > get 1  
  
  
  
###### active your response if you dont for each command you must send response key  
BCL.Response._Manager > active 1  
  
  
  
###### go to response view panel  
BCL.Response._Manager > res_view  
  
  
  
###### oage command write all front-end code in console  
BCL.Response._View > page    
  
  
  
  
  
Using the record panel you can get your own configuration file as a txt file.  
What the record panel does is record any commands you enter, even if it's in play mode.  
You can change this in the record panel  

# Sample
###### record panel  
> rec_view  


###### display records 
###### BLC.{panel route}  void {command function}  [function or command args]
BCL.Records._View > list  
[INFO] : 1      BCL.Request._Manager    Void _GenerateNewRequest(System.String, System.String)  [1,http://google.com,]  
[INFO] : 2      BCL.Response._Manager   Void _CreateNewResponse(System.String)  [1,]  
[INFO] : 3      BCL.Response._Manager   Void _ActivateResponse(System.String)   [1,]  
[INFO] : 4      BCL.Response._View      Void _showHtmlPage(System.String)       []  


# Prerequests
* .net core 3.1
