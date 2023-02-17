namespace PortalGrupoAlyne.Model.Dtos.Sankhya
{
    public class QueryResponse
    {
        public string serviceName { get; set; } = string.Empty;
        public string status { get; set; } = string.Empty;
        public string pendingPrinting { get; set; } = string.Empty;
        public string transactionId { get; set; } = string.Empty;
        public QueryResponseBody responseBody { get; set; }
        public string statusMessage { get; set;} = string.Empty;
    }

    public struct QueryResponseBody 
    {
        public IList<QueryFieldsMetadata> fieldsMetadata { get; set; }
        public IList<IList<Object>> rows { get; set; }
        public bool burstLimit { get; set; }
        public string timeQuery { get; set; }
        public string timeResultSet { get; set; }
    }

    public struct QueryFieldsMetadata 
    {
        public string name { get; set; }
        public string description { get; set; }
        public int order { get; set; }
        public string userType { get; set; }
    }

}
