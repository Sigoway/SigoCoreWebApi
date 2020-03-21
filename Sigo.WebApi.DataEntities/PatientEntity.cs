namespace Sigo.WebApi.DataEntities
{
    /// <summary>
    /// 患者信息实体类
    /// </summary>
    public class PatientEntity: BaseEntity
    {
        //TODO 封装具体业务属性

        public string PatientId { get; set; }

        public string PatientName { get; set; }

        public string Sex { get; set; }

        public string Age { get; set; }

        public string WardArea { get; set; }

        public string Status { get; set; }

        public bool IsEmpty => string.IsNullOrEmpty(PatientId);
    }
}
