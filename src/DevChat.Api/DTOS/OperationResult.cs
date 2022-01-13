namespace DevChat.Api.DTOS
{
    [Serializable]
    public class OperationResult
    {
        public int StatusCode { get; set; }
        public string Success { get; set; }

        public string Message { get; set; }

        public Object Entity { get; set; }
    }
}
