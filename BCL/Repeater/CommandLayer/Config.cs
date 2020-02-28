namespace BCL.Repeater {
    [ClassCommandInfo ("repeater")]
    public class _Config : ConfigAction {
        [ClassCommandInfo ("repeat")]
        public void _Repeat (string hasThread, string number="", string threadNumber="") {
            Repeat (hasThread, number, threadNumber);
        }
    }
}