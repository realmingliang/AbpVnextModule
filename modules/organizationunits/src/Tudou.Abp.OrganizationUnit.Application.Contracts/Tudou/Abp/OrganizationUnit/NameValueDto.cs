namespace Tudou.Abp.OrganizationUnit
{
    public class NameValueDto
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public NameValueDto()
        {

        }
        public NameValueDto(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }
}