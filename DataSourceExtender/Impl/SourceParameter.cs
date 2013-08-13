namespace DataSourceExtender.Impl
{
    /// <summary>
    /// 
    /// </summary>
    public class SourceParameter
        : ISourceParameter
    {
        private readonly string name;
        private readonly object value;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public SourceParameter(string name, object value)
        {
            this.name = name;
            this.value = value;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            get { return this.name; }
        }

        /// <summary>
        /// 
        /// </summary>
        public object Value
        {
            get { return this.value; }
        }


        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj is SourceParameter)
                return this.GetHashCode() == obj.GetHashCode();

            return false;
        }


        public override int GetHashCode()
        {
            return this.Name.GetHashCode(); 
        }


        public override string ToString()
        {
            return string.Format("Name: {0}, Value: {1}", this.Name, this.Value);
        }
    }
}
