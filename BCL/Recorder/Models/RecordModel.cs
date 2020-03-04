namespace BCL {
    public class RecordModel {
        public int Id { get; set; }
        public string ModelInstanceName { get; set; }
        public string MethodName { get; set; }
        public object[] args { get; set; }
        public string ReturnedName { get; set; }
        public bool Once { get; set; }
        public object Value { get; set; }
        public string State { get; set; }
    }
}