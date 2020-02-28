using System;
using System.Collections.Generic;
using System.Text;

namespace BCL.Records {
    [ClassCommandInfo ("rec_config")]
    class _Config : ConfigAction {

        [ClassCommandInfo ("remove_record", "remove one record from history of user input command")]
        public void _RemoveRecord (string key) {
            RemoveRecord (key);
        }

        [ClassCommandInfo ("remove_group_record", "remove list of commands by their keys", argsInfo : new string[] { "prefic=>commands contain this prefix" })]
        public void _RemoveRecords (string prefix) {
            RemoveRecords (prefix);
        }

        [ClassCommandInfo ("clear", "clear history")]
        public void _ClearRecords () {
            ClearRecords ();
        }

        [ClassCommandInfo ("set_state", "pause recrd", argsInfo : new string[] { "state=>state of record (false:pause)(true:play)" })]
        public void _PauseRecord (string state) {
            PauseRecord (state);
        }

        [ClassCommandInfo ("dynamic")]
        public void _RecordDynamicize (string key, string position, string value) {
            RecordDynamicize (key, position, value);
        }

        [ClassCommandInfo ("copy")]
        public void _CopyRecords (string name) {
            CopyRecords (name);
        }

        [ClassCommandInfo ("paste")]
        public void _PasteRecords (string name) {
            PasteRecords (name);
        }

        [ClassCommandInfo ("export")]
        public void _ExportRecords (string name = "") {
            ExportRecords (name);
        }

        [ClassCommandInfo ("import")]
        public void _ImportRecords (string fileName) {
            ImportRecords (fileName);
        }

        [ClassCommandInfo ("set_command_in_arg")]
        public void _SetCommandInArg (string targetCommandId, string argIndex, string commandId = "", string customeArg = "") {
            SetCommandInArg (targetCommandId, argIndex, commandId, customeArg);
        }

        [ClassCommandInfo ("execute")]
        public void _ExecuteRecords (string fileName) {
            ExecuteRecords (fileName);
        }
    }
}