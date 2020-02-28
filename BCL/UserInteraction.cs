using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using McMaster.Extensions.CommandLineUtils;

namespace BCL {
    class UserInteraction {
        public void StartInteraction () {

            var route = string.Empty;
            var input = new string[] { };
            while (true) {

                try {
                    input = CMD.ReadeUserCommandLineInput (route).Where (s => !(String.IsNullOrEmpty (s)) || !(string.IsNullOrWhiteSpace (s))).ToArray ();
                    if (ProgramStorageQueries.IsClassCommandExist (input[0])) {
                        route = ProgramStorageQueries.GetClassName (input[0]);
                    }
                    if (ProgramStorageQueries.IsFunctionCommand (input[0]) || input[0].Contains ("--help")) {
                        ExecuteApp (route, input);
                    }
                    switch (Utilities.GetUtilitiesCommand (input[0])) {
                        case "restart_route":
                            route = "";
                            break;
                        case "clear_console":
                            Console.Clear ();
                            break;
                    }
                    if (!(ProgramStorageQueries.IsClassCommandExist (input[0]) || ProgramStorageQueries.IsFunctionCommand (input[0]) || input[0].Contains ("--help") ||
                            Utilities.GetUtilitiesCommand (input[0]) != "not found")) {
                        CMD.ShowApplicationMessageToUser ("command not found .", showType : ShowType.ALERT);
                    }
                } catch (Exception e) {
                    CMD.ShowApplicationMessageToUser ($"message : {e.Message}\nroute : {this.ToString()}", showType : ShowType.DANGER);

                }

            }
        }

        private void ExecuteApp (string className, string[] input) {
            try {
                var feature = ProgramStorageQueries.GetFeatureInfo (ClassesCommandFeaturesName.AppsList);
                (ProgramStorageQueries.GetInfoValue (feature, className) as CommandLineApplication).Execute (input);
            } catch (Exception e) {
                CMD.ShowApplicationMessageToUser ($"message : {e.Message}\nroute : {this.ToString()}", showType : ShowType.DANGER);
            }
        }

    }
}