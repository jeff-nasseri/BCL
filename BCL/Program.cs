using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using Newtonsoft.Json;
using RecordExecuter;

namespace BCL {
    class Program {

        static void Main (string[] args) {

            string image = @"
 ________  ________  ___          
|\   __  \|\   ____\|\  \         
\ \  \|\ /\ \  \___|\ \  \        
 \ \   __  \ \  \    \ \  \       
  \ \  \|\  \ \  \____\ \  \____  
   \ \_______\ \_______\ \_______\
    \|_______|\|_______|\|_______|
                                  
                
    open source proj at https://github.com/alireza-nasseri/BCL

    active panel :
    combo_config |combo_view
    proxy_config | proxy_view
    rec_config | rec_view
    repeater
    req_manager | req_config | req_config
    res_manager | res_fetch | res_view 
    var_config | var_config

        
            ";

            var types = Assembly.GetExecutingAssembly ().GetTypes ().Where (t => t.Name[0] == Utilities.Mode_4[0] /*for remove <PrivateImplementationDetails>*/ && t.Name[1] != '_');
            var onProccess = new OnProccess (types);
            onProccess.SetAppsInStaticStorage ();
            onProccess.CallConventions ();
            onProccess.InsertFunctionsCommandInStaticStorage ();

            var userInteraction = new UserInteraction ();
            userInteraction.StartInteraction ();

        }
    }

    public enum ShowType {
        INFO,
        DANGER,
        SUCCESS,
        ALERT,
        DataTarget
    }

    public enum ClassesCommandFeaturesName {
        AppsList,
        ClassesCommand,
        ClassesName
    }

}
