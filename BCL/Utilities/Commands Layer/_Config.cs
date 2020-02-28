using System;
using System.Collections.Generic;
using System.Text;

namespace BCL.Utility {
    [ClassCommandInfo ("utl_config")]
    class _Config : ConfigAction {

        [ClassCommandInfo ("add_req_headers", "add new header(s) to default request headers", argsInfo : new string[] { "headers=>http web request headers" })]
        public void _AddNewRequestDefaultHeaders (string headers) {
            AddNewRequestDefaultsHeader (headers);
        }

        [ClassCommandInfo ("add_res_headers", "add new header(s) to default response headers", argsInfo : new string[] { "headers=>http web response headers" })]
        public void _AddNewResponseDefaultHeaders (string headers) {
            AddNewResponseDefaultsHeader (headers);
        }

        [ClassCommandInfo ("cut_last_of")]
        public void _CutFirstOf () {
            CutFirstOf ();
        }

    }
}