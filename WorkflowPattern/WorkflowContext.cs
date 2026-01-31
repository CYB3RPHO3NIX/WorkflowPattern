namespace WorkflowPattern
{
    public class WorkflowContext
    {
        private readonly Dictionary<string, object> _data = new();

        public void Set<T>(string key, T value) => _data[key] = value!;
        public T Get<T>(string key) => (T)_data[key];
        public bool Contains(string key) => _data.ContainsKey(key);
    }
}
